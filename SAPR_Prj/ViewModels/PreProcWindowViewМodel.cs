using SAPR_Prj.Commands;
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

        public PreProcWindowViewМodel()
        {
            _model.InitModel();
            _model.AddRods(2);
        }


        public ObservableCollection<Rod> Rods
        {
            get { return new ObservableCollection<Rod>(_model.GetRods()); }
        }

        public ObservableCollection<Node> Nodes
        {
            get { return new ObservableCollection<Node>(_model.GetNodes()); }
        }


        public ICommand CommitChange
        {
            get
            {

                return new DelegateCommand((obj =>
                {
                    _model.ReCalcNods();
                    
                }));
            }
        }

        public ICommand DellRods
        {
            get
            {
                return new DelegateCommand((obj =>
                {
                    int id = (int)obj;

                }));

            }
        }

        public ICommand AddRods
        {
            get
            {
                return new DelegateCommand((obj =>
                {
                    if(obj!=null && (int)obj>0)
                    {
                        _model.AddRods((int)obj);
                        OnPropertyChanged();
                    }
                    
                }));
            }
        }

    }
}
