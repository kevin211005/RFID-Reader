namespace RFIDReader
{
    partial class RFIDReader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txb_log = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_initialize = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.txb_cmd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_CT = new System.Windows.Forms.Button();
            this.btn_write = new System.Windows.Forms.Button();
            this.btn_read = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txb_write_startAdd = new System.Windows.Forms.TextBox();
            this.txb_read_startAdd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txb_write_data = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txb_numOfWords = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txb_channel = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txb_log
            // 
            this.txb_log.Location = new System.Drawing.Point(132, 2);
            this.txb_log.Multiline = true;
            this.txb_log.Name = "txb_log";
            this.txb_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txb_log.Size = new System.Drawing.Size(683, 497);
            this.txb_log.TabIndex = 0;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(677, 15);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(111, 35);
            this.btn_send.TabIndex = 1;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // btn_initialize
            // 
            this.btn_initialize.Location = new System.Drawing.Point(5, 49);
            this.btn_initialize.Name = "btn_initialize";
            this.btn_initialize.Size = new System.Drawing.Size(114, 38);
            this.btn_initialize.TabIndex = 2;
            this.btn_initialize.Text = "Initialize";
            this.btn_initialize.UseVisualStyleBackColor = true;
            this.btn_initialize.Click += new System.EventHandler(this.btn_initialize_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(4, 93);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(114, 38);
            this.btn_stop.TabIndex = 5;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // txb_cmd
            // 
            this.txb_cmd.Location = new System.Drawing.Point(5, 27);
            this.txb_cmd.Name = "txb_cmd";
            this.txb_cmd.Size = new System.Drawing.Size(660, 20);
            this.txb_cmd.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Command ";
            // 
            // btn_CT
            // 
            this.btn_CT.Location = new System.Drawing.Point(5, 137);
            this.btn_CT.Name = "btn_CT";
            this.btn_CT.Size = new System.Drawing.Size(114, 38);
            this.btn_CT.TabIndex = 10;
            this.btn_CT.Text = "Change Tag\r\n(轉換Tag)\r\n";
            this.btn_CT.UseVisualStyleBackColor = true;
            this.btn_CT.Click += new System.EventHandler(this.btn_CT_Click);
            // 
            // btn_write
            // 
            this.btn_write.Location = new System.Drawing.Point(5, 97);
            this.btn_write.Name = "btn_write";
            this.btn_write.Size = new System.Drawing.Size(112, 38);
            this.btn_write.TabIndex = 11;
            this.btn_write.Text = "Write\n";
            this.btn_write.UseVisualStyleBackColor = true;
            this.btn_write.Click += new System.EventHandler(this.btn_write_Click);
            // 
            // btn_read
            // 
            this.btn_read.Location = new System.Drawing.Point(3, 93);
            this.btn_read.Name = "btn_read";
            this.btn_read.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_read.Size = new System.Drawing.Size(114, 38);
            this.btn_read.TabIndex = 12;
            this.btn_read.Text = "Read";
            this.btn_read.UseVisualStyleBackColor = true;
            this.btn_read.Click += new System.EventHandler(this.btn_read_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Start Address";
            // 
            // txb_write_startAdd
            // 
            this.txb_write_startAdd.Location = new System.Drawing.Point(6, 32);
            this.txb_write_startAdd.Name = "txb_write_startAdd";
            this.txb_write_startAdd.Size = new System.Drawing.Size(111, 20);
            this.txb_write_startAdd.TabIndex = 14;
            // 
            // txb_read_startAdd
            // 
            this.txb_read_startAdd.Location = new System.Drawing.Point(5, 28);
            this.txb_read_startAdd.Name = "txb_read_startAdd";
            this.txb_read_startAdd.Size = new System.Drawing.Size(112, 20);
            this.txb_read_startAdd.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Start Address";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Data";
            // 
            // txb_write_data
            // 
            this.txb_write_data.Location = new System.Drawing.Point(6, 71);
            this.txb_write_data.Name = "txb_write_data";
            this.txb_write_data.Size = new System.Drawing.Size(111, 20);
            this.txb_write_data.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panel1.Controls.Add(this.btn_write);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txb_write_data);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txb_write_startAdd);
            this.panel1.Location = new System.Drawing.Point(7, 196);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 150);
            this.panel1.TabIndex = 19;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.YellowGreen;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.btn_initialize);
            this.panel2.Controls.Add(this.txb_channel);
            this.panel2.Controls.Add(this.btn_stop);
            this.panel2.Controls.Add(this.btn_CT);
            this.panel2.Location = new System.Drawing.Point(7, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(123, 188);
            this.panel2.TabIndex = 20;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txb_numOfWords);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btn_read);
            this.panel3.Controls.Add(this.txb_read_startAdd);
            this.panel3.Location = new System.Drawing.Point(7, 352);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(123, 147);
            this.panel3.TabIndex = 20;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel4.Controls.Add(this.btn_send);
            this.panel4.Controls.Add(this.txb_cmd);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(7, 505);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(808, 59);
            this.panel4.TabIndex = 21;
            // 
            // txb_numOfWords
            // 
            this.txb_numOfWords.Location = new System.Drawing.Point(5, 67);
            this.txb_numOfWords.Name = "txb_numOfWords";
            this.txb_numOfWords.Size = new System.Drawing.Size(112, 20);
            this.txb_numOfWords.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Channel ";
            // 
            // txb_channel
            // 
            this.txb_channel.Location = new System.Drawing.Point(6, 23);
            this.txb_channel.Name = "txb_channel";
            this.txb_channel.Size = new System.Drawing.Size(111, 20);
            this.txb_channel.TabIndex = 21;
            // 
            // RFIDReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(822, 568);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txb_log);
            this.Name = "RFIDReader";
            this.Text = "RFIDReader";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_log;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_initialize;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.TextBox txb_cmd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_CT;
        private System.Windows.Forms.Button btn_write;
        private System.Windows.Forms.Button btn_read;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_write_startAdd;
        private System.Windows.Forms.TextBox txb_read_startAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txb_write_data;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txb_numOfWords;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txb_channel;
    }
}

