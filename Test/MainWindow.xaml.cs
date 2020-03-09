using System;
using System.Collections.Generic;
using System.Windows;

using Test.BL;

namespace Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public string Path { get; set; } = @"C:\Users\User\Desktop\торговая сеть сент 2017.csv";
        public List<IModelFile> Csv { get; set; }
        public List<IModelFile> Xlsx { get; set; }
        public List<IModelFile> Difference { get; set; }

        public void DgDbHistirySet(List<IModelFile> value) { dg.ItemsSource = value; }

        public MainWindow() {
            InitializeComponent();

            MainPresetner mainPresetner = new MainPresetner
            (this, new FileManager(@"C:\Users\User\Desktop\Банк выгрузка 2017 Сент.xlsx", 1));

        }    

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            CsvFile?.Invoke(this, EventArgs.Empty);
            XlsxFile?.Invoke(this, EventArgs.Empty);
            FilesDifference?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CsvFile;
        public event EventHandler XlsxFile;
        public event EventHandler FilesDifference;
    }
}
