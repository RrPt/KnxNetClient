using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Timers;
using EIBDef;


namespace Knx
{
    enum KnxConnectionState
    {
        unkonown, conReq, connected, disConReq, disconnected,
        hbReq
    }

    enum knxnetip_services
	{
        SEARCH_REQUEST                =  0x0201,
        SEARCH_RESPONSE               =  0x0202,
        DESCRIPTION_REQUEST           =  0x0203,
        DESCRIPTION_RESPONSE          =  0x0204,
        CONNECT_REQUEST               =  0x0205,
        CONNECT_RESPONSE              =  0x0206,
        CONNECTIONSTATE_REQUEST       =  0x0207,
        CONNECTIONSTATE_RESPONSE      =  0x0208,
        DISCONNECT_REQUEST            =  0x0209,
        DISCONNECT_RESPONSE           =  0x020A,
        DEVICE_CONFIGURATION_REQUEST  =  0x0310,
        DEVICE_CONFIGURATION_ACK      =  0x0311,
        TUNNELLING_REQUEST            =  0x0420,
        TUNNELLING_ACK                =  0x0421,
        TUNNEL_LINKLAYER              =  0x02,
        TUNNEL_RAW                    =  0x04,
        TUNNEL_BUSMONITOR             =  0x80,
    }

    class KnxIpHeader
    {
        private byte [] header = new byte[6];
        private knxnetip_services service;

        public KnxIpHeader(knxnetip_services pService)
        {
            header[0] = 0x06;
            header[1] = 0x10;
            Service = pService;
            header[4] = 0x01;
            header[5] = 0x06;
        }


        public knxnetip_services Service
        {
            get { return service; }
            set 
            { 
                service = value;
                header[2] = (byte)(((int)value) >> 8);
                header[3] = (byte)(((int)value) & 0xFF);
            }
        }

        public short Length
        {
            get 
            {
                int i = (header[4] << 8) +header[5];
                return (short)i;
            }
            set
            {
                header[4] = (byte)(((int)value) >> 8);
                header[5] = (byte)(((int)value) & 0xFF);
            }
        }


        public byte [] bytes 
        { 
            get {return header;} 
            set {header = value;} 
        }



    }

    class KnxHpai
    {
        private byte[] ipport = new byte[8];

        public KnxHpai(String ip, int port)
        {
            IPAddress serverIp = IPAddress.Parse(ip);
            Set(serverIp, port);
        }

        public KnxHpai(IPAddress serverIp, int port)
        {
            Set(serverIp, port);
        }

        private void Set(IPAddress serverIp, int port)
        {
            byte[] _ip = serverIp.GetAddressBytes();

            ipport[0] = 0x08;     // client HPAI structure len (8 bytes)
            ipport[1] = 0x01;     // protocol type (1 = UDP);
            ipport[2] = _ip[0];   // IP
            ipport[3] = _ip[1];
            ipport[4] = _ip[2];
            ipport[5] = _ip[3];
            ipport[6] = (byte)(port >> 8);  // Port
            ipport[7] = (byte)(port & 0xFF);
        }



        public byte[] bytes
        {
            get 
            {
                return ipport; 
            }
            //set { header = value; }
        }

    }

    class KnxCri
    {
        private byte[] cri = new byte[4];
    
        public KnxCri ()
	        {

                cri[0] = 0x04;      // Structure len (4 bytes)
                cri[1] = 0x04;      // Tunnel Connection
                cri[2] = 0x02;      // KNX Layer (Tunnel Link Layer)
                cri[3] = 0x00;      // Reserved
            }



            public byte[] bytes
        {
            get 
            {
                return cri; 
            }
            //set { header = value; }
        }

    }

    class KnxIpTelegramm
    {
        byte[] _bytes;
        KnxNetConnection con;

        public KnxIpTelegramm(KnxNetConnection con)
        {
            this.con = con;
        }

        public byte[] bytes 
        {
            get { return _bytes;} 
            //set; 
        }

        internal void Connect()
        {
            List<byte> l = new List<byte>();

            KnxIpHeader header = new KnxIpHeader(knxnetip_services.CONNECT_REQUEST);
            KnxHpai Control = new KnxHpai(con.myIP, con.clientPort);
            KnxHpai Data = new KnxHpai(con.myIP, con.clientPort);
            KnxCri Cri = new KnxCri();

            header.Length = (short)(header.bytes.Length + Control.bytes.Length + Data.bytes.Length + Cri.bytes.Length);
            l.AddRange(header.bytes);
            l.AddRange(Control.bytes);
            l.AddRange(Data.bytes);
            l.AddRange(Cri.bytes);
           
            _bytes = l.ToArray();        }


