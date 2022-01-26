using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace RESTfulAPI
{
    /// <summary>
    /// Api_login.xaml 的互動邏輯
    /// </summary>
    public partial class Api_login : Page
    {
        private MainWindow API_host;
        public Api_login(MainWindow mainWindow)
        {
            InitializeComponent();
            API_host = mainWindow;
        }
        private void Send_login(object sender, RoutedEventArgs e)
        {
            Tb_message.Text = "";
            bool CanSend = true;
            if (API_host.Tb_host.Text == "")
            {
                API_host.Row_host.Height = new GridLength(70);
                API_host.Tb_host.Text = "";
                API_host.host_error.Text = "Does not meet rules.";
                API_host.Tb_host.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                API_host.Main.IsEnabled = false;
                CanSend = false;
            }
            if (Tb_user.Text == "")
            {
                Row_user.Height = new GridLength(70);
                Tb_user.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                user_error.Text = "Pleace input User account";
                CanSend = false;
            }
            else
            {
                Row_user.Height = new GridLength(50);
                Tb_user.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
                user_error.Text = "";
            }
            if (Pb_pwd.Password == "")
            {
                Row_password.Height = new GridLength(70);
                Pb_pwd.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                pwd_error.Text = "Pleace input User password";
                CanSend = false;
            }
            else
            {
                Row_password.Height = new GridLength(50);
                Pb_pwd.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
                pwd_error.Text = "";
            }
            if (CanSend)
            {
                HttpClient Client = new HttpClient();
                try
                {
                    Client.BaseAddress = new Uri(API_host.Tb_host.Text);
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", Tb_user.Text),
                        new KeyValuePair<string, string>("password", Pb_pwd.Password)
                    });
                    HttpResponseMessage ResponseMessage = Client.PostAsync("api/login", formContent).Result;
                    var responseJson = ResponseMessage.Content.ReadAsStringAsync().Result;
                    var jObject = JObject.Parse(responseJson);
                    Tb_message.Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102));
                    Tb_message.Text = jObject.ToString();
                    if ((bool)jObject["success"])
                    {
                        API_host.UserName.Content = "Hi " + jObject["data"]["name"].ToString();
                        API_host.token = jObject["data"]["token"].ToString();
                        API_host.UserIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.User;
                        API_host.loginuser.Foreground = new SolidColorBrush(Color.FromRgb(173, 255, 47));
                        API_host.UserName.Foreground = new SolidColorBrush(Color.FromRgb(173, 255, 47));
                    }
                    else
                    {
                        API_host.UserName.Content = "Not login";
                        API_host.UserIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.UserOutline;
                        API_host.token = "";
                        API_host.loginuser.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                        API_host.UserName.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    }
                }
                catch (Exception)
                {
                    API_host.UserName.Content = "Not login";
                    API_host.token = "";
                    API_host.UserIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.UserOutline;
                    API_host.loginuser.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    API_host.UserName.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    Tb_message.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    Tb_message.Text = "Can't connect Web server.";
                }
            }
        }

        private void When_On_focus(object sender, RoutedEventArgs e)
        {
            Tb_message.Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102));
            Tb_message.Text = "";
        }
    }
}
