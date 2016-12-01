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
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.cBGatewayIP = new System.Windows.Forms.ComboBox();
            this.timerFileName = new System.Windows.Forms.Timer(this.components);
            this.btn_DimHeller = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.btn_DimDunkler = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.tBHBIntervall = new System.Windows.Forms.TextBox();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.btnRolloRrPtKorrektur = new System.Windows.Forms.Button();
            this.timerRollosRrPt = new System.Windows.Forms.Timer(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 39);
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
            this.button2.Location = new System.Drawing.Point(105, 39);
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
            this.tBResponse.Location = new System.Drawing.Point(23, 90);
            this.tBResponse.Multiline = true;
            this.tBResponse.Name = "tBResponse";
            this.tBResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBResponse.Size = new System.Drawing.Size(1433, 449);
            this.tBResponse.TabIndex = 2;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(1271, 17);
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
            this.button5.Location = new System.Drawing.Point(1271, 55);
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
            "192.168.22.3",
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
            // btn_DimHeller
            // 
            this.btn_DimHeller.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DimHeller.Location = new System.Drawing.Point(367, 12);
            this.btn_DimHeller.Name = "btn_DimHeller";
            this.btn_DimHeller.Size = new System.Drawing.Size(61, 35);
            this.btn_DimHeller.TabIndex = 11;
            this.btn_DimHeller.Text = "Dim RrPt heller";
            this.btn_DimHeller.UseVisualStyleBackColor = true;
            this.btn_DimHeller.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_DimHeller_MouseDown);
            this.btn_DimHeller.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_DimHeller_MouseUp);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Location = new System.Drawing.Point(278, 12);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(83, 35);
            this.button7.TabIndex = 5;
            this.button7.Text = "Licht RrPt an";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.bt_SendAn_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Location = new System.Drawing.Point(278, 49);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(83, 35);
            this.button8.TabIndex = 6;
            this.button8.Text = "Licht RrPt aus";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.btn_SendAus_Click);
            // 
            // btn_DimDunkler
            // 
            this.btn_DimDunkler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DimDunkler.Location = new System.Drawing.Point(367, 49);
            this.btn_DimDunkler.Name = "btn_DimDunkler";
            this.btn_DimDunkler.Size = new System.Drawing.Size(61, 35);
            this.btn_DimDunkler.TabIndex = 10;
            this.btn_DimDunkler.Text = "Dim RrPt dunkler";
            this.btn_DimDunkler.UseVisualStyleBackColor = true;
            this.btn_DimDunkler.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_DimDunkler_MouseDown);
            this.btn_DimDunkler.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_DimDunkler_MouseUp);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Location = new System.Drawing.Point(434, 12);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(83, 35);
            this.button6.TabIndex = 12;
            this.button6.Text = "Dim RrPt Stop";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btn_SendDimStop_Click);
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.Location = new System.Drawing.Point(698, 48);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(83, 35);
            this.button10.TabIndex = 15;
            this.button10.Text = "Lamelle RrPt Ab";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.bt_LamelleAb_Click);
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button11.Location = new System.Drawing.Point(609, 11);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(83, 35);
            this.button11.TabIndex = 14;
            this.button11.Text = "Rollo RrPt auf";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.btn_RolloAuf_Click);
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.Location = new System.Drawing.Point(609, 48);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(83, 35);
            this.button12.TabIndex = 13;
            this.button12.Text = "Rollo RrPt ab";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.bt_RolloAb_Click);
            // 
            // button13
            // 
            this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button13.Location = new System.Drawing.Point(698, 11);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(83, 35);
            this.button13.TabIndex = 16;
            this.button13.Text = "Lamelle RrPt Auf";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.btn_LamelleAuf_Click);
            // 
            // tBHBIntervall
            // 
            this.tBHBIntervall.Location = new System.Drawing.Point(24, 63);
            this.tBHBIntervall.Name = "tBHBIntervall";
            this.tBHBIntervall.Size = new System.Drawing.Size(100, 20);
            this.tBHBIntervall.TabIndex = 4;
            this.tBHBIntervall.Text = "60";
            this.tBHBIntervall.TextChanged += new System.EventHandler(this.tBHBIntervall_TextChanged);
            // 
            // button14
            // 
            this.button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button14.Location = new System.Drawing.Point(1363, 18);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(93, 21);
            this.button14.TabIndex = 17;
            this.button14.Text = "load ESF ads";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button15.Location = new System.Drawing.Point(1363, 55);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(93, 23);
            this.button15.TabIndex = 18;
            this.button15.Text = "load ESF Petz";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // btnRolloRrPtKorrektur
            // 
            this.btnRolloRrPtKorrektur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRolloRrPtKorrektur.Location = new System.Drawing.Point(787, 12);
            this.btnRolloRrPtKorrektur.Name = "btnRolloRrPtKorrektur";
            this.btnRolloRrPtKorrektur.Size = new System.Drawing.Size(83, 35);
            this.btnRolloRrPtKorrektur.TabIndex = 19;
            this.btnRolloRrPtKorrektur.Text = "Lamelle RrPt Korrektur";
            this.btnRolloRrPtKorrektur.UseVisualStyleBackColor = true;
            this.btnRolloRrPtKorrektur.Click += new System.EventHandler(this.btnRolloRrPtKorrektur_Click);
            // 
            // timerRollosRrPt
            // 
            this.timerRollosRrPt.Tick += new System.EventHandler(this.timerRollosRrPt_Tick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::Knx.Properties.Resources.Rollo;
            this.pictureBox2.Location = new System.Drawing.Point(523, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(80, 72);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = global::Knx.Properties.Resources.Gluehlampe2;
            this.pictureBox1.Location = new System.Drawing.Point(187, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(85, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // KnxNetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 551);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnRolloRrPtKorrektur);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.tBHBIntervall);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.btn_DimHeller);
            this.Controls.Add(this.btn_DimDunkler);
            this.Controls.Add(this.cBGatewayIP);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.tBResponse);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "KnxNetForm";
            this.Text = "KnxNetClient      V16.6.29";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KnxNetForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tBResponse;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox cBGatewayIP;
        private System.Windows.Forms.Timer timerFileName;
        private System.Windows.Forms.Button btn_DimHeller;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button btn_DimDunkler;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.TextBox tBHBIntervall;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button btnRolloRrPtKorrektur;
        private System.Windows.Forms.Timer timerRollosRrPt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

