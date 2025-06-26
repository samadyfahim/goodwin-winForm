using System;
using System.Windows.Forms;
using goodwin_winForm.Models;
using goodwin_winForm.Controllers;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace goodwin_winForm.Forms
{
    /// <summary>
    /// Touch-optimized form for adding new machines to the system.
    /// Provides a user-friendly interface with touch-friendly controls for machine data entry.
    /// </summary>
    public partial class AddMachineForm : BaseForm
    {
        private readonly IMachineController _machineController;
        private string? _selectedImagePath;

        /// <summary>
        /// Initializes a new instance of the AddMachineForm with the specified machine controller.
        /// </summary>
        /// <param name="machineController">The machine controller for data operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when machineController is null.</exception>
        public AddMachineForm(IMachineController machineController)
        {
            _machineController = machineController ?? throw new ArgumentNullException(nameof(machineController));
            InitializeComponent();
            InitializeComboBoxes();
            SetupForm();
        }

        /// <summary>
        /// Sets up the form with touch-friendly configuration and control setup.
        /// </summary>
        private void SetupForm()
        {
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
            this.Text = "Add New Machine";
            
            // Setup loading button
            SetupLoadingButton(btnSave, "Save");
            
            // Initialize image controls
            picMachineImage.Image = null;
            _selectedImagePath = null;
        }

        /// <summary>
        /// Initializes combo boxes with machine status and department options.
        /// </summary>
        private void InitializeComboBoxes()
        {
            // Initialize Status ComboBox
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(Enum.GetNames(typeof(MachineStatus)));
            cboStatus.SelectedIndex = 0; // Default to Operational

            // Initialize Department ComboBox with common departments
            cboDepartment.Items.Clear();
            cboDepartment.Items.AddRange(new string[] { "Production", "Packaging", "Assembly", "Quality Control", "Maintenance", "Warehouse" });
        }

        /// <summary>
        /// Handles the save button click event with form validation and machine creation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                SetLoadingState(btnSave, true, "Saving...");

                // Copy image to proper directory if one is selected
                string? finalImagePath = null;
                if (!string.IsNullOrEmpty(_selectedImagePath))
                {
                    finalImagePath = await CopyImageToDirectoryAsync(_selectedImagePath, txtSerialNumber.Text.Trim());
                }

                var machine = CreateMachineFromForm();
                machine.ImagePath = finalImagePath;
                
                if (await _machineController.AddMachineAsync(machine))
                {
                    // Machine added successfully - close form and let parent refresh automatically
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowErrorMessage("Failed to add machine. Please check your input and try again.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error adding machine: {ex.Message}");
            }
            finally
            {
                SetLoadingState(btnSave, false);
            }
        }

        /// <summary>
        /// Creates a Machine object from the form data.
        /// </summary>
        /// <returns>A new Machine object populated with form data.</returns>
        private Machine CreateMachineFromForm()
        {
            return new Machine
            {
                Name = txtName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                SerialNumber = txtSerialNumber.Text.Trim(),
                Model = txtModel.Text.Trim(),
                Manufacturer = txtManufacturer.Text.Trim(),
                InstallationDate = dtpInstallationDate.Value,
                Status = (MachineStatus)Enum.Parse(typeof(MachineStatus), cboStatus.Text),
                Location = txtLocation.Text.Trim(),
                Department = cboDepartment.Text,
                LastMaintenanceDate = DateTime.Now,
                NextMaintenanceDate = DateTime.Now.AddDays(30),
                MaintenanceIntervalDays = 30,
                Notes = txtNotes.Text.Trim()
            };
        }

        /// <summary>
        /// Validates all form fields according to business rules.
        /// </summary>
        /// <returns>True if all validations pass; otherwise, false.</returns>
        private bool ValidateForm()
        {
            if (!ValidateRequiredField(txtName, "machine name"))
                return false;

            if (!ValidateRequiredField(txtSerialNumber, "serial number"))
                return false;

            if (!ValidateRequiredField(txtModel, "model"))
                return false;

            if (!ValidateRequiredField(txtManufacturer, "manufacturer"))
                return false;

            if (!ValidateDateField(dtpInstallationDate, "Installation date"))
                return false;

            return true;
        }

        /// <summary>
        /// Handles the cancel button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Handles the browse image button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            // Use a synchronous approach to avoid threading issues
            string? selectedFilePath = ShowFileDialog();
            
            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                try
                {
                    // Load and display the image
                    picMachineImage.Image = System.Drawing.Image.FromFile(selectedFilePath);
                    
                    // Store the original path for now - will be copied when saving
                    _selectedImagePath = selectedFilePath;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Error loading image: {ex.Message}");
                    picMachineImage.Image = null;
                    _selectedImagePath = null;
                }
            }
        }

        /// <summary>
        /// Shows the file dialog for image selection.
        /// </summary>
        /// <returns>The selected file path or null if cancelled.</returns>
        private string? ShowFileDialog()
        {
            // Ensure we're on the UI thread
            if (InvokeRequired)
            {
                string? result = null;
                Invoke(new Action(() => result = ShowFileDialog()));
                return result;
            }

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*";
                    openFileDialog.Title = "Select Machine Image";
                    openFileDialog.Multiselect = false;
                    openFileDialog.CheckFileExists = true;
                    openFileDialog.CheckPathExists = true;

                    if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        return openFileDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening file dialog: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Handles the clear image button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnClearImage_Click(object sender, EventArgs e)
        {
            picMachineImage.Image = null;
            _selectedImagePath = null;
        }

        /// <summary>
        /// Copies the selected image to the machine images directory with a unique filename.
        /// </summary>
        /// <param name="sourcePath">The path to the source image file.</param>
        /// <param name="serialNumber">The machine serial number to use in the filename.</param>
        /// <returns>The relative path to the copied image file.</returns>
        private async Task<string> CopyImageToDirectoryAsync(string sourcePath, string serialNumber)
        {
            try
            {
                // Ensure the images directory exists
                string imageDir = @"C:\GoodwinImages\Machines";
                if (!Directory.Exists(imageDir))
                {
                    Directory.CreateDirectory(imageDir);
                }

                // Create a unique filename based on serial number and timestamp
                string fileExtension = Path.GetExtension(sourcePath);
                string fileName = $"{serialNumber}_{DateTime.Now:yyyyMMdd_HHmmss}{fileExtension}";
                string destinationPath = Path.Combine(imageDir, fileName);

                // Copy the file
                await Task.Run(() => File.Copy(sourcePath, destinationPath, true));

                // Return the relative path for database storage
                return fileName;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to copy image: {ex.Message}", ex);
            }
        }
    }
} 