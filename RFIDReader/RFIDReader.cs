using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RFIDReader
{
    public partial class RFIDReader : Form
    {
        public delegate void Delegateshowlog(string msg);
        public static Delegateshowlog delegateshowlog;
        public RFIDReader()
        {
            InitializeComponent();
            delegateshowlog = WriteLog;
        }
        private void WriteLog(string msg)
        {
            txb_log.AppendText(msg + "\r\n");
        }

        private void btn_initialize_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txb_channel.Text))
            {
                MessageBox.Show("Please input valid RFID channel 請輸入正確RFID channel");
                return;
            }
            else
            {
                if (txb_channel.Text == "1" || txb_channel.Text == "2")
                {
                    TCPCommunication.Init(int.Parse(txb_channel.Text));
                }
                else
                {
                    MessageBox.Show("Please input valid RFID channel 請輸入正確RFID channel");
                    return;
                }
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txb_cmd.Text))
            {
                MessageBox.Show("Please input validaddress 請輸入正確地址");
                return; 
            }
            string[] strCommand = txb_cmd.Text.Split(':');
            byte[] cmd = new byte[strCommand.Length];
            try
            {
                for (int i = 0; i < strCommand.Length; i++)
                {
                    cmd[i] = byte.Parse(strCommand[i], System.Globalization.NumberStyles.HexNumber);
                }
            }
            catch
            {
                WriteLog("Address error 輸入地址錯誤");

            }

            TCPCommunication.WriteTCPCommand(cmd);

        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            TCPCommunication.Close();
        }

        private void btn_write_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txb_write_data.Text) && !string.IsNullOrEmpty(txb_write_startAdd.Text))
                {
                    int add = int.Parse(txb_write_startAdd.Text);
                    byte[] startAdd = new byte[2];
                    startAdd[0] = (byte)(add / 256);
                    startAdd[1] = (byte)(add % 256);
                    string[] strCommand = txb_cmd.Text.Split(':');
                    byte[] cmd = new byte[strCommand.Length];
                    if (strCommand.Length % 8 != 0)
                    {
                        for (int i = 0; i < strCommand.Length; i++)
                        {
                            cmd[i] = byte.Parse(strCommand[i], System.Globalization.NumberStyles.HexNumber);
                        }
                        TCPCommunication.WriteDataToTag(startAdd, cmd);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid data lenght, one word eqaul to 8 bytes 錯誤資料輸入長度, 1單位長度為 8 bytes");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog("Address or data error 輸入地址或資料格式錯誤" + ex);
            }

        }

        private void btn_read_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txb_read_startAdd.Text) && !string.IsNullOrEmpty(txb_numOfWords.Text))
                {
                    int add = int.Parse(txb_read_startAdd.Text);
                    byte[] startAdd = new byte[2];
                    startAdd[0] = (byte)(add / 256);
                    startAdd[1] = (byte)(add % 256);
                    int numWords = int.Parse(txb_numOfWords.Text);
                    TCPCommunication.ReadDataFromTag(startAdd, numWords);
                }
            }
            catch
            {
                WriteLog("Address or data error 輸入地址或資料格式錯誤");
            }
        }

        private void btn_CT_Click(object sender, EventArgs e)
        {
            bool success = TCPCommunication.ChangTag("21");
            if (success)
            {
                WriteLog("Changed tag successfully 成功改變Tag型態");
            }
            else
            {
                WriteLog("Failed to Change tag  改變Tag型態失敗");
            }
        }
    }
}
