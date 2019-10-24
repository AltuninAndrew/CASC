using SAPR_Prj.Objects;
using SAPR_Prj.Services;
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
        private int _numRigidSup = 0;
        public int NumOfRods { get => _rods.Count(); }
        public int NumOfNodes { get => _nodes.Count(); }
        public int RigidSupp
        {
            set
            {
                if (value >= 0 && value<=3)
                {
                    _numRigidSup = value;
                }
                else
                {
                    //
                }
            }

            get
            {
                return _numRigidSup;
            }

        }

        public PreProcModel()
        {
            _nodes = new List<Node>();
            _rods = new List<Rod>();
        }

        public void InitModel()
        {
            Rod firstRod = new Rod(0);
            firstRod.PropertyChanged += Rod_LengthChanged;
            _rods.Add(firstRod);
            Node firstNode = new Node(0, 0, firstRod);
            //firstNode.IsHaveRigidSupp = true;
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
                    Rod newRod = new Rod(i);
                    newRod.PropertyChanged += Rod_LengthChanged;
                    _rods.Add(newRod);
                    _nodes.Add(new Node(i + 1, _nodes[i].PosX + _rods[i].Length, _rods[i]));
                }
            }
            else
            {
                //to implement notification system
            }

        }

        private void Rod_LengthChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
           ReCalcNods();
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

        private void ReCalcNods()
        {
            for(int i = 0;i<_rods.Count;i++)
            {
                _nodes[i + 1].PosX = _rods[i].Length + _nodes[i].PosX;
            }
        }

        public void SaveData(string path, string fileName)
        {
            FileProjectManager.SaveFile(new ModelToSave { Rods = _rods, Nodes = _nodes, RigidSuppType = _numRigidSup }, path,fileName);
        }

        public void LoadModelFromFile(string path)
        {
            ModelToSave fromModel = FileProjectManager.LoadFile(path);
            if(fromModel.Rods!=null)
            {
                _rods = fromModel.Rods;
                _nodes = fromModel.Nodes;
                _numRigidSup = fromModel.RigidSuppType;
            }
        }

    }
}
