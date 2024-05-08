namespace Knuckles
{
    partial class Authorization
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
            this.bt_accept = new System.Windows.Forms.Button();
            this.tb_nick = new System.Windows.Forms.TextBox();
            this.lb_nick = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bt_accept
            // 
            this.bt_accept.Location = new System.Drawing.Point(178, 146);
            this.bt_accept.Name = "bt_accept";
            this.bt_accept.Size = new System.Drawing.Size(100, 31);
            this.bt_accept.TabIndex = 0;
            this.bt_accept.Text = "Принять";
            this.bt_accept.UseVisualStyleBackColor = true;
            this.bt_accept.Click += new System.EventHandler(this.bt_accept_Click);
            // 
            // tb_nick
            // 
            this.tb_nick.Location = new System.Drawing.Point(133, 79);
            this.tb_nick.Name = "tb_nick";
            this.tb_nick.Size = new System.Drawing.Size(194, 20);
            this.tb_nick.TabIndex = 1;
            this.tb_nick.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lb_nick
            // 
            this.lb_nick.AutoSize = true;
            this.lb_nick.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_nick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_nick.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_nick.ForeColor = System.Drawing.Color.White;
            this.lb_nick.Location = new System.Drawing.Point(182, 54);
            this.lb_nick.Name = "lb_nick";
            this.lb_nick.Size = new System.Drawing.Size(89, 22);
            this.lb_nick.TabIndex = 2;
            this.lb_nick.Text = "Nickname";
            this.lb_nick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Authorization
            // 
            this.AcceptButton = this.bt_accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(468, 269);
            this.Controls.Add(this.lb_nick);
            this.Controls.Add(this.tb_nick);
            this.Controls.Add(this.bt_accept);
            this.Name = "Authorization";
            this.Text = "Авторизация";
            this.Activated += new System.EventHandler(this.Authorization_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_accept;
        private System.Windows.Forms.TextBox tb_nick;
        private System.Windows.Forms.Label lb_nick;
    }
}