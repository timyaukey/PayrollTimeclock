namespace PayrollTimeclock
{
    partial class HomeForm
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
            this.lstEmployees = new System.Windows.Forms.ListBox();
            this.lblEmployees = new System.Windows.Forms.Label();
            this.btnClockInOut = new System.Windows.Forms.Button();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.lnkWebSite = new System.Windows.Forms.LinkLabel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.timerBlogRefresh = new System.Windows.Forms.Timer(this.components);
            this.lblNews = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstEmployees
            // 
            this.lstEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstEmployees.FormattingEnabled = true;
            this.lstEmployees.IntegralHeight = false;
            this.lstEmployees.Location = new System.Drawing.Point(12, 25);
            this.lstEmployees.Name = "lstEmployees";
            this.lstEmployees.Size = new System.Drawing.Size(221, 416);
            this.lstEmployees.TabIndex = 0;
            this.lstEmployees.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstEmployees_MouseDoubleClick);
            // 
            // lblEmployees
            // 
            this.lblEmployees.AutoSize = true;
            this.lblEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployees.Location = new System.Drawing.Point(12, 9);
            this.lblEmployees.Name = "lblEmployees";
            this.lblEmployees.Size = new System.Drawing.Size(71, 13);
            this.lblEmployees.TabIndex = 1;
            this.lblEmployees.Text = "Employees:";
            // 
            // btnClockInOut
            // 
            this.btnClockInOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClockInOut.Location = new System.Drawing.Point(27, 447);
            this.btnClockInOut.Name = "btnClockInOut";
            this.btnClockInOut.Size = new System.Drawing.Size(191, 23);
            this.btnClockInOut.TabIndex = 2;
            this.btnClockInOut.Text = "Clock In/Out Selected Employee";
            this.btnClockInOut.UseVisualStyleBackColor = true;
            this.btnClockInOut.Click += new System.EventHandler(this.btnClockInOut_Click);
            // 
            // btnAdmin
            // 
            this.btnAdmin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdmin.Location = new System.Drawing.Point(27, 476);
            this.btnAdmin.Name = "btnAdmin";
            this.btnAdmin.Size = new System.Drawing.Size(191, 23);
            this.btnAdmin.TabIndex = 3;
            this.btnAdmin.Text = "Administrative Functions";
            this.btnAdmin.UseVisualStyleBackColor = true;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);
            // 
            // lnkWebSite
            // 
            this.lnkWebSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkWebSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkWebSite.Location = new System.Drawing.Point(27, 511);
            this.lnkWebSite.Name = "lnkWebSite";
            this.lnkWebSite.Size = new System.Drawing.Size(191, 26);
            this.lnkWebSite.TabIndex = 4;
            this.lnkWebSite.TabStop = true;
            this.lnkWebSite.Text = "The Book Of Schmidt\'s";
            this.lnkWebSite.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lnkWebSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTheBook_LinkClicked);
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(248, 25);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(467, 509);
            this.webBrowser.TabIndex = 5;
            this.webBrowser.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // timerBlogRefresh
            // 
            this.timerBlogRefresh.Interval = 1000;
            this.timerBlogRefresh.Tick += new System.EventHandler(this.timerBlogRefresh_Tick);
            // 
            // lblNews
            // 
            this.lblNews.AutoSize = true;
            this.lblNews.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNews.Location = new System.Drawing.Point(245, 9);
            this.lblNews.Name = "lblNews";
            this.lblNews.Size = new System.Drawing.Size(42, 13);
            this.lblNews.TabIndex = 6;
            this.lblNews.Text = "News:";
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 546);
            this.Controls.Add(this.lblNews);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.lnkWebSite);
            this.Controls.Add(this.btnAdmin);
            this.Controls.Add(this.btnClockInOut);
            this.Controls.Add(this.lblEmployees);
            this.Controls.Add(this.lstEmployees);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HomeForm";
            this.Text = "Payroll Timeclock";
            this.Load += new System.EventHandler(this.HomeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstEmployees;
        private System.Windows.Forms.Label lblEmployees;
        private System.Windows.Forms.Button btnClockInOut;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.LinkLabel lnkWebSite;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Timer timerBlogRefresh;
        private System.Windows.Forms.Label lblNews;
    }
}