        internal void Disconnect()
        {
            List<byte> l = new List<byte>();

            KnxIpHeader header = new KnxIpHeader( knxnetip_services.DISCONNECT_REQUEST);
            KnxHpai Control = new KnxHpai(con.myIP, con.clientPort);

            header.Length = (short)(header.bytes.Length + Control.bytes.Length + 2);
            l.AddRange(header.bytes);
            l.Add(con.channelId);
            l.Add(0x00);
            l.AddRange(Control.bytes);

            _bytes = l.ToArray();
        }


        internal void Heartbeat()
        {
            List<byte> l = new List<byte>();

            KnxIpHeader header = new KnxIpHeader(knxnetip_services.CONNECTIONSTATE_REQUEST);
            KnxHpai Control = new KnxHpai(con.myIP, con.clientPort);

            header.Length = (short)(header.bytes.Length + Control.bytes.Length + 2);
            l.AddRange(header.bytes);
            l.Add(con.channelId);
            l.Add(0x00);
            l.AddRange(Control.bytes);

            _bytes = l.ToArray();
        }



        internal void DataAck(byte SeqCounter)
        {
            List<byte> l = new List<byte>();

            KnxIpHeader header = new KnxIpHeader(knxnetip_services.TUNNELLING_ACK);

            header.Length = (short)(header.bytes.Length + 4);
            l.AddRange(header.bytes);
            l.Add(0x04);
            l.Add(con.channelId);
            l.Add(SeqCounter);
            l.Add(0x00);

            _bytes = l.ToArray();

        }


        internal void Data(cEMI emi,byte SeqCounter)
        {
            List<byte> l = new List<byte>();

            KnxIpHeader header = new KnxIpHeader(knxnetip_services.TUNNELLING_REQUEST);

            header.Length = (short)(header.bytes.Length + emi.DataLen + 14);
            l.AddRange(header.bytes);
            l.Add(0x04);
            l.Add(con.channelId);
            l.Add(SeqCounter);
            l.Add(0x00);
            l.AddRange(emi.GetTelegramm());

            _bytes = l.ToArray();
        }



    }

    class KnxNetConnection
    {   // öffentliche Member
        public IPAddress myIP;
        public int clientPort = 18001;
        public int gatewayPort = 3671;
        public string gatewayIp;
        public delegate void LoggingDelegate(string Text);
        public delegate void TelegramReceivedDelegate(cEMI emi);
        public delegate void DataChangedDelegate(HDKnx hdKnx);

        // private Member
        const int OpenTimeout = 5;
        KnxConnectionState ConnectionState = KnxConnectionState.unkonown;
        UdpClient udpClient ;
        IAsyncResult ar;
        int AnzTelegramme = 0;
        Queue<cEMI> fromKnxQueue = new Queue<cEMI>();
        byte _channelId = 0;
        private static Timer timerHeartbeat;
        LoggingDelegate Log = null;
        TelegramReceivedDelegate TelegramReceived = null;
        DataChangedDelegate dataChanged = null;
        byte SeqCounter = 0;
        System.Windows.Forms.Control telegramReceivedControl;
        System.Windows.Forms.Control dataChangedControl;
        

        /// <summary>
        /// Setzt die Funktion, an die Logausgaben übergeben werden sollen
        /// wird keine angegeben so wird LogIntern verwendet
        /// </summary>
        /// <param name="LogFunction"></param>
        public void SetLog(LoggingDelegate LogFunction)
        {
            Log = LogFunction;
        }


        /// <summary>
        /// Verwendete Logfunktion, falls keine externe über SetLog gemeldet wurde
        /// </summary>
        /// <param name="txt"></param>
        private void LogIntern(String txt)
        {
            Console.WriteLine(txt);
        }


        /// <summary>
        /// Setzt die Funktion, an die empfangene Telegramme übergeben werden sollen
        /// wird keine angegeben so werden diese in eine interne Queue gespeichert
        /// </summary>
        /// <param name="ReceivedFunction"> Funktion an die die Telegrammdaten übergeben werden soll</param>
        /// <param name="control">Angabe eines evtl. Controls, falls ein Invoke durchgeführt werden muss.
        ///                       Kann auch null gesetzt werden</param>
        public void SetReceivedFunction(TelegramReceivedDelegate ReceivedFunction, System.Windows.Forms.Control control)
        {
            TelegramReceived = ReceivedFunction;
            telegramReceivedControl = control;
        }


