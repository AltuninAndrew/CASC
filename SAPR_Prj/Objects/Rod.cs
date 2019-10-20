using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPR_Prj.Objects
{
    class Rod
    {
        public int Id { get; set; } = -1;
        public float Length { get; set; } = 1;
        public float Sectional { get; set; } = 0;
        public float ElasticModulus { get; set; } = 0;
        public float AllowStress { get; set; } = 0;
        public float RunningLoad { get; set; } = 0;

        public Rod(int id)
        {
            Id = id;
        }


    }
}
