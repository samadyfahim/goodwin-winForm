namespace goodwin_winForm.Forms
{
    partial class AddMaintenanceForm
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
            lblTitle = new Label();
            lblType = new Label();
            cboType = new ComboBox();
            lblMaintenanceDate = new Label();
            dtpMaintenanceDate = new DateTimePicker();
            lblTitleField = new Label();
            txtTitle = new TextBox();
            lblDescription = new Label();
            txtDescription = new TextBox();
            lblPerformedBy = new Label();
            txtPerformedBy = new TextBox();
            lblCost = new Label();
            txtCost = new TextBox();
            lblPartsUsed = new Label();
            txtPartsUsed = new TextBox();
            lblStatus = new Label();
            cboStatus = new ComboBox();
            lblCompletedDate = new Label();
            dtpCompletedDate = new DateTimePicker();
            lblNotes = new Label();
            txtNotes = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            mainPanel = new Panel();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.AutoScroll = true;
            mainPanel.AutoScrollMinSize = new Size(600, 900);
            mainPanel.Controls.Add(lblTitle);
            mainPanel.Controls.Add(btnCancel);
            mainPanel.Controls.Add(btnSave);
            mainPanel.Controls.Add(txtNotes);
            mainPanel.Controls.Add(lblNotes);
            mainPanel.Controls.Add(dtpCompletedDate);
            mainPanel.Controls.Add(lblCompletedDate);
            mainPanel.Controls.Add(cboStatus);
            mainPanel.Controls.Add(lblStatus);
            mainPanel.Controls.Add(txtPartsUsed);
            mainPanel.Controls.Add(lblPartsUsed);
            mainPanel.Controls.Add(txtCost);
            mainPanel.Controls.Add(lblCost);
            mainPanel.Controls.Add(txtPerformedBy);
            mainPanel.Controls.Add(lblPerformedBy);
            mainPanel.Controls.Add(txtDescription);
            mainPanel.Controls.Add(lblDescription);
            mainPanel.Controls.Add(txtTitle);
            mainPanel.Controls.Add(lblTitleField);
            mainPanel.Controls.Add(dtpMaintenanceDate);
            mainPanel.Controls.Add(lblMaintenanceDate);
            mainPanel.Controls.Add(cboType);
            mainPanel.Controls.Add(lblType);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(31, 37);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(10);
            mainPanel.Size = new Size(620, 779);
            mainPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(19, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(560, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add Maintenance Record";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(19, 93);
            lblType.Margin = new Padding(5, 0, 5, 0);
            lblType.Name = "lblType";
            lblType.Size = new Size(158, 28);
            lblType.TabIndex = 1;
            lblType.Text = "Maintenance Type:";
            // 
            // cboType
            // 
            cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboType.Location = new Point(189, 88);
            cboType.Margin = new Padding(5, 6, 5, 6);
            cboType.Name = "cboType";
            cboType.Size = new Size(391, 36);
            cboType.TabIndex = 2;
            // 
            // lblMaintenanceDate
            // 
            lblMaintenanceDate.AutoSize = true;
            lblMaintenanceDate.Location = new Point(19, 149);
            lblMaintenanceDate.Margin = new Padding(5, 0, 5, 0);
            lblMaintenanceDate.Name = "lblMaintenanceDate";
            lblMaintenanceDate.Size = new Size(158, 28);
            lblMaintenanceDate.TabIndex = 3;
            lblMaintenanceDate.Text = "Maintenance Date:";
            // 
            // dtpMaintenanceDate
            // 
            dtpMaintenanceDate.Location = new Point(189, 144);
            dtpMaintenanceDate.Margin = new Padding(5, 6, 5, 6);
            dtpMaintenanceDate.Name = "dtpMaintenanceDate";
            dtpMaintenanceDate.Size = new Size(391, 34);
            dtpMaintenanceDate.TabIndex = 4;
            // 
            // lblTitleField
            // 
            lblTitleField.AutoSize = true;
            lblTitleField.Location = new Point(19, 205);
            lblTitleField.Margin = new Padding(5, 0, 5, 0);
            lblTitleField.Name = "lblTitleField";
            lblTitleField.Size = new Size(58, 28);
            lblTitleField.TabIndex = 5;
            lblTitleField.Text = "Title:";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(189, 200);
            txtTitle.Margin = new Padding(5, 6, 5, 6);
            txtTitle.MaxLength = 200;
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(391, 34);
            txtTitle.TabIndex = 6;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(19, 261);
            lblDescription.Margin = new Padding(5, 0, 5, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(116, 28);
            lblDescription.TabIndex = 7;
            lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(189, 256);
            txtDescription.Margin = new Padding(5, 6, 5, 6);
            txtDescription.MaxLength = 1000;
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(391, 80);
            txtDescription.TabIndex = 8;
            // 
            // lblPerformedBy
            // 
            lblPerformedBy.AutoSize = true;
            lblPerformedBy.Location = new Point(19, 357);
            lblPerformedBy.Margin = new Padding(5, 0, 5, 0);
            lblPerformedBy.Name = "lblPerformedBy";
            lblPerformedBy.Size = new Size(133, 28);
            lblPerformedBy.TabIndex = 9;
            lblPerformedBy.Text = "Performed By:";
            // 
            // txtPerformedBy
            // 
            txtPerformedBy.Location = new Point(189, 352);
            txtPerformedBy.Margin = new Padding(5, 6, 5, 6);
            txtPerformedBy.MaxLength = 100;
            txtPerformedBy.Name = "txtPerformedBy";
            txtPerformedBy.Size = new Size(391, 34);
            txtPerformedBy.TabIndex = 10;
            // 
            // lblCost
            // 
            lblCost.AutoSize = true;
            lblCost.Location = new Point(19, 413);
            lblCost.Margin = new Padding(5, 0, 5, 0);
            lblCost.Name = "lblCost";
            lblCost.Size = new Size(58, 28);
            lblCost.TabIndex = 11;
            lblCost.Text = "Cost:";
            // 
            // txtCost
            // 
            txtCost.Location = new Point(189, 408);
            txtCost.Margin = new Padding(5, 6, 5, 6);
            txtCost.MaxLength = 10;
            txtCost.Name = "txtCost";
            txtCost.Size = new Size(391, 34);
            txtCost.TabIndex = 12;
            // 
            // lblPartsUsed
            // 
            lblPartsUsed.AutoSize = true;
            lblPartsUsed.Location = new Point(19, 469);
            lblPartsUsed.Margin = new Padding(5, 0, 5, 0);
            lblPartsUsed.Name = "lblPartsUsed";
            lblPartsUsed.Size = new Size(116, 28);
            lblPartsUsed.TabIndex = 13;
            lblPartsUsed.Text = "Parts Used:";
            // 
            // txtPartsUsed
            // 
            txtPartsUsed.Location = new Point(189, 464);
            txtPartsUsed.Margin = new Padding(5, 6, 5, 6);
            txtPartsUsed.MaxLength = 100;
            txtPartsUsed.Name = "txtPartsUsed";
            txtPartsUsed.Size = new Size(391, 34);
            txtPartsUsed.TabIndex = 14;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(19, 525);
            lblStatus.Margin = new Padding(5, 0, 5, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(69, 28);
            lblStatus.TabIndex = 15;
            lblStatus.Text = "Status:";
            // 
            // cboStatus
            // 
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Location = new Point(189, 520);
            cboStatus.Margin = new Padding(5, 6, 5, 6);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(391, 36);
            cboStatus.TabIndex = 16;
            // 
            // lblCompletedDate
            // 
            lblCompletedDate.AutoSize = true;
            lblCompletedDate.Location = new Point(19, 581);
            lblCompletedDate.Margin = new Padding(5, 0, 5, 0);
            lblCompletedDate.Name = "lblCompletedDate";
            lblCompletedDate.Size = new Size(158, 28);
            lblCompletedDate.TabIndex = 17;
            lblCompletedDate.Text = "Completed Date:";
            // 
            // dtpCompletedDate
            // 
            dtpCompletedDate.Location = new Point(189, 576);
            dtpCompletedDate.Margin = new Padding(5, 6, 5, 6);
            dtpCompletedDate.Name = "dtpCompletedDate";
            dtpCompletedDate.Size = new Size(391, 34);
            dtpCompletedDate.TabIndex = 18;
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.Location = new Point(19, 637);
            lblNotes.Margin = new Padding(5, 0, 5, 0);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(69, 28);
            lblNotes.TabIndex = 19;
            lblNotes.Text = "Notes:";
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(189, 632);
            txtNotes.Margin = new Padding(5, 6, 5, 6);
            txtNotes.MaxLength = 500;
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(391, 60);
            txtNotes.TabIndex = 20;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(189, 720);
            btnSave.Margin = new Padding(5, 6, 5, 6);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(157, 56);
            btnSave.TabIndex = 21;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(128, 128, 128);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(356, 720);
            btnCancel.Margin = new Padding(5, 6, 5, 6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(157, 56);
            btnCancel.TabIndex = 22;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // AddMaintenanceForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 853);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 6, 4, 6);
            Name = "AddMaintenanceForm";
            Padding = new Padding(31, 37, 31, 37);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Maintenance Record";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label lblMaintenanceDate;
        private System.Windows.Forms.DateTimePicker dtpMaintenanceDate;
        private System.Windows.Forms.Label lblTitleField;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblPerformedBy;
        private System.Windows.Forms.TextBox txtPerformedBy;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.TextBox txtCost;
        private System.Windows.Forms.Label lblPartsUsed;
        private System.Windows.Forms.TextBox txtPartsUsed;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblCompletedDate;
        private System.Windows.Forms.DateTimePicker dtpCompletedDate;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel mainPanel;
    }
} 