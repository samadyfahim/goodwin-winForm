using System;
using System.Windows.Forms;
using goodwin_winForm.Models;
using goodwin_winForm.Controllers;

namespace goodwin_winForm.Forms
{
    /// <summary>
    /// Touch-optimized form for adding new maintenance records to machines.
    /// Provides a user-friendly interface with touch-friendly controls for maintenance data entry.
    /// </summary>
    public partial class AddMaintenanceForm : BaseForm
    {
        private readonly IMaintenanceController _maintenanceController;
        private readonly int _machineId;
        private readonly string _machineName;

        /// <summary>
        /// Initializes a new instance of the AddMaintenanceForm with the specified maintenance controller and machine information.
        /// </summary>
        /// <param name="maintenanceController">The maintenance controller for data operations.</param>
        /// <param name="machineId">The ID of the machine to add maintenance for.</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <exception cref="ArgumentNullException">Thrown when maintenanceController is null.</exception>
        public AddMaintenanceForm(IMaintenanceController maintenanceController, int machineId, string machineName)
        {
            _maintenanceController = maintenanceController ?? throw new ArgumentNullException(nameof(maintenanceController));
            _machineId = machineId;
            _machineName = machineName ?? throw new ArgumentNullException(nameof(machineName));
            InitializeComponent();
            InitializeComboBoxes();
            SetupForm();
        }

        /// <summary>
        /// Sets up the form with touch-friendly configuration and control setup.
        /// </summary>
        private void SetupForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
            this.Text = $"Add Maintenance Record - {_machineName}";
            
            // Setup loading button
            SetupLoadingButton(btnSave, "Save");
            
            // Set default values
            dtpMaintenanceDate.Value = DateTime.Today;
            dtpCompletedDate.Value = DateTime.Today;
            cboStatus.SelectedIndex = 0; // Default to Scheduled
        }

        /// <summary>
        /// Initializes combo boxes with maintenance type and status options.
        /// </summary>
        private void InitializeComboBoxes()
        {
            // Initialize Type ComboBox
            cboType.Items.Clear();
            cboType.Items.AddRange(Enum.GetNames(typeof(MaintenanceType)));
            cboType.SelectedIndex = 0; // Default to Preventive

            // Initialize Status ComboBox
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(Enum.GetNames(typeof(MaintenanceStatus)));
            cboStatus.SelectedIndex = 0; // Default to Scheduled
        }

        /// <summary>
        /// Handles the save button click event with form validation and maintenance record creation.
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

                var maintenanceRecord = CreateMaintenanceRecordFromForm();
                
                if (await _maintenanceController.AddMaintenanceRecordAsync(maintenanceRecord))
                {
                    ShowSuccessMessage("Maintenance record added successfully!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowErrorMessage("Failed to add maintenance record. Please check your input and try again.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error adding maintenance record: {ex.Message}");
            }
            finally
            {
                SetLoadingState(btnSave, false);
            }
        }

        /// <summary>
        /// Creates a MaintenanceRecord object from the form data.
        /// </summary>
        /// <returns>A new MaintenanceRecord object populated with form data.</returns>
        private MaintenanceRecord CreateMaintenanceRecordFromForm()
        {
            return new MaintenanceRecord
            {
                MachineId = _machineId,
                Type = (MaintenanceType)Enum.Parse(typeof(MaintenanceType), cboType.Text),
                MaintenanceDate = dtpMaintenanceDate.Value,
                Title = txtTitle.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                PerformedBy = txtPerformedBy.Text.Trim(),
                Cost = decimal.TryParse(txtCost.Text, out var cost) ? cost : 0,
                PartsUsed = txtPartsUsed.Text.Trim(),
                Status = (MaintenanceStatus)Enum.Parse(typeof(MaintenanceStatus), cboStatus.Text),
                CompletedDate = cboStatus.Text == "Completed" ? dtpCompletedDate.Value : null,
                Notes = txtNotes.Text.Trim()
            };
        }

        /// <summary>
        /// Validates all form fields according to business rules.
        /// </summary>
        /// <returns>True if all validations pass; otherwise, false.</returns>
        private bool ValidateForm()
        {
            if (!ValidateRequiredField(txtTitle, "title"))
                return false;

            if (!ValidateRequiredField(txtPerformedBy, "performed by"))
                return false;

            if (!ValidateDateField(dtpMaintenanceDate, "Maintenance date"))
                return false;

            if (cboStatus.Text == "Completed" && !ValidateDateField(dtpCompletedDate, "Completed date"))
                return false;

            if (!string.IsNullOrEmpty(txtCost.Text) && !decimal.TryParse(txtCost.Text, out _))
            {
                ShowErrorMessage("Please enter a valid cost amount.");
                txtCost.Focus();
                return false;
            }

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
    }
} 