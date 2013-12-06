package de.text.knx;

import java.net.InetAddress;
import java.net.MulticastSocket;
import java.net.DatagramPacket;
import java.net.DatagramSocket;

public class Main
{

  private static InetAddress gatewayAddress;
  private static int gatewayPort;


  public static void main(String[] args) throws Exception
  {

    Thread datagramReceiverThread = new Thread(new ListenerThread());
    datagramReceiverThread.start();

    InetAddress multicastGroup = InetAddress.getByName("224.0.23.12");
    MulticastSocket mcastSocket = new MulticastSocket(3671);

    mcastSocket.joinGroup(multicastGroup);


    int hibyte1 = 0xC0;
    int hibyte2 = 0xA8;

    byte[] clientDiscoveryPayload = new byte[]
        {
          0x06, 0x10, 0x02, 0x01, 0x00, 0x0E, 0x08, 0x01, (byte)hibyte1, (byte)hibyte2, 0x01, 0x34, 0x09, 0x29
        };

    DatagramPacket clientDiscovery = new DatagramPacket(
        clientDiscoveryPayload,
        clientDiscoveryPayload.length,
        multicastGroup,
        3671
    );

    mcastSocket.send(clientDiscovery);
    createConnection();
  }


  private static void createConnection()
  {
    try
    {
      DatagramSocket socket = new DatagramSocket(18001);

      socket.connect(gatewayAddress, gatewayPort);

      int clientIPClassA = 192;          // This is a fixed client-side IP address (192.168.1.53:18001)
      int clientIPClassB = 168;
      int clientIPClassC = 0;
      int clientIPClassD = 18;

      int clientUDPPortHiByte = 0x46;    // port 18001 = 0x4651
      int clientUDPPortLoByte = 0x51;

      byte[] serverIPAddress = gatewayAddress.getAddress(); 
      byte serverPortHiByte = (byte)((gatewayPort & 0x0000FF00) >> 8);
      byte serverPortLoByte = (byte)(gatewayPort & 0xFF);

      byte[] buffer = new byte[26];

      // Connect Header --------------------------------------------------

      buffer[0] = 0x06;     // header size = 6 for version 1.0 of protocol
      buffer[1] = 0x10;     // protocol version 1.0
      buffer[2] = 0x02;     // connect request (2 bytes)
      buffer[3] = 0x05;     //  -- '' --
      buffer[4] = 0x00;     // full size of packet (2 bytes) = v1.0 header size (6) + client + server HPAI (16 -- 8 bytes per HPAI) + CRI size (4) == 26 == 0x1A
      buffer[5] = 0x1A;

      // Client HPAI -----------------------------------------------------

      buffer[6] = 0x08;     // client HPAI structure len (8 bytes)
      buffer[7] = 0x01;     // protocol type (1 = UDP)
      buffer[8] = (byte)clientIPClassA;         // Client IP Address (4 bytes)
      buffer[9] = (byte)clientIPClassB;         //  -- '' --
      buffer[10] = (byte)clientIPClassC;        //  -- '' --
      buffer[11] = (byte)clientIPClassD;        //  -- '' --
      buffer[12] = (byte)clientUDPPortHiByte;   // Client UDP Port  (2 bytes)
      buffer[13] = (byte)clientUDPPortLoByte;   //  -- '' --

      // Server HPAI ------------------------------------------------------

      buffer[14] = 0x08;     // Server HPAI structure len (8 bytes)
      buffer[15] = 0x01;     // protocol type (1 = UDP)
      buffer[16] = serverIPAddress[0];          // Server IP Address (4 bytes)
      buffer[17] = serverIPAddress[1];          //  -- '' --
      buffer[18] = serverIPAddress[2];          //  -- '' --
      buffer[19] = serverIPAddress[3];          //  -- '' --
      buffer[20] = serverPortHiByte;            // Server Port (2 bytes)
      buffer[21] = serverPortLoByte;            //  -- '' --

      // CRI ---------------------------------------------------------------

      buffer[22] = 0x04;      // Structure len (4 bytes)
      buffer[23] = 0x04;      // Tunnel Connection
      buffer[24] = 0x02;      // KNX Layer (Tunnel Link Layer)
      buffer[25] = 0x00;      // Reserved


      DatagramPacket packet = new DatagramPacket(buffer, buffer.length);

      socket.send(packet);

      byte[] receivebuffer = new byte[8192];

      DatagramPacket receivePacket = new DatagramPacket(receivebuffer, receivebuffer.length);

      socket.receive(receivePacket);


      System.out.println("Channel ID       : " + receivebuffer[6]);
      System.out.println("Status           : " + receivebuffer[7]);


      final byte channelID = receivebuffer[6];

      int serverIPClassA = receivebuffer[10] & 0xFF;
      int serverIPClassB = receivebuffer[11] & 0xFF;
      int serverIPClassC = receivebuffer[12] & 0xFF;
      int serverIPClassD = receivebuffer[13] & 0xFF;

      int _serverPortHiByte = (receivebuffer[14] & 0xFF) << 8;
      int _serverPortLoByte = receivebuffer[15] & 0xFF;
      int port = _serverPortHiByte + _serverPortLoByte;

      System.out.println(
          "Gateway IP & Port: " +
          serverIPClassA + "." + serverIPClassB + "." + serverIPClassC + "." + serverIPClassD +
          ":" + port
      );

      System.out.println("");


      byte[] writebuffer = new byte[21];

      // EIBnet IP Header ----------------------------------------

      writebuffer[0] = 0x06;      // Header size: 6 bytes
      writebuffer[1] = 0x10;      // EIBnet IP Version
      writebuffer[2] = 0x04;      // Tunneling Request (2 bytes)
      writebuffer[3] = 0x20;      //  -- '' --
      writebuffer[4] = 0x00;      // size (2 bytes) == headize size (6b) + connection header (4b) + cEMI frame (11b for short telegram) == 21 == 0x15
      writebuffer[5] = 0x15;      //  -- '' --

      // Connection Header ---------------------------------------
      
      writebuffer[6] = 0x04;      // struct len: 4 bytes  -- TODO : wiki claims 0x06 value!
      writebuffer[7] = channelID;
      writebuffer[8] = 0x00;      // sequence counter
      writebuffer[9] = 0x00;      // reserved


      // cEMI frame ----------------------------------------------

      int controlField1 = 0x8C;
      int controlField2 = 0xE0;
      int switchON      = 0x81;

      writebuffer[10] = 0x11;     // message code
      writebuffer[11] = 0x00;     // additional info length : 0
      writebuffer[12] = (byte)controlField1;
      writebuffer[13] = (byte)controlField2;
      writebuffer[14] = 0x00;     // source address hi byte  (to be filled by gateway)
      writebuffer[15] = 0x00;     // source address lo byte  (to be filled by gateway)
      writebuffer[16] = 0x00;     // dest address hi byte
      writebuffer[17] = 0x04;     // dest address lo byte
      writebuffer[18] = 0x01;     // data len
      writebuffer[19] = 0x00;     // TPCI/APCI
      writebuffer[20] = (byte)switchON;     // APCI/data    (0x80 + 1 == bit on)

      DatagramPacket writepacket = new DatagramPacket(writebuffer, writebuffer.length);


      socket.send(writepacket);

      byte[] tunnelingACK = new byte[8192];

      DatagramPacket tunnelAckPacket = new DatagramPacket(tunnelingACK, tunnelingACK.length);

      socket.receive(tunnelAckPacket);

      byte[] data = tunnelAckPacket.getData();


    }
    catch (Throwable t)
    {
      return;
    }

  }



