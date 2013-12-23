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
            l.AddRange(emi.DataToByte());

            _bytes = l.ToArray();
        }

    }


    class KnxNetConnection
    {
        public IPAddress myIP;
        public int clientPort = 18001;

        public int gatewayPort = 3671;
        public string gatewayIp;

        UdpClient udpClient ;
        IAsyncResult ar;
        int AnzTelegramme = 0;
        Queue<byte[]> fromKnxQueue = new Queue<byte[]>();
        public byte channelId = 0;
        private static Timer timerHeartbeat;
        public delegate void LoggingDelegate(string Text);
        LoggingDelegate Log = null;
        byte SeqCounter = 0;

        public KnxNetConnection()
        {
            IPHostEntry Host = Dns.GetHostEntry(Dns.GetHostName());
            myIP = Host.AddressList[1];
            udpClient = new UdpClient(clientPort);
            timerHeartbeat = new System.Timers.Timer(60000);
            timerHeartbeat.Elapsed += new ElapsedEventHandler(OnTimedEventHeartbeat);
        }

        public void SetLog(LoggingDelegate LogFunction)
        {
            Log = LogFunction; 
        }

        /// <summary>
        /// Öffnen der Verbindung
        /// </summary>
        /// <param name="gatewayIp"> IP Adresse des KnxIp-Gateways</param>
        internal byte[] Open(string gatewayIp)
        {
            Byte[] receiveBytes = null;
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

           }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // nun den Listener starten
            ar = udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), AnzTelegramme);

            // Den Heartbeat Timer starten
            timerHeartbeat.Start();

            return receiveBytes;
        }


        /// <summary>
        /// Schließen der Verbindung
        /// </summary>
        internal byte []  Close()
        {
            Byte[] receiveBytes = null;
            try
            {
                KnxIpTelegramm Tele = new KnxIpTelegramm(this);
                Tele.Disconnect();
                byte[] TeleBytes = Tele.bytes;

                //KnxNetForm.tb_Log.AppendText(Environment.NewLine + "C>:" + KnxTools.BytesToString(TeleBytes));
                Log("C>:" + KnxTools.BytesToString(TeleBytes));

                udpClient.Send(TeleBytes, TeleBytes.Length);

                //IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

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
            return receiveBytes;
        }


        /// <summary>
        /// HerzschlagRequest der Verbindung
        /// </summary>
        internal byte[] Heartbeat()
        {
            Byte[] receiveBytes = null;
            try
            {
                KnxIpTelegramm Tele = new KnxIpTelegramm(this);
                Tele.Heartbeat();
                byte[] TeleBytes = Tele.bytes;

               // KnxNetForm
               Log("H>:" + KnxTools.BytesToString(TeleBytes));

                udpClient.Send(TeleBytes, TeleBytes.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return receiveBytes;
        }


        /// <summary>
        /// Daten auf den Bus senden
        /// </summary>
        internal byte[] Send(cEMI emi)
        {
            Byte[] receiveBytes = null;
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
            return receiveBytes;
        }



        public void ReceiveCallback(IAsyncResult ar)
        {
            int Anz = (int)ar.AsyncState;
            IPEndPoint e = new IPEndPoint(IPAddress.Any, 0);
            Byte[] receiveBytes =  udpClient.EndReceive(ar, ref e);
            Anz++;
            Console.WriteLine("Telegr[" + Anz + "]=" + KnxTools.BytesToString(receiveBytes));

            // prüfen ob ControlTelegramm
            if (receiveBytes[2] == 0x02)
            {   // Controlltelegramm
                switch (receiveBytes[3])
                {
                    case 0x06:  // Connect Response
                        channelId = receiveBytes[6];
                        Console.WriteLine("ChannelId = " + channelId);
                        break;
                    case 0x08:  // Heartbeat Response
                        Console.WriteLine("HeartbeatResponse from ChannelId = " + channelId);
                        break;
                    case 0x0A:  // Disconnect Response
                        Console.WriteLine("Disconnected ChannelId = " + channelId);
                        break;
                    default: break;
                }

                Log("<c:" + KnxTools.BytesToString(receiveBytes));
            }
            else if (receiveBytes[2] == 0x04)
            {   // Kein Controlltelegramm
                if (receiveBytes[3] == 0x20)
                {   // Datentelegramm
                    int idx = 0x0A;
                    int len = receiveBytes.Length - idx;

                    byte[] t = new byte[len];
                    Array.Copy(receiveBytes, idx, t, 0, len);

                    cEMI emi = new cEMI(t);
                    Log(emi.ToString());

                    lock (fromKnxQueue)
                    {
                        fromKnxQueue.Enqueue(receiveBytes);
                    }
                }
                else if (receiveBytes[3] == 0x21)
                {   // Bestätigung eines Datentelegramm
                    Console.WriteLine("Daten bestätigt  status = " + receiveBytes[9]);
                }
            }
            ar = udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), Anz);
        }

        private void OnTimedEventHeartbeat(object source, ElapsedEventArgs e)
        {
            Heartbeat();
        }

        internal Byte[] GetData()
        {
            byte[] bytes;
            lock (fromKnxQueue)
            {
                if (fromKnxQueue.Count > 0) bytes = (byte[]) fromKnxQueue.Dequeue();
                else bytes = null;
            }
            return bytes;
        }


    }
}
