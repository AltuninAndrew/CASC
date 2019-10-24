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

namespace SAPR_Prj.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class RodVisualElement : UserControl
    {

        public const float UnitSizeWidth = 120;
        public const float UnitSizeHeight = 60;

        public bool isHaveSupp;

        public RodVisualElement()
        {
            InitializeComponent();

            Width = UnitSizeWidth;
            Height = UnitSizeHeight;

           

        }

        public void SetForce(bool isRightForce)
        {
            if(isRightForce)
            {
                CommitColumns();
                for (int i = 0; i < MainGrid.ColumnDefinitions.Count; i++)
                {
                    Arrow arrow = new Arrow();

                    Grid.SetRow(arrow, 1);
                    Grid.SetColumn(arrow, i);
                    MainGrid.Children.Add(arrow);


                }
            }
            else
            {
                CommitColumns();
                for (int i = 0; i < MainGrid.ColumnDefinitions.Count; i++)
                {
                    ArrowLeft arrow = new ArrowLeft();
                    Grid.SetRow(arrow, 1);
                    Grid.SetColumn(arrow, i);
                    MainGrid.Children.Add(arrow);
                    
                }
            }

        }

        private void CommitColumns()
        {
            int numOfGrid = (int)(Width / 12);
            for (int i = 0; i < numOfGrid; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(12);
                MainGrid.ColumnDefinitions.Add(cd);
            }
            Grid.SetColumnSpan(RodElement, numOfGrid);
            Grid.SetColumnSpan(RigidSuppObjRight, numOfGrid);
            Grid.SetColumnSpan(TextForce, numOfGrid);
            Grid.SetColumnSpan(TextLength, numOfGrid);


        }
    }
}
