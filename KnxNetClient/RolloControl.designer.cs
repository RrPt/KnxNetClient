namespace Knx
{
    partial class RolloControl
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
            this.btn_auf = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_Lamelle_ab = new System.Windows.Forms.Button();
            this.btn_Lamelle_auf = new System.Windows.Forms.Button();
            this.btn_ab = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_auf
            // 
            this.btn_auf.BackColor = System.Drawing.SystemColors.Control;
            this.btn_auf.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_auf.Location = new System.Drawing.Point(20, 19);
            this.btn_auf.Name = "btn_auf";
            this.btn_auf.Size = new System.Drawing.Size(75, 30);
            this.btn_auf.TabIndex = 0;
            this.btn_auf.Text = "auf";
            this.btn_auf.UseVisualStyleBackColor = false;
            this.btn_auf.Click += new System.EventHandler(this.btn_auf_Click);
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.btn_Stop);
            this.groupBox.Controls.Add(this.btn_Lamelle_ab);
            this.groupBox.Controls.Add(this.btn_Lamelle_auf);
            this.groupBox.Controls.Add(this.btn_ab);
            this.groupBox.Controls.Add(this.btn_auf);
            this.groupBox.Location = new System.Drawing.Point(3, 3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(265, 96);
            this.groupBox.TabIndex = 1;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Licht";
            this.groupBox.Resize += new System.EventHandler(this.groupBox_Resize);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Stop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Stop.Location = new System.Drawing.Point(191, 19);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(66, 66);
            this.btn_Stop.TabIndex = 4;
            this.btn_Stop.Text = "Korr";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Lamelle_Korr_Click);
            // 
            // btn_Lamelle_ab
            // 
            this.btn_Lamelle_ab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Lamelle_ab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Lamelle_ab.Location = new System.Drawing.Point(101, 55);
            this.btn_Lamelle_ab.Name = "btn_Lamelle_ab";
            this.btn_Lamelle_ab.Size = new System.Drawing.Size(84, 30);
            this.btn_Lamelle_ab.TabIndex = 3;
            this.btn_Lamelle_ab.Text = "Lamelle ab";
            this.btn_Lamelle_ab.UseVisualStyleBackColor = true;
            this.btn_Lamelle_ab.Click += new System.EventHandler(this.btn_Lamelle_ab_Click);
            // 
            // btn_Lamelle_auf
            // 
            this.btn_Lamelle_auf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Lamelle_auf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_Lamelle_auf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Lamelle_auf.Location = new System.Drawing.Point(101, 19);
            this.btn_Lamelle_auf.Name = "btn_Lamelle_auf";
            this.btn_Lamelle_auf.Size = new System.Drawing.Size(84, 30);
            this.btn_Lamelle_auf.TabIndex = 2;
            this.btn_Lamelle_auf.Text = "Lamelle auf";
            this.btn_Lamelle_auf.UseVisualStyleBackColor = false;
            this.btn_Lamelle_auf.Click += new System.EventHandler(this.btn_Lamelle_auf_Click);
            // 
            // btn_ab
            // 
            this.btn_ab.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ab.ForeColor = System.Drawing.Color.White;
            this.btn_ab.Location = new System.Drawing.Point(20, 55);
            this.btn_ab.Name = "btn_ab";
            this.btn_ab.Size = new System.Drawing.Size(75, 30);
            this.btn_ab.TabIndex = 1;
            this.btn_ab.Text = "ab";
            this.btn_ab.UseVisualStyleBackColor = true;
            this.btn_ab.Click += new System.EventHandler(this.btn_ab_Click);
            // 
            // RolloControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.groupBox);
            this.Name = "RolloControl";
            this.Size = new System.Drawing.Size(282, 102);
            this.groupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_auf;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button btn_ab;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_Lamelle_ab;
        private System.Windows.Forms.Button btn_Lamelle_auf;
    }
}
