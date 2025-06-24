namespace goodwin_winForm.Forms
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            lblPin = new Label();
            txtPin = new TextBox();
            btnLogin = new Button();
            btnCancel = new Button();
            lblAttempts = new Label();
            lblRequirements = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblPin
            // 
            lblPin.Font = new Font("Segoe UI", 12F);
            lblPin.Location = new Point(259, 244);
            lblPin.Name = "lblPin";
            lblPin.Size = new Size(200, 35);
            lblPin.TabIndex = 3;
            lblPin.Text = "Enter PIN:";
            lblPin.TextAlign = ContentAlignment.MiddleLeft;
            lblPin.Click += lblPin_Click;
            // 
            // txtPin
            // 
            txtPin.BackColor = Color.White;
            txtPin.Font = new Font("Segoe UI", 14F);
            txtPin.Location = new Point(259, 282);
            txtPin.MaxLength = 10;
            txtPin.Name = "txtPin";
            txtPin.PasswordChar = '*';
            txtPin.Size = new Size(397, 39);
            txtPin.TabIndex = 0;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.White;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.Location = new Point(303, 350);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(140, 55);
            btnLogin.TabIndex = 1;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancel.Location = new Point(485, 350);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(140, 55);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblAttempts
            // 
            lblAttempts.Font = new Font("Segoe UI", 11F);
            lblAttempts.ForeColor = Color.Red;
            lblAttempts.Location = new Point(309, 497);
            lblAttempts.Name = "lblAttempts";
            lblAttempts.Size = new Size(300, 25);
            lblAttempts.TabIndex = 1;
            lblAttempts.Text = "Attempts remaining: 3";
            lblAttempts.TextAlign = ContentAlignment.MiddleCenter;
            lblAttempts.Visible = false;
            // 
            // lblRequirements
            // 
            lblRequirements.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblRequirements.ForeColor = Color.Gray;
            lblRequirements.Location = new Point(304, 449);
            lblRequirements.Name = "lblRequirements";
            lblRequirements.Size = new Size(300, 25);
            lblRequirements.TabIndex = 0;
            lblRequirements.Text = "PIN requirements";
            lblRequirements.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Center;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(250, 102);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(406, 91);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(900, 600);
            Controls.Add(pictureBox1);
            Controls.Add(lblRequirements);
            Controls.Add(lblAttempts);
            Controls.Add(btnCancel);
            Controls.Add(btnLogin);
            Controls.Add(txtPin);
            Controls.Add(lblPin);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(300, 200);
            Name = "LoginForm";
            Load += LoginForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label lblPin;
        private System.Windows.Forms.TextBox txtPin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblAttempts;
        private System.Windows.Forms.Label lblRequirements;
        private PictureBox pictureBox1;
    }
}