        /// <summary>
        /// Setzt die Funktion, an die empfangene Telegramme übergeben werden sollen
        /// wird keine angegeben so werden diese in eine interne Queue gespeichert
        /// </summary>
        /// <param name="ReceivedFunction"> Funktion an die die Telegrammdaten übergeben werden soll</param>
        /// <param name="control">Angabe eines evtl. Controls, falls ein Invoke durchgeführt werden muss.
        ///                       Kann auch null gesetzt werden</param>
        public void SetDataChangedFunction(DataChangedDelegate DataChangedFunction, System.Windows.Forms.Control control)
        {
            dataChanged = DataChangedFunction;
            dataChangedControl = control;
        }

        public bool QueueEnable { get; set; }

        /// <summary>
        /// Abfrage der ChannnelId die vom Gateway für diese Verbindung festgelegt wird.
        /// Dieser werdt ist erst nach erfolgreichem Open gültig, ansonsten 0
        /// </summary>
        public byte channelId
        {
            get
            {
                return _channelId;
            }
        }



        /// <summary>
        /// Konstruktor, legt neue KnxNet Connection an, diese wird aber noch nicht geöffnet
        /// </summary>
        public KnxNetConnection()
        {
            QueueEnable = false;
            Log = LogIntern;
            IPHostEntry Host = Dns.GetHostEntry(Dns.GetHostName());
            myIP = Host.AddressList[1];
            while (udpClient == null)
            {
                try
                {

                    udpClient = new UdpClient(clientPort);
                    Log("ClientPort " + clientPort + " geöffnet");

                }
                catch (Exception)
                {
                    Log("ClientPort " + clientPort + " bereits belegt");
                    clientPort++;
                }
            }
            timerHeartbeat = new System.Timers.Timer(60000);
            timerHeartbeat.Elapsed += new ElapsedEventHandler(OnTimedEventHeartbeat);
        }



        /// <summary>
        /// Öffnen der Verbindung
        /// </summary>
        /// <param name="gatewayIp"> IP Adresse des KnxIp-Gateways</param>
        /// <returns>Rückgabewert true bedeutet die Verbindung konnte geöffnet werden</returns>
        internal bool Open(string gatewayIp)
        {
            this.gatewayIp = gatewayIp;

            try
            {

                udpClient.Connect(gatewayIp, gatewayPort);

                KnxIpTelegramm Tele = new KnxIpTelegramm(this);
                Tele.Connect();
                byte[] TeleBytes = Tele.bytes;

                //KnxNetForm.tb_Log.AppendText(Environment.NewLine + "O>:" + KnxTools.BytesToString(TeleBytes));
                Log("O>:" + KnxTools.BytesToString(TeleBytes));

                udpClient.Send(TeleBytes,TeleBytes.Length);
                ConnectionState = KnxConnectionState.conReq;

           }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // nun den Listener starten
            ar = udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), AnzTelegramme);

            // Warten auf Antwort, maximal OpenTimeout Sekunden
            int i = 0;

            while ((i < 10 * OpenTimeout) & (ConnectionState != KnxConnectionState.connected))
            {
                System.Threading.Thread.Sleep(100);
                i++;
            }

            // Den Heartbeat Timer starten
            timerHeartbeat.Start();

