using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace JezBox
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

        private async void button_Click(object sender, RoutedEventArgs e) {
            var client = AssetSyncServiceClientFactory.Client;
            bool result;
            try {
                result = await client.IsAlive();
            }
            catch (Exception ex) {
                MessageBox.Show("Exception: " + ex.Message);
                return;
            }

            MessageBox.Show("Result: " + result);
        }
    }
}
