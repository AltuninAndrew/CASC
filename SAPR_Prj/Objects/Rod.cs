using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPR_Prj.Objects
{
    public class Rod: INotifyPropertyChanged
    {
        private int _id = 0;
        private float _length = 1;
        private float _sectional = 1;
        private float _eLascticModulus = 1;
        private float _allowStress = 1;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public int Id
        {
            set
            {
                if (value >= 0)
                {
                    _id = value;
                }

                else
                {
                    //
                }

            }

            get
            {
                return _id;
            }

        }

        public float Length
        {
            set
            {
                if (value > 0)
                {
                    _length = value;
                    OnPropertyChanged();
                }
                else
                {

                }
            }

            get
            {
                return _length;
            }

        }

        public float Sectional
        {
            set
            {
                if (value > 0)
                {
                    _sectional = value;
                    OnPropertyChanged();
                }
                else
                {

                }
            }

            get
            {
                return _sectional;
            }
        }

        public float ElasticModulus
        {
            set
            {
                if (value > 0)
                {
                    _eLascticModulus = value;
                    OnPropertyChanged();
                }
                else
                {

                }
            }

            get
            {
                return _eLascticModulus;
            }
        }

        public float AllowStress
        {
            set
            {
                if (value > 0)
                {
                    _allowStress = value;
                    OnPropertyChanged();
                }
                else
                {

                }
            }

            get
            {
                return _allowStress;
            }
        }

        public float RunningLoad { get; set; } = 0;

        public Rod(int id)
        {
            Id = id;
        }


    }
}
