namespace PayrollTimeclock
{
    partial class EmployeeTimesForm
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
            this.lvwPairs = new System.Windows.Forms.ListView();
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDayOfWeek = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEnd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClockNow = new System.Windows.Forms.Button();
            this.btnAddSpecific = new System.Windows.Forms.Button();
            this.btnDeleteSpecific = new System.Windows.Forms.Button();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.txtSpecificDateTime = new System.Windows.Forms.TextBox();
            this.btnCurrentPeriod = new System.Windows.Forms.Button();
            this.btnLastPeriod = new System.Windows.Forms.Button();
            this.lblPresentHours = new System.Windows.Forms.Label();
            this.lblPeriodDates = new System.Windows.Forms.Label();
            this.lblOvertimeHours = new System.Windows.Forms.Label();
            this.lvwMessages = new System.Windows.Forms.ListView();
            this.colMsgDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMsgSubject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMsgSender = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblMessages = new System.Windows.Forms.Label();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.btnReadMessage = new System.Windows.Forms.Button();
            this.lblProgess = new System.Windows.Forms.Label();
            this.btnAddAbsent = new System.Windows.Forms.Button();
            this.lblAbsentHours = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvwPairs
            // 
            this.lvwPairs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwPairs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colDayOfWeek,
            this.colStart,
            this.colEnd,
            this.colLength});
            this.lvwPairs.FullRowSelect = true;
            this.lvwPairs.Location = new System.Drawing.Point(12, 12);
            this.lvwPairs.MultiSelect = false;
            this.lvwPairs.Name = "lvwPairs";
            this.lvwPairs.Size = new System.Drawing.Size(439, 367);
            this.lvwPairs.TabIndex = 0;
            this.lvwPairs.UseCompatibleStateImageBehavior = false;
            this.lvwPairs.View = System.Windows.Forms.View.Details;
            // 
            // colDate
            // 
            this.colDate.Text = "Date";
            this.colDate.Width = 80;
            // 
            // colDayOfWeek
            // 
            this.colDayOfWeek.Text = "Day Of Week";
            this.colDayOfWeek.Width = 85;
            // 
            // colStart
            // 
            this.colStart.Text = "Start";
            this.colStart.Width = 80;
            // 
            // colEnd
            // 
            this.colEnd.Text = "End";
            this.colEnd.Width = 80;
            // 
            // colLength
            // 
            this.colLength.Text = "Hours";
            this.colLength.Width = 89;
            // 
            // btnClockNow
            // 
            this.btnClockNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClockNow.Enabled = false;
            this.btnClockNow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClockNow.Location = new System.Drawing.Point(460, 12);
            this.btnClockNow.Name = "btnClockNow";
            this.btnClockNow.Size = new System.Drawing.Size(181, 29);
            this.btnClockNow.TabIndex = 1;
            this.btnClockNow.Text = "Clock In/Out NOW";
            this.btnClockNow.UseVisualStyleBackColor = true;
            this.btnClockNow.Click += new System.EventHandler(this.btnClockNow_Click);
            // 
            // btnAddSpecific
            // 
            this.btnAddSpecific.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSpecific.Enabled = false;
            this.btnAddSpecific.Location = new System.Drawing.Point(460, 139);
            this.btnAddSpecific.Name = "btnAddSpecific";
            this.btnAddSpecific.Size = new System.Drawing.Size(181, 23);
            this.btnAddSpecific.TabIndex = 6;
            this.btnAddSpecific.Text = "Add Specific Date and Time";
            this.btnAddSpecific.UseVisualStyleBackColor = true;
            this.btnAddSpecific.Click += new System.EventHandler(this.btnAddSpecific_Click);
            // 
            // btnDeleteSpecific
            // 
            this.btnDeleteSpecific.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSpecific.Enabled = false;
            this.btnDeleteSpecific.Location = new System.Drawing.Point(460, 168);
            this.btnDeleteSpecific.Name = "btnDeleteSpecific";
            this.btnDeleteSpecific.Size = new System.Drawing.Size(181, 23);
            this.btnDeleteSpecific.TabIndex = 7;
            this.btnDeleteSpecific.Text = "Delete Specific Date and Time";
            this.btnDeleteSpecific.UseVisualStyleBackColor = true;
            this.btnDeleteSpecific.Click += new System.EventHandler(this.btnDeleteSpecific_Click);
            // 
            // lblDateTime
            // 
            this.lblDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Location = new System.Drawing.Point(460, 236);
            this.lblDateTime.Margin = new System.Windows.Forms.Padding(3);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(80, 13);
            this.lblDateTime.TabIndex = 9;
            this.lblDateTime.Text = "Date and Time:";
            // 
            // txtSpecificDateTime
            // 
            this.txtSpecificDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSpecificDateTime.Location = new System.Drawing.Point(460, 255);
            this.txtSpecificDateTime.Name = "txtSpecificDateTime";
            this.txtSpecificDateTime.Size = new System.Drawing.Size(159, 20);
            this.txtSpecificDateTime.TabIndex = 10;
            // 
            // btnCurrentPeriod
            // 
            this.btnCurrentPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCurrentPeriod.Enabled = false;
            this.btnCurrentPeriod.Location = new System.Drawing.Point(460, 327);
            this.btnCurrentPeriod.Name = "btnCurrentPeriod";
            this.btnCurrentPeriod.Size = new System.Drawing.Size(181, 23);
            this.btnCurrentPeriod.TabIndex = 11;
            this.btnCurrentPeriod.Text = "Show Current Period";
            this.btnCurrentPeriod.UseVisualStyleBackColor = true;
            this.btnCurrentPeriod.Click += new System.EventHandler(this.btnCurrentPeriod_Click);
            // 
            // btnLastPeriod
            // 
            this.btnLastPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLastPeriod.Enabled = false;
            this.btnLastPeriod.Location = new System.Drawing.Point(460, 356);
            this.btnLastPeriod.Name = "btnLastPeriod";
            this.btnLastPeriod.Size = new System.Drawing.Size(181, 23);
            this.btnLastPeriod.TabIndex = 12;
            this.btnLastPeriod.Text = "Show Previous Period";
            this.btnLastPeriod.UseVisualStyleBackColor = true;
            this.btnLastPeriod.Click += new System.EventHandler(this.btnLastPeriod_Click);
            // 
            // lblPresentHours
            // 
            this.lblPresentHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPresentHours.AutoSize = true;
            this.lblPresentHours.Location = new System.Drawing.Point(460, 66);
            this.lblPresentHours.Margin = new System.Windows.Forms.Padding(3);
            this.lblPresentHours.Name = "lblPresentHours";
            this.lblPresentHours.Size = new System.Drawing.Size(77, 13);
            this.lblPresentHours.TabIndex = 3;
            this.lblPresentHours.Text = "(present hours)";
            // 
            // lblPeriodDates
            // 
            this.lblPeriodDates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPeriodDates.AutoSize = true;
            this.lblPeriodDates.Location = new System.Drawing.Point(460, 47);
            this.lblPeriodDates.Margin = new System.Windows.Forms.Padding(3);
            this.lblPeriodDates.Name = "lblPeriodDates";
            this.lblPeriodDates.Size = new System.Drawing.Size(42, 13);
            this.lblPeriodDates.TabIndex = 2;
            this.lblPeriodDates.Text = "(period)";
            // 
            // lblOvertimeHours
            // 
            this.lblOvertimeHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOvertimeHours.AutoSize = true;
            this.lblOvertimeHours.Location = new System.Drawing.Point(460, 85);
            this.lblOvertimeHours.Margin = new System.Windows.Forms.Padding(3);
            this.lblOvertimeHours.Name = "lblOvertimeHours";
            this.lblOvertimeHours.Size = new System.Drawing.Size(82, 13);
            this.lblOvertimeHours.TabIndex = 4;
            this.lblOvertimeHours.Text = "(overtime hours)";
            // 
            // lvwMessages
            // 
            this.lvwMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMsgDate,
            this.colMsgSubject,
            this.colMsgSender});
            this.lvwMessages.FullRowSelect = true;
            this.lvwMessages.Location = new System.Drawing.Point(12, 404);
            this.lvwMessages.MultiSelect = false;
            this.lvwMessages.Name = "lvwMessages";
            this.lvwMessages.Size = new System.Drawing.Size(439, 206);
            this.lvwMessages.TabIndex = 15;
            this.lvwMessages.UseCompatibleStateImageBehavior = false;
            this.lvwMessages.View = System.Windows.Forms.View.Details;
            this.lvwMessages.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwMessages_MouseDoubleClick);
            // 
            // colMsgDate
            // 
            this.colMsgDate.Text = "Date";
            this.colMsgDate.Width = 80;
            // 
            // colMsgSubject
            // 
            this.colMsgSubject.Text = "Subject";
            this.colMsgSubject.Width = 231;
            // 
            // colMsgSender
            // 
            this.colMsgSender.Text = "Sender";
            this.colMsgSender.Width = 100;
            // 
            // lblMessages
            // 
            this.lblMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMessages.AutoSize = true;
            this.lblMessages.Location = new System.Drawing.Point(12, 385);
            this.lblMessages.Margin = new System.Windows.Forms.Padding(3);
            this.lblMessages.Name = "lblMessages";
            this.lblMessages.Size = new System.Drawing.Size(58, 13);
            this.lblMessages.TabIndex = 13;
            this.lblMessages.Text = "Messages:";
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendMessage.Enabled = false;
            this.btnSendMessage.Location = new System.Drawing.Point(460, 587);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(181, 23);
            this.btnSendMessage.TabIndex = 17;
            this.btnSendMessage.Text = "New Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // btnReadMessage
            // 
            this.btnReadMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadMessage.Enabled = false;
            this.btnReadMessage.Location = new System.Drawing.Point(460, 558);
            this.btnReadMessage.Name = "btnReadMessage";
            this.btnReadMessage.Size = new System.Drawing.Size(181, 23);
            this.btnReadMessage.TabIndex = 16;
            this.btnReadMessage.Text = "Read Selected Message";
            this.btnReadMessage.UseVisualStyleBackColor = true;
            this.btnReadMessage.Click += new System.EventHandler(this.btnReadMessage_Click);
            // 
            // lblProgess
            // 
            this.lblProgess.AutoSize = true;
            this.lblProgess.Location = new System.Drawing.Point(76, 385);
            this.lblProgess.Name = "lblProgess";
            this.lblProgess.Size = new System.Drawing.Size(53, 13);
            this.lblProgess.TabIndex = 14;
            this.lblProgess.Text = "(progress)";
            // 
            // btnAddAbsent
            // 
            this.btnAddAbsent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAbsent.Enabled = false;
            this.btnAddAbsent.Location = new System.Drawing.Point(460, 197);
            this.btnAddAbsent.Name = "btnAddAbsent";
            this.btnAddAbsent.Size = new System.Drawing.Size(181, 23);
            this.btnAddAbsent.TabIndex = 8;
            this.btnAddAbsent.Text = "Add Absent Date and Time";
            this.btnAddAbsent.UseVisualStyleBackColor = true;
            this.btnAddAbsent.Click += new System.EventHandler(this.btnAddAbsent_Click);
            // 
            // lblAbsentHours
            // 
            this.lblAbsentHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAbsentHours.AutoSize = true;
            this.lblAbsentHours.Location = new System.Drawing.Point(460, 104);
            this.lblAbsentHours.Margin = new System.Windows.Forms.Padding(3);
            this.lblAbsentHours.Name = "lblAbsentHours";
            this.lblAbsentHours.Size = new System.Drawing.Size(74, 13);
            this.lblAbsentHours.TabIndex = 5;
            this.lblAbsentHours.Text = "(absent hours)";
            // 
            // EmployeeTimesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 622);
            this.Controls.Add(this.lblAbsentHours);
            this.Controls.Add(this.btnAddAbsent);
            this.Controls.Add(this.lblProgess);
            this.Controls.Add(this.btnReadMessage);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.lblMessages);
            this.Controls.Add(this.lvwMessages);
            this.Controls.Add(this.lblOvertimeHours);
            this.Controls.Add(this.lblPeriodDates);
            this.Controls.Add(this.lblPresentHours);
            this.Controls.Add(this.btnLastPeriod);
            this.Controls.Add(this.btnCurrentPeriod);
            this.Controls.Add(this.txtSpecificDateTime);
            this.Controls.Add(this.lblDateTime);
            this.Controls.Add(this.btnDeleteSpecific);
            this.Controls.Add(this.btnAddSpecific);
            this.Controls.Add(this.btnClockNow);
            this.Controls.Add(this.lvwPairs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeTimesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Employee Times";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EmployeeTimesForm_FormClosed);
            this.Shown += new System.EventHandler(this.EmployeeTimesForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwPairs;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colStart;
        private System.Windows.Forms.ColumnHeader colEnd;
        private System.Windows.Forms.ColumnHeader colDayOfWeek;
        private System.Windows.Forms.Button btnClockNow;
        private System.Windows.Forms.Button btnAddSpecific;
        private System.Windows.Forms.Button btnDeleteSpecific;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.TextBox txtSpecificDateTime;
        private System.Windows.Forms.Button btnCurrentPeriod;
        private System.Windows.Forms.Button btnLastPeriod;
        private System.Windows.Forms.ColumnHeader colLength;
        private System.Windows.Forms.Label lblPresentHours;
        private System.Windows.Forms.Label lblPeriodDates;
        private System.Windows.Forms.Label lblOvertimeHours;
        private System.Windows.Forms.ListView lvwMessages;
        private System.Windows.Forms.Label lblMessages;
        private System.Windows.Forms.ColumnHeader colMsgDate;
        private System.Windows.Forms.ColumnHeader colMsgSubject;
        private System.Windows.Forms.ColumnHeader colMsgSender;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Button btnReadMessage;
        private System.Windows.Forms.Label lblProgess;
        private System.Windows.Forms.Button btnAddAbsent;
        private System.Windows.Forms.Label lblAbsentHours;
    }
}