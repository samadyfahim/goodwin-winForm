namespace goodwin_winForm.Forms
{
    partial class AddAlertForm
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
            lblSeverity = new Label();
            cboSeverity = new ComboBox();
            lblTitleField = new Label();
            txtTitle = new TextBox();
            lblMessage = new Label();
            txtMessage = new TextBox();
            lblStatus = new Label();
            cboStatus = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            mainPanel = new Panel();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(19, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(560, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add Alert";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(19, 93);
            lblType.Margin = new Padding(5, 0, 5, 0);
            lblType.Name = "lblType";
            lblType.Size = new Size(104, 28);
            lblType.TabIndex = 1;
            lblType.Text = "Alert Type:";
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
            // lblSeverity
            // 
            lblSeverity.AutoSize = true;
            lblSeverity.Location = new Point(19, 149);
            lblSeverity.Margin = new Padding(5, 0, 5, 0);
            lblSeverity.Name = "lblSeverity";
            lblSeverity.Size = new Size(86, 28);
            lblSeverity.TabIndex = 3;
            lblSeverity.Text = "Severity:";
            // 
            // cboSeverity
            // 
            cboSeverity.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSeverity.Location = new Point(189, 144);
            cboSeverity.Margin = new Padding(5, 6, 5, 6);
            cboSeverity.Name = "cboSeverity";
            cboSeverity.Size = new Size(391, 36);
            cboSeverity.TabIndex = 4;
            // 
            // lblTitleField
            // 
            lblTitleField.AutoSize = true;
            lblTitleField.Location = new Point(19, 205);
            lblTitleField.Margin = new Padding(5, 0, 5, 0);
            lblTitleField.Name = "lblTitleField";
            lblTitleField.Size = new Size(53, 28);
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
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(19, 261);
            lblMessage.Margin = new Padding(5, 0, 5, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(92, 28);
            lblMessage.TabIndex = 7;
            lblMessage.Text = "Message:";
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(189, 256);
            txtMessage.Margin = new Padding(5, 6, 5, 6);
            txtMessage.MaxLength = 1000;
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(391, 120);
            txtMessage.TabIndex = 8;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(19, 397);
            lblStatus.Margin = new Padding(5, 0, 5, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(69, 28);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "Status:";
            // 
            // cboStatus
            // 
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cboStatus.Location = new Point(189, 392);
            cboStatus.Margin = new Padding(5, 6, 5, 6);
            cboStatus.Name = "cboStatus";
            cboStatus.Size = new Size(391, 36);
            cboStatus.TabIndex = 10;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(189, 492);
            btnSave.Margin = new Padding(5, 6, 5, 6);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(157, 56);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(128, 128, 128);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(422, 492);
            btnCancel.Margin = new Padding(5, 6, 5, 6);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(157, 56);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // mainPanel
            // 
            mainPanel.AutoScroll = true;
            mainPanel.AutoScrollMinSize = new Size(600, 800);
            mainPanel.Controls.Add(lblTitle);
            mainPanel.Controls.Add(btnCancel);
            mainPanel.Controls.Add(btnSave);
            mainPanel.Controls.Add(cboStatus);
            mainPanel.Controls.Add(lblStatus);
            mainPanel.Controls.Add(txtMessage);
            mainPanel.Controls.Add(lblMessage);
            mainPanel.Controls.Add(txtTitle);
            mainPanel.Controls.Add(lblTitleField);
            mainPanel.Controls.Add(cboSeverity);
            mainPanel.Controls.Add(lblSeverity);
            mainPanel.Controls.Add(cboType);
            mainPanel.Controls.Add(lblType);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(31, 37);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(10);
            mainPanel.Size = new Size(620, 621);
            mainPanel.TabIndex = 0;
            // 
            // AddAlertForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 695);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 6, 4, 6);
            Name = "AddAlertForm";
            Padding = new Padding(31, 37, 31, 37);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Alert";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label lblSeverity;
        private System.Windows.Forms.ComboBox cboSeverity;
        private System.Windows.Forms.Label lblTitleField;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel mainPanel;
    }
} 