            return (ConnectionState == KnxConnectionState.connected) ;
        }


        /// <summary>
        /// Schließen der Verbindung
        /// </summary>
        /// <returns>Rückgabewert true bedeutet die Verbindung konnte geschlossen werden</returns>
        internal bool Close()
        {
            try
            {
                KnxIpTelegramm Tele = new KnxIpTelegramm(this);
                Tele.Disconnect();
                byte[] TeleBytes = Tele.bytes;

                //KnxNetForm.tb_Log.AppendText(Environment.NewLine + "C>:" + KnxTools.BytesToString(TeleBytes));
                Log("C>:" + KnxTools.BytesToString(TeleBytes));

                udpClient.Send(TeleBytes, TeleBytes.Length);
                ConnectionState = KnxConnectionState.disConReq;

                // Warten auf Antwort, maximal OpenTimeout Sekunden
                int i = 0;

                while ((i < 10 * OpenTimeout) & (ConnectionState != KnxConnectionState.disconnected))
                {
                    System.Threading.Thread.Sleep(100);
                    i++;
                }


                // Den Heartbeat Timer stoppen
                timerHeartbeat.Stop();

                int Anz = (int)ar.AsyncState;
                IPEndPoint e = new IPEndPoint(IPAddress.Any, 0);
                udpClient.EndReceive(ar, ref e);

                udpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return ConnectionState == KnxConnectionState.disconnected;
        }


        /// <summary>
        /// HerzschlagRequest der Verbindung
        /// </summary>
        /// Sendet zyklich ein Telegramm an das Gateway dass die Verbindung noch aktiv ist
        internal void Heartbeat()
        {
            if (ConnectionState != KnxConnectionState.connected)
            {   // Upps, da stimmt was mit der Verbindung nicht
                Log("Connection ist im Status:" + ConnectionState.ToString());
            }
            try
            {
                KnxIpTelegramm Tele = new KnxIpTelegramm(this);
                Tele.Heartbeat();
                byte[] TeleBytes = Tele.bytes;

               // KnxNetForm
               Log("H>:" + KnxTools.BytesToString(TeleBytes));

                udpClient.Send(TeleBytes, TeleBytes.Length);
                ConnectionState = KnxConnectionState.hbReq;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return ;
        }



        internal void DataAck(byte seqNo)
        {
            try
            {
                KnxIpTelegramm Tele = new KnxIpTelegramm(this);
                Tele.DataAck(seqNo);
                byte[] TeleBytes = Tele.bytes;

                // KnxNetForm
                Log("d>:" + KnxTools.BytesToString(TeleBytes));

                udpClient.Send(TeleBytes, TeleBytes.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return;
        }


        /// <summary>
        /// Daten auf den Bus senden
        /// </summary>
        /// <param name="emi">Telegrammdaten die gesendet werden sollen</param>
        internal void Send(cEMI emi)
        {
            try
            {
                KnxIpTelegramm Tele = new KnxIpTelegramm(this);
                Tele.Data(emi,SeqCounter++);
                byte[] TeleBytes = Tele.bytes;

                // KnxNetForm
                Log("H>:" + KnxTools.BytesToString(TeleBytes));

                udpClient.Send(TeleBytes, TeleBytes.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return ;
        }


        /// <summary>
        /// Diese Funktion wird vom UDP-Client aufgerufen wenn ein neues Telegramm eingetroffen ist
        /// </summary>
        /// <param name="ar"></param>
        public void ReceiveCallback(IAsyncResult ar)
        {
            int Anz = (int)ar.AsyncState;
            IPEndPoint e = new IPEndPoint(IPAddress.Any, 0);
            Byte[] receiveBytes =  udpClient.EndReceive(ar, ref e);
            Anz++;
            Console.WriteLine("Telegr[" + Anz + "]=" + KnxTools.BytesToString(receiveBytes));
            try
            {

                // prüfen ob es ein ControlTelegramm ist
                if (receiveBytes[2] == 0x02)
                {   // es ist ein Controlltelegramm
                    switch (receiveBytes[3])
                    {
                        case 0x01:  // Search Request
                            Console.WriteLine("Search Request from Gateway");
                            Log("<S:" + KnxTools.BytesToString(receiveBytes));
                            break;
                        case 0x02:  // Search Response
                            _channelId = receiveBytes[6];
                            Console.WriteLine("Search Response from Gateway");
                            Log("<s:" + KnxTools.BytesToString(receiveBytes));
                            break;
                        case 0x03:  // Description Request
                            Console.WriteLine("Description Request from Gateway");
                            Log("<D:" + KnxTools.BytesToString(receiveBytes));
                            break;
                        case 0x04:  // Description Response
                            _channelId = receiveBytes[6];
                            Console.WriteLine("Description Response from Gateway");
                            Log("<d:" + KnxTools.BytesToString(receiveBytes));
                            break;
                        case 0x05:  // Connect Request
                            Console.WriteLine("Connection Request from Gateway");
                            Log("<O:" + KnxTools.BytesToString(receiveBytes));
                            break;
                        case 0x06:  // Connect Response
                            _channelId = receiveBytes[6];
                            Console.WriteLine("ChannelId = " + _channelId);
                            Log("<o:" + KnxTools.BytesToString(receiveBytes));
                            if (receiveBytes[7] == 0) ConnectionState = KnxConnectionState.connected;
                            break;
                        case 0x07:  // Heartbeat Request
                            Console.WriteLine("HeartbeatRequest for ChannelId = " + _channelId);
                            Log("<H:" + KnxTools.BytesToString(receiveBytes));
                            break;
                        case 0x08:  // Heartbeat Response
                            Console.WriteLine("HeartbeatResponse from ChannelId = " + _channelId);
                            Log("<h:" + KnxTools.BytesToString(receiveBytes));
                            if (receiveBytes[7] == 0) ConnectionState = KnxConnectionState.connected;
                            break;
                        case 0x09:  // Disconnect Request
                            Console.WriteLine("DisconnectRequest for ChannelId = " + _channelId);
                            Log("<C:" + KnxTools.BytesToString(receiveBytes));
                            break;
                        case 0x0A:  // Disconnect Response
                            Console.WriteLine("Disconnected ChannelId = " + _channelId);
                            Log("<c:" + KnxTools.BytesToString(receiveBytes));
                            if (receiveBytes[7] == 0) ConnectionState = KnxConnectionState.disconnected;
                            break;
                        default:
                            Log("<?:" + KnxTools.BytesToString(receiveBytes));
                            break;
                    }

                }
                else if (receiveBytes[2] == 0x04)
                {   // es ist kein Controlltelegramm
                    if (receiveBytes[3] == 0x20)
                    {   // es ist ein Datentelegramm
                        // erst ein Ack senden
                        DataAck(receiveBytes[8]);
                        // Header entfernen
                        int idx = 0x0A;
                        int len = receiveBytes.Length - idx;

                        byte[] t = new byte[len];
                        Array.Copy(receiveBytes, idx, t, 0, len);
                        // und cemi Telegramm daraus erzeugen
                        cEMI emi = new cEMI(t);
                        Log(emi.ToString());
                        // Suchen des passenden HDKnx Objektes
                        HDKnx hdKnx = HDKnxHandler.GetObject(emi);
                        // und dort den Wert setzen, falls erforderlich
                        hdKnx.SetValue(emi);

                        // geänderte Daten melden falls gewünscht
                        if (dataChanged != null)
                        {
                            DataCangedWeiterleiten(hdKnx);
                        }

                        if (TelegramReceived != null)
                        {   // Ein Delegate ist eingerichtet, dann diesen aufrufen
                            TelegramWeiterleiten(emi);
                        }
                        if (QueueEnable)
                        {   // in Queue speichern
                            lock (fromKnxQueue)
                            {
                                fromKnxQueue.Enqueue(emi);
                            }
                        }


                    }
                    else if (receiveBytes[3] == 0x21)
                    {   // Bestätigung eines Datentelegramm
                        Console.WriteLine("Daten bestätigt  status = " + receiveBytes[9]);
                    }
                }
                ar = udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), Anz);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }


        /// <summary>
        /// Prüft ob für die Weiterleitung dr Telegramme ein Invoke notwendig ist und führt dies gegebenenfalls durch
        /// </summary>
        /// <param name="receiveBytes"></param>
        private void TelegramWeiterleiten(cEMI emi)
        {
            if (telegramReceivedControl != null)
            {
                if (telegramReceivedControl.InvokeRequired)
                {
                    Console.WriteLine("Invoke KnxNet.TelegramReceived(...)");
                    telegramReceivedControl.BeginInvoke(new TelegramReceivedDelegate(TelegramWeiterleiten), new object[] { emi });
                }
                else
                {
                    Console.WriteLine("KnxNet.TelegramReceived(...) ausführen");
                    TelegramReceived(emi);
                }
            }
            else
            {
                Console.WriteLine("KnxNet.TelegramReceived(...) ausführen");
                TelegramReceived(emi);
            }
        }


        /// <summary>
        /// Prüft ob für die Weiterleitung dr Telegramme ein Invoke notwendig ist und führt dies gegebenenfalls durch
        /// </summary>
        /// <param name="receiveBytes"></param>
        private void DataCangedWeiterleiten(HDKnx hdKnx)
        {
            try
            {
                if (dataChangedControl != null)
                {
                    if (dataChangedControl.InvokeRequired)
                    {
                        Console.WriteLine("Invoke KnxNet.DataCangedWeiterleiten(...)");
                        dataChangedControl.BeginInvoke(new DataChangedDelegate(DataCangedWeiterleiten), new object[] { hdKnx });
                    }
                    else
                    {
                        Console.WriteLine("KnxNet.DataCangedWeiterleiten(" + hdKnx + ") ausführen 1");
                        dataChanged(hdKnx);
                    }
                }
                else
                {
                    Console.WriteLine("KnxNet.DataCangedWeiterleiten(" + hdKnx + ") ausführen 2");
                    dataChanged(hdKnx);
                }

            }
            catch (Exception e)
            {
                
                throw e;
            }
        }




        /// <summary>
        /// Timer für den Heartbeat ruft diese Funktion
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEventHeartbeat(object source, ElapsedEventArgs e)
        {
            Heartbeat();
        }


        /// <summary>
        /// Abrufend der Daten (Pull-Verfahren), wenn diese nicht über einen Delegate automatisch gemeldet werden
        /// </summary>
        /// <returns></returns>
        internal cEMI GetData()
        {
            cEMI emi;
            lock (fromKnxQueue)
            {
                if (fromKnxQueue.Count > 0) emi = (cEMI) fromKnxQueue.Dequeue();
                else emi = null;
            }
            return emi;
        }


    }
}
