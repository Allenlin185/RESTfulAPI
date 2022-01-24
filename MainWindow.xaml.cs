using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace RESTfulAPI
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Api_uri = "/api/login";
        public string token = "";
        private bool CanuseAPI = false;
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Api_login(this);
            Set_main_content("login");
            Tb_host.Focus();
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void show_login_page(object sender, RoutedEventArgs e)
        {
            Api_uri = "/api/login";
            Main.Content = new Api_login(this);
            Set_main_content("login");
        }
        private void show_regist_page(object sender, RoutedEventArgs e)
        {
            Api_uri = "/api/regist";
            Main.Content = new Api_regist(this);
            Set_main_content("regist");
        }
        private void Set_button_style(string ButtonName)
        {
            switch (ButtonName)
            {
                case "regist":
                    Bt_register.Foreground = new SolidColorBrush(Color.FromRgb(173, 255, 47));
                    Bt_signin.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    Bt_result.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    break;
                case "login":
                    Bt_register.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    Bt_signin.Foreground = new SolidColorBrush(Color.FromRgb(173, 255, 47));
                    Bt_result.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    break;
                case "result":
                    Bt_register.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    Bt_signin.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    Bt_result.Foreground = new SolidColorBrush(Color.FromRgb(173, 255, 47));
                    break;
                default:
                    Bt_register.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    Bt_signin.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    Bt_result.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    break;
            }
            Tbl_url.Text = Tb_host.Text + Api_uri;
        }

        private void Set_host_style(object sender, RoutedEventArgs e)
        {
            if (!CanuseAPI)
            {
                Row_host.Height = new GridLength(70);
                host_error.Text = "Does not meet rules.";
                Tb_host.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                Main.IsEnabled = false;
            }
            else
            {
                Row_host.Height = new GridLength(50);
                host_error.Text = "";
                Tb_host.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
                Main.IsEnabled = true;
            }
            Tbl_url.Text = Tb_host.Text + Api_uri;
        }

        private void Check_host_rule(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Regex HostRegex = new Regex(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", RegexOptions.IgnoreCase);
            CanuseAPI = HostRegex.IsMatch(Tb_host.Text);
            Main.IsEnabled = CanuseAPI;
        }
        private void Set_main_content(string functionname)
        {
            Set_button_style(functionname);
            Main.IsEnabled = CanuseAPI;
        }

        private void show_result_page(object sender, RoutedEventArgs e)
        {
            if (token == "") return;
            Api_uri = "/api/processresult";
            Main.Content = new Api_result(this);
            Set_main_content("result");
        }

        private void do_logout(object sender, RoutedEventArgs e)
        {
            if (token == "") return;
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri(Tb_host.Text);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage ResponseMessage = Client.PostAsync("api/logout", null).Result;
            var responseJson = ResponseMessage.Content.ReadAsStringAsync().Result;
            var jObject = JObject.Parse(responseJson);
            if ((bool)jObject["success"])
            {
                UserName.Content = "Not login";
                UserIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.UserOutline;
                token = "";
                loginuser.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                UserName.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                Api_uri = "/api/login";
                Main.Content = new Api_login(this);
                Set_main_content("login");
            }
        }
    }
}
