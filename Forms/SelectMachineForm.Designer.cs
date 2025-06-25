namespace goodwin_winForm.Forms
{
    partial class SelectMachineForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectMachineForm));
            btnAddMachine = new Button();
            btnEditMachine = new Button();
            btnViewDetails = new Button();
            btnRefresh = new Button();
            lblTitle = new Label();
            flowLayoutPanelMachines = new FlowLayoutPanel();
            logo = new PictureBox();
            btnSettings = new Button();
            ((System.ComponentModel.ISupportInitialize)logo).BeginInit();
            SuspendLayout();
            // 
            // btnAddMachine
            // 
            btnAddMachine.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddMachine.BackColor = Color.White;
            btnAddMachine.Location = new Point(19, 558);
            btnAddMachine.Margin = new Padding(5, 6, 5, 6);
            btnAddMachine.Name = "btnAddMachine";
            btnAddMachine.Size = new Size(157, 56);
            btnAddMachine.TabIndex = 1;
            btnAddMachine.Text = "Add Machine";
            btnAddMachine.UseVisualStyleBackColor = false;
            btnAddMachine.Click += btnAddMachine_Click;
            // 
            // btnEditMachine
            // 
            btnEditMachine.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditMachine.BackColor = Color.White;
            btnEditMachine.Location = new Point(185, 558);
            btnEditMachine.Margin = new Padding(5, 6, 5, 6);
            btnEditMachine.Name = "btnEditMachine";
            btnEditMachine.Size = new Size(157, 56);
            btnEditMachine.TabIndex = 2;
            btnEditMachine.Text = "Edit Machine";
            btnEditMachine.UseVisualStyleBackColor = false;
            btnEditMachine.Click += btnEditMachine_Click;
            // 
            // btnViewDetails
            // 
            btnViewDetails.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnViewDetails.BackColor = Color.White;
            btnViewDetails.Location = new Point(352, 558);
            btnViewDetails.Margin = new Padding(5, 6, 5, 6);
            btnViewDetails.Name = "btnViewDetails";
            btnViewDetails.Size = new Size(157, 56);
            btnViewDetails.TabIndex = 3;
            btnViewDetails.Text = "View Details";
            btnViewDetails.UseVisualStyleBackColor = false;
            btnViewDetails.Click += btnViewDetails_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRefresh.Location = new Point(806, 558);
            btnRefresh.Margin = new Padding(5, 6, 5, 6);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(157, 56);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(19, 73);
            lblTitle.Margin = new Padding(5, 0, 5, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(379, 37);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "Machine Monitoring System";
            // 
            // flowLayoutPanelMachines
            // 
            flowLayoutPanelMachines.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanelMachines.AutoScroll = true;
            flowLayoutPanelMachines.BackColor = Color.White;
            flowLayoutPanelMachines.Location = new Point(31, 140);
            flowLayoutPanelMachines.Name = "flowLayoutPanelMachines";
            flowLayoutPanelMachines.Padding = new Padding(10);
            flowLayoutPanelMachines.Size = new Size(920, 404);
            flowLayoutPanelMachines.TabIndex = 7;
            // 
            // logo
            // 
            logo.Image = (Image)resources.GetObject("logo.Image");
            logo.Location = new Point(19, 8);
            logo.Name = "logo";
            logo.Size = new Size(379, 62);
            logo.SizeMode = PictureBoxSizeMode.StretchImage;
            logo.TabIndex = 9;
            logo.TabStop = false;
            // 
            // btnSettings
            // 
            btnSettings.BackColor = Color.FromArgb(243, 244, 246);
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            btnSettings.Location = new Point(887, 73);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(64, 54);
            btnSettings.TabIndex = 10;
            btnSettings.Text = "⚙";
            btnSettings.UseVisualStyleBackColor = false;
            btnSettings.Click += btnSettings_Click;
            // 
            // SelectMachineForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(982, 653);
            Controls.Add(btnSettings);
            Controls.Add(logo);
            Controls.Add(flowLayoutPanelMachines);
            Controls.Add(lblTitle);
            Controls.Add(btnRefresh);
            Controls.Add(btnViewDetails);
            Controls.Add(btnEditMachine);
            Controls.Add(btnAddMachine);
            Margin = new Padding(5, 6, 5, 6);
            Name = "SelectMachineForm";
            Padding = new Padding(31, 37, 31, 37);
            Text = "Goodwin Machine Monitoring System";
            ((System.ComponentModel.ISupportInitialize)logo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btnAddMachine;
        private System.Windows.Forms.Button btnEditMachine;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblTitle;
        private FlowLayoutPanel flowLayoutPanelMachines;
        private PictureBox logo;
        private Button btnSettings;
    }
}