using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using goodwin_winForm.Models;

namespace goodwin_winForm.Forms
{
    /// <summary>
    /// Custom user control for displaying machine information in a card format.
    /// Provides touch-friendly interface with visual status indicators.
    /// </summary>
    public partial class MachineCard : UserControl
    {
        private Machine _machine;
        private bool _isSelected = false;
        private Color _borderColor = Color.LightGray;
        private Color _selectedBorderColor = Color.Blue;
        private Color _backgroundColor = Color.White;

        public event EventHandler<Machine> MachineSelected;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Machine Machine
        {
            get => _machine;
            set
            {
                _machine = value;
                UpdateDisplay();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                UpdateVisualState();
            }
        }

        public MachineCard()
        {
            InitializeComponent();
            SetupCard();
        }

        public MachineCard(Machine machine) : this()
        {
            Machine = machine;
        }

        private void InitializeComponent()
        {
            lblName = new Label();
            lblSerialNumber = new Label();
            lblModel = new Label();
            lblStatus = new Label();
            lblLocation = new Label();
            lblNextMaintenance = new Label();
            statusPanel = new Panel();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblName.Location = new Point(17, 22);
            lblName.Name = "lblName";
            lblName.Size = new Size(185, 32);
            lblName.TabIndex = 0;
            lblName.Text = "Machine Name";
            // 
            // lblSerialNumber
            // 
            lblSerialNumber.AutoSize = true;
            lblSerialNumber.Font = new Font("Segoe UI", 10F);
            lblSerialNumber.Location = new Point(17, 133);
            lblSerialNumber.Name = "lblSerialNumber";
            lblSerialNumber.Size = new Size(136, 23);
            lblSerialNumber.TabIndex = 1;
            lblSerialNumber.Text = "Serial: SN123456";
            // 
            // lblModel
            // 
            lblModel.AutoSize = true;
            lblModel.Font = new Font("Segoe UI", 10F);
            lblModel.Location = new Point(17, 100);
            lblModel.Name = "lblModel";
            lblModel.Size = new Size(133, 23);
            lblModel.TabIndex = 2;
            lblModel.Text = "Model: ABC-123";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStatus.Location = new Point(17, 66);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(165, 23);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Status: Operational";
            // 
            // lblLocation
            // 
            lblLocation.AutoSize = true;
            lblLocation.Font = new Font("Segoe UI", 10F);
            lblLocation.Location = new Point(17, 167);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(162, 23);
            lblLocation.TabIndex = 4;
            lblLocation.Text = "Location: Building A";
            // 
            // lblNextMaintenance
            // 
            lblNextMaintenance.AutoSize = true;
            lblNextMaintenance.Font = new Font("Segoe UI", 10F);
            lblNextMaintenance.Location = new Point(17, 200);
            lblNextMaintenance.Name = "lblNextMaintenance";
            lblNextMaintenance.Size = new Size(245, 23);
            lblNextMaintenance.TabIndex = 5;
            lblNextMaintenance.Text = "Next Maintenance: 12/31/2024";
            // 
            // statusPanel
            // 
            statusPanel.BackColor = Color.Green;
            statusPanel.BorderStyle = BorderStyle.FixedSingle;
            statusPanel.Location = new Point(249, 66);
            statusPanel.Margin = new Padding(3, 4, 3, 4);
            statusPanel.Name = "statusPanel";
            statusPanel.Size = new Size(119, 23);
            statusPanel.TabIndex = 6;
            // 
            // MachineCard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblName);
            Controls.Add(lblSerialNumber);
            Controls.Add(lblModel);
            Controls.Add(lblStatus);
            Controls.Add(lblLocation);
            Controls.Add(lblNextMaintenance);
            Controls.Add(statusPanel);
            Cursor = Cursors.Hand;
            Margin = new Padding(11, 13, 11, 13);
            Name = "MachineCard";
            Size = new Size(400, 250);
            Load += MachineCard_Load;
            Click += MachineCard_Click;
            ResumeLayout(false);
            PerformLayout();
        }

        private void SetupCard()
        {
            // Enable double buffering to prevent flickering
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            // Make the card touch-friendly
            this.Font = new Font("Segoe UI", 10F);
        }

        private void UpdateDisplay()
        {
            if (_machine == null) return;

            lblName.Text = _machine.Name;
            lblSerialNumber.Text = $"Serial: {_machine.SerialNumber}";
            lblModel.Text = $"Model: {_machine.Model}";
            lblStatus.Text = $"Status: {_machine.Status}";
            lblLocation.Text = $"Location: {_machine.Location ?? "N/A"}";
            lblNextMaintenance.Text = $"Next Maintenance: {_machine.NextMaintenanceDate.ToShortDateString()}";

            UpdateStatusColor();
        }

        private void UpdateStatusColor()
        {
            if (_machine == null) return;

            switch (_machine.Status)
            {
                case MachineStatus.Operational:
                    statusPanel.BackColor = Color.Green;
                    break;
                case MachineStatus.Warning:
                    statusPanel.BackColor = Color.Yellow;
                    break;
                case MachineStatus.Critical:
                    statusPanel.BackColor = Color.Red;
                    break;
                case MachineStatus.UnderMaintenance:
                    statusPanel.BackColor = Color.Orange;
                    break;
                case MachineStatus.OutOfService:
                    statusPanel.BackColor = Color.Gray;
                    break;
            }
        }

        private void UpdateVisualState()
        {
            if (_isSelected)
            {
                this.BorderStyle = BorderStyle.Fixed3D;
                this.BackColor = Color.LightBlue;
            }
            else
            {
                this.BorderStyle = BorderStyle.FixedSingle;
                this.BackColor = _backgroundColor;
            }
        }

        private void MachineCard_Click(object sender, EventArgs e)
        {
            IsSelected = true;
            MachineSelected?.Invoke(this, _machine);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw custom border
            using (Pen pen = new Pen(_isSelected ? _selectedBorderColor : _borderColor, 2))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
            }
        }

        // Designer components
        private Label lblName;
        private Label lblSerialNumber;
        private Label lblModel;
        private Label lblStatus;
        private Label lblLocation;
        private Label lblNextMaintenance;
        private Panel statusPanel;

        private void MachineCard_Load(object sender, EventArgs e)
        {

        }
    }
}