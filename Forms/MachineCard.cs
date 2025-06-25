using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using goodwin_winForm.Models;
using System.IO;

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
            picMachineImage = new PictureBox();
            ((ISupportInitialize)picMachineImage).BeginInit();
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
            statusPanel.Location = new Point(279, 66);
            statusPanel.Margin = new Padding(3, 4, 3, 4);
            statusPanel.Name = "statusPanel";
            statusPanel.Size = new Size(120, 25);
            statusPanel.TabIndex = 6;
            // 
            // picMachineImage
            // 
            picMachineImage.BorderStyle = BorderStyle.FixedSingle;
            picMachineImage.Location = new Point(279, 104);
            picMachineImage.Name = "picMachineImage";
            picMachineImage.Size = new Size(119, 119);
            picMachineImage.SizeMode = PictureBoxSizeMode.Zoom;
            picMachineImage.TabIndex = 7;
            picMachineImage.TabStop = false;
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
            Controls.Add(picMachineImage);
            Cursor = Cursors.Hand;
            Margin = new Padding(11, 13, 11, 13);
            Name = "MachineCard";
            Size = new Size(423, 250);
            Load += MachineCard_Load;
            Click += MachineCard_Click;
            ((ISupportInitialize)picMachineImage).EndInit();
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
            LoadMachineImage();
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

        /// <summary>
        /// Loads and displays the machine image thumbnail.
        /// </summary>
        private void LoadMachineImage()
        {
            try
            {
                if (!string.IsNullOrEmpty(_machine.ImagePath))
                {
                    // Construct the full path from the filename stored in database
                    string fullImagePath = Path.Combine(@"C:\GoodwinImages\Machines", _machine.ImagePath);
                    
                    // Check if the image file exists
                    if (File.Exists(fullImagePath))
                    {
                        using (var image = Image.FromFile(fullImagePath))
                        {
                            picMachineImage.Image = new Bitmap(image);
                        }
                    }
                    else
                    {
                        // Image file not found, show default image
                        SetDefaultImage();
                    }
                }
                else
                {
                    // No image path specified, show default image
                    SetDefaultImage();
                }
            }
            catch (Exception ex)
            {
                // Error loading image, show default image
                SetDefaultImage();
                System.Diagnostics.Debug.WriteLine($"Error loading machine image: {ex.Message}");
            }
        }

        /// <summary>
        /// Sets a default placeholder image when no machine image is available.
        /// </summary>
        private void SetDefaultImage()
        {
            try
            {
                // Create a simple placeholder image
                var placeholderImage = new Bitmap(119, 119);
                using (var graphics = Graphics.FromImage(placeholderImage))
                {
                    graphics.Clear(Color.LightGray);
                    
                    // Draw a simple machine icon
                    using (var pen = new Pen(Color.DarkGray, 2))
                    {
                        // Draw a simple rectangle representing a machine
                        graphics.DrawRectangle(pen, 20, 20, 79, 59);
                        
                        // Draw some lines to represent machine parts
                        graphics.DrawLine(pen, 30, 35, 89, 35);
                        graphics.DrawLine(pen, 30, 50, 89, 50);
                        graphics.DrawLine(pen, 30, 65, 89, 65);
                    }
                    
                    // Add text
                    using (var font = new Font("Arial", 8, FontStyle.Bold))
                    using (var brush = new SolidBrush(Color.DarkGray))
                    {
                        graphics.DrawString("No Image", font, brush, 35, 85);
                    }
                }
                
                picMachineImage.Image = placeholderImage;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating placeholder image: {ex.Message}");
                picMachineImage.Image = null;
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
        private PictureBox picMachineImage;

        private void MachineCard_Load(object sender, EventArgs e)
        {

        }
    }
}