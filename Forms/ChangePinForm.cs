using System;
using System.Windows.Forms;
using goodwin_winForm.Controllers;

namespace goodwin_winForm.Forms
{
    /// <summary>
    /// Touch-optimized form for changing the system PIN code.
    /// Provides a user-friendly interface with touch-friendly controls for PIN management.
    /// </summary>
    public partial class ChangePinForm : BaseForm
    {
        private readonly IAuthController _authController;

        // Form controls
        private Label lblTitle;
        private Label lblCurrentPin;
        private TextBox txtCurrentPin;
        private Label lblNewPin;
        private TextBox txtNewPin;
        private Label lblConfirmPin;
        private TextBox txtConfirmPin;
        private Button btnSave;
        private Button btnCancel;
        private Label lblRequirements;

        /// <summary>
        /// Initializes a new instance of the ChangePinForm with the specified authentication controller.
        /// </summary>
        /// <param name="authController">The authentication controller for PIN operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when authController is null.</exception>
        public ChangePinForm(IAuthController authController)
        {
            _authController = authController ?? throw new ArgumentNullException(nameof(authController));
            InitializeComponent();
            SetupForm();
        }

        /// <summary>
        /// Sets up the form with touch-friendly configuration and PIN requirements.
        /// </summary>
        private void SetupForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
            this.Text = "Change PIN";
            
            // Set PIN requirements text
            lblRequirements.Text = _authController.GetPinRequirements();
            
            // Setup loading button
            SetupLoadingButton(btnSave, "Save");
        }

        /// <summary>
        /// Initializes all form controls with touch-friendly styling and layout.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblCurrentPin = new Label();
            txtCurrentPin = new TextBox();
            lblNewPin = new Label();
            txtNewPin = new TextBox();
            lblConfirmPin = new Label();
            txtConfirmPin = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            lblRequirements = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(50, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 35);
            lblTitle.TabIndex = 8;
            lblTitle.Text = "Change PIN Code";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPin
            // 
            lblCurrentPin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCurrentPin.Location = new Point(50, 90);
            lblCurrentPin.Name = "lblCurrentPin";
            lblCurrentPin.Size = new Size(150, 25);
            lblCurrentPin.TabIndex = 7;
            lblCurrentPin.Text = "Current PIN:";
            // 
            // txtCurrentPin
            // 
            txtCurrentPin.Font = new Font("Segoe UI", 12F);
            txtCurrentPin.Location = new Point(50, 120);
            txtCurrentPin.MaxLength = 10;
            txtCurrentPin.Name = "txtCurrentPin";
            txtCurrentPin.PasswordChar = '*';
            txtCurrentPin.Size = new Size(200, 34);
            txtCurrentPin.TabIndex = 0;
            // 
            // lblNewPin
            // 
            lblNewPin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblNewPin.Location = new Point(50, 180);
            lblNewPin.Name = "lblNewPin";
            lblNewPin.Size = new Size(150, 25);
            lblNewPin.TabIndex = 6;
            lblNewPin.Text = "New PIN:";
            // 
            // txtNewPin
            // 
            txtNewPin.Font = new Font("Segoe UI", 12F);
            txtNewPin.Location = new Point(50, 210);
            txtNewPin.MaxLength = 10;
            txtNewPin.Name = "txtNewPin";
            txtNewPin.PasswordChar = '*';
            txtNewPin.Size = new Size(200, 34);
            txtNewPin.TabIndex = 1;
            // 
            // lblConfirmPin
            // 
            lblConfirmPin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblConfirmPin.Location = new Point(50, 270);
            lblConfirmPin.Name = "lblConfirmPin";
            lblConfirmPin.Size = new Size(150, 25);
            lblConfirmPin.TabIndex = 5;
            lblConfirmPin.Text = "Confirm PIN:";
            // 
            // txtConfirmPin
            // 
            txtConfirmPin.Font = new Font("Segoe UI", 12F);
            txtConfirmPin.Location = new Point(50, 300);
            txtConfirmPin.MaxLength = 10;
            txtConfirmPin.Name = "txtConfirmPin";
            txtConfirmPin.PasswordChar = '*';
            txtConfirmPin.Size = new Size(200, 34);
            txtConfirmPin.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSave.Location = new Point(50, 380);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 40);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(128, 128, 128);
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(170, 380);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 40);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblRequirements
            // 
            lblRequirements.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblRequirements.ForeColor = Color.Gray;
            lblRequirements.Location = new Point(50, 340);
            lblRequirements.Name = "lblRequirements";
            lblRequirements.Size = new Size(300, 30);
            lblRequirements.TabIndex = 0;
            lblRequirements.Text = "PIN requirements";
            lblRequirements.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ChangePinForm
            // 
            ClientSize = new Size(400, 500);
            Controls.Add(lblRequirements);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtConfirmPin);
            Controls.Add(lblConfirmPin);
            Controls.Add(txtNewPin);
            Controls.Add(lblNewPin);
            Controls.Add(txtCurrentPin);
            Controls.Add(lblCurrentPin);
            Controls.Add(lblTitle);
            Name = "ChangePinForm";
            ResumeLayout(false);
            PerformLayout();
        }

        /// <summary>
        /// Handles the save button click event with PIN validation and change.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                SetLoadingState(btnSave, true, "Saving...");

                if (await _authController.ChangePinAsync(txtCurrentPin.Text.Trim(), txtNewPin.Text.Trim()))
                {
                    ShowSuccessMessage("PIN changed successfully!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ShowErrorMessage("Failed to change PIN. Please verify your current PIN is correct.");
                    txtCurrentPin.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error changing PIN: {ex.Message}");
            }
            finally
            {
                SetLoadingState(btnSave, false);
            }
        }

        /// <summary>
        /// Validates all input fields according to business rules.
        /// </summary>
        /// <returns>True if all validations pass; otherwise, false.</returns>
        private bool ValidateInputs()
        {
            if (!ValidateRequiredField(txtCurrentPin, "current PIN"))
                return false;

            if (!ValidateRequiredField(txtNewPin, "new PIN"))
                return false;

            if (txtNewPin.Text.Trim() != txtConfirmPin.Text.Trim())
            {
                ShowValidationError("New PIN and confirmation PIN do not match.", txtConfirmPin);
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