using SAPR_Prj.Models;
using SAPR_Prj.Objects;
using SAPR_Prj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SAPR_Prj
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class PreProcWindow : Window
    {
        PreProcWindowViewМodel vm;
        public PreProcWindow()
        {
            InitializeComponent();
            vm = DataContext as PreProcWindowViewМodel;
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.FileOk += SaveDialog_FileOk;
            saveDialog.Filter = "json file(.json) | *json";
            saveDialog.DefaultExt = "json";
            saveDialog.AddExtension = true;
            saveDialog.ShowDialog();
             
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "json file(.json) | *json",
                DefaultExt = "json",
                AddExtension = true        
            };
            openDialog.FileOk += OpenDialog_FileOk;
            openDialog.ShowDialog();
        }

        private void OpenDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string fileName = (sender as OpenFileDialog).FileName;
            vm.LoadModel.Execute(fileName);
        }

        private void SaveDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string fileName = (sender as SaveFileDialog).FileName;
            vm.SaveModel.Execute(fileName);
        }


    }
}