  static class ListenerThread implements Runnable
  {

    public void run()
    {
      try
      {
        final DatagramSocket responseListener = new DatagramSocket(2345);

        byte[] buffer = new byte[8092];

        DatagramPacket responsePacket = new DatagramPacket(buffer, buffer.length);

        responseListener.receive(responsePacket);

        gatewayAddress = responsePacket.getAddress();
        gatewayPort = responsePacket.getPort();

        
        byte[] responseData = responsePacket.getData();

        int searchResponse = responseData[2] << 8;
        searchResponse += responseData[3];

        int size1 = responseData[4];
        int size2 = responseData[5];
        int bodySize = size1+size2;

        System.out.println("EIBnet/IP bodysize: " + bodySize);
        System.out.println("Description size: " + (bodySize - 0x06 - 0x08));

        System.out.println("HPAI len: " + responseData[6] + "(0x08)");
        System.out.println("protocol type: " + responseData[7] + "(0x01 == IPv4 UDP)");

        int  ipClassA = (int) responseData[8] & 0xFF;
        int  ipClassB = (int) responseData[9] & 0xFF;
        int  ipClassC = (int) responseData[10] & 0xFF;
        int  ipClassD = (int) responseData[11] & 0xFF;

        int HPAIport = responseData[12] << 8;
        HPAIport += responseData[13];

        System.out.println("IP Address: " + ipClassA + "." +
                                            ipClassB + "." +
                                            ipClassC + "." +
                                            ipClassD + ":" + HPAIport);

        System.out.println("");
        System.out.println("DIB: Device Hardware");
        System.out.println("====================");

        System.out.println("Structure length: " + responseData[14]);
        System.out.println("Description Type Code: " + responseData[15]);
        System.out.println("KNX Medium: " + responseData[16]);
        System.out.println("Device Status: " + responseData[17]);

        int physicalAddress = (int) responseData[18] & 0xFF << 8;
        physicalAddress += (int) responseData[19] & 0xFF;

        System.out.println("Physical Address: " + physicalAddress);


        int projectInstallationID = (int) responseData[20] & 0xFF << 8;
        projectInstallationID += (int) responseData[21] & 0xFF;

        System.out.println("Project Installation ID: " + projectInstallationID);        

        int serialPart1 = ((int) responseData[22] & 0xFF) << 16;
        serialPart1 += ((int) responseData[23] & 0xFF) << 8;
        serialPart1 += (int) responseData[24] & 0xFF;

        int serialPart2 = ((int) responseData[25] & 0xFF) << 16;
        serialPart1 += ((int) responseData[26] & 0xFF) << 8;
        serialPart1 += (int) responseData[27] & 0xFF;

        System.out.println("Device Serial Number: " + serialPart1 + "" + serialPart2);

        int routingMulticastAddressClassA = (int) responseData[28];
        int routingMulticastAddressClassB = (int) responseData[29];
        int routingMulticastAddressClassC = (int) responseData[30];
        int routingMulticastAddressClassD = (int) responseData[31];

        System.out.println("Routing Multicast Address: " +
            routingMulticastAddressClassA + "." +
            routingMulticastAddressClassB + "." +
            routingMulticastAddressClassC + "." +
            routingMulticastAddressClassD
        );

        int macPart1 = ((int) responseData[32] & 0xFF) << 16;
        macPart1 += ((int) responseData[33] & 0xFF) << 8;
        macPart1 += (int) responseData[34] & 0xFF;

        int macPart2 = ((int) responseData[35] & 0xFF) << 16;
        macPart2 += ((int) responseData[36] & 0xFF) << 8;
        macPart2 += (int) responseData[37] & 0xFF;


        System.out.println("MAC: " + Integer.toHexString(macPart1) + "" + Integer.toHexString(macPart2));

        byte name[] = new byte[30];
        for (int i = 38; i < 68; ++i)
        {
          name[i - 38] = responseData[i];
        }

        System.out.println("DEVICE NAME: " + new String(name).trim());

        System.out.println("");
        System.out.println("DIB: Supported Service Families");
        System.out.println("===============================");

        System.out.println("Structure length: " + responseData[68]);
        System.out.println("Description Type Code: " + responseData[69]);

        System.out.println("Service Family ID: " + responseData[70]);
        System.out.println("Service Family Version: " + responseData[71]);

        System.out.println("Service Family ID: " + responseData[72]);
        System.out.println("Service Family Version: " + responseData[73]);

        System.out.println("Service Family ID: " + responseData[74]);
        System.out.println("Service Family Version: " + responseData[75]);

      }
      catch (Throwable t)
      {
        throw new Error(t);
      }
    }
  }


}
