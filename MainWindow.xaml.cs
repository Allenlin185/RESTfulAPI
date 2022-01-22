using System;
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
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Api_login(this);
            Set_button_style("login");
            Tbl_url.Text = Tb_host.Text + Api_uri;
        }
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void show_login_page(object sender, RoutedEventArgs e)
        {
            Api_uri = "/api/login";
            Main.Content = new Api_login(this);
            Set_button_style("login");
            Tbl_url.Text = Tb_host.Text + Api_uri;
            Main.IsEnabled = false;
        }
        private void show_regist_page(object sender, RoutedEventArgs e)
        {
            Api_uri = "/regist";
            Main.Content = new Api_regist();
            Set_button_style("regist");
            Tbl_url.Text = Tb_host.Text + Api_uri;
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
        }

        private void Set_host_style(object sender, RoutedEventArgs e)
        {
            Regex HostRegex = new Regex(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$");
            bool Is_regex = HostRegex.IsMatch(Tb_host.Text);
            if (!Is_regex)
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
    }
}
