using System;
using System.IO;
using System.Windows;

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
