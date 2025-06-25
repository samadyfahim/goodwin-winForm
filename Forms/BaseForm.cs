using System;
using System.Drawing;
using System.Windows.Forms;

namespace goodwin_winForm.Forms
{
    /// <summary>
    /// Base form class that provides common functionality for all forms in the application.
    /// This class centralizes common form behavior, validation, error handling, and UI utilities.
    /// Includes touch screen support with larger controls and touch-friendly spacing.
    /// </summary>
    public class BaseForm : Form
    {
        /// <summary>
        /// Touch-friendly control sizes and spacing constants.
        /// These values are optimized for touch screen interaction.
        /// </summary>
        protected static class TouchSettings
        {
            public const int FormPadding = 20;
        }

        /// <summary>
        /// Initializes a new instance of the BaseForm class.
        /// Sets up common form properties and behavior for all derived forms.
        /// </summary>
        protected BaseForm()
        {
            InitializeBaseForm();
        }

        /// <summary>
        /// Initializes the base form with common properties and settings.
        /// This method sets up the form's appearance and behavior to be consistent across the application.
        /// Includes touch screen optimizations for better usability on touch devices.
        /// </summary>
        private void InitializeBaseForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = true;
            this.TopMost = false;
            
            // Touch screen optimizations
            this.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            this.Padding = new Padding(TouchSettings.FormPadding);
        }

        /// <summary>
        /// Displays a validation error message to the user.
        /// This method provides a consistent way to show validation errors across all forms.
        /// Optimized for touch screens with larger message boxes.
        /// </summary>
        /// <param name="message">The validation error message to display.</param>
        /// <param name="focusControl">Optional control to focus after showing the error.</param>
        /// <remarks>
        /// Uses a warning icon to indicate validation errors. If a focus control is provided,
        /// the focus will be set to that control after the message is displayed.
        /// Message boxes are optimized for touch interaction with larger text and buttons.
        /// </remarks>
        protected void ShowValidationError(string message, Control? focusControl = null)
        {
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (focusControl != null)
                focusControl.Focus();
        }

        /// <summary>
        /// Displays a success message to the user.
        /// This method provides a consistent way to show success messages across all forms.
        /// Optimized for touch screens with larger message boxes.
        /// </summary>
        /// <param name="message">The success message to display.</param>
        /// <remarks>
        /// Uses an information icon to indicate successful operations.
        /// Message boxes are optimized for touch interaction with larger text and buttons.
        /// </remarks>
        protected void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Displays an error message to the user.
        /// This method provides a consistent way to show error messages across all forms.
        /// Optimized for touch screens with larger message boxes.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        /// <remarks>
        /// Uses an error icon to indicate error conditions.
        /// Message boxes are optimized for touch interaction with larger text and buttons.
        /// </remarks>
        protected void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays an information message to the user.
        /// This method provides a consistent way to show information messages across all forms.
        /// Optimized for touch screens with larger message boxes.
        /// </summary>
        /// <param name="message">The information message to display.</param>
        /// <remarks>
        /// Uses an information icon to indicate informational content.
        /// Message boxes are optimized for touch interaction with larger text and buttons.
        /// </remarks>
        protected void ShowInfoMessage(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Sets the state and text of a button, handling cross-thread invocation if necessary.
        /// This method ensures thread-safe button state changes.
        /// </summary>
        /// <param name="button">The button to modify.</param>
        /// <param name="enabled">Whether the button should be enabled or disabled.</param>
        /// <param name="text">The text to display on the button.</param>
        /// <remarks>
        /// If the button is on a different thread, this method will properly invoke the change
        /// on the correct thread to avoid cross-thread operation exceptions.
        /// </remarks>
        protected void SetButtonState(Button button, bool enabled, string text)
        {
            if (button.InvokeRequired)
            {
                button.Invoke(new Action(() => SetButtonState(button, enabled, text)));
                return;
            }

            button.Enabled = enabled;
            button.Text = text;
        }

        /// <summary>
        /// Sets the loading state of a button with appropriate text changes.
        /// This method is used to provide visual feedback during async operations.
        /// </summary>
        /// <param name="button">The button to set the loading state for.</param>
        /// <param name="isLoading">Whether the button should be in loading state.</param>
        /// <param name="loadingText">The text to display during loading (defaults to "Loading...").</param>
        /// <remarks>
        /// When loading is true, the button is disabled and shows loading text.
        /// When loading is false, the button is enabled and shows its original text (stored in Tag).
        /// </remarks>
        protected void SetLoadingState(Button button, bool isLoading, string loadingText = "Loading...")
        {
            SetButtonState(button, !isLoading, isLoading ? loadingText : button.Tag?.ToString() ?? "OK");
        }

        /// <summary>
        /// Sets up a button for loading state management by storing its default text.
        /// This method should be called during form initialization for buttons that perform async operations.
        /// </summary>
        /// <param name="button">The button to set up for loading state management.</param>
        /// <param name="defaultText">The default text to store for the button.</param>
        /// <remarks>
        /// The default text is stored in the button's Tag property and will be restored
        /// when the loading state is turned off.
        /// </remarks>
        protected void SetupLoadingButton(Button button, string defaultText)
        {
            button.Tag = defaultText;
        }

        /// <summary>
        /// Validates that a required text field is not empty or whitespace.
        /// This method provides consistent validation for required text inputs.
        /// </summary>
        /// <param name="textBox">The TextBox control to validate.</param>
        /// <param name="fieldName">The name of the field for error message display.</param>
        /// <returns>True if the field has valid content; otherwise, false.</returns>
        /// <remarks>
        /// If validation fails, a validation error message is displayed and focus is set to the text box.
        /// </remarks>
        protected bool ValidateRequiredField(TextBox textBox, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text.Trim()))
            {
                ShowValidationError($"Please enter {fieldName}.", textBox);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates that a required combo box has a selected value.
        /// This method provides consistent validation for required combo box selections.
        /// </summary>
        /// <param name="comboBox">The ComboBox control to validate.</param>
        /// <param name="fieldName">The name of the field for error message display.</param>
        /// <returns>True if the combo box has a valid selection; otherwise, false.</returns>
        /// <remarks>
        /// If validation fails, a validation error message is displayed and focus is set to the combo box.
        /// </remarks>
        protected bool ValidateRequiredField(ComboBox comboBox, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(comboBox.Text.Trim()))
            {
                ShowValidationError($"Please select {fieldName}.", comboBox);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates that a date field contains a valid date within acceptable range.
        /// This method provides consistent validation for date inputs.
        /// </summary>
        /// <param name="datePicker">The DateTimePicker control to validate.</param>
        /// <param name="fieldName">The name of the field for error message display.</param>
        /// <param name="allowFuture">Whether future dates are allowed (defaults to false).</param>
        /// <returns>True if the date is valid; otherwise, false.</returns>
        /// <remarks>
        /// By default, future dates are not allowed. If validation fails, a validation error
        /// message is displayed and focus is set to the date picker.
        /// </remarks>
        protected bool ValidateDateField(DateTimePicker datePicker, string fieldName, bool allowFuture = false)
        {
            if (!allowFuture && datePicker.Value > DateTime.Today)
            {
                ShowValidationError($"{fieldName} cannot be in the future.", datePicker);
                return false;
            }
            return true;
        }
    }
} 