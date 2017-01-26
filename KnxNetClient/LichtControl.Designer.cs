namespace Knx
{
    partial class LichtControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_an = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.btn_aus = new System.Windows.Forms.Button();
            this.btn_dunkler = new System.Windows.Forms.Button();
            this.btn_heller = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_an
            // 
            this.btn_an.BackColor = System.Drawing.Color.Yellow;
            this.btn_an.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_an.Location = new System.Drawing.Point(20, 19);
            this.btn_an.Name = "btn_an";
            this.btn_an.Size = new System.Drawing.Size(75, 30);
            this.btn_an.TabIndex = 0;
            this.btn_an.Text = "an";
            this.btn_an.UseVisualStyleBackColor = false;
            this.btn_an.Click += new System.EventHandler(this.btn_an_Click);
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.btn_Stop);
            this.groupBox.Controls.Add(this.btn_dunkler);
            this.groupBox.Controls.Add(this.btn_heller);
            this.groupBox.Controls.Add(this.btn_aus);
            this.groupBox.Controls.Add(this.btn_an);
            this.groupBox.Location = new System.Drawing.Point(3, 3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(265, 96);
            this.groupBox.TabIndex = 1;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Licht";
            // 
            // btn_aus
            // 
            this.btn_aus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_aus.ForeColor = System.Drawing.Color.White;
            this.btn_aus.Location = new System.Drawing.Point(20, 55);
            this.btn_aus.Name = "btn_aus";
            this.btn_aus.Size = new System.Drawing.Size(75, 30);
            this.btn_aus.TabIndex = 1;
            this.btn_aus.Text = "aus";
            this.btn_aus.UseVisualStyleBackColor = true;
            this.btn_aus.Click += new System.EventHandler(this.btn_aus_Click);
            // 
            // btn_dunkler
            // 
            this.btn_dunkler.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dunkler.Location = new System.Drawing.Point(101, 55);
            this.btn_dunkler.Name = "btn_dunkler";
            this.btn_dunkler.Size = new System.Drawing.Size(84, 30);
            this.btn_dunkler.TabIndex = 3;
            this.btn_dunkler.Text = "dunkler";
            this.btn_dunkler.UseVisualStyleBackColor = true;
            this.btn_dunkler.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_dunkler_MouseDown);
            this.btn_dunkler.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_dunkler_MouseUp);
            // 
            // btn_heller
            // 
            this.btn_heller.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_heller.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_heller.Location = new System.Drawing.Point(101, 19);
            this.btn_heller.Name = "btn_heller";
            this.btn_heller.Size = new System.Drawing.Size(84, 30);
            this.btn_heller.TabIndex = 2;
            this.btn_heller.Text = "heller";
            this.btn_heller.UseVisualStyleBackColor = false;
            this.btn_heller.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_heller_MouseDown);
            this.btn_heller.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_heller_MouseUp);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Stop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Stop.Location = new System.Drawing.Point(191, 19);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(66, 66);
            this.btn_Stop.TabIndex = 4;
            this.btn_Stop.Text = "stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // LichtControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.groupBox);
            this.Name = "LichtControl";
            this.Size = new System.Drawing.Size(282, 102);
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_an;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button btn_aus;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_dunkler;
        private System.Windows.Forms.Button btn_heller;
    }
}
