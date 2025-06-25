namespace goodwin_winForm.Forms
{
    partial class MachineDetailsForm
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
                // Dispose of the machine image
                if (pictureBoxMachine.Image != null)
                {
                    pictureBoxMachine.Image.Dispose();
                    pictureBoxMachine.Image = null;
                }
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
            tabControl = new TabControl();
            tabInfo = new TabPage();
            pictureBoxMachine = new PictureBox();
            txtNotes = new TextBox();
            txtDescription = new TextBox();
            lblNextMaintenance = new Label();
            lblLastMaintenance = new Label();
            lblInstallationDate = new Label();
            lblDepartment = new Label();
            lblLocation = new Label();
            lblStatus = new Label();
            lblManufacturer = new Label();
            lblModel = new Label();
            lblSerialNumber = new Label();
            lblMachineName = new Label();
            lblMachineNameValue = new Label();
            lblSerialNumberValue = new Label();
            lblModelValue = new Label();
            lblManufacturerValue = new Label();
            lblStatusValue = new Label();
            lblLocationValue = new Label();
            lblDepartmentValue = new Label();
            lblInstallationDateValue = new Label();
            lblLastMaintenanceValue = new Label();
            lblNextMaintenanceValue = new Label();
            lblDescription = new Label();
            lblNotes = new Label();
            tabMaintenance = new TabPage();
            listViewMaintenance = new ListView();
            columnMaintenanceDate = new ColumnHeader();
            columnType = new ColumnHeader();
            columnTitle = new ColumnHeader();
            columnPerformedBy = new ColumnHeader();
            columnStatus = new ColumnHeader();
            columnCost = new ColumnHeader();
            tabAlerts = new TabPage();
            listViewAlerts = new ListView();
            columnAlertDate = new ColumnHeader();
            columnAlertType = new ColumnHeader();
            columnSeverity = new ColumnHeader();
            columnAlertTitle = new ColumnHeader();
            columnAlertStatus = new ColumnHeader();
            btnAddMaintenance = new Button();
            btnAddAlert = new Button();
            btnEditMachine = new Button();
            btnClose = new Button();
            btnRefresh = new Button();
            tabControl.SuspendLayout();
            tabInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMachine).BeginInit();
            tabMaintenance.SuspendLayout();
            tabAlerts.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabInfo);
            tabControl.Controls.Add(tabMaintenance);
            tabControl.Controls.Add(tabAlerts);
            tabControl.Location = new Point(19, 22);
            tabControl.Margin = new Padding(4, 6, 4, 6);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1220, 840);
            tabControl.TabIndex = 0;
            // 
            // tabInfo
            // 
            tabInfo.Controls.Add(pictureBoxMachine);
            tabInfo.Controls.Add(txtNotes);
            tabInfo.Controls.Add(txtDescription);
            tabInfo.Controls.Add(lblNextMaintenance);
            tabInfo.Controls.Add(lblLastMaintenance);
            tabInfo.Controls.Add(lblInstallationDate);
            tabInfo.Controls.Add(lblDepartment);
            tabInfo.Controls.Add(lblLocation);
            tabInfo.Controls.Add(lblStatus);
            tabInfo.Controls.Add(lblManufacturer);
            tabInfo.Controls.Add(lblModel);
            tabInfo.Controls.Add(lblSerialNumber);
            tabInfo.Controls.Add(lblMachineName);
            tabInfo.Controls.Add(lblMachineNameValue);
            tabInfo.Controls.Add(lblSerialNumberValue);
            tabInfo.Controls.Add(lblModelValue);
            tabInfo.Controls.Add(lblManufacturerValue);
            tabInfo.Controls.Add(lblStatusValue);
            tabInfo.Controls.Add(lblLocationValue);
            tabInfo.Controls.Add(lblDepartmentValue);
            tabInfo.Controls.Add(lblInstallationDateValue);
            tabInfo.Controls.Add(lblLastMaintenanceValue);
            tabInfo.Controls.Add(lblNextMaintenanceValue);
            tabInfo.Controls.Add(lblDescription);
            tabInfo.Controls.Add(lblNotes);
            tabInfo.Location = new Point(4, 37);
            tabInfo.Margin = new Padding(4, 6, 4, 6);
            tabInfo.Name = "tabInfo";
            tabInfo.Padding = new Padding(4, 6, 4, 6);
            tabInfo.Size = new Size(1212, 799);
            tabInfo.TabIndex = 0;
            tabInfo.Text = "Machine Information";
            tabInfo.UseVisualStyleBackColor = true;
            // 
            // pictureBoxMachine
            // 
            pictureBoxMachine.BackColor = Color.LightGray;
            pictureBoxMachine.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxMachine.Location = new Point(568, 18);
            pictureBoxMachine.Margin = new Padding(4);
            pictureBoxMachine.Name = "pictureBoxMachine";
            pictureBoxMachine.Size = new Size(593, 420);
            pictureBoxMachine.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxMachine.TabIndex = 12;
            pictureBoxMachine.TabStop = false;
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(692, 594);
            txtNotes.Margin = new Padding(4, 6, 4, 6);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.ReadOnly = true;
            txtNotes.Size = new Size(469, 193);
            txtNotes.TabIndex = 11;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(15, 594);
            txtDescription.Margin = new Padding(4, 6, 4, 6);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.Size = new Size(469, 193);
            txtDescription.TabIndex = 10;
            // 
            // lblNextMaintenance
            // 
            lblNextMaintenance.AutoSize = true;
            lblNextMaintenance.Location = new Point(15, 516);
            lblNextMaintenance.Margin = new Padding(4, 0, 4, 0);
            lblNextMaintenance.Name = "lblNextMaintenance";
            lblNextMaintenance.Size = new Size(174, 28);
            lblNextMaintenance.TabIndex = 9;
            lblNextMaintenance.Text = "Next Maintenance:";
            // 
            // lblLastMaintenance
            // 
            lblLastMaintenance.AutoSize = true;
            lblLastMaintenance.Location = new Point(15, 466);
            lblLastMaintenance.Margin = new Padding(4, 0, 4, 0);
            lblLastMaintenance.Name = "lblLastMaintenance";
            lblLastMaintenance.Size = new Size(167, 28);
            lblLastMaintenance.TabIndex = 8;
            lblLastMaintenance.Text = "Last Maintenance:";
            // 
            // lblInstallationDate
            // 
            lblInstallationDate.AutoSize = true;
            lblInstallationDate.Location = new Point(15, 410);
            lblInstallationDate.Margin = new Padding(4, 0, 4, 0);
            lblInstallationDate.Name = "lblInstallationDate";
            lblInstallationDate.Size = new Size(158, 28);
            lblInstallationDate.TabIndex = 7;
            lblInstallationDate.Text = "Installation Date:";
            // 
            // lblDepartment
            // 
            lblDepartment.AutoSize = true;
            lblDepartment.Location = new Point(15, 354);
            lblDepartment.Margin = new Padding(4, 0, 4, 0);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.Size = new Size(121, 28);
            lblDepartment.TabIndex = 6;
            lblDepartment.Text = "Department:";
            // 
            // lblLocation
            // 
            lblLocation.AutoSize = true;
            lblLocation.Location = new Point(15, 298);
            lblLocation.Margin = new Padding(4, 0, 4, 0);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(91, 28);
            lblLocation.TabIndex = 5;
            lblLocation.Text = "Location:";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(15, 242);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(69, 28);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Status:";
            // 
            // lblManufacturer
            // 
            lblManufacturer.AutoSize = true;
            lblManufacturer.Location = new Point(15, 186);
            lblManufacturer.Margin = new Padding(4, 0, 4, 0);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.Size = new Size(133, 28);
            lblManufacturer.TabIndex = 3;
            lblManufacturer.Text = "Manufacturer:";
            // 
            // lblModel
            // 
            lblModel.AutoSize = true;
            lblModel.Location = new Point(15, 130);
            lblModel.Margin = new Padding(4, 0, 4, 0);
            lblModel.Name = "lblModel";
            lblModel.Size = new Size(73, 28);
            lblModel.TabIndex = 2;
            lblModel.Text = "Model:";
            // 
            // lblSerialNumber
            // 
            lblSerialNumber.AutoSize = true;
            lblSerialNumber.Location = new Point(15, 74);
            lblSerialNumber.Margin = new Padding(4, 0, 4, 0);
            lblSerialNumber.Name = "lblSerialNumber";
            lblSerialNumber.Size = new Size(141, 28);
            lblSerialNumber.TabIndex = 1;
            lblSerialNumber.Text = "Serial Number:";
            // 
            // lblMachineName
            // 
            lblMachineName.AutoSize = true;
            lblMachineName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblMachineName.Location = new Point(15, 18);
            lblMachineName.Margin = new Padding(4, 0, 4, 0);
            lblMachineName.Name = "lblMachineName";
            lblMachineName.Size = new Size(160, 28);
            lblMachineName.TabIndex = 0;
            lblMachineName.Text = "Machine Name:";
            // 
            // lblMachineNameValue
            // 
            lblMachineNameValue.AutoSize = true;
            lblMachineNameValue.Font = new Font("Segoe UI", 12F);
            lblMachineNameValue.Location = new Point(180, 18);
            lblMachineNameValue.Margin = new Padding(4, 0, 4, 0);
            lblMachineNameValue.Name = "lblMachineNameValue";
            lblMachineNameValue.Size = new Size(195, 28);
            lblMachineNameValue.TabIndex = 13;
            lblMachineNameValue.Text = "Machine Name Value";
            // 
            // lblSerialNumberValue
            // 
            lblSerialNumberValue.AutoSize = true;
            lblSerialNumberValue.Location = new Point(180, 74);
            lblSerialNumberValue.Margin = new Padding(4, 0, 4, 0);
            lblSerialNumberValue.Name = "lblSerialNumberValue";
            lblSerialNumberValue.Size = new Size(189, 28);
            lblSerialNumberValue.TabIndex = 14;
            lblSerialNumberValue.Text = "Serial Number Value";
            // 
            // lblModelValue
            // 
            lblModelValue.AutoSize = true;
            lblModelValue.Location = new Point(180, 130);
            lblModelValue.Margin = new Padding(4, 0, 4, 0);
            lblModelValue.Name = "lblModelValue";
            lblModelValue.Size = new Size(121, 28);
            lblModelValue.TabIndex = 15;
            lblModelValue.Text = "Model Value";
            // 
            // lblManufacturerValue
            // 
            lblManufacturerValue.AutoSize = true;
            lblManufacturerValue.Location = new Point(180, 186);
            lblManufacturerValue.Margin = new Padding(4, 0, 4, 0);
            lblManufacturerValue.Name = "lblManufacturerValue";
            lblManufacturerValue.Size = new Size(181, 28);
            lblManufacturerValue.TabIndex = 16;
            lblManufacturerValue.Text = "Manufacturer Value";
            // 
            // lblStatusValue
            // 
            lblStatusValue.AutoSize = true;
            lblStatusValue.Location = new Point(180, 242);
            lblStatusValue.Margin = new Padding(4, 0, 4, 0);
            lblStatusValue.Name = "lblStatusValue";
            lblStatusValue.Size = new Size(117, 28);
            lblStatusValue.TabIndex = 17;
            lblStatusValue.Text = "Status Value";
            // 
            // lblLocationValue
            // 
            lblLocationValue.AutoSize = true;
            lblLocationValue.Location = new Point(180, 298);
            lblLocationValue.Margin = new Padding(4, 0, 4, 0);
            lblLocationValue.Name = "lblLocationValue";
            lblLocationValue.Size = new Size(139, 28);
            lblLocationValue.TabIndex = 18;
            lblLocationValue.Text = "Location Value";
            // 
            // lblDepartmentValue
            // 
            lblDepartmentValue.AutoSize = true;
            lblDepartmentValue.Location = new Point(180, 354);
            lblDepartmentValue.Margin = new Padding(4, 0, 4, 0);
            lblDepartmentValue.Name = "lblDepartmentValue";
            lblDepartmentValue.Size = new Size(169, 28);
            lblDepartmentValue.TabIndex = 19;
            lblDepartmentValue.Text = "Department Value";
            // 
            // lblInstallationDateValue
            // 
            lblInstallationDateValue.AutoSize = true;
            lblInstallationDateValue.Location = new Point(180, 410);
            lblInstallationDateValue.Margin = new Padding(4, 0, 4, 0);
            lblInstallationDateValue.Name = "lblInstallationDateValue";
            lblInstallationDateValue.Size = new Size(206, 28);
            lblInstallationDateValue.TabIndex = 20;
            lblInstallationDateValue.Text = "Installation Date Value";
            // 
            // lblLastMaintenanceValue
            // 
            lblLastMaintenanceValue.AutoSize = true;
            lblLastMaintenanceValue.Location = new Point(180, 466);
            lblLastMaintenanceValue.Margin = new Padding(4, 0, 4, 0);
            lblLastMaintenanceValue.Name = "lblLastMaintenanceValue";
            lblLastMaintenanceValue.Size = new Size(215, 28);
            lblLastMaintenanceValue.TabIndex = 21;
            lblLastMaintenanceValue.Text = "Last Maintenance Value";
            // 
            // lblNextMaintenanceValue
            // 
            lblNextMaintenanceValue.AutoSize = true;
            lblNextMaintenanceValue.Location = new Point(197, 521);
            lblNextMaintenanceValue.Margin = new Padding(4, 0, 4, 0);
            lblNextMaintenanceValue.Name = "lblNextMaintenanceValue";
            lblNextMaintenanceValue.Size = new Size(222, 28);
            lblNextMaintenanceValue.TabIndex = 22;
            lblNextMaintenanceValue.Text = "Next Maintenance Value";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(15, 544);
            lblDescription.Margin = new Padding(4, 0, 4, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(116, 28);
            lblDescription.TabIndex = 23;
            lblDescription.Text = "Description:";
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.Location = new Point(692, 544);
            lblNotes.Margin = new Padding(4, 0, 4, 0);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(68, 28);
            lblNotes.TabIndex = 24;
            lblNotes.Text = "Notes:";
            // 
            // tabMaintenance
            // 
            tabMaintenance.Controls.Add(listViewMaintenance);
            tabMaintenance.Location = new Point(4, 37);
            tabMaintenance.Margin = new Padding(4, 6, 4, 6);
            tabMaintenance.Name = "tabMaintenance";
            tabMaintenance.Padding = new Padding(4, 6, 4, 6);
            tabMaintenance.Size = new Size(1212, 799);
            tabMaintenance.TabIndex = 1;
            tabMaintenance.Text = "Maintenance History";
            tabMaintenance.UseVisualStyleBackColor = true;
            // 
            // listViewMaintenance
            // 
            listViewMaintenance.Columns.AddRange(new ColumnHeader[] { columnMaintenanceDate, columnType, columnTitle, columnPerformedBy, columnStatus, columnCost });
            listViewMaintenance.Dock = DockStyle.Fill;
            listViewMaintenance.FullRowSelect = true;
            listViewMaintenance.GridLines = true;
            listViewMaintenance.Location = new Point(4, 6);
            listViewMaintenance.Margin = new Padding(4, 6, 4, 6);
            listViewMaintenance.Name = "listViewMaintenance";
            listViewMaintenance.Size = new Size(1204, 787);
            listViewMaintenance.TabIndex = 0;
            listViewMaintenance.UseCompatibleStateImageBehavior = false;
            listViewMaintenance.View = View.Details;
            // 
            // columnMaintenanceDate
            // 
            columnMaintenanceDate.Text = "Date";
            columnMaintenanceDate.Width = 200;
            // 
            // columnType
            // 
            columnType.Text = "Type";
            columnType.Width = 200;
            // 
            // columnTitle
            // 
            columnTitle.Text = "Title";
            columnTitle.Width = 200;
            // 
            // columnPerformedBy
            // 
            columnPerformedBy.Text = "Performed By";
            columnPerformedBy.Width = 200;
            // 
            // columnStatus
            // 
            columnStatus.Text = "Status";
            columnStatus.Width = 200;
            // 
            // columnCost
            // 
            columnCost.Text = "Cost";
            columnCost.Width = 200;
            // 
            // tabAlerts
            // 
            tabAlerts.Controls.Add(listViewAlerts);
            tabAlerts.Location = new Point(4, 37);
            tabAlerts.Margin = new Padding(4, 6, 4, 6);
            tabAlerts.Name = "tabAlerts";
            tabAlerts.Padding = new Padding(4, 6, 4, 6);
            tabAlerts.Size = new Size(1212, 799);
            tabAlerts.TabIndex = 2;
            tabAlerts.Text = "Alerts";
            tabAlerts.UseVisualStyleBackColor = true;
            // 
            // listViewAlerts
            // 
            listViewAlerts.Columns.AddRange(new ColumnHeader[] { columnAlertDate, columnAlertType, columnSeverity, columnAlertTitle, columnAlertStatus });
            listViewAlerts.Dock = DockStyle.Fill;
            listViewAlerts.FullRowSelect = true;
            listViewAlerts.GridLines = true;
            listViewAlerts.Location = new Point(4, 6);
            listViewAlerts.Margin = new Padding(4, 6, 4, 6);
            listViewAlerts.Name = "listViewAlerts";
            listViewAlerts.Size = new Size(1204, 787);
            listViewAlerts.TabIndex = 0;
            listViewAlerts.UseCompatibleStateImageBehavior = false;
            listViewAlerts.View = View.Details;
            // 
            // columnAlertDate
            // 
            columnAlertDate.Text = "Date";
            columnAlertDate.Width = 250;
            // 
            // columnAlertType
            // 
            columnAlertType.Text = "Type";
            columnAlertType.Width = 250;
            // 
            // columnSeverity
            // 
            columnSeverity.Text = "Severity";
            columnSeverity.Width = 250;
            // 
            // columnAlertTitle
            // 
            columnAlertTitle.Text = "Title";
            columnAlertTitle.Width = 250;
            // 
            // columnAlertStatus
            // 
            columnAlertStatus.Text = "Status";
            columnAlertStatus.Width = 200;
            // 
            // btnAddMaintenance
            // 
            btnAddMaintenance.Location = new Point(19, 896);
            btnAddMaintenance.Margin = new Padding(4, 6, 4, 6);
            btnAddMaintenance.Name = "btnAddMaintenance";
            btnAddMaintenance.Size = new Size(188, 56);
            btnAddMaintenance.TabIndex = 1;
            btnAddMaintenance.Text = "Add Maintenance";
            btnAddMaintenance.UseVisualStyleBackColor = true;
            btnAddMaintenance.Click += btnAddMaintenance_Click;
            // 
            // btnAddAlert
            // 
            btnAddAlert.Location = new Point(217, 896);
            btnAddAlert.Margin = new Padding(4, 6, 4, 6);
            btnAddAlert.Name = "btnAddAlert";
            btnAddAlert.Size = new Size(157, 56);
            btnAddAlert.TabIndex = 2;
            btnAddAlert.Text = "Add Alert";
            btnAddAlert.UseVisualStyleBackColor = true;
            btnAddAlert.Click += btnAddAlert_Click;
            // 
            // btnEditMachine
            // 
            btnEditMachine.Location = new Point(384, 896);
            btnEditMachine.Margin = new Padding(4, 6, 4, 6);
            btnEditMachine.Name = "btnEditMachine";
            btnEditMachine.Size = new Size(157, 56);
            btnEditMachine.TabIndex = 3;
            btnEditMachine.Text = "Edit Machine";
            btnEditMachine.UseVisualStyleBackColor = true;
            btnEditMachine.Click += btnEditMachine_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(1081, 896);
            btnClose.Margin = new Padding(4, 6, 4, 6);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(157, 56);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(551, 896);
            btnRefresh.Margin = new Padding(4, 6, 4, 6);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(157, 56);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // MachineDetailsForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1257, 970);
            Controls.Add(btnRefresh);
            Controls.Add(btnClose);
            Controls.Add(btnEditMachine);
            Controls.Add(btnAddAlert);
            Controls.Add(btnAddMaintenance);
            Controls.Add(tabControl);
            Margin = new Padding(4, 6, 4, 6);
            MinimumSize = new Size(932, 706);
            Name = "MachineDetailsForm";
            Padding = new Padding(28);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Machine Details";
            tabControl.ResumeLayout(false);
            tabInfo.ResumeLayout(false);
            tabInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMachine).EndInit();
            tabMaintenance.ResumeLayout(false);
            tabAlerts.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.TabPage tabMaintenance;
        private System.Windows.Forms.TabPage tabAlerts;
        private System.Windows.Forms.Label lblMachineName;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Label lblInstallationDate;
        private System.Windows.Forms.Label lblLastMaintenance;
        private System.Windows.Forms.Label lblNextMaintenance;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ListView listViewMaintenance;
        private System.Windows.Forms.ColumnHeader columnMaintenanceDate;
        private System.Windows.Forms.ColumnHeader columnType;
        private System.Windows.Forms.ColumnHeader columnTitle;
        private System.Windows.Forms.ColumnHeader columnPerformedBy;
        private System.Windows.Forms.ColumnHeader columnStatus;
        private System.Windows.Forms.ColumnHeader columnCost;
        private System.Windows.Forms.ListView listViewAlerts;
        private System.Windows.Forms.ColumnHeader columnAlertDate;
        private System.Windows.Forms.ColumnHeader columnAlertType;
        private System.Windows.Forms.ColumnHeader columnSeverity;
        private System.Windows.Forms.ColumnHeader columnAlertTitle;
        private System.Windows.Forms.ColumnHeader columnAlertStatus;
        private System.Windows.Forms.Button btnAddMaintenance;
        private System.Windows.Forms.Button btnAddAlert;
        private System.Windows.Forms.Button btnEditMachine;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private TextBox txtNotes;
        private PictureBox pictureBoxMachine;
        private System.Windows.Forms.Label lblMachineNameValue;
        private System.Windows.Forms.Label lblSerialNumberValue;
        private System.Windows.Forms.Label lblModelValue;
        private System.Windows.Forms.Label lblManufacturerValue;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.Label lblLocationValue;
        private System.Windows.Forms.Label lblDepartmentValue;
        private System.Windows.Forms.Label lblInstallationDateValue;
        private System.Windows.Forms.Label lblLastMaintenanceValue;
        private System.Windows.Forms.Label lblNextMaintenanceValue;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblNotes;
    }
} 