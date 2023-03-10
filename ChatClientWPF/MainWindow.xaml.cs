using ChatClientWPF.dto;
using ChatClientWPF.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogIn_btn_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.ShowDialog();
        }

        private void register_btn_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }
    }
}
