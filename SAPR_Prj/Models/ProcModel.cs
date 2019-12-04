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
        public List<float> vectorMovement;

        public List<float> vectorFromNullN;
        public List<float> vectorFromLengthN;

        public List<float> vectorFromNullU;
        public List<float> vectorFromLengthU;

        public List<float> vectorFromNullNormStress;
        public List<float> vectorFromLengthNormStress;

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
            CalcMovement();
            CalcVectorN();
            CalcVectorU();
            CalcNormalStress();
        }


        void InitMatrixA()
        { 
           
            matrixA[0][0] = (_rods[0].Sectional * _rods[0].ElasticModulus) / (_rods[0].Length);
            matrixA[1][0] = -(_rods[0].Sectional * _rods[0].ElasticModulus) / (_rods[0].Length);
            matrixA[0][1] = -(_rods[0].Sectional * _rods[0].ElasticModulus) / (_rods[0].Length);

            int k = 1;
            for (int i=0;i<_nodes.Count-1;i++)
            {
                for(int j=0;j< _nodes.Count - 1;j++)
                {
                    if(i==j && i>0 && i<_nodes.Count)
                    {
                        matrixA[i][j] = (_rods[k - 1].Sectional * _rods[k - 1].ElasticModulus) / (_rods[k-1].Length) + ((_rods[k].Sectional * _rods[k].ElasticModulus) / (_rods[k].Length));
                        matrixA[i + 1][j] = -(_rods[k].Sectional * _rods[k].ElasticModulus) / (_rods[k].Length);
                        matrixA[i][j + 1] = matrixA[i + 1][j];
                        k++;
                    }
                    
                }
            }
            matrixA[matrixA.Count - 1][matrixA.Count - 1] = (_rods[_rods.Count - 1].Sectional * _rods[_rods.Count - 1].ElasticModulus) / _rods[_rods.Count - 1].Length;


          
            if (_typeRigidSupp == 3)  //all
            {
                matrixA[0][0] = 1;
                matrixA[matrixA.Count-1][matrixA.Count - 1] = 1;
                matrixA[1][0] = matrixA[0][1] = 0;
                matrixA[matrixA.Count - 2][matrixA.Count - 1] = matrixA[matrixA.Count - 1][matrixA.Count - 2] = 0;
            }
            else if (_typeRigidSupp == 1) // left
            {
                matrixA[0][0] = 1;
                matrixA[1][0] = matrixA[0][1] = 0;
            }
            else if (_typeRigidSupp == 2) //right
            {
                matrixA[matrixA.Count - 1][matrixA.Count - 1] = 1;
                matrixA[matrixA.Count - 2][matrixA.Count - 1] = matrixA[matrixA.Count - 1][matrixA.Count - 2] = 0;
            }
            else
            {
                throw new Exception("calculations are impossible"); //miss
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

            List<List<float>> tmpMatrixA = matrixA;


            for(int i=0;i<matrixA.Count;i++)
            {
                tmpMatrixA[i].Add(vectorReaction[i]);
            }

            List<float> vectorX = new List<float>();
            float m = 0;


            for (int i = 0; i < matrixA.Count; i++)
            {
                vectorX.Add(tmpMatrixA[i][matrixA.Count]);
            }

            for (int k = 1; k < matrixA.Count; k++)
            {
                for (int j = k; j < matrixA.Count; j++)
                {
                    m = tmpMatrixA[j][k - 1] / tmpMatrixA[k - 1][k - 1];
                    for (int i = 0; i < matrixA.Count; i++)
                    {
                        tmpMatrixA[j][i] = tmpMatrixA[j][i] - m * tmpMatrixA[k - 1][i];
                    }
                    vectorX[j] = vectorX[j] - m * vectorX[k - 1];
                }
            }



            for (int i = matrixA.Count - 1; i >= 0; i--)
            {
                for (int j = i + 1; j < matrixA.Count; j++)
                {
                    vectorX[i] -= tmpMatrixA[i][j] * vectorX[j];

                }
                vectorX[i] = vectorX[i] / tmpMatrixA[i][i];

            }

            vectorMovement = vectorX;
        
        }

        void CalcVectorN()
        {
            vectorFromLengthN = new List<float>(_nodes.Count-1);
            vectorFromNullN = new List<float>(_nodes.Count - 1);


            for (int i = 0; i < _nodes.Count-1; i++)
            {

                float L = _rods[i].Length;
                float EA = _rods[i].Sectional * _rods[i].ElasticModulus;
                float qL = _rods[i].RunningLoad * L;
                float x = 0;
                vectorFromNullN.Add(EA / L * (vectorMovement[i + 1] - vectorMovement[i]) + (qL / 2) * (1 - 2 * x / L));
                x = L;
                vectorFromLengthN.Add(EA / L * (vectorMovement[i + 1] - vectorMovement[i]) + (qL / 2) * (1 - 2 * x / L));

            }
            
        }

        void CalcVectorU()
        {
            vectorFromLengthU = new List<float>(_nodes.Count - 1);
            vectorFromNullU = new List<float>(_nodes.Count - 1);

            for (int i = 0; i < _nodes.Count - 1; i++)
            {
                float L = _rods[i].Length;
                float EA = _rods[i].Sectional * _rods[i].ElasticModulus;
                float qL = _rods[i].RunningLoad * L;
                float x = 0;
                vectorFromNullU.Add(vectorMovement[i] + (x / L) * (vectorMovement[i + 1] - vectorMovement[i]) + ((qL * L) / 2 * EA) * (x / L) * (1 - x / L));
                x = L;
                vectorFromLengthU.Add(vectorMovement[i] + (x / L) * (vectorMovement[i + 1] - vectorMovement[i]) + ((qL * L) / 2 * EA) * (x / L) * (1 - x / L));
            }
        }

        void CalcNormalStress()
        {
            vectorFromLengthNormStress = new List<float>(_nodes.Count - 1);
            vectorFromNullNormStress = new List<float>(_nodes.Count - 1); 

            for (int i = 0; i < _nodes.Count - 1; i++)
            {
                vectorFromNullNormStress.Add(vectorFromNullN[i] / _rods[i].Sectional);
                vectorFromLengthNormStress.Add(vectorFromLengthN[i] / _rods[i].Sectional);
            }
        }

    }
}
