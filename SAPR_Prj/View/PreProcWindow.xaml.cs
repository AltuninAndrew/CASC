using SAPR_Prj.ViewModels;
using System.Windows;
using System.Windows.Forms;

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
