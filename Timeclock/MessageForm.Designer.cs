namespace PayrollTimeclock
{
    partial class MessageForm
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
            this.lblSendDate = new System.Windows.Forms.Label();
            this.txtSendDate = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtSender = new System.Windows.Forms.TextBox();
            this.lblSender = new System.Windows.Forms.Label();
            this.lblRecipients = new System.Windows.Forms.Label();
            this.lstRecipients = new System.Windows.Forms.ListBox();
            this.btnSendToAll = new System.Windows.Forms.Button();
            this.lblBody = new System.Windows.Forms.Label();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.btnOkay = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSendDate
            // 
            this.lblSendDate.AutoSize = true;
            this.lblSendDate.Location = new System.Drawing.Point(12, 15);
            this.lblSendDate.Name = "lblSendDate";
            this.lblSendDate.Size = new System.Drawing.Size(49, 13);
            this.lblSendDate.TabIndex = 0;
            this.lblSendDate.Text = "Sent On:";
            // 
            // txtSendDate
            // 
            this.txtSendDate.Location = new System.Drawing.Point(79, 12);
            this.txtSendDate.Name = "txtSendDate";
            this.txtSendDate.Size = new System.Drawing.Size(130, 20);
            this.txtSendDate.TabIndex = 1;
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(293, 38);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(397, 20);
            this.txtSubject.TabIndex = 8;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(290, 15);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(46, 13);
            this.lblSubject.TabIndex = 7;
            this.lblSubject.Text = "Subject:";
            // 
            // txtSender
            // 
            this.txtSender.Location = new System.Drawing.Point(79, 38);
            this.txtSender.Name = "txtSender";
            this.txtSender.Size = new System.Drawing.Size(188, 20);
            this.txtSender.TabIndex = 3;
            // 
            // lblSender
            // 
            this.lblSender.AutoSize = true;
            this.lblSender.Location = new System.Drawing.Point(12, 41);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(44, 13);
            this.lblSender.TabIndex = 2;
            this.lblSender.Text = "Sender:";
            // 
            // lblRecipients
            // 
            this.lblRecipients.AutoSize = true;
            this.lblRecipients.Location = new System.Drawing.Point(13, 64);
            this.lblRecipients.Margin = new System.Windows.Forms.Padding(3);
            this.lblRecipients.Name = "lblRecipients";
            this.lblRecipients.Size = new System.Drawing.Size(60, 13);
            this.lblRecipients.TabIndex = 4;
            this.lblRecipients.Text = "Recipients:";
            // 
            // lstRecipients
            // 
            this.lstRecipients.FormattingEnabled = true;
            this.lstRecipients.Location = new System.Drawing.Point(79, 64);
            this.lstRecipients.Name = "lstRecipients";
            this.lstRecipients.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstRecipients.Size = new System.Drawing.Size(188, 290);
            this.lstRecipients.TabIndex = 5;
            // 
            // btnSendToAll
            // 
            this.btnSendToAll.Location = new System.Drawing.Point(79, 360);
            this.btnSendToAll.Name = "btnSendToAll";
            this.btnSendToAll.Size = new System.Drawing.Size(130, 23);
            this.btnSendToAll.TabIndex = 6;
            this.btnSendToAll.Text = "All Recipients";
            this.btnSendToAll.UseVisualStyleBackColor = true;
            this.btnSendToAll.Click += new System.EventHandler(this.btnSendToAll_Click);
            // 
            // lblBody
            // 
            this.lblBody.AutoSize = true;
            this.lblBody.Location = new System.Drawing.Point(290, 64);
            this.lblBody.Margin = new System.Windows.Forms.Padding(3);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new System.Drawing.Size(80, 13);
            this.lblBody.TabIndex = 9;
            this.lblBody.Text = "Message Body:";
            // 
            // txtBody
            // 
            this.txtBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBody.Location = new System.Drawing.Point(293, 83);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBody.Size = new System.Drawing.Size(397, 352);
            this.txtBody.TabIndex = 10;
            // 
            // btnOkay
            // 
            this.btnOkay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOkay.Location = new System.Drawing.Point(454, 458);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Size = new System.Drawing.Size(115, 23);
            this.btnOkay.TabIndex = 12;
            this.btnOkay.Text = "OK";
            this.btnOkay.UseVisualStyleBackColor = true;
            this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(575, 458);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnReply
            // 
            this.btnReply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReply.Location = new System.Drawing.Point(333, 458);
            this.btnReply.Name = "btnReply";
            this.btnReply.Size = new System.Drawing.Size(115, 23);
            this.btnReply.TabIndex = 11;
            this.btnReply.Text = "Reply";
            this.btnReply.UseVisualStyleBackColor = true;
            this.btnReply.Visible = false;
            this.btnReply.Click += new System.EventHandler(this.btnReply_Click);
            // 
            // MessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 499);
            this.Controls.Add(this.btnReply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOkay);
            this.Controls.Add(this.txtBody);
            this.Controls.Add(this.lblBody);
            this.Controls.Add(this.btnSendToAll);
            this.Controls.Add(this.lstRecipients);
            this.Controls.Add(this.lblRecipients);
            this.Controls.Add(this.txtSender);
            this.Controls.Add(this.lblSender);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.txtSendDate);
            this.Controls.Add(this.lblSendDate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageForm";
            this.Text = "Message";
            this.Shown += new System.EventHandler(this.MessageForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSendDate;
        private System.Windows.Forms.TextBox txtSendDate;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSender;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.Label lblRecipients;
        private System.Windows.Forms.ListBox lstRecipients;
        private System.Windows.Forms.Button btnSendToAll;
        private System.Windows.Forms.Label lblBody;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.Button btnOkay;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReply;
    }
}