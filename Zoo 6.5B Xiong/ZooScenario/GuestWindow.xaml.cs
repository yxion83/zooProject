using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using People;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for GuestWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class GuestWindow : Window
    {
        /// <summary>
        /// The Guest in the zoo.
        /// </summary>
        private Guest guest;

        /// <summary>
        /// Initializes a new instance of the GuestWindow class.
        /// </summary>
        /// <param name="guest">Guest from the zoo.</param>
        public GuestWindow(Guest guest)
        {
            this.guest = guest;
            this.InitializeComponent();
        }

        /// <summary>
        /// Method to load the guest Window.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            // Assigns text input to the textbox.
            this.nameTextBox.Text = this.guest.Name.ToString();
            this.ageTextBox.Text = this.guest.Age.ToString();

            // Gives list of genders available in gendercombobox.
            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.genderComboBox.SelectedItem = this.guest.Gender;

            // Gives list of wallet color available in walletcombobox.
            this.walletColorComboBox.ItemsSource = Enum.GetValues(typeof(WalletColor));
            this.walletColorComboBox.SelectedItem = this.guest.Wallet.Color;

            // Assigns values to the money balances labels and combobox.
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
            this.moneyAmountComboBox.Items.Add(1);
            this.moneyAmountComboBox.Items.Add(5);
            this.moneyAmountComboBox.Items.Add(10);
            this.moneyAmountComboBox.Items.Add(20);
            this.moneyAmountComboBox.SelectedItem = this.moneyAmountComboBox.Items[0];

            // Assigns values to the account balance labels and combobox.
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
            this.accountComboBox.Items.Add(1);
            this.accountComboBox.Items.Add(5);
            this.accountComboBox.Items.Add(10);
            this.accountComboBox.Items.Add(20);
            this.accountComboBox.Items.Add(50);
            this.accountComboBox.Items.Add(100);
            this.accountComboBox.SelectedItem = this.accountComboBox.Items[0];
        }

        /// <summary>
        /// Button to confirm.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// Updates name value in text box.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Name = this.nameTextBox.Text;
            }
            catch (FormatException)
            {
                MessageBox.Show("Names must only contain letters and spaces.");
            }
        }

        /// <summary>
        /// Updates age value in textbox.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void ageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Age = int.Parse(this.ageTextBox.Text);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Ages must be between 0 and 120.");
            }
        }

        /// <summary>
        /// Selects the guest's gender.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Sets the guest gender to the selected gender.
            this.guest.Gender = (Gender)this.genderComboBox.SelectedItem;
        }

        /// <summary>
        /// Selects the guest's wallet color.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void walletColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Sets the guest wallet color to the selected color.
            this.guest.Wallet.Color = (WalletColor)this.walletColorComboBox.SelectedItem;
        }

        /// <summary>
        /// Adds money to the guest's wallet.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void addMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            // Adds money to guest wallet and assigns new balance.
            this.guest.Wallet.AddMoney(decimal.Parse(this.moneyAmountComboBox.Text));
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Removes money from the guest's wallet.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void subtractMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            // Removes money from guest wallet and assigns new balance.
            this.guest.Wallet.RemoveMoney(decimal.Parse(this.moneyAmountComboBox.Text));
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Adds money to guest account.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void addAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.CheckingAccount.AddMoney(decimal.Parse(this.accountComboBox.Text));
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }

        /// <summary>
        /// Removes money from guest account.
        /// </summary>
        /// <param name="sender">The object that initiated the event.</param>
        /// <param name="e">The event arguments for the event.</param>
        private void subtractAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.CheckingAccount.RemoveMoney(decimal.Parse(this.accountComboBox.Text));
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }
    }
}
