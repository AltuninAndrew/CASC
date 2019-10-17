using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPR_Prj.Objects
{
    class NodeObj
    {
        public float PosX { get; private set; } = 0;

        /// <summary>
        /// Fj
        /// </summary>
        public float LongForce { get; set; } = 0;

        public NodeObj(float posX)
        {
            PosX = posX;
        }
    }
}
