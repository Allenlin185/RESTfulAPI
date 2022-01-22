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
                user_error.Text = "請輸入User account";
                CanSend = false;
            }
            else
            {
                Row_user.Height = new GridLength(50);
                user_error.Text = "";
            }
            if (Pb_pwd.Password == "")
            {
                Row_password.Height = new GridLength(70);
                pwd_error.Text = "請輸入User password";
                CanSend = false;
            }
            else
            {
                Row_password.Height = new GridLength(50);
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
                    Tb_message.Text = jObject.ToString();
                }
                catch (Exception ex)
                {
                    Tb_message.Text = ex.Message;
                }
            }
        }
    }
}
