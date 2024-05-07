namespace Knuckles
{
    partial class Leaderboard
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
            this.lb_rating = new System.Windows.Forms.Label();
            this.lbox_users = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lb_rating
            // 
            this.lb_rating.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_rating.AutoSize = true;
            this.lb_rating.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_rating.Location = new System.Drawing.Point(157, 21);
            this.lb_rating.Name = "lb_rating";
            this.lb_rating.Size = new System.Drawing.Size(114, 31);
            this.lb_rating.TabIndex = 0;
            this.lb_rating.Text = "Рейтинг";
            // 
            // lbox_users
            // 
            this.lbox_users.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbox_users.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbox_users.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbox_users.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbox_users.FormattingEnabled = true;
            this.lbox_users.ItemHeight = 20;
            this.lbox_users.Location = new System.Drawing.Point(0, 109);
            this.lbox_users.Name = "lbox_users";
            this.lbox_users.Size = new System.Drawing.Size(452, 342);
            this.lbox_users.TabIndex = 1;
            // 
            // Leaderboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(452, 451);
            this.Controls.Add(this.lbox_users);
            this.Controls.Add(this.lb_rating);
            this.Name = "Leaderboard";
            this.Text = "Рейтинг";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_rating;
        private System.Windows.Forms.ListBox lbox_users;
    }
}