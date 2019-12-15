using SAPR_Prj.Objects;
using System.Collections.Generic;

namespace SAPR_Prj.Services
{
    class ModelToSave
    {
        public List<Rod> Rods { get; set; }
        public List<Node> Nodes { get; set; }
        public int RigidSuppType { get; set; }
    }
}
