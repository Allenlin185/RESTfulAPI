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
    /// Api_result.xaml 的互動邏輯
    /// </summary>
    public partial class Api_result : Page
    {
        private MainWindow API_host;
        public Api_result(MainWindow mainWindow)
        {
            InitializeComponent();
            API_host = mainWindow;
        }
        private void When_On_focus(object sender, RoutedEventArgs e)
        {
            Tb_message.Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102));
            Tb_message.Text = "";
        }

        private void Send_result(object sender, RoutedEventArgs e)
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
            if (Tb_workstation.Text == "")
            {
                Row_workstation.Height = new GridLength(70);
                Tb_workstation.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                workstation_error.Text = "Pleace input workstation.";
                CanSend = false;
            }
            else
            {
                Row_workstation.Height = new GridLength(50);
                Tb_workstation.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
                workstation_error.Text = "";
            }
            if (Tb_wip.Text == "")
            {
                Row_wip.Height = new GridLength(70);
                Tb_wip.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                wip_error.Text = "Pleace input wip.";
                CanSend = false;
            }
            else
            {
                Row_wip.Height = new GridLength(50);
                Tb_wip.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
                wip_error.Text = "";
            }
            if (CanSend)
            {
                HttpClient Client = new HttpClient();
                try
                {
                    Client.BaseAddress = new Uri(API_host.Tb_host.Text);
                    Client.DefaultRequestHeaders.Accept.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", API_host.token);
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("workstation", Tb_workstation.Text),
                        new KeyValuePair<string, string>("wip", Tb_wip.Text),
                    });
                    HttpResponseMessage ResponseMessage = Client.PostAsync("api/processresult", formContent).Result;
                    var responseJson = ResponseMessage.Content.ReadAsStringAsync().Result;
                    var jObject = JObject.Parse(responseJson);
                    Tb_message.Foreground = new SolidColorBrush(Color.FromRgb(102, 102, 102));
                    Tb_message.Text = jObject.ToString();
                }
                catch (Exception)
                {
                    Tb_message.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    Tb_message.Text = "Can't connect Web server.";
                }
            }
        }
    }
}
