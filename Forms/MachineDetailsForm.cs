using System;
using System.Linq;
using System.Windows.Forms;
using goodwin_winForm.Models;
using goodwin_winForm.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using System.IO;
using goodwin_winForm.Controllers;

namespace goodwin_winForm.Forms
{
    public partial class MachineDetailsForm : BaseForm
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Machine _machine;
        private Machine _detailedMachine;

        public MachineDetailsForm(IServiceProvider serviceProvider, Machine machine)
        {
            _serviceProvider = serviceProvider;
            _machine = machine;
            _detailedMachine = machine;
            InitializeComponent();
            LoadMachineDetails();
        }

        private async void LoadMachineDetails()
        {
            try
            {
                if (_detailedMachine != null)
                {
                    DisplayMachineInfo();
                    LoadMaintenanceHistory();
                    LoadAlerts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading machine details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayMachineInfo()
        {
            lblMachineNameValue.Text = _detailedMachine.Name;
            lblSerialNumberValue.Text = _detailedMachine.SerialNumber;
            lblModelValue.Text = _detailedMachine.Model;
            lblManufacturerValue.Text = _detailedMachine.Manufacturer ?? "N/A";
            lblStatusValue.Text = _detailedMachine.Status.ToString();
            lblLocationValue.Text = _detailedMachine.Location ?? "N/A";
            lblDepartmentValue.Text = _detailedMachine.Department ?? "N/A";
            lblInstallationDateValue.Text = _detailedMachine.InstallationDate.ToShortDateString();
            lblLastMaintenanceValue.Text = _detailedMachine.LastMaintenanceDate.ToShortDateString();
            lblNextMaintenanceValue.Text = _detailedMachine.NextMaintenanceDate.ToShortDateString();
            txtDescription.Text = _detailedMachine.Description ?? "";
            txtNotes.Text = _detailedMachine.Notes ?? "";

            // Load machine image
            LoadMachineImage();

            // Set status color
            switch (_detailedMachine.Status)
            {
                case MachineStatus.Operational:
                    lblStatusValue.ForeColor = System.Drawing.Color.Green;
                    break;
                case MachineStatus.Warning:
                    lblStatusValue.ForeColor = System.Drawing.Color.Orange;
                    break;
                case MachineStatus.Critical:
                    lblStatusValue.ForeColor = System.Drawing.Color.Red;
                    break;
                case MachineStatus.UnderMaintenance:
                    lblStatusValue.ForeColor = System.Drawing.Color.Blue;
                    break;
                case MachineStatus.OutOfService:
                    lblStatusValue.ForeColor = System.Drawing.Color.Gray;
                    break;
            }
        }

        /// <summary>
        /// Loads and displays the machine image from the database.
        /// </summary>
        private void LoadMachineImage()
        {
            try
            {
                if (!string.IsNullOrEmpty(_detailedMachine.ImagePath))
                {
                    // Construct the full path from the filename stored in database
                    string fullImagePath = Path.Combine(@"C:\GoodwinImages\Machines", _detailedMachine.ImagePath);

                    // Check if the image file exists
                    if (File.Exists(fullImagePath))
                    {
                        using (var image = Image.FromFile(fullImagePath))
                        {
                            pictureBoxMachine.Image = new Bitmap(image);
                        }
                    }
                    else
                    {
                        // Image file not found, show default image or placeholder
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
                var placeholderImage = new Bitmap(300, 200);
                using (var graphics = Graphics.FromImage(placeholderImage))
                {
                    graphics.Clear(Color.LightGray);

                    // Draw a simple machine icon
                    using (var pen = new Pen(Color.DarkGray, 2))
                    {
                        // Draw a simple rectangle representing a machine
                        graphics.DrawRectangle(pen, 50, 50, 200, 100);

                        // Draw some lines to represent machine parts
                        graphics.DrawLine(pen, 70, 70, 230, 70);
                        graphics.DrawLine(pen, 70, 90, 230, 90);
                        graphics.DrawLine(pen, 70, 110, 230, 110);
                    }

                    // Add text
                    using (var font = new Font("Arial", 12, FontStyle.Bold))
                    using (var brush = new SolidBrush(Color.DarkGray))
                    {
                        graphics.DrawString("No Image", font, brush, 120, 160);
                    }
                }

                pictureBoxMachine.Image = placeholderImage;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating placeholder image: {ex.Message}");
                pictureBoxMachine.Image = null;
            }
        }

        private void LoadMaintenanceHistory()
        {
            listViewMaintenance.Items.Clear();

            if (_detailedMachine.MaintenanceRecords != null)
            {
                foreach (var record in _detailedMachine.MaintenanceRecords.Take(10)) // Show last 10 records
                {
                    var item = new ListViewItem(record.MaintenanceDate.ToShortDateString());
                    item.SubItems.Add(record.Type.ToString());
                    item.SubItems.Add(record.Title);
                    item.SubItems.Add(record.PerformedBy);
                    item.SubItems.Add(record.Status.ToString());
                    item.SubItems.Add(record.Cost.ToString("C"));
                    item.Tag = record;

                    listViewMaintenance.Items.Add(item);
                }
            }
        }

        private void LoadAlerts()
        {
            listViewAlerts.Items.Clear();

            if (_detailedMachine.Alerts != null)
            {
                foreach (var alert in _detailedMachine.Alerts.Where(a => a.Status == AlertStatus.Active).Take(10))
                {
                    var item = new ListViewItem(alert.CreatedDate.ToShortDateString());
                    item.SubItems.Add(alert.Type.ToString());
                    item.SubItems.Add(alert.Severity.ToString());
                    item.SubItems.Add(alert.Title);
                    item.SubItems.Add(alert.Status.ToString());
                    item.Tag = alert;

                    // Set color based on severity
                    switch (alert.Severity)
                    {
                        case AlertSeverity.Low:
                            item.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        case AlertSeverity.Medium:
                            item.BackColor = System.Drawing.Color.Yellow;
                            break;
                        case AlertSeverity.High:
                            item.BackColor = System.Drawing.Color.Orange;
                            break;
                        case AlertSeverity.Critical:
                            item.BackColor = System.Drawing.Color.Red;
                            break;
                    }

                    listViewAlerts.Items.Add(item);
                }
            }
        }

        private void btnAddMaintenance_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the maintenance controller from the service provider
                var maintenanceController = _serviceProvider.GetRequiredService<IMaintenanceController>();

                // Open the add maintenance form
                using (var addMaintenanceForm = new AddMaintenanceForm(maintenanceController, _detailedMachine.MachineId, _detailedMachine.Name))
                {
                    if (addMaintenanceForm.ShowDialog(this) == DialogResult.OK)
                    {
                        // Maintenance record was added successfully, refresh the details
                        LoadMachineDetails();
                        ShowSuccessMessage("Maintenance record added successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening add maintenance form: {ex.Message}");
            }
        }

        private void btnAddAlert_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the alert controller from the service provider
                var alertController = _serviceProvider.GetRequiredService<IAlertController>();

                // Open the add alert form
                using (var addAlertForm = new AddAlertForm(alertController, _detailedMachine.MachineId, _detailedMachine.Name))
                {
                    if (addAlertForm.ShowDialog(this) == DialogResult.OK)
                    {
                        // Alert was added successfully, refresh the details
                        LoadMachineDetails();
                        ShowSuccessMessage("Alert added successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening add alert form: {ex.Message}");
            }
        }

        private void btnEditMachine_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the machine controller from the service provider
                var machineController = _serviceProvider.GetRequiredService<IMachineController>();

                // Open the edit machine form
                using (var editForm = new EditMachineForm(machineController, _detailedMachine))
                {
                    if (editForm.ShowDialog(this) == DialogResult.OK)
                    {
                        // Machine was updated successfully, refresh the details
                        LoadMachineDetails();
                        ShowSuccessMessage("Machine updated successfully!");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening edit form: {ex.Message}");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMachineDetails();
        }

       
    }
} 