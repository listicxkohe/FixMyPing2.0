namespace FixMyPing2._0
{
    partial class Form1
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
            this.dns_slection_combobox = new System.Windows.Forms.ComboBox();
            this.exit = new System.Windows.Forms.Button();
            this.setDns_btn = new System.Windows.Forms.Button();
            this.pingView_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dns_slection_combobox
            // 
            this.dns_slection_combobox.FormattingEnabled = true;
            this.dns_slection_combobox.Location = new System.Drawing.Point(84, 79);
            this.dns_slection_combobox.Name = "dns_slection_combobox";
            this.dns_slection_combobox.Size = new System.Drawing.Size(237, 21);
            this.dns_slection_combobox.TabIndex = 0;
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(526, 12);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(75, 23);
            this.exit.TabIndex = 1;
            this.exit.Text = "exit";
            this.exit.UseVisualStyleBackColor = true;
            // 
            // setDns_btn
            // 
            this.setDns_btn.Location = new System.Drawing.Point(154, 106);
            this.setDns_btn.Name = "setDns_btn";
            this.setDns_btn.Size = new System.Drawing.Size(75, 23);
            this.setDns_btn.TabIndex = 2;
            this.setDns_btn.Text = "button1";
            this.setDns_btn.UseVisualStyleBackColor = true;
            //this.setDns_btn.Click += new System.EventHandler(this.setDns_btn_Click);
            // 
            // pingView_label
            // 
            this.pingView_label.AutoSize = true;
            this.pingView_label.Location = new System.Drawing.Point(349, 82);
            this.pingView_label.Name = "pingView_label";
            this.pingView_label.Size = new System.Drawing.Size(35, 13);
            this.pingView_label.TabIndex = 3;
            this.pingView_label.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 403);
            this.Controls.Add(this.pingView_label);
            this.Controls.Add(this.setDns_btn);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.dns_slection_combobox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox dns_slection_combobox;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button setDns_btn;
        private System.Windows.Forms.Label pingView_label;
    }
}

