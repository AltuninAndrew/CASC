﻿using SAPR_Prj.Models;
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
        public PreProcWindow()
        {
            InitializeComponent();
            //var obj = DataContext;
        }

        private void BoxSuppVariant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
