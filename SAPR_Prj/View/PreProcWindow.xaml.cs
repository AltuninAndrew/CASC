using SAPR_Prj.Models;
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
        PreProcWindowViewМodel vm = new PreProcWindowViewМodel();
        public PreProcWindow()
        {
            InitializeComponent();
           
            DataContext = vm;
        }

        private void TableRods_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

           string element;
           element = e.Column.Header.ToString();
           if (element == "Длина")
           {
                var elem = vm.Rods;
                vm.Command1.Execute(objEdit);
           }


        }

    }
}
