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

        PreProcModel model = new PreProcModel();
        public PreProcWindow()
        {
            InitializeComponent();

            TableRods.DataContext = model.GetRods();
            TableNodes.DataContext = model.GetNodes();
        }


        private void BtnAddRods_Click(object sender, RoutedEventArgs e)
        {
            var numOfRods = BoxNumOfRods.Value;
            if (numOfRods != null)
            {
                BoxNumOfRods.Value = null;
                model.AddRods((int)numOfRods);
                TableRods.DataContext = model.GetRods();
                TableNodes.DataContext = model.GetNodes();
            }
            
        }
    }
}
