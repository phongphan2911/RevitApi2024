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


namespace RevitApi2024.DemoWpf
{
    /// <summary>
    /// Interaction logic for ComboBoxWpf.xaml
    /// </summary>
    public partial class ComboBoxWpf : Window
    {
        public ComboBoxWpf()
        {
            InitializeComponent();
        }

        private void btnCreateDuct(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
