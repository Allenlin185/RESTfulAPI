using System.Windows;
using System.Windows.Controls;

namespace RESTfulAPI
{
    /// <summary>
    /// Api_regist_page.xaml 的互動邏輯
    /// </summary>
    public partial class Api_regist : Page
    {
        public Api_regist()
        {
            InitializeComponent();
        }

        private void Hots_LostFocus(object sender, RoutedEventArgs e)
        {
            Lb_url.Content = Tb_host.Text + "/regist";
        }
    }
}
