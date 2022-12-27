using BLL.Models;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserService _userService;
        public RegisterWindow()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (passwordTB.Password == confirmPassTB.Password)
            {
                UserDTO tempUser = new UserDTO()
                {
                    Name = nameTB.Text,
                    Email = emailTB.Text,
                    Password = MD5Encryptor.EncryptPassword(passwordTB.Password, "my salt")
                };
                _userService.Create(tempUser);
                this.Close();
                ChatWindow chatWindow = new ChatWindow(tempUser);
                chatWindow.ShowDialog();
            }
            else
                MessageBox.Show("Error with password confirmation");
        }
        private readonly Regex validateEmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (String.IsNullOrEmpty(nameTB.Text))
                registerBtn.IsEnabled = false;
            else if (String.IsNullOrEmpty(emailTB.Text))
                registerBtn.IsEnabled = false;
            else if (!validateEmailRegex.IsMatch(emailTB.Text))
                registerBtn.IsEnabled = false;
            else if (String.IsNullOrEmpty(passwordTB.Password))
                registerBtn.IsEnabled = false;
            else if (String.IsNullOrEmpty(confirmPassTB.Password))
                registerBtn.IsEnabled = false;
            else
                registerBtn.IsEnabled = true;
        }
    }
}
