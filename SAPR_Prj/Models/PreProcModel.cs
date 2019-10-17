using SAPR_Prj.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SAPR_Prj.Models
{
    class PreProcModel
    {
        public string Notif { get; private set; } = "";
        public int NumberOfRods { get; private set; } = 0;
        public int NumberOfNodes => CalcNumberOfNode();

        public List<RodObj> _rods;
        private List<NodeObj> _nodes;

        public PreProcModel(RodObj[] rods)
        {
            NumberOfRods = rods.Length;
            _rods = new List<RodObj>(NumberOfRods);

            _nodes = new List<NodeObj>(NumberOfNodes);

            _nodes.Add(rods[0].StartNode);

            AddRods(rods);
        }

        private int CalcNumberOfNode()
        {
            return NumberOfRods + 1;
        }

        public IReadOnlyList<RodObj> GetRods()
        {
            return _rods.AsReadOnly();
        }

        public IReadOnlyList<NodeObj> GetNodes()
        {
           
            return _nodes.AsReadOnly();
        }

        public void RemoveRod(int index)
        {
            _rods.RemoveAt(index);
            NumberOfRods = _rods.Count; 
            _nodes.RemoveRange(index, index + 1);
        }

        public void AddRods(RodObj[] rodObjs)
        {

            List<RodObj> sortedRod = rodObjs.ToList();

            sortedRod.Sort((a, b) => a.StartNode.PosX.CompareTo(b.StartNode.PosX));
            
            foreach(var element in sortedRod)
            {
                if(_nodes.Last().PosX==element.StartNode.PosX)
                {
                    
                    _nodes.Add(element.EndNode);
                    _rods.Add(element);
                }
                else
                {
                    Notif += " \n Please set the node where the new node is further than the previous one";
                }
            }

            NumberOfRods = _rods.Count;
            
        }

    }
}
