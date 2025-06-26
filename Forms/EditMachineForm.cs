using System;
using System.Windows.Forms;
using goodwin_winForm.Models;
using goodwin_winForm.Controllers;
using System.IO;
using System.Threading.Tasks;

namespace goodwin_winForm.Forms
{
    /// <summary>
    /// Touch-optimized form for editing existing machines in the system.
    /// Provides a user-friendly interface with touch-friendly controls for machine data editing.
    /// </summary>
    public partial class EditMachineForm : BaseForm
    {
        private readonly IMachineController _machineController;
        private readonly Machine _originalMachine;
        private string? _selectedImagePath;
        private string? _originalImagePath;

        /// <summary>
        /// Initializes a new instance of the EditMachineForm with the specified machine controller and machine to edit.
        /// </summary>
        /// <param name="machineController">The machine controller for data operations.</param>
        /// <param name="machine">The machine to edit.</param>
        /// <exception cref="ArgumentNullException">Thrown when machineController or machine is null.</exception>
        public EditMachineForm(IMachineController machineController, Machine machine)
        {
            _machineController = machineController ?? throw new ArgumentNullException(nameof(machineController));
            _originalMachine = machine ?? throw new ArgumentNullException(nameof(machine));
            InitializeComponent();
            InitializeComboBoxes();
            SetupForm();
            LoadMachineData();
        }

        /// <summary>
        /// Sets up the form with touch-friendly configuration and control setup.
        /// </summary>
        private void SetupForm()
        {
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
            this.Text = "Edit Machine";
            
            // Setup loading button
            SetupLoadingButton(btnSave, "Save");
            
            // Initialize image controls
            picMachineImage.Image = null;
            _selectedImagePath = null;
            _originalImagePath = _originalMachine.ImagePath;
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
        /// Loads the existing machine data into the form controls.
        /// </summary>
        private void LoadMachineData()
        {
            txtName.Text = _originalMachine.Name;
            txtDescription.Text = _originalMachine.Description ?? "";
            txtSerialNumber.Text = _originalMachine.SerialNumber;
            txtModel.Text = _originalMachine.Model;
            txtManufacturer.Text = _originalMachine.Manufacturer ?? "";
            dtpInstallationDate.Value = _originalMachine.InstallationDate;
            cboStatus.Text = _originalMachine.Status.ToString();
            txtLocation.Text = _originalMachine.Location ?? "";
            cboDepartment.Text = _originalMachine.Department ?? "";
            txtNotes.Text = _originalMachine.Notes ?? "";

            // Load existing image if available
            LoadExistingImage();
        }

        /// <summary>
        /// Loads the existing machine image if available.
        /// </summary>
        private void LoadExistingImage()
        {
            if (!string.IsNullOrEmpty(_originalMachine.ImagePath))
            {
                try
                {
                    // Construct the full path from the filename stored in database
                    string fullImagePath = Path.Combine(@"C:\GoodwinImages\Machines", _originalMachine.ImagePath);
                    
                    // Check if the image file exists
                    if (File.Exists(fullImagePath))
                    {
                        picMachineImage.Image = System.Drawing.Image.FromFile(fullImagePath);
                        _selectedImagePath = fullImagePath; // Set to full path for comparison
                    }
                    else
                    {
                        // Image file not found, show default image
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
            else
            {
                // No image path specified, show default image
                SetDefaultImage();
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
                var placeholderImage = new System.Drawing.Bitmap(200, 125);
                using (var graphics = System.Drawing.Graphics.FromImage(placeholderImage))
                {
                    graphics.Clear(System.Drawing.Color.LightGray);
                    
                    // Draw a simple machine icon
                    using (var pen = new System.Drawing.Pen(System.Drawing.Color.DarkGray, 2))
                    {
                        // Draw a simple rectangle representing a machine
                        graphics.DrawRectangle(pen, 50, 30, 100, 65);
                        
                        // Draw some lines to represent machine parts
                        graphics.DrawLine(pen, 60, 45, 140, 45);
                        graphics.DrawLine(pen, 60, 60, 140, 60);
                        graphics.DrawLine(pen, 60, 75, 140, 75);
                    }
                    
                    // Add text
                    using (var font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold))
                    using (var brush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkGray))
                    {
                        graphics.DrawString("No Image", font, brush, 75, 100);
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

        /// <summary>
        /// Handles the save button click event with form validation and machine update.
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

                // Copy image to proper directory if a new one is selected
                string? finalImagePath = _originalImagePath; // Keep original if no new image
                
                // Check if a new image was selected (different from the original)
                bool hasNewImage = !string.IsNullOrEmpty(_selectedImagePath);
                bool hasOriginalImage = !string.IsNullOrEmpty(_originalImagePath);
                
                if (hasNewImage)
                {
                    // If we have a new image, always copy it
                    finalImagePath = await CopyImageToDirectoryAsync(_selectedImagePath, txtSerialNumber.Text.Trim());
                }
                else if (!hasOriginalImage)
                {
                    // If no new image and no original image, set to null
                    finalImagePath = null;
                }

                // Update the original machine object with new values
                UpdateMachineFromForm(_originalMachine);
                _originalMachine.ImagePath = finalImagePath;
                _originalMachine.UpdatedAt = DateTime.Now;
                
                if (await _machineController.UpdateMachineAsync(_originalMachine))
                {
                    // Machine updated successfully - close form and let parent refresh automatically
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowErrorMessage("Failed to update machine. Please check your input and try again.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error updating machine: {ex.Message}\n\n{ex.StackTrace}");
            }
            finally
            {
                SetLoadingState(btnSave, false);
            }
        }

        /// <summary>
        /// Updates the original machine object with values from the form.
        /// </summary>
        /// <param name="machine">The machine to update.</param>
        private void UpdateMachineFromForm(Machine machine)
        {
            machine.Name = txtName.Text.Trim();
            machine.Description = txtDescription.Text.Trim();
            machine.SerialNumber = txtSerialNumber.Text.Trim();
            machine.Model = txtModel.Text.Trim();
            machine.Manufacturer = txtManufacturer.Text.Trim();
            machine.InstallationDate = dtpInstallationDate.Value;
            machine.Status = (MachineStatus)Enum.Parse(typeof(MachineStatus), cboStatus.Text);
            machine.Location = txtLocation.Text.Trim();
            machine.Department = cboDepartment.Text?.Trim() ?? string.Empty;
            machine.LastMaintenanceDate = _originalMachine.LastMaintenanceDate;
            machine.NextMaintenanceDate = _originalMachine.NextMaintenanceDate;
            machine.MaintenanceIntervalDays = _originalMachine.MaintenanceIntervalDays;
            machine.Notes = txtNotes.Text.Trim();
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
            SetDefaultImage();
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