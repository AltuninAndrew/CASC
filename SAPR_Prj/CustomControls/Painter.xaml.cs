using SAPR_Prj.Objects;
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

namespace SAPR_Prj.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для Painter.xaml
    /// </summary>
    public partial class Painter : UserControl
    {
        public static DependencyProperty NumberOfRodsProperty = DependencyProperty.Register("NumOfRods", typeof(int), typeof(Painter));
        public static DependencyProperty RodsProperty = DependencyProperty.Register("Rods", typeof(ObservableCollection<Rod>), typeof(Painter));
        public static DependencyProperty NumberOfRigidSuppProperty = DependencyProperty.Register("TypeRigidSupp", typeof(int), typeof(Painter));
        public static DependencyProperty NodesProperty = DependencyProperty.Register("Nodes", typeof(ObservableCollection<Node>), typeof(Painter));


        public int NumOfRods
        {
            get { return (int)GetValue(NumberOfRodsProperty); }
            set { SetValue(NumberOfRodsProperty, value); }
        }


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
            get { return (int)GetValue(NumberOfRigidSuppProperty); }
            set { SetValue(NumberOfRigidSuppProperty, value); }
        }
       

        public Painter()
        {
            InitializeComponent();
            

        }


        public void Paint(EventArgs e)
        {
            MainPanel.Children.Clear();


            int count = Rods.Count;

            List<RodVisualElement> rodElements = new List<RodVisualElement>();
            
            foreach (var element in Rods)
            {

                RodVisualElement rodVisualElement = new RodVisualElement();

                float longForceInNode = Nodes.ElementAt(element.Id + 1).LongForce;

                if(longForceInNode>0)
                {
                    rodVisualElement.ArowNodeForceLeftObj.Visibility = Visibility.Hidden;
                    rodVisualElement.TextForceNode.Text = String.Format("{0}F", longForceInNode);
                }
                else if(longForceInNode<0)
                {
                    rodVisualElement.ArowNodeForceObj.Visibility = Visibility.Hidden;
                    rodVisualElement.TextForceNode.Text = String.Format("{0}F", longForceInNode);
                }
                else
                {
                    rodVisualElement.ArowNodeForceObj.Visibility = Visibility.Hidden;
                    rodVisualElement.ArowNodeForceLeftObj.Visibility = Visibility.Hidden;
                    rodVisualElement.TextForceNode.Visibility = Visibility.Hidden;
                }


                rodVisualElement.Width *= element.Length;
                rodVisualElement.Height *=element.Sectional;
                rodVisualElement.RigidSuppObjLeft.Visibility = Visibility.Hidden;
                rodVisualElement.RigidSuppObjRight.Visibility = Visibility.Hidden;
                rodVisualElement.TextForce.Text = String.Format("{0}E, {1}A, {2}q", element.ElasticModulus,element.Sectional,element.RunningLoad);
                rodVisualElement.TextLength.Text = String.Format("{0}L", element.Length);

                if (element.RunningLoad>0)
                {
                    rodVisualElement.SetForce(true);
                }
                else if(element.RunningLoad<0)
                {
                    rodVisualElement.SetForce(false);
                }
                
                MainPanel.Children.Add(rodVisualElement);
                rodElements.Add(rodVisualElement);
            }

            switch (TypeRigidSupp)
            {
                case 0:
                    break;
                case 1:
                    rodElements[0].RigidSuppObjLeft.Visibility = Visibility.Visible;
                    break;
                case 2:
                    rodElements.Last().RigidSuppObjRight.Visibility = Visibility.Visible;
                    break;
                case 3:
                    rodElements[0].RigidSuppObjLeft.Visibility = Visibility.Visible;
                    rodElements.Last().RigidSuppObjRight.Visibility = Visibility.Visible;
                    break;
            }

        }

        private void Element_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Paint(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Paint(e);
            Rods.CollectionChanged += Rods_CollectionChanged;
            foreach(var element in Rods)
            {
                element.PropertyChanged += Element_PropertyChanged;
            }
        }

        private void Rods_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

        }
    }
}
