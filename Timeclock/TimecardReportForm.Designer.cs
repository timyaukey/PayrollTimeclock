namespace PayrollTimeclock
{
    partial class TimecardReportForm
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
            this.lblPeriod = new System.Windows.Forms.Label();
            this.lvwTimecards = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colExtID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotalHours = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRegHours = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOvrHours = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colVacHours = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAbsentHours = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOkay = new System.Windows.Forms.Button();
            this.lblTotals = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.colExtraHours = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(12, 9);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(42, 13);
            this.lblPeriod.TabIndex = 0;
            this.lblPeriod.Text = "(period)";
            // 
            // lvwTimecards
            // 
            this.lvwTimecards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwTimecards.CheckBoxes = true;
            this.lvwTimecards.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colExtID,
            this.colTotalHours,
            this.colRegHours,
            this.colOvrHours,
            this.colVacHours,
            this.colAbsentHours,
            this.colExtraHours});
            this.lvwTimecards.FullRowSelect = true;
            this.lvwTimecards.GridLines = true;
            this.lvwTimecards.Location = new System.Drawing.Point(12, 25);
            this.lvwTimecards.Name = "lvwTimecards";
            this.lvwTimecards.Size = new System.Drawing.Size(883, 323);
            this.lvwTimecards.TabIndex = 1;
            this.lvwTimecards.UseCompatibleStateImageBehavior = false;
            this.lvwTimecards.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Employee Name";
            this.colName.Width = 209;
            // 
            // colExtID
            // 
            this.colExtID.Text = "External ID";
            this.colExtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colExtID.Width = 90;
            // 
            // colTotalHours
            // 
            this.colTotalHours.Text = "Present Hours";
            this.colTotalHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colTotalHours.Width = 90;
            // 
            // colRegHours
            // 
            this.colRegHours.Text = "Reg Hours";
            this.colRegHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colRegHours.Width = 90;
            // 
            // colOvrHours
            // 
            this.colOvrHours.Text = "Overtime Hours";
            this.colOvrHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colOvrHours.Width = 90;
            // 
            // colVacHours
            // 
            this.colVacHours.Text = "Vacation Hours";
            this.colVacHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colVacHours.Width = 90;
            // 
            // colAbsentHours
            // 
            this.colAbsentHours.Text = "Absent Hours";
            this.colAbsentHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colAbsentHours.Width = 90;
            // 
            // btnOkay
            // 
            this.btnOkay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOkay.Location = new System.Drawing.Point(804, 354);
            this.btnOkay.Name = "btnOkay";
            this.btnOkay.Size = new System.Drawing.Size(91, 23);
            this.btnOkay.TabIndex = 4;
            this.btnOkay.Text = "OK";
            this.btnOkay.UseVisualStyleBackColor = true;
            this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
            // 
            // lblTotals
            // 
            this.lblTotals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotals.AutoSize = true;
            this.lblTotals.Location = new System.Drawing.Point(12, 359);
            this.lblTotals.Name = "lblTotals";
            this.lblTotals.Size = new System.Drawing.Size(38, 13);
            this.lblTotals.TabIndex = 2;
            this.lblTotals.Text = "(totals)";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(707, 354);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(91, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // colExtraHours
            // 
            this.colExtraHours.Text = "Extra Hours";
            this.colExtraHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colExtraHours.Width = 90;
            // 
            // TimecardReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 389);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblTotals);
            this.Controls.Add(this.btnOkay);
            this.Controls.Add(this.lvwTimecards);
            this.Controls.Add(this.lblPeriod);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimecardReportForm";
            this.Text = "Timecard Report";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.ListView lvwTimecards;
        private System.Windows.Forms.Button btnOkay;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colExtID;
        private System.Windows.Forms.ColumnHeader colTotalHours;
        private System.Windows.Forms.ColumnHeader colRegHours;
        private System.Windows.Forms.ColumnHeader colOvrHours;
        private System.Windows.Forms.ColumnHeader colVacHours;
        private System.Windows.Forms.Label lblTotals;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ColumnHeader colAbsentHours;
        private System.Windows.Forms.ColumnHeader colExtraHours;
    }
}