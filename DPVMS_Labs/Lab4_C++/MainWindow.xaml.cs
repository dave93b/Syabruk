using System;
using System.Collections.Generic;
using System.IO;
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

namespace Lab4_C__
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var envs = Environment.GetFolderPath(Environment.SpecialFolder.NetworkShortcuts);
            DirectoryInfo directoryInfo=new DirectoryInfo(envs);

            foreach (var dir in directoryInfo.GetDirectories())
            {
                textBoxForResult.Text += dir.Name+Environment.NewLine;
            }
        }
    }
}
