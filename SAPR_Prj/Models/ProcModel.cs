using SAPR_Prj.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPR_Prj.Models
{
    class ProcModel
    {
        private List<Rod> _rods;
        private List<Node> _nodes;
        private int _typeRigidSupp;

        public List<List<float>> matrixA;
        public List<float> vectorReaction;

        public ProcModel(List<Node> nodes, List<Rod> rods,int typeRigidSupp)
        {
            _rods = rods;
            _nodes = nodes;
            _typeRigidSupp = typeRigidSupp;

            //start init matrix 
            matrixA = new List<List<float>>(nodes.Count);
            
            for(int i=0;i<nodes.Count;i++)
            {
                matrixA.Add(new List<float>());
                for (int j = 0; j < nodes.Count; j++)
                {
                    matrixA[i].Add(0);
                }
            }
            InitMatrixA();
            vectorReaction = new List<float>(matrixA.Count);
            CalcReaction();
        }


        void InitMatrixA()
        {
            matrixA[0][0] = (_rods[0].Sectional * _rods[0].ElasticModulus) / (_rods[0].Length);
            matrixA[1][0] = -(_rods[0].Sectional * _rods[0].ElasticModulus) / (_rods[0].Length);
            matrixA[0][1] = -(_rods[0].Sectional * _rods[0].ElasticModulus) / (_rods[0].Length);

            int k = 1;
            for (int i=0;i<matrixA.Count-1;i++)
            {
                for(int j=0;j<matrixA.Count-1;j++)
                {
                    if(i==j && i>0 && i<matrixA.Count)
                    {
                        matrixA[i][j] = (_rods[k - 1].Sectional * _rods[k - 1].ElasticModulus) / (_rods[k-1].Length) + (_rods[k].Sectional * _rods[k].ElasticModulus) / (_rods[k].Length);
                        matrixA[i + 1][j] = -(_rods[k].Sectional * _rods[k].ElasticModulus) / (_rods[k].Length);
                        matrixA[i][j + 1] = -(_rods[k].Sectional * _rods[k].ElasticModulus) / (_rods[k].Length);
                        k++;
                    }
                    matrixA[matrixA.Count - 1][matrixA.Count - 1] = (_rods[_rods.Count - 1].Sectional * _rods[_rods.Count - 1].ElasticModulus) / _rods[_rods.Count - 1].Length;
                }
            }


        }

        void CalcReaction()
        {
            float tmpValue = 0;
          
            for(int i=0; i<matrixA.Count;i++)
            {
                //force on nod
                tmpValue = 0;
                tmpValue += _nodes[i].LongForce;

                //force on rods
                tmpValue += (_nodes[i].TiedRod.RunningLoad * _nodes[i].TiedRod.Length) / 2;
                if (i>0 && i<_nodes.Count-1)
                {
                    tmpValue += (_nodes[i + 1].TiedRod.RunningLoad*_nodes[i+1].TiedRod.Length)/2;
                }

                vectorReaction.Add(tmpValue);
            }


            //rewrite
            if(_typeRigidSupp==3)
            {
                vectorReaction[0] = 0;
                vectorReaction[vectorReaction.Count-1] = 0;
            } else if(_typeRigidSupp==1)
            {
                vectorReaction[0] = 0;
            }
            else if(_typeRigidSupp == 2)
            {
                vectorReaction[vectorReaction.Count - 1] = 0;
            } else
            {
                throw new Exception("calculations are impossible");
            }


        }

        void CalcMovement()
        {
            
        }






    }
}
