namespace Knx
{
    partial class KnxNetForm
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tBResponse = new System.Windows.Forms.TextBox();
            this.bt_SendAn = new System.Windows.Forms.Button();
            this.btn_SendAus = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.cBGatewayIP = new System.Windows.Forms.ComboBox();
            this.timerFileName = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(186, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "StartConnection";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Open_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button2.Location = new System.Drawing.Point(267, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "EndConnection";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Close_Click);
            // 
            // tBResponse
            // 
            this.tBResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBResponse.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBResponse.Location = new System.Drawing.Point(23, 41);
            this.tBResponse.Multiline = true;
            this.tBResponse.Name = "tBResponse";
            this.tBResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBResponse.Size = new System.Drawing.Size(812, 316);
            this.tBResponse.TabIndex = 2;
            // 
            // bt_SendAn
            // 
            this.bt_SendAn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_SendAn.Location = new System.Drawing.Point(417, 12);
            this.bt_SendAn.Name = "bt_SendAn";
            this.bt_SendAn.Size = new System.Drawing.Size(94, 23);
            this.bt_SendAn.TabIndex = 5;
            this.bt_SendAn.Text = "Licht RrPt an";
            this.bt_SendAn.UseVisualStyleBackColor = true;
            this.bt_SendAn.Click += new System.EventHandler(this.bt_SendAn_Click);
            // 
            // btn_SendAus
            // 
            this.btn_SendAus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SendAus.Location = new System.Drawing.Point(517, 12);
            this.btn_SendAus.Name = "btn_SendAus";
            this.btn_SendAus.Size = new System.Drawing.Size(83, 23);
            this.btn_SendAus.TabIndex = 6;
            this.btn_SendAus.Text = "Licht RrPt aus";
            this.btn_SendAus.UseVisualStyleBackColor = true;
            this.btn_SendAus.Click += new System.EventHandler(this.btn_SendAus_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(741, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "save XML";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button_WriteXML_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(660, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "load XML";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button_ReadXML_Click);
            // 
            // cBGatewayIP
            // 
            this.cBGatewayIP.FormattingEnabled = true;
            this.cBGatewayIP.Items.AddRange(new object[] {
            "192.168.0.3",
            "10.70.30.56"});
            this.cBGatewayIP.Location = new System.Drawing.Point(24, 12);
            this.cBGatewayIP.Name = "cBGatewayIP";
            this.cBGatewayIP.Size = new System.Drawing.Size(156, 21);
            this.cBGatewayIP.TabIndex = 9;
            // 
            // timerFileName
            // 
            this.timerFileName.Interval = 60000;
            this.timerFileName.Tick += new System.EventHandler(this.timerFileName_Tick);
            // 
            // KnxNetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 369);
            this.Controls.Add(this.cBGatewayIP);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btn_SendAus);
            this.Controls.Add(this.bt_SendAn);
            this.Controls.Add(this.tBResponse);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "KnxNetForm";
            this.Text = "KnxNetClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tBResponse;
        private System.Windows.Forms.Button bt_SendAn;
        private System.Windows.Forms.Button btn_SendAus;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox cBGatewayIP;
        private System.Windows.Forms.Timer timerFileName;
    }
}

