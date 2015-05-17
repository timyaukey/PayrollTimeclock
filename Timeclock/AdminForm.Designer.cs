namespace PayrollTimeclock
{
    partial class AdminForm
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
            this.btnTimecardsCurrent = new System.Windows.Forms.Button();
            this.btnTimecardsLast = new System.Windows.Forms.Button();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.cboEndDate = new System.Windows.Forms.ComboBox();
            this.btnTimecardsSelected = new System.Windows.Forms.Button();
            this.dlgSelectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSelectDataFolder = new System.Windows.Forms.Button();
            this.lblDataFolder = new System.Windows.Forms.Label();
            this.txtDataFolder = new System.Windows.Forms.TextBox();
            this.chkShowHiddenEmployees = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnTimecardsCurrent
            // 
            this.btnTimecardsCurrent.Location = new System.Drawing.Point(12, 12);
            this.btnTimecardsCurrent.Name = "btnTimecardsCurrent";
            this.btnTimecardsCurrent.Size = new System.Drawing.Size(214, 26);
            this.btnTimecardsCurrent.TabIndex = 0;
            this.btnTimecardsCurrent.Text = "Timecard Report For Current Period";
            this.btnTimecardsCurrent.UseVisualStyleBackColor = true;
            this.btnTimecardsCurrent.Click += new System.EventHandler(this.btnTimecardsCurrent_Click);
            // 
            // btnTimecardsLast
            // 
            this.btnTimecardsLast.Location = new System.Drawing.Point(232, 12);
            this.btnTimecardsLast.Name = "btnTimecardsLast";
            this.btnTimecardsLast.Size = new System.Drawing.Size(228, 26);
            this.btnTimecardsLast.TabIndex = 1;
            this.btnTimecardsLast.Text = "Timecard Report For Last Period";
            this.btnTimecardsLast.UseVisualStyleBackColor = true;
            this.btnTimecardsLast.Click += new System.EventHandler(this.btnTimecardsLast_Click);
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(12, 51);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(88, 13);
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "Period End Date:";
            // 
            // cboEndDate
            // 
            this.cboEndDate.FormattingEnabled = true;
            this.cboEndDate.Location = new System.Drawing.Point(106, 48);
            this.cboEndDate.Name = "cboEndDate";
            this.cboEndDate.Size = new System.Drawing.Size(120, 21);
            this.cboEndDate.TabIndex = 3;
            // 
            // btnTimecardsSelected
            // 
            this.btnTimecardsSelected.Location = new System.Drawing.Point(232, 44);
            this.btnTimecardsSelected.Name = "btnTimecardsSelected";
            this.btnTimecardsSelected.Size = new System.Drawing.Size(228, 26);
            this.btnTimecardsSelected.TabIndex = 4;
            this.btnTimecardsSelected.Text = "Timecard Report For Selected Period";
            this.btnTimecardsSelected.UseVisualStyleBackColor = true;
            this.btnTimecardsSelected.Click += new System.EventHandler(this.btnTimecardsSelected_Click);
            // 
            // btnSelectDataFolder
            // 
            this.btnSelectDataFolder.Location = new System.Drawing.Point(393, 106);
            this.btnSelectDataFolder.Name = "btnSelectDataFolder";
            this.btnSelectDataFolder.Size = new System.Drawing.Size(67, 23);
            this.btnSelectDataFolder.TabIndex = 7;
            this.btnSelectDataFolder.Text = "Browse...";
            this.btnSelectDataFolder.UseVisualStyleBackColor = true;
            this.btnSelectDataFolder.Click += new System.EventHandler(this.btnSelectDataFolder_Click);
            // 
            // lblDataFolder
            // 
            this.lblDataFolder.AutoSize = true;
            this.lblDataFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataFolder.Location = new System.Drawing.Point(12, 92);
            this.lblDataFolder.Name = "lblDataFolder";
            this.lblDataFolder.Size = new System.Drawing.Size(65, 13);
            this.lblDataFolder.TabIndex = 5;
            this.lblDataFolder.Text = "Data Folder:";
            // 
            // txtDataFolder
            // 
            this.txtDataFolder.Location = new System.Drawing.Point(12, 108);
            this.txtDataFolder.Name = "txtDataFolder";
            this.txtDataFolder.Size = new System.Drawing.Size(375, 20);
            this.txtDataFolder.TabIndex = 6;
            // 
            // chkShowHiddenEmployees
            // 
            this.chkShowHiddenEmployees.AutoSize = true;
            this.chkShowHiddenEmployees.Location = new System.Drawing.Point(12, 144);
            this.chkShowHiddenEmployees.Name = "chkShowHiddenEmployees";
            this.chkShowHiddenEmployees.Size = new System.Drawing.Size(188, 17);
            this.chkShowHiddenEmployees.TabIndex = 8;
            this.chkShowHiddenEmployees.Text = "Show Hidden Employees Until Exit";
            this.chkShowHiddenEmployees.UseVisualStyleBackColor = true;
            this.chkShowHiddenEmployees.CheckedChanged += new System.EventHandler(this.chkShowHiddenEmployees_CheckedChanged);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 179);
            this.Controls.Add(this.chkShowHiddenEmployees);
            this.Controls.Add(this.txtDataFolder);
            this.Controls.Add(this.lblDataFolder);
            this.Controls.Add(this.btnSelectDataFolder);
            this.Controls.Add(this.btnTimecardsSelected);
            this.Controls.Add(this.cboEndDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.btnTimecardsLast);
            this.Controls.Add(this.btnTimecardsCurrent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Administrative Functions";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTimecardsCurrent;
        private System.Windows.Forms.Button btnTimecardsLast;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.ComboBox cboEndDate;
        private System.Windows.Forms.Button btnTimecardsSelected;
        private System.Windows.Forms.FolderBrowserDialog dlgSelectFolder;
        private System.Windows.Forms.Button btnSelectDataFolder;
        private System.Windows.Forms.Label lblDataFolder;
        private System.Windows.Forms.TextBox txtDataFolder;
        private System.Windows.Forms.CheckBox chkShowHiddenEmployees;
    }
}