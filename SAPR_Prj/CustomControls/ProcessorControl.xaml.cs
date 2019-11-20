using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SAPR_Prj.Models;
using SAPR_Prj.Objects;

namespace SAPR_Prj.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для ProcessorControl.xaml
    /// </summary>
    public partial class ProcessorControl : UserControl
    {
        public static DependencyProperty RodsProperty = DependencyProperty.Register("Rods", typeof(ObservableCollection<Rod>), typeof(ProcessorControl));
        public static DependencyProperty NodesProperty = DependencyProperty.Register("Nodes", typeof(ObservableCollection<Node>), typeof(ProcessorControl));
        public static DependencyProperty TypeRigidSuppProperty = DependencyProperty.Register("TypeRigidSupp", typeof(int), typeof(ProcessorControl));


        public ObservableCollection<Rod> Rods
        {
            get { return (ObservableCollection<Rod>)GetValue(RodsProperty); }
            set { SetValue(RodsProperty, value); }
        }

        public ObservableCollection<Node> Nodes
        {
            get { return (ObservableCollection<Node>)GetValue(NodesProperty); }
            set { SetValue(NodesProperty, value); }
        }

        public int TypeRigidSupp
        {
            get { return (int)GetValue(TypeRigidSuppProperty); }
            set { SetValue(TypeRigidSuppProperty, value); }
        }


        ProcModel procModel;
        public ProcessorControl()
        {
            InitializeComponent();
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            procModel = new ProcModel(Nodes.ToList(),Rods.ToList(),TypeRigidSupp);
            string result="";
            for(int i =0;i<procModel.matrixA.Count;i++)
            {
                
                for(int j=0;j<procModel.matrixA.Count;j++)
                {
                    result += " "+ procModel.matrixA[i][j].ToString();
                }
                result += "\n";
            }

            txtBox.Document.Blocks.Clear();
            txtBox.AppendText(result);

            string vecReactRes = "";
            foreach (var element in procModel.vectorReaction)
            {
                vecReactRes += element + " ";
            }

            txtBox.AppendText("\n" + vecReactRes + "  -React vector");


            
        }
    }
}
