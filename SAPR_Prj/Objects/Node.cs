using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPR_Prj.Objects
{
    class Node
    {
        public int Id { get; set; } = 0;
        public float PosX { get; set; } = 0;
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
