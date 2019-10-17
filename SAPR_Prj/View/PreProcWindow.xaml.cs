using SAPR_Prj.Models;
using SAPR_Prj.Objects;
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
        class TestItem
        {
            public int Id { get; set; }
            public float Value { get; set; }
        }

        public PreProcWindow()
        {

            InitializeComponent();


            PreProcModel preProcModel = new PreProcModel(new RodObj[] { new RodObj(0, 1), new RodObj(1,1.5f), new RodObj(1.5f,7) });
            

            int countNodes = preProcModel.NumberOfNodes;

            List<RodObj> rodObjs = preProcModel.GetRods().ToList();

            Table1.DataContext = rodObjs;
        }
        
    }
}
