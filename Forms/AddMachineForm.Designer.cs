namespace goodwin_winForm.Forms
{
    partial class AddMachineForm
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
            lblName = new Label();
            txtName = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblSerialNumber = new Label();
            txtSerialNumber = new TextBox();
            lblModel = new Label();
            txtModel = new TextBox();
            lblManufacturer = new Label();
            txtManufacturer = new TextBox();
            lblInstallationDate = new Label();
            dtpInstallationDate = new DateTimePicker();
            lblStatus = new Label();
            cboStatus = new ComboBox();
            lblLocation = new Label();
            txtLocation = new TextBox();
            lblDepartment = new Label();
            cboDepartment = new ComboBox();
            lblNotes = new Label();
            txtNotes = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            lblTitle = new Label();
            lblImage = new Label();
            picMachineImage = new PictureBox();
            btnBrowseImage = new Button();
            btnClearImage = new Button();
            mainPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)picMachineImage).BeginInit();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(19, 93);
            lblName.Margin = new Padding(5, 0, 5, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(68, 28);
            lblName.TabIndex = 0;
            lblName.Text = "Name:";
            // 
            // txtName
            // 
            txtName.Location = new Point(189, 88);
            txtName.Margin = new Padding(5, 6, 5, 6);
            txtName.Name = "txtName";
            txtName.Size = new Size(391, 34);
            txtName.TabIndex = 1;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(19, 149);
            lblDescription.Margin = new Padding(5, 0, 5, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(116, 28);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(189, 144);
            txtDescription.Margin = new Padding(5, 6, 5, 6);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(391, 109);
            txtDescription.TabIndex = 3;
            // 
            // lblSerialNumber
            // 
            lblSerialNumber.AutoSize = true;
            lblSerialNumber.Location = new Point(19, 280);
            lblSerialNumber.Margin = new Padding(5, 0, 5, 0);
            lblSerialNumber.Name = "lblSerialNumber";
            lblSerialNumber.Size = new Size(141, 28);
            lblSerialNumber.TabIndex = 4;
            lblSerialNumber.Text = "Serial Number:";
            // 
            // txtSerialNumber
            // 
            txtSerialNumber.Location = new Point(189, 274);
            txtSerialNumber.Margin = new Padding(5, 6, 5, 6);
            txtSerialNumber.Name = "txtSerialNumber";
            txtSerialNumber.Size = new Size(391, 34);
            txtSerialNumber.TabIndex = 5;
            // 
            // lblModel
            // 
            lblModel.AutoSize = true;
            lblModel.Location = new Point(19, 336);
            lblModel.Margin = new Padding(5, 0, 5, 0);
            lblModel.Name = "lblModel";
            lblModel.Size = new Size(73, 28);
            lblModel.TabIndex = 6;
            lblModel.Text = "Model:";
            // 
            // txtModel
            // 
            txtModel.Location = new Point(189, 330);
            txtModel.Margin = new Padding(5, 6, 5, 6);
            txtModel.Name = "txtModel";
            txtModel.Size = new Size(391, 34);
            txtModel.TabIndex = 7;
            // 
            // lblManufacturer
            // 
            lblManufacturer.AutoSize = true;
            lblManufacturer.Location = new Point(19, 392);
            lblManufacturer.Margin = new Padding(5, 0, 5, 0);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.Size = new Size(133, 28);
            lblManufacturer.TabIndex = 8;
            lblManufacturer.Text = "Manufacturer:";
            // 
            // txtManufacturer
            // 
            txtManufacturer.Location = new Point(189, 386);
            txtManufacturer.Margin = new Padding(5, 6, 5, 6);
            txtManufacturer.Name = "txtManufacturer";
            txtManufacturer.Size = new Size(391, 34);
            txtManufacturer.TabIndex = 9;
            // 
            // lblInstallationDate
            // 
            lblInstallationDate.AutoSize = true;
            lblInstallationDate.Location = new Point(19, 448);
            lblInstallationDate.Margin = new Padding(5, 0, 5, 0);
            lblInstallationDate.Name = "lblInstallationDate";
            lblInstallationDate.Size = new Size(158, 28);
            lblInstallationDate.TabIndex = 10;
            lblInstallationDate.Text = "Installation Date:";
            // 
            // dtpInstallationDate
            // 
            dtpInstallationDate.Location = new Point(189, 442);
            dtpInstallationDate.Margin = new Padding(5, 6, 5, 6);
            dtpInstallationDate.Name = "dtpInstallationDate";
            dtpInstallationDate.Size = new Size(391, 34);
            dtpInstallationDate.TabIndex = 11;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(19, 504);
            lblStatus.Margin = new Padding(5, 0, 5, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(69, 28);
            lblStatus.TabIndex = 12;
            lblStatus.Text = "Status:";
            // 
            // cboStatus
            // 
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Location = new Point(189, 498);
            cboStatus.Margin = new Padding(5, 6, 5, 6);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(391, 36);
            cboStatus.TabIndex = 13;
            // 
            // lblLocation
            // 
            lblLocation.AutoSize = true;
            lblLocation.Location = new Point(19, 560);
            lblLocation.Margin = new Padding(5, 0, 5, 0);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(91, 28);
            lblLocation.TabIndex = 14;
            lblLocation.Text = "Location:";
            // 
            // txtLocation
            // 
            txtLocation.Location = new Point(189, 554);
            txtLocation.Margin = new Padding(5, 6, 5, 6);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(391, 34);
            txtLocation.TabIndex = 15;
            // 
            // lblDepartment
            // 
            lblDepartment.AutoSize = true;
            lblDepartment.Location = new Point(19, 616);
            lblDepartment.Margin = new Padding(5, 0, 5, 0);
            lblDepartment.Name = "lblDepartment";
            lblDepartment.Size = new Size(121, 28);
            lblDepartment.TabIndex = 16;
            lblDepartment.Text = "Department:";
            // 
            // cboDepartment
            // 
            cboDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDepartment.Location = new Point(189, 610);
            cboDepartment.Margin = new Padding(5, 6, 5, 6);
            cboDepartment.Name = "cboDepartment";
            cboDepartment.Size = new Size(391, 36);
            cboDepartment.TabIndex = 17;
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.Location = new Point(19, 672);
            lblNotes.Margin = new Padding(5, 0, 5, 0);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(68, 28);
            lblNotes.TabIndex = 18;
            lblNotes.Text = "Notes:";
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(189, 666);
            txtNotes.Margin = new Padding(5, 6, 5, 6);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(391, 109);
            txtNotes.TabIndex = 19;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(189, 978);
            btnSave.Margin = new Padding(5, 6, 5, 6);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(157, 56);
            btnSave.TabIndex = 24;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(388, 978);
            btnCancel.Margin = new Padding(5, 6, 5, 6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(157, 56);
            btnCancel.TabIndex = 25;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(19, 17);
            lblTitle.Margin = new Padding(5, 0, 5, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(165, 32);
            lblTitle.TabIndex = 26;
            lblTitle.Text = "Add Machine";
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Location = new Point(19, 800);
            lblImage.Margin = new Padding(5, 0, 5, 0);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(70, 28);
            lblImage.TabIndex = 20;
            lblImage.Text = "Image:";
            // 
            // picMachineImage
            // 
            picMachineImage.BorderStyle = BorderStyle.FixedSingle;
            picMachineImage.Location = new Point(189, 795);
            picMachineImage.Margin = new Padding(5, 6, 5, 6);
            picMachineImage.Name = "picMachineImage";
            picMachineImage.Size = new Size(200, 125);
            picMachineImage.SizeMode = PictureBoxSizeMode.Zoom;
            picMachineImage.TabIndex = 21;
            picMachineImage.TabStop = false;
            // 
            // btnBrowseImage
            // 
            btnBrowseImage.Location = new Point(399, 800);
            btnBrowseImage.Margin = new Padding(5, 6, 5, 6);
            btnBrowseImage.Name = "btnBrowseImage";
            btnBrowseImage.Size = new Size(175, 45);
            btnBrowseImage.TabIndex = 22;
            btnBrowseImage.Text = "Browse Image";
            btnBrowseImage.UseVisualStyleBackColor = true;
            btnBrowseImage.Click += btnBrowseImage_Click;
            // 
            // btnClearImage
            // 
            btnClearImage.Location = new Point(399, 875);
            btnClearImage.Margin = new Padding(5, 6, 5, 6);
            btnClearImage.Name = "btnClearImage";
            btnClearImage.Size = new Size(175, 45);
            btnClearImage.TabIndex = 23;
            btnClearImage.Text = "Clear Image";
            btnClearImage.UseVisualStyleBackColor = true;
            btnClearImage.Click += btnClearImage_Click;
            // 
            // mainPanel
            // 
            mainPanel.AutoScroll = true;
            mainPanel.AutoScrollMinSize = new Size(600, 900);
            mainPanel.Controls.Add(lblTitle);
            mainPanel.Controls.Add(btnCancel);
            mainPanel.Controls.Add(btnSave);
            mainPanel.Controls.Add(btnClearImage);
            mainPanel.Controls.Add(btnBrowseImage);
            mainPanel.Controls.Add(picMachineImage);
            mainPanel.Controls.Add(lblImage);
            mainPanel.Controls.Add(txtNotes);
            mainPanel.Controls.Add(lblNotes);
            mainPanel.Controls.Add(cboDepartment);
            mainPanel.Controls.Add(lblDepartment);
            mainPanel.Controls.Add(txtLocation);
            mainPanel.Controls.Add(lblLocation);
            mainPanel.Controls.Add(cboStatus);
            mainPanel.Controls.Add(lblStatus);
            mainPanel.Controls.Add(dtpInstallationDate);
            mainPanel.Controls.Add(lblInstallationDate);
            mainPanel.Controls.Add(txtManufacturer);
            mainPanel.Controls.Add(lblManufacturer);
            mainPanel.Controls.Add(txtModel);
            mainPanel.Controls.Add(lblModel);
            mainPanel.Controls.Add(txtSerialNumber);
            mainPanel.Controls.Add(lblSerialNumber);
            mainPanel.Controls.Add(txtDescription);
            mainPanel.Controls.Add(lblDescription);
            mainPanel.Controls.Add(txtName);
            mainPanel.Controls.Add(lblName);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(31, 37);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(10);
            mainPanel.Size = new Size(620, 779);
            mainPanel.TabIndex = 0;
            // 
            // AddMachineForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 853);
            Controls.Add(mainPanel);
            Margin = new Padding(5, 6, 5, 6);
            Name = "AddMachineForm";
            Padding = new Padding(31, 37, 31, 37);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Machine";
            ((System.ComponentModel.ISupportInitialize)picMachineImage).EndInit();
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.TextBox txtManufacturer;
        private System.Windows.Forms.Label lblInstallationDate;
        private System.Windows.Forms.DateTimePicker dtpInstallationDate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.PictureBox picMachineImage;
        private System.Windows.Forms.Button btnBrowseImage;
        private System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.Panel mainPanel;
    }
} 