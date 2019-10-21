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
        private const int MaxNumOfRodsInSystem = 1000;
        private List<Node> _nodes;
        private List<Rod> _rods;
        public int NumOfRods { get => _rods.Count(); }
        public int NumOfNodes { get => _nodes.Count(); }



        public PreProcModel()
        {
            _nodes = new List<Node>();
            _rods = new List<Rod>();
        }

        public void InitModel()
        {
            Rod firstRod = new Rod(0);
            _rods.Add(firstRod);
            Node firstNode = new Node(0, 0, firstRod);
            firstNode.IsHaveRigidSupp = true;
            _nodes.Add(firstNode);
            _nodes.Add(new Node(1, firstRod.Length, firstRod));
           
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
            if(numOfNewRods>0 && numOfNewRods<=MaxNumOfRodsInSystem)
            {
                int ammount = _rods.Count + numOfNewRods;
                for (int i = _rods.Count; i < ammount; i++)
                {
                    _rods.Add(new Rod(i));
                    _nodes.Add(new Node(i + 1, _nodes[i].PosX + _rods[i].Length, _rods[i]));
                }
            }
            else
            {
                //to implement notification system
            }

        }

        public void DeleteRod(int index)
        {
            if (_rods.Count > 1)
            {
                int indexEndNode = index + 1;
                _rods.RemoveAt(index);
                _nodes[0].TiedRod = _rods[0];
                _nodes.RemoveAt(indexEndNode);
                for (int i = 0; i < _rods.Count; i++)
                {
                    _rods[i].Id = i;
                    _nodes[i + 1].TiedRod = _rods[i];
                }

                for (int i = 0; i < _nodes.Count; i++)
                {
                    _nodes[i].Id = i;
                }

                ReCalcNods();
            }
            else
            {
                //to implement notification system
            }



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
