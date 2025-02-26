using BusinessLogicLayer.Services;
using EntitiesLayer.Entities;
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

//Autor: David Brckan

namespace ChessTracker
{
   
    public partial class LoginWindow : Window
    {
        private readonly UserService _userService;

        public LoginWindow()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = pbPassword.Password;
            string message;

            bool isAuthenticated = _userService.Login(username, password, out message);

            if (isAuthenticated)
            {
                lblErrorMessage.Visibility = Visibility.Collapsed;

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                lblErrorMessage.Content = message;
                lblErrorMessage.Visibility = Visibility.Visible;
            }
        }
    }
}
