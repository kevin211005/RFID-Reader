using System;
using Modbus.Device;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace RFIDReader
{
    public class StateObject
    {
        // Client socket.     
        public Socket workSocket = null;
        // Size of receive buffer.     
        public const int BufferSize = 256;
        // Receive buffer.     
        public byte[] buffer = new byte[BufferSize];
        // Received data string.     
        public StringBuilder sb = new StringBuilder();
    }

    public class TCPCommunication
    {
        //***************    master/Client  ************
        public static string Master_IP = "169.254.10.12";
        public static byte Channel = 02;
        public static int Port = 10000;
        public static Socket client;
        // ManualResetEvent instances signal completion.     
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        private static bool success;
        private static string msg;

        public static bool Init(int channel)
        {
            bool success = false;
            try
            {
                Channel = BitConverter.GetBytes(channel * 2)[0];
                Connect();
                bool result = connectDone.WaitOne(10000);
                if (!result)
                {
                    msg = "Fail to connect server, timeout, 連線超時";
                    success = false;
                }
                else
                {
                    msg = "Successfully connect to the Master via TCP/IP 成功建立TCP/IP 連線";
                    success = true;
                }
            }
            catch (Exception ex)
            {
                success = false;
                msg = "Failed to create Master via TCP/IP, 建立TCP/IP 連線失敗!! \r\n" + ex.Message;
            }

            RFIDReader.delegateshowlog.Invoke(msg);
            return success;
        }

        public static bool ChangTag(string tagType)
        {
            byte tagTypeHightByte = byte.Parse("3" + tagType[0], System.Globalization.NumberStyles.HexNumber);
            byte tagTypeLowByte = byte.Parse("3" + tagType[1], System.Globalization.NumberStyles.HexNumber);
            byte[] cmd = new byte[] {00, 06, 04, Channel, tagTypeHightByte, tagTypeLowByte};
            success = SentTCPCommand(cmd);
            return success;
        }

        public static bool WriteDataToTag(byte[] startAdd, byte[] data)
        {
            success = false;
            int telegramLen = 6 + data.Length;
            byte[] cmd = new byte[telegramLen];
            byte telegramLenH = (byte) ((telegramLen + 1) / 256);
            byte telegramLenL = (byte)((telegramLen + 1) % 256);
            cmd[0] = telegramLenH;
            cmd[1] = telegramLenL;
            cmd[2] = 64;
            cmd[3] = (byte)(data.Length * 16 + Channel);
            cmd[4] = startAdd[0];
            cmd[5] = startAdd[1];
            for (int i = 6; i < telegramLen; i++)
            {
                cmd[i] = data[i - 6];
            }
            success = SentTCPCommand(cmd);
            return success;
        }

        public static bool ReadDataFromTag(byte[] startAdd, int numWords)
        {
            success = false;
            byte WordsChannel = (byte)(numWords * 16 + Channel);
            byte[] cmd = new byte[] { 00, 10, 04, WordsChannel, startAdd[0], startAdd[1]};
            success = SentTCPCommand(cmd);
            return success;
        }

        public static void WriteTCPCommand(byte[] cmd)
        {
            success = SentTCPCommand(cmd);
        }


        #region TCP/IP Communication 
        private static void Connect()
        {
            IPAddress ipAddress = IPAddress.Parse(Master_IP);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);
            // Create a TCP/IP socket.     
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Connect to the remote endpoint.     
            client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
        }
        private static void ConnectCallback(IAsyncResult asyresult)
        {
            try
            {
                // Retrieve the socket from the state object.     
                Socket client = (Socket)asyresult.AsyncState;
                // Complete the connection.     
                client.EndConnect(asyresult);
                msg = "Socket connected to {0}" + client.RemoteEndPoint.ToString();
                // Signal that the connection has been made.     
                connectDone.Set();
            }
            catch (Exception e)
            {
                msg = e.ToString();
            }
            Console.WriteLine(msg);
        }
        public static bool Close()
        {
            byte[] cmd = new byte[] { 00, 04, 02, Channel };
            success = SentTCPCommand(cmd);
            return success;
        }
        private static bool SentTCPCommand(byte[] cmd)
        {
            success = false;
            try
            {
                client.BeginSend(cmd, 0, cmd.Length, 0, new AsyncCallback(SendCallback), client);
                success = sendDone.WaitOne(10000);
                if (!success)
                {
                    msg = "Failed to send data: timeout, 傳送資料時間超時" + "\r\n";
                }
                else
                {
                    Receive(client);
                    success = receiveDone.WaitOne(10000);
                    if (!success)
                    {
                        msg = "Failed to reveive data: timeout, 接收時間超時" + "\r\n";
                    }
                    else
                    {
                        msg += "Successfully sending data , 傳送資料成功" + "\r\n";
                    }
                }
 
            }
            catch (Exception e)
            {
                msg = "Command :" + BitConverter.ToString(cmd).Replace('-', ' ') + "Failed \r\n";
                msg += "Send data to Server failed via TCP/IP, TCP/CP 傳送資料失敗" + e.ToString() + "\r\n";
            }
            RFIDReader.delegateshowlog.Invoke(msg);
            return success;
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.     
                Socket client = (Socket)ar.AsyncState;
                // Complete sending the data to the remote device.     
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);
                // Signal that all bytes have been sent.     
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void Receive(Socket client)
        {
            try
            {
                // Create the state object.     
                StateObject state = new StateObject();
                state.workSocket = client;
                // Begin receiving the data from the remote device.     
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket     
                // from the asynchronous state object.     
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                // Read data from the remote device.     
                int bytesRead = client.EndReceive(ar);
                // Check if the process finished 
                if (state.buffer[4] == 255)
                {
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    return;
                }
                msg = "";
                string hexString = BitConverter.ToString(state.buffer).Replace('-', ' ');
                msg = hexString + "\r\n";
                receiveDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion

    }
}
