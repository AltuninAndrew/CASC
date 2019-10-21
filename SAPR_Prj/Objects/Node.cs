using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPR_Prj.Objects
{
    class Node
    {
        private int _id = 0;
        private float _posX = 0;

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

        public float PosX
        {
            set
            {
                if(value>=0)
                {
                    _posX = value;
                }
                else
                {
                    //
                }
            }

            get
            {
                return _posX;
            }
        }

        public bool IsHaveRigidSupp { get; set; } = false;

        public float LongForce { get; set; } = 0;

        public Rod TiedRod { get; set; } = null;

        public Node()
        {

        }

        public Node(int id, float posX, Rod tiedRod)
        {
            Id = id;
            PosX = posX;
            TiedRod = tiedRod;
        }

        public Node(int id, float posX, Rod tiedRod, bool isHaveRigidSupp, float longForce)
        {
            Id = id;
            PosX = posX;
            TiedRod = tiedRod;
            IsHaveRigidSupp = isHaveRigidSupp;
            LongForce = longForce;
        }

    }
}
