using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using goodwin_winForm.Services;

namespace goodwin_winForm.Forms
{
    /// <summary>
    /// Form for user authentication using PIN.
    /// Provides a secure login interface with PIN validation.
    /// </summary>
    public partial class LoginForm : Form
    {
        private readonly IPinService _pinService;

        /// <summary>
        /// Initializes a new instance of the LoginForm with the specified PIN service.
        /// </summary>
        /// <param name="pinService">The PIN service for PIN validation and requirements.</param>
        /// <exception cref="ArgumentNullException">Thrown when pinService is null.</exception>
        public LoginForm(IPinService pinService)
        {
            InitializeComponent();
            _pinService = pinService ?? throw new ArgumentNullException(nameof(pinService));
        }

        /// <summary>
        /// Handles the form load event to check if PIN is set and configure the form accordingly.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private async void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                bool isPinSet = await _pinService.IsPinSetAsync();
                
                if (!isPinSet)
                {
                    // First time setup - show initial PIN setup
                    lblPin.Text = "Enter PIN (min 4 characters):";
                    btnLogin.Text = "Set PIN";
                    txtPin.PasswordChar = '*';
                    txtPin.MaxLength = 10;
                }
                else
                {
                    // Normal login
                    lblPin.Text = "Enter PIN:";
                    btnLogin.Text = "Login";
                    txtPin.PasswordChar = '*';
                    txtPin.MaxLength = 10;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing login form: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the login button click event to validate PIN or set initial PIN.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPin.Text))
            {
                MessageBox.Show("Please enter a PIN.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Processing...";

            try
            {
                bool isPinSet = await _pinService.IsPinSetAsync();
                
                if (!isPinSet)
                {
                    // First time setup
                    if (txtPin.Text.Length < 4)
                    {
                        MessageBox.Show("PIN must be at least 4 characters long.", "Validation Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnLogin.Enabled = true;
                        btnLogin.Text = "Set PIN";
                        return;
                    }

                    await _pinService.SetInitialPinAsync(txtPin.Text.Trim());
                    MessageBox.Show("Initial PIN set successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    // Normal login
                    if (await _pinService.ValidatePinAsync(txtPin.Text.Trim()))
                    {
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Invalid PIN. Please try again.", "Authentication Failed", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPin.Clear();
                        txtPin.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during authentication: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                bool isPinSet = await _pinService.IsPinSetAsync();
                btnLogin.Text = isPinSet ? "Login" : "Set PIN";
            }
        }

        /// <summary>
        /// Handles the cancel button click event to close the form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Handles the key press event on the PIN text box to allow login on Enter key.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnLogin_Click(sender, e);
            }
        }

        /// <summary>
        /// Handles the PIN label click event to focus the PIN text box.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void lblPin_Click(object sender, EventArgs e)
        {
            txtPin.Focus();
        }
    }
}
