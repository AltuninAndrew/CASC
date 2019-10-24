﻿using SAPR_Prj.Commands;
using SAPR_Prj.Models;
using SAPR_Prj.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SAPR_Prj.ViewModels
{
    class PreProcWindowViewМodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly PreProcModel _model = new PreProcModel();

        public string[] RigidSuppVariant { get; } = { "Нет", "Слева", "Справа", "Обе" };

        public int RigidSuppState
        {
            get { return _model.RigidSupp; }
            set { _model.RigidSupp = value; }
        }

        public PreProcWindowViewМodel()
        {
            _model.InitModel();
        }


        public ObservableCollection<Rod> Rods
        {
            get { return new ObservableCollection<Rod>(_model.GetRods()); }
        }

        public ObservableCollection<Node> Nodes
        {
            get { return new ObservableCollection<Node>(_model.GetNodes()); }
        }

        public ICommand SetRigidSupp
        {
            get
            {

                return new DelegateCommand((obj =>
                {
                 
                    if(obj !=null)
                    {
                        switch ((string)obj)
                        {
                            case "Нет":
                                RigidSuppState = 0;
                                break;
                            case "Слева":
                                RigidSuppState = 1;
                                break;
                            case "Справа":
                                RigidSuppState = 2;
                                break;
                            case "Обе":
                                RigidSuppState = 3;
                                break;
                            default:
                                RigidSuppState = 0;
                                break;

                        }
                        OnPropertyChanged();
                    }
                   

                }));
            }
        }

        public ICommand DellRods
        {
            get
            {
                return new DelegateCommand((obj =>
                {
                    _model.DeleteRod((int)obj);
                    OnPropertyChanged();

                }),(obj)=>_model.NumOfRods>1);

            }
        }

        public ICommand AddRods
        {
            get
            {
                return new DelegateCommand((obj =>
                {
                    if(obj!=null)
                    {
                        _model.AddRods((int)obj);
                        OnPropertyChanged();
                    }
                    
                }));
            }
        }

        //public ICommand SaveModel
        //{

        //}

        //public ICommand LoadModel
        //{

        //}

    }
}
