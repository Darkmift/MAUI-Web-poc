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
using System.ServiceModel;

namespace DBServiceHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceHost service;
        public MainWindow()
        {
            InitializeComponent();
            service = new ServiceHost(typeof(DBService.Service1));  
            service.Open();
        }
        public void cmdEnd_Click(object sender, RoutedEventArgs e)
        {
            service.Close();
            Application.Current.Shutdown();
        }
    }
}
