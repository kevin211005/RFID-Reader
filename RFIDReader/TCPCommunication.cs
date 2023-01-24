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
    public enum ErrorCode
    {

    }
    public class TCPCommunication
    {
        //***************    master/Client  ************
        public static string Master_IP = "169.254.10.12";

        public static byte Channel = 02; // RFID Channel either 2 (channel 1) or 4 (channel 2) RFID 通道: Channel1: 02, Channel2:04 
        public static int Port = 10000;  // RFID TCP/IP Port RFID TCP/IP 通訊協定 埠
        public static Socket client; // TCP/IP Client, TCP/IP 應用端 
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
                Channel = BitConverter.GetBytes(channel * 2)[0]; // Changed Channel data to corresponding byte for RFID TCP/IP, 轉換RFID 通道值
                Connect(); // Connect to Sever by TCP/IP, 連接RFID Server 端
                bool result = connectDone.WaitOne(10000); //Wait Server response, 等待Server 回應
                if (!result)
                {
                    throw new TimeoutException("Connect to RFID Server Timeout, 連接RFID 超時");
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

        public static bool Close()
        {
            byte[] cmd = new byte[] { 00, 04, 02, Channel }; // Run Quit cmd 執行退出指令
            success = SentTCPCommand(cmd);
            return success;
        }

        public static bool ChangTag(string tagType)
        {
            success = false;
            //String tagType to corresponding byte tagType 轉換tagtype 至 byte 
            try
            {
                byte tagTypeHightByte = byte.Parse("3" + tagType[0], System.Globalization.NumberStyles.HexNumber);
                byte tagTypeLowByte = byte.Parse("3" + tagType[1], System.Globalization.NumberStyles.HexNumber);
                byte[] cmd = new byte[] { 00, 06, 04, Channel, tagTypeHightByte, tagTypeLowByte };
                success = SentTCPCommand(cmd);
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }
           
            return success;
        }

        public static bool WriteDataToTag(byte[] startAdd, byte[] data)
        {
            success = false;
            try
            {
                int telegramLen = 6 + data.Length;
                byte[] cmd = new byte[telegramLen];
                // Changed telegram length to high byte telegramLenH and low byte telegramLenL 轉換電報長度至 telegramLenH, telegramLenH
                byte telegramLenH = (byte)((telegramLen + 1) / 256);
                byte telegramLenL = (byte)((telegramLen + 1) % 256);
                cmd[0] = telegramLenH;
                cmd[1] = telegramLenL;
                cmd[2] = 64; //Single Write Data cmd 單次寫入資料指令
                cmd[3] = (byte)(data.Length * 16 + Channel);
                cmd[4] = startAdd[0];
                cmd[5] = startAdd[1];
                for (int i = 6; i < telegramLen; i++)
                {
                    cmd[i] = data[i - 6];
                }
                success = SentTCPCommand(cmd);
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }
            return success;
        }

        public static bool ReadDataFromTag(byte[] startAdd, int numWords)
        {
            success = false;
            try
            {
                byte WordsChannel = (byte)(numWords * 16 + Channel);
                byte[] cmd = new byte[] { 00, 10, 04, WordsChannel, startAdd[0], startAdd[1]};
                success = SentTCPCommand(cmd);
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }
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
            // Create a TCP/IP socket 建立tcp/ip 資料傳輸接口     
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Connect to the remote endpoint 連接tcp/ip server 端
            client.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), client);
        }

        private static bool SentTCPCommand(byte[] cmd)
        {
            success = false;
            try
            {
                client.BeginSend(cmd, 0, cmd.Length, 0, new AsyncCallback(SendCallback), client); // Send TCP cmd 傳遞tcp cmd 
                success = sendDone.WaitOne(10000);
                if (!success)
                {
                    msg = "Failed to send data: timeout, 傳送資料時間超時" + "\r\n";
                }
                else
                {
                    Receive(client); //Receive response 接收Server 端回應
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

        private static void Receive(Socket client)
        {
            try
            {
                // Create the state object.  建立狀態物件用來接收資訊及狀態  
                StateObject state = new StateObject();
                state.workSocket = client;
                // Begin receiving the data from the remote device. 開始接收來至 Server 端資料     
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        #region callback function 回乎函數        
        private static void ConnectCallback(IAsyncResult asyresult)
        {
            try
            {
                // Retrieve the socket from the state object. 從街口端獲取資料
                Socket client = (Socket)asyresult.AsyncState;
                // Complete the connection. 完成連線     
                client.EndConnect(asyresult);
                // Signal that the connection has been made.     
                connectDone.Set();
            }
            catch (Exception e)
            {
                msg = e.ToString();
            }
            Console.WriteLine(msg);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.     
                Socket client = (Socket)ar.AsyncState;
                // Complete sending the data to the remote device. 完成傳送資料
                int bytesSent = client.EndSend(ar);
                // Signal that all bytes have been sent.     
                sendDone.Set();
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
                // Read data from the remote device. 完成接收資料     
                int bytesRead = client.EndReceive(ar);
                // Check if the process finished 檢查RFID 回應狀態
                if (state.buffer[4] == 255)
                {
                    //Processing cmd req, retrieve response again,處理RFID cmd 中 再次送指令接收資訊
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    return;
                }
                else if (state.buffer[4] == 00) // RFID send data without err 成功通訊
                {
                    msg = "";
                    string hexString = BitConverter.ToString(state.buffer).Replace('-', ' ');
                    msg = hexString + "\r\n";
                }
                else //Error response from RFID // RFID 回傳錯誤資訊
                {

                }
                receiveDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion
        #endregion

    }
}
