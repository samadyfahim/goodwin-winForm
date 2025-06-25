using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using goodwin_winForm.Models;
using goodwin_winForm.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace goodwin_winForm.Forms
{
    /// <summary>
    /// Touch-optimized main form for machine selection and management.
    /// Provides a user-friendly interface with touch-friendly controls for viewing and managing machines.
    /// </summary>
    public partial class SelectMachineForm : BaseForm
    {
        private readonly IMachineController _machineController;
        private readonly IAuthController _authController;
        private List<Machine> _machines = new List<Machine>();
        private List<MachineCard> _machineCards = new List<MachineCard>();
        private MachineCard _selectedCard = null;

        /// <summary>
        /// Initializes a new instance of the SelectMachineForm with the specified controllers.
        /// </summary>
        /// <param name="machineController">The machine controller for data operations.</param>
        /// <param name="authController">The authentication controller for PIN management.</param>
        /// <exception cref="ArgumentNullException">Thrown when either controller is null.</exception>
        public SelectMachineForm(IMachineController machineController, IAuthController authController)
        {
            _machineController = machineController ?? throw new ArgumentNullException(nameof(machineController));
            _authController = authController ?? throw new ArgumentNullException(nameof(authController));
            InitializeComponent();
            SetupForm();
            LoadMachines();
        }

        /// <summary>
        /// Sets up the form with touch-friendly configuration and menu setup.
        /// </summary>
        private void SetupForm()
        {
            this.Text = "Machine Management System";

            // Touch-friendly form size
            this.ClientSize = new System.Drawing.Size(1000, 700);

            // Setup loading button
            SetupLoadingButton(btnRefresh, "Refresh");

            // Setup flow layout panel for cards
            SetupFlowLayoutPanel();

            // Setup settings button
            SetupSettingsButton();
        }

        /// <summary>
        /// Sets up the flow layout panel for displaying machine cards.
        /// </summary>
        private void SetupFlowLayoutPanel()
        {
            flowLayoutPanelMachines.AutoScroll = true;
            flowLayoutPanelMachines.WrapContents = true;
            flowLayoutPanelMachines.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanelMachines.Padding = new Padding(10);
        }

        /// <summary>
        /// Loads machines from the database asynchronously with loading state management.
        /// </summary>
        private async void LoadMachines()
        {
            try
            {
                SetLoadingState(btnRefresh, true, "Loading...");
                _machines = await _machineController.GetAllMachinesAsync();
                RefreshMachineCards();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading machines: {ex.Message}");
            }
            finally
            {
                SetLoadingState(btnRefresh, false);
            }
        }

        /// <summary>
        /// Refreshes the machine cards display with current data.
        /// </summary>
        private void RefreshMachineCards()
        {
            // Clear existing cards
            flowLayoutPanelMachines.Controls.Clear();
            _machineCards.Clear();
            _selectedCard = null;

            // Create new cards for each machine
            foreach (var machine in _machines)
            {
                var card = new MachineCard(machine);
                card.MachineSelected += OnMachineCardSelected;
                _machineCards.Add(card);
                flowLayoutPanelMachines.Controls.Add(card);
            }
        }

        /// <summary>
        /// Handles machine card selection events.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="machine">The selected machine.</param>
        private void OnMachineCardSelected(object sender, Machine machine)
        {
            // Deselect previously selected card
            if (_selectedCard != null)
            {
                _selectedCard.IsSelected = false;
            }

            // Select new card
            _selectedCard = sender as MachineCard;
            if (_selectedCard != null)
            {
                _selectedCard.IsSelected = true;
            }
        }

        /// <summary>
        /// Gets the currently selected machine from the cards.
        /// </summary>
        /// <returns>The selected machine or null if none selected.</returns>
        private Machine GetSelectedMachine()
        {
            return _selectedCard?.Machine;
        }

        /// <summary>
        /// Handles the add machine button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnAddMachine_Click(object sender, EventArgs e)
        {
            var addMachineForm = new AddMachineForm(_machineController);
            if (addMachineForm.ShowDialog() == DialogResult.OK)
            {
                LoadMachines(); // Refresh the list after adding
            }
        }

        /// <summary>
        /// Handles the edit machine button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnEditMachine_Click(object sender, EventArgs e)
        {
            var selectedMachine = GetSelectedMachine();
            if (selectedMachine == null)
            {
                ShowInfoMessage("Please select a machine to edit");
                return;
            }

            // TODO: Open Edit Machine Form
            ShowInfoMessage($"Edit Machine: {selectedMachine.Name}");
        }

        /// <summary>
        /// Handles the view details button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            var selectedMachine = GetSelectedMachine();
            if (selectedMachine == null)
            {
                ShowInfoMessage("Please select a machine to view details");
                return;
            }

            // Create a service provider scope for the details form
            using var scope = Program.CreateHostBuilder().Build().Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var detailsForm = new MachineDetailsForm(serviceProvider, selectedMachine);
            detailsForm.ShowDialog();
        }

        /// <summary>
        /// Handles the refresh button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMachines();
        }

        /// <summary>
        /// Handles the settings button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var changePinForm = new ChangePinForm(_authController);
                changePinForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error opening settings: {ex.Message}");
            }
        }

        /// <summary>
        /// Sets up the settings button with interactive features.
        /// </summary>
        private void SetupSettingsButton()
        {
            if (btnSettings != null)
            {
                // Make the settings button interactive
                btnSettings.Cursor = Cursors.Hand;
                btnSettings.BackColor = Color.FromArgb(243, 244, 246); // Match form background
                btnSettings.ForeColor = Color.FromArgb(75, 85, 99); // Gray color for the icon

                // Add hover effects
                btnSettings.MouseEnter += (sender, e) =>
                {
                    btnSettings.BackColor = Color.FromArgb(220, 221, 225); // Slightly darker on hover
                    btnSettings.ForeColor = Color.FromArgb(55, 65, 81); // Darker gray on hover
                };

                btnSettings.MouseLeave += (sender, e) =>
                {
                    btnSettings.BackColor = Color.FromArgb(243, 244, 246); // Return to form background
                    btnSettings.ForeColor = Color.FromArgb(75, 85, 99); // Return to original gray
                };
            }
        }

     
    }
}
