using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPR_Prj.Objects
{
    class RodObj
    {
        /// <summary>
        /// Li
        /// </summary>
        public float Length { get => CalcLength(); }

        public NodeObj StartNode { get; private set; } = null;
        public NodeObj EndNode { get; private set; } = null;


        public RodObj(float startPosX, float endPosX)
        {

            if (endPosX > startPosX)
            {
                StartNode = new NodeObj(startPosX);
                EndNode = new NodeObj(endPosX);
            }

            else
            {
                throw new Exception("StartPosX>EndPosX");
            }
        }
        /// <summary>
        /// qi
        /// </summary>
        public float RunningLoad { get; set; } = 0;

        private float CalcLength()
        {
            if(StartNode!=null && EndNode!=null)
            {
                return Math.Abs(EndNode.PosX - StartNode.PosX);
            }
            else
            {
                return -1;
            }
        }

    }
}
