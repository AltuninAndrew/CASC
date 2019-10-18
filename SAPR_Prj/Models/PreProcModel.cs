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
        private List<Node> _nodes;
        private List<Rod> _rods;
        public int NumOfRods { get => _rods.Count(); }
        public int NumOfNodes { get => _nodes.Count(); }



        public PreProcModel()
        {
            _nodes = new List<Node>();
            _rods = new List<Rod>();

        }

        public void InitModel(int numOfRods)
        {
            Rod firstRod = new Rod(0);
            _rods.Add(firstRod);
            _nodes.Add(new Node(0, 0, firstRod));
            _nodes.Add(new Node(1, firstRod.Length, firstRod));

            AddRods(numOfRods-1);
           
        }

        public IReadOnlyList<Rod> GetRods()
        {
            return _rods.AsReadOnly();
        }
        
        public IReadOnlyList<Node> GetNodes()
        {
            return _nodes.AsReadOnly();
        }

        public void AddRods(int numOfNewRods)
        {
            int ammount = _rods.Count + numOfNewRods;
            for(int i = _rods.Count; i<ammount; i++)
            {
                _rods.Add(new Rod(i));
                _nodes.Add(new Node(i + 1, _nodes[i].PosX + _rods[i].Length, _rods[i]));
            }
        }

        public void DeleteRod(int index)
        {
            _rods.RemoveAt(index);
            _nodes.RemoveRange(index,1);

        }

        public void ReCalcNods()
        {
            for(int i = 0;i<_rods.Count;i++)
            {
                _nodes[i + 1].PosX = _rods[i].Length + _nodes[i].PosX;
            }
        }

    }
}
