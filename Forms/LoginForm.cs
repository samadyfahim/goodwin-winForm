using System;
using System.Windows.Forms;
using goodwin_winForm.Controllers;
using System.Threading.Tasks;

namespace goodwin_winForm.Forms
{
    /// <summary>
    /// Touch-optimized login form for the Machine Management System.
    /// Provides PIN-based authentication with touch-friendly controls and layout.
    /// </summary>
    public partial class LoginForm : BaseForm
    {
        private readonly IAuthController _authController;
        private int maxAttempts = 3;
        private int currentAttempts = 0;

        /// <summary>
        /// Initializes a new instance of the LoginForm with the specified authentication controller.
        /// </summary>
        /// <param name="authController">The authentication controller for PIN validation.</param>
        /// <exception cref="ArgumentNullException">Thrown when authController is null.</exception>
        public LoginForm(IAuthController authController)
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
            this.AcceptButton = btnLogin;
            this.CancelButton = btnCancel;
            this.Text = "Login - Machine Management System";

            // Touch-friendly form size
            this.ClientSize = new System.Drawing.Size(900, 600);

            // Set PIN requirements text
            lblRequirements.Text = _authController.GetPinRequirements();

            // Setup loading button
            SetupLoadingButton(btnLogin, "Login");
        }

        /// <summary>
        /// Handles the login button click event with PIN validation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidateRequiredField(txtPin, "PIN code"))
                return;

            try
            {
                SetLoadingState(btnLogin, true, "Validating...");

                // Disable form during validation
                this.Enabled = false;

                // Use Task.Run to avoid blocking the UI thread
                var isValid = await Task.Run(async () =>
                    await _authController.ValidatePinAsync(txtPin.Text.Trim()));

                if (isValid)
                {
                    SetLoadingState(btnLogin, true, "Loading...");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    HandleFailedLogin();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error during authentication: {ex.Message}");
            }
            finally
            {
                SetLoadingState(btnLogin, false);
                this.Enabled = true;
            }
        }

        /// <summary>
        /// Handles failed login attempts with attempt counting and user feedback.
        /// </summary>
        private void HandleFailedLogin()
        {
            currentAttempts++;
            txtPin.Clear();
            txtPin.Focus();

            if (currentAttempts >= maxAttempts)
            {
                ShowErrorMessage("Maximum login attempts exceeded. Application will close.");
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                UpdateAttemptsDisplay();
                ShowInfoMessage($"Invalid PIN. {maxAttempts - currentAttempts} attempts remaining.");
            }
        }

        /// <summary>
        /// Updates the attempts display to show remaining login attempts.
        /// </summary>
        private void UpdateAttemptsDisplay()
        {
            lblAttempts.Text = $"Attempts remaining: {maxAttempts - currentAttempts}";
            lblAttempts.Visible = true;
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
        /// Handles the PIN label click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void lblPin_Click(object sender, EventArgs e)
        {
            // Focus on the PIN text box when the label is clicked
            txtPin.Focus();
        }

        /// <summary>
        /// Handles the form load event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Set focus to the PIN text box when the form loads
            txtPin.Focus();
        }

       
    }
}
