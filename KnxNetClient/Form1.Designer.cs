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
            this.button3 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_Heartbeat = new System.Windows.Forms.Button();
            this.bt_Send = new System.Windows.Forms.Button();
            this.btn_Send0 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "StartConnection";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Open_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(636, 22);
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
            this.tBResponse.Location = new System.Drawing.Point(23, 61);
            this.tBResponse.Multiline = true;
            this.tBResponse.Name = "tBResponse";
            this.tBResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBResponse.Size = new System.Drawing.Size(688, 285);
            this.tBResponse.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(130, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Get";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_Heartbeat
            // 
            this.btn_Heartbeat.Location = new System.Drawing.Point(237, 22);
            this.btn_Heartbeat.Name = "btn_Heartbeat";
            this.btn_Heartbeat.Size = new System.Drawing.Size(75, 23);
            this.btn_Heartbeat.TabIndex = 4;
            this.btn_Heartbeat.Text = "Heartbeat";
            this.btn_Heartbeat.UseVisualStyleBackColor = true;
            this.btn_Heartbeat.Click += new System.EventHandler(this.btn_Heartbeat_Click);
            // 
            // bt_Send
            // 
            this.bt_Send.Location = new System.Drawing.Point(355, 22);
            this.bt_Send.Name = "bt_Send";
            this.bt_Send.Size = new System.Drawing.Size(75, 23);
            this.bt_Send.TabIndex = 5;
            this.bt_Send.Text = "Send Az auf";
            this.bt_Send.UseVisualStyleBackColor = true;
            this.bt_Send.Click += new System.EventHandler(this.bt_Send_Click);
            // 
            // btn_Send0
            // 
            this.btn_Send0.Location = new System.Drawing.Point(447, 22);
            this.btn_Send0.Name = "btn_Send0";
            this.btn_Send0.Size = new System.Drawing.Size(75, 23);
            this.btn_Send0.TabIndex = 6;
            this.btn_Send0.Text = "Send Az zu";
            this.btn_Send0.UseVisualStyleBackColor = true;
            this.btn_Send0.Click += new System.EventHandler(this.btn_Send0_Click);
            // 
            // KnxNetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 369);
            this.Controls.Add(this.btn_Send0);
            this.Controls.Add(this.bt_Send);
            this.Controls.Add(this.btn_Heartbeat);
            this.Controls.Add(this.button3);
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_Heartbeat;
        private System.Windows.Forms.Button bt_Send;
        private System.Windows.Forms.Button btn_Send0;
    }
}

