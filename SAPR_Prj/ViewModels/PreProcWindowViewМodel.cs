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


        private readonly static PreProcModel _model = new PreProcModel(2);

        public List<Rod> Rods
        {
            get { return _model.GetRods().ToList(); }
        }

        public ICommand Command1
        {
            get
            {

                return new DelegateCommand((obj =>
                {
                   
                    _model.GetRods();
                }));
            }
        }


    }
}
