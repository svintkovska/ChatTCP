using BLL.Models;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatClientWPF.Windows
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        private UserService _userService;
        public LogInWindow()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void logInBtn_Click(object sender, RoutedEventArgs e)
        {
            UserDTO tempUser = _userService.SearchUser(email_tb.Text, password_tb.Text);
            if (tempUser != null)
            {
                ChatWindow chatWindow = new ChatWindow(tempUser);
                chatWindow.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect login or password");
            }
        }
    }
}
