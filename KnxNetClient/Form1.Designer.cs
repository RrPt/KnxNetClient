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
            this.timerFileName = new System.Windows.Forms.Timer(this.components);
            this.timerRollosRrPt = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpDebug = new System.Windows.Forms.TabPage();
            this.btn_load_ESF_petz = new System.Windows.Forms.Button();
            this.btn_load_ESF_ads = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tBHBIntervall = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cBGatewayIP = new System.Windows.Forms.ComboBox();
            this.tBResponse = new System.Windows.Forms.TextBox();
            this.tpSteuerung = new System.Windows.Forms.TabPage();
            this.lblAussenHelligkeit = new System.Windows.Forms.Label();
            this.tBTemperatur = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblHelligkeitSued = new System.Windows.Forms.Label();
            this.lblHelligkeitOst = new System.Windows.Forms.Label();
            this.lblHelligkeitWest = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rC_Markise = new Knx.RolloControl();
            this.btnTest = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpDebug.SuspendLayout();
            this.tpSteuerung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerFileName
            // 
            this.timerFileName.Interval = 60000;
            this.timerFileName.Tick += new System.EventHandler(this.timerFileName_Tick);
            // 
            // timerRollosRrPt
            // 
            this.timerRollosRrPt.Tick += new System.EventHandler(this.timerRollosRrPt_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpDebug);
            this.tabControl1.Controls.Add(this.tpSteuerung);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(872, 527);
            this.tabControl1.TabIndex = 22;
            // 
            // tpDebug
            // 
            this.tpDebug.Controls.Add(this.btnTest);
            this.tpDebug.Controls.Add(this.btn_load_ESF_petz);
            this.tpDebug.Controls.Add(this.btn_load_ESF_ads);
            this.tpDebug.Controls.Add(this.button5);
            this.tpDebug.Controls.Add(this.button4);
            this.tpDebug.Controls.Add(this.tBHBIntervall);
            this.tpDebug.Controls.Add(this.button2);
            this.tpDebug.Controls.Add(this.button1);
            this.tpDebug.Controls.Add(this.cBGatewayIP);
            this.tpDebug.Controls.Add(this.tBResponse);
            this.tpDebug.Location = new System.Drawing.Point(4, 22);
            this.tpDebug.Name = "tpDebug";
            this.tpDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tpDebug.Size = new System.Drawing.Size(864, 501);
            this.tpDebug.TabIndex = 0;
            this.tpDebug.Text = "Debug";
            this.tpDebug.UseVisualStyleBackColor = true;
            // 
            // btn_load_ESF_petz
            // 
            this.btn_load_ESF_petz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_load_ESF_petz.Location = new System.Drawing.Point(759, 7);
            this.btn_load_ESF_petz.Name = "btn_load_ESF_petz";
            this.btn_load_ESF_petz.Size = new System.Drawing.Size(93, 23);
            this.btn_load_ESF_petz.TabIndex = 22;
            this.btn_load_ESF_petz.Text = "load ESF Petz";
            this.btn_load_ESF_petz.UseVisualStyleBackColor = true;
            this.btn_load_ESF_petz.Click += new System.EventHandler(this.btn_load_ESF_petz_Click);
            // 
            // btn_load_ESF_ads
            // 
            this.btn_load_ESF_ads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_load_ESF_ads.Location = new System.Drawing.Point(660, 8);
            this.btn_load_ESF_ads.Name = "btn_load_ESF_ads";
            this.btn_load_ESF_ads.Size = new System.Drawing.Size(93, 21);
            this.btn_load_ESF_ads.TabIndex = 21;
            this.btn_load_ESF_ads.Text = "load ESF ads";
            this.btn_load_ESF_ads.UseVisualStyleBackColor = true;
            this.btn_load_ESF_ads.Click += new System.EventHandler(this.btn_load_ESF_ads_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(487, 7);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 20;
            this.button5.Text = "load XML";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button_ReadXML_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(568, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 19;
            this.button4.Text = "save XML";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button_WriteXML_Click);
            // 
            // tBHBIntervall
            // 
            this.tBHBIntervall.Location = new System.Drawing.Point(343, 8);
            this.tBHBIntervall.Name = "tBHBIntervall";
            this.tBHBIntervall.Size = new System.Drawing.Size(38, 20);
            this.tBHBIntervall.TabIndex = 13;
            this.tBHBIntervall.Text = "60";
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button2.Location = new System.Drawing.Point(262, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "EndConnection";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Close_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(181, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "StartConnection";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Open_Click);
            // 
            // cBGatewayIP
            // 
            this.cBGatewayIP.FormattingEnabled = true;
            this.cBGatewayIP.Items.AddRange(new object[] {
            "192.168.22.3",
            "10.70.30.56"});
            this.cBGatewayIP.Location = new System.Drawing.Point(8, 7);
            this.cBGatewayIP.Name = "cBGatewayIP";
            this.cBGatewayIP.Size = new System.Drawing.Size(156, 21);
            this.cBGatewayIP.TabIndex = 10;
            // 
            // tBResponse
            // 
            this.tBResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tBResponse.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBResponse.Location = new System.Drawing.Point(8, 34);
            this.tBResponse.Multiline = true;
            this.tBResponse.Name = "tBResponse";
            this.tBResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBResponse.Size = new System.Drawing.Size(846, 433);
            this.tBResponse.TabIndex = 3;
            // 
            // tpSteuerung
            // 
            this.tpSteuerung.Controls.Add(this.label1);
            this.tpSteuerung.Controls.Add(this.label2);
            this.tpSteuerung.Controls.Add(this.label3);
            this.tpSteuerung.Controls.Add(this.label4);
            this.tpSteuerung.Controls.Add(this.lblHelligkeitWest);
            this.tpSteuerung.Controls.Add(this.lblHelligkeitOst);
            this.tpSteuerung.Controls.Add(this.lblHelligkeitSued);
            this.tpSteuerung.Controls.Add(this.lblAussenHelligkeit);
            this.tpSteuerung.Controls.Add(this.tBTemperatur);
            this.tpSteuerung.Controls.Add(this.pictureBox2);
            this.tpSteuerung.Controls.Add(this.pictureBox1);
            this.tpSteuerung.Location = new System.Drawing.Point(4, 22);
            this.tpSteuerung.Name = "tpSteuerung";
            this.tpSteuerung.Padding = new System.Windows.Forms.Padding(3);
            this.tpSteuerung.Size = new System.Drawing.Size(864, 501);
            this.tpSteuerung.TabIndex = 1;
            this.tpSteuerung.Text = "Steuerung";
            this.tpSteuerung.UseVisualStyleBackColor = true;
            // 
            // lblAussenHelligkeit
            // 
            this.lblAussenHelligkeit.AutoSize = true;
            this.lblAussenHelligkeit.Location = new System.Drawing.Point(742, 12);
            this.lblAussenHelligkeit.Name = "lblAussenHelligkeit";
            this.lblAussenHelligkeit.Size = new System.Drawing.Size(38, 13);
            this.lblAussenHelligkeit.TabIndex = 35;
            this.lblAussenHelligkeit.Text = "Außen";
            // 
            // tBTemperatur
            // 
            this.tBTemperatur.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBTemperatur.Location = new System.Drawing.Point(301, 33);
            this.tBTemperatur.Name = "tBTemperatur";
            this.tBTemperatur.Size = new System.Drawing.Size(100, 38);
            this.tBTemperatur.TabIndex = 34;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::Knx.Properties.Resources.Rollo;
            this.pictureBox2.Location = new System.Drawing.Point(532, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(80, 72);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 33;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = global::Knx.Properties.Resources.Gluehlampe2;
            this.pictureBox1.Location = new System.Drawing.Point(132, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(85, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rC_Markise);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(864, 501);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Markise";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblHelligkeitSued
            // 
            this.lblHelligkeitSued.AutoSize = true;
            this.lblHelligkeitSued.Location = new System.Drawing.Point(742, 33);
            this.lblHelligkeitSued.Name = "lblHelligkeitSued";
            this.lblHelligkeitSued.Size = new System.Drawing.Size(14, 13);
            this.lblHelligkeitSued.TabIndex = 36;
            this.lblHelligkeitSued.Text = "S";
            // 
            // lblHelligkeitOst
            // 
            this.lblHelligkeitOst.AutoSize = true;
            this.lblHelligkeitOst.Location = new System.Drawing.Point(742, 51);
            this.lblHelligkeitOst.Name = "lblHelligkeitOst";
            this.lblHelligkeitOst.Size = new System.Drawing.Size(15, 13);
            this.lblHelligkeitOst.TabIndex = 37;
            this.lblHelligkeitOst.Text = "O";
            // 
            // lblHelligkeitWest
            // 
            this.lblHelligkeitWest.AutoSize = true;
            this.lblHelligkeitWest.Location = new System.Drawing.Point(742, 73);
            this.lblHelligkeitWest.Name = "lblHelligkeitWest";
            this.lblHelligkeitWest.Size = new System.Drawing.Size(18, 13);
            this.lblHelligkeitWest.TabIndex = 38;
            this.lblHelligkeitWest.Text = "W";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(682, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "W";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(682, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "O";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(682, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "S";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(682, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Helligkeit";
            // 
            // rC_Markise
            // 
            this.rC_Markise.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rC_Markise.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rC_Markise.Location = new System.Drawing.Point(6, 26);
            this.rC_Markise.Name = "rC_Markise";
            this.rC_Markise.Size = new System.Drawing.Size(832, 469);
            this.rC_Markise.TabIndex = 0;
            this.rC_Markise.Titel = "Licht";
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(444, 6);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(40, 23);
            this.btnTest.TabIndex = 23;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // KnxNetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 551);
            this.Controls.Add(this.tabControl1);
            this.Name = "KnxNetForm";
            this.Text = "KnxNetClient      V17.1.26";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KnxNetForm_FormClosing);
            this.Load += new System.EventHandler(this.KnxNetForm_Load);
            this.Shown += new System.EventHandler(this.KnxNetForm_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tpDebug.ResumeLayout(false);
            this.tpDebug.PerformLayout();
            this.tpSteuerung.ResumeLayout(false);
            this.tpSteuerung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerFileName;
        private System.Windows.Forms.Timer timerRollosRrPt;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpDebug;
        private System.Windows.Forms.Button btn_load_ESF_petz;
        private System.Windows.Forms.Button btn_load_ESF_ads;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox tBHBIntervall;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cBGatewayIP;
        private System.Windows.Forms.TextBox tBResponse;
        private System.Windows.Forms.TabPage tpSteuerung;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tBTemperatur;
        private System.Windows.Forms.TabPage tabPage1;
        private RolloControl rC_Markise;
        private System.Windows.Forms.Label lblAussenHelligkeit;
        private System.Windows.Forms.Label lblHelligkeitWest;
        private System.Windows.Forms.Label lblHelligkeitOst;
        private System.Windows.Forms.Label lblHelligkeitSued;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTest;
    }
}

