using System;
using System.Windows.Forms;
using goodwin_winForm.Models;
using goodwin_winForm.Controllers;

namespace goodwin_winForm.Forms
{
    /// <summary>
    /// Touch-optimized form for adding new alerts to machines.
    /// Provides a user-friendly interface with touch-friendly controls for alert data entry.
    /// </summary>
    public partial class AddAlertForm : BaseForm
    {
        private readonly IAlertController _alertController;
        private readonly int _machineId;
        private readonly string _machineName;

        /// <summary>
        /// Initializes a new instance of the AddAlertForm with the specified alert controller and machine information.
        /// </summary>
        /// <param name="alertController">The alert controller for data operations.</param>
        /// <param name="machineId">The ID of the machine to add alert for.</param>
        /// <param name="machineName">The name of the machine.</param>
        /// <exception cref="ArgumentNullException">Thrown when alertController is null.</exception>
        public AddAlertForm(IAlertController alertController, int machineId, string machineName)
        {
            _alertController = alertController ?? throw new ArgumentNullException(nameof(alertController));
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
            this.Text = $"Add Alert - {_machineName}";
            
            // Setup loading button
            SetupLoadingButton(btnSave, "Save");
            
            // Set default values
            cboType.SelectedIndex = 0; // Default to MaintenanceDue
            cboSeverity.SelectedIndex = 1; // Default to Medium
            cboStatus.SelectedIndex = 0; // Default to Active
        }

        /// <summary>
        /// Initializes combo boxes with alert type, severity, and status options.
        /// </summary>
        private void InitializeComboBoxes()
        {
            // Initialize Type ComboBox
            cboType.Items.Clear();
            cboType.Items.AddRange(Enum.GetNames(typeof(AlertType)));
            cboType.SelectedIndex = 0; // Default to MaintenanceDue

            // Initialize Severity ComboBox
            cboSeverity.Items.Clear();
            cboSeverity.Items.AddRange(Enum.GetNames(typeof(AlertSeverity)));
            cboSeverity.SelectedIndex = 1; // Default to Medium

            // Initialize Status ComboBox
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(Enum.GetNames(typeof(AlertStatus)));
            cboStatus.SelectedIndex = 0; // Default to Active
        }

        /// <summary>
        /// Handles the save button click event with form validation and alert creation.
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

                var alert = CreateAlertFromForm();
                
                if (await _alertController.AddAlertAsync(alert))
                {
                    ShowSuccessMessage("Alert added successfully!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowErrorMessage("Failed to add alert. Please check your input and try again.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error adding alert: {ex.Message}");
            }
            finally
            {
                SetLoadingState(btnSave, false);
            }
        }

        /// <summary>
        /// Creates an Alert object from the form data.
        /// </summary>
        /// <returns>A new Alert object populated with form data.</returns>
        private Alert CreateAlertFromForm()
        {
            return new Alert
            {
                MachineId = _machineId,
                Type = (AlertType)Enum.Parse(typeof(AlertType), cboType.Text),
                Severity = (AlertSeverity)Enum.Parse(typeof(AlertSeverity), cboSeverity.Text),
                Title = txtTitle.Text.Trim(),
                Message = txtMessage.Text.Trim(),
                Status = (AlertStatus)Enum.Parse(typeof(AlertStatus), cboStatus.Text),
                CreatedDate = DateTime.Now
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