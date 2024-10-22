using System.Windows;
using System.Windows.Controls;

namespace Blockchain_Tool
{
    public partial class WalletDashboardPage : Page
    {
        public WalletDashboardPage()
        {
            InitializeComponent();
        }

        // Event handler for Home button click
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the MainWindow (Home)
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Close the current window where the Page is hosted
            Window.GetWindow(this).Close();
        }

        // Event handler for Wallet Dashboard button click (optional if needed)
        private void WalletDashboardButton_Click(object sender, RoutedEventArgs e)
        {
            // Reload the WalletDashboardPage if needed
            var walletDashboardPage = new WalletDashboardPage();
            this.NavigationService.Navigate(walletDashboardPage);
        }
    }
}
