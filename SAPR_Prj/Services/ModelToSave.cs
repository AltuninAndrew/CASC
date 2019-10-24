using SAPR_Prj.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPR_Prj.Services
{
    class ModelToSave
    {
        public List<Rod> Rods { get; set; }
        public List<Node> Nodes { get; set; }
        public int RigidSuppType { get; set; }
    }
}
