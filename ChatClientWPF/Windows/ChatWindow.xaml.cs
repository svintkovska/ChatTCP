using BLL.Models;
using BLL.Services;
using ChatClientWPF.dto;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private UserDTO _userDTO;
        private MessageService _messageService;
        TcpClient client = new TcpClient();
        NetworkStream ns;
        Thread thread;
        ChatMessage _message = new ChatMessage();
        string base64Image;
        public ChatWindow(UserDTO user)
        {
            InitializeComponent();
            _userDTO = user;
            txtUserName.Text = _userDTO.Name;
            _messageService = new MessageService();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileName = "config.txt";
                IPAddress ip;
                int port;
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        ip = IPAddress.Parse(sr.ReadLine());
                        port = int.Parse(sr.ReadLine());
                    }
                }
                _message.UserName = txtUserName.Text;
                _message.UserId = Guid.NewGuid().ToString(); // генерує рандомле значення id
                _message.Photo = base64Image;
                client.Connect(ip, port);
                ns = client.GetStream();
                thread = new Thread(o => ReceiveData((TcpClient)o));
                thread.Start(client);

                bntSend.IsEnabled = true;
                btnConnect.IsEnabled = false;
                _message.Text = "Приєднався в чат";
                var buffer = _message.Serialize();
                ns.Write(buffer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проблема при роботі", ex.Message);
            }
        }

        private void ReceiveData(TcpClient client) //отримуємо дані від сервера
        {
            NetworkStream ns = client.GetStream();
            var receiveBytes = new byte[16054400];
            int byte_count;
            while ((byte_count = ns.Read(receiveBytes)) > 0)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    ChatMessage message = ChatMessage.Desserialize(receiveBytes);


                    var grid = new Grid();
                    for (int i = 0; i < 2; i++)
                    {
                        var colDef = new ColumnDefinition();
                        colDef.Width = GridLength.Auto;
                        grid.ColumnDefinitions.Add(colDef);
                    }
                    BitmapImage bmp = new BitmapImage();
                    string someUrl = $"https://bv012.novakvova.com{message.Photo}";
                    using (var webClient = new WebClient())
                    {
                        byte[] imageBytes = webClient.DownloadData(someUrl);
                        bmp = ChatMessage.ToBitmapImage(imageBytes);
                    }

                    var image = new Image();
                    image.Source = bmp;
                    image.Width = 50;
                    image.Height = 50;
                    var textBlock = new TextBlock();
                    Grid.SetColumn(textBlock, 1);
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.Margin = new Thickness(5, 0, 0, 0);
                    textBlock.Text = message.UserName + " -> " + message.Text;

                    grid.Children.Add(image);
                    grid.Children.Add(textBlock);

                    lbInfo.Items.Add(grid);
                    lbInfo.Items.MoveCurrentToLast();
                    lbInfo.ScrollIntoView(lbInfo.Items.CurrentItem);
                }));
            }
        }

        private void bntSend_Click(object sender, RoutedEventArgs e)
        {
            _message.Text = txtText.Text;
            var buffer = _message.Serialize();
            ns.Write(buffer); //відправляємо на сервер текст
            _messageService.Create(new MessageDTO
            {
                Time = DateTime.Now,
                Text = txtText.Text,
                //UserId = _userDTO.Id,
                User = _userDTO
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _message.Text = "Покинув чат";
            var buffer = _message.Serialize();
            ns.Write(buffer);
            client.Client.Shutdown(SocketShutdown.Send);
            client.Close();
        }
        private void btnPhotoSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            string filePath = dialog.FileName;
            byte[] imageBytes = File.ReadAllBytes(filePath);

            base64Image = Convert.ToBase64String(imageBytes);

            base64Image = UploadImage(base64Image);

        }

        private string UploadImage(string base64)
        {
            string server = "https://bv012.novakvova.com";
            UploadDTO upload = new UploadDTO();
            upload.Photo = base64Image;
            string json = JsonConvert.SerializeObject(upload);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            WebRequest request = WebRequest.Create($"{server}/api/account/upload");
            request.Method = "POST";
            request.ContentType = "application/json";
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);

            }

            try
            {
                var response = request.GetResponse();
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    string data = stream.ReadToEnd();
                    var resp = JsonConvert.DeserializeObject<UploadResponseDTO>(data);
                    btnConnect.IsEnabled = true;
                    btnPhotoSelect.IsEnabled = false;
                    return resp.Image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
            }

            return null;
        }
    }
}
