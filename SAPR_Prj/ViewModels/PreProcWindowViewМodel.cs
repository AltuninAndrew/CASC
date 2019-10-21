using SAPR_Prj.Commands;
using SAPR_Prj.Models;
using SAPR_Prj.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public IReadOnlyList<Rod> Rods
        {
            get { return _model.GetRods(); }
        }

        public IReadOnlyList<Node> Nodes
        {
            get { return _model.GetNodes(); }
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

        public ICommand AddRods
        {
            get
            {
                return new DelegateCommand((obj =>
                {
                    _model.AddRods((int)obj);
                }));
            }
        }


    }
}
