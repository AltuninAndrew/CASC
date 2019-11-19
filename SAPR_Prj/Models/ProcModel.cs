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

        List<List<float>> matrixA;

        public ProcModel(List<Node> nodes, List<Rod> rods)
        {
            _rods = rods;
            _nodes = nodes;

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

        }


        void InitMatrixA()
        {
            matrixA[0][0] = (_rods[0].Sectional * _rods[0].ElasticModulus) / (_rods[0].Length);
            matrixA[1][0] = -(_rods[0].Sectional * _rods[0].ElasticModulus) / (_rods[0].Length);
            matrixA[0][1] = -(_rods[0].Sectional * _rods[0].ElasticModulus) / (_rods[0].Length);

            int k = 1;
            for (int i=0;i<matrixA.Count;i++)
            {
                for(int j=0;j<matrixA.Count;j++)
                {
                    if(i==j && i>0 && i<matrixA.Count)
                    {
                        matrixA[i][j] = (_rods[k - 1].Sectional * _rods[k - 1].ElasticModulus) / (_rods[k-1].Length) + (_rods[k].Sectional * _rods[k].ElasticModulus) / (_rods[k].Length);
                        matrixA[i + 1][j] = -(_rods[k].Sectional * _rods[k].ElasticModulus) / (_rods[k].Length);
                        matrixA[i][j + 1] = -(_rods[k].Sectional * _rods[k].ElasticModulus) / (_rods[k].Length);
                        k++;
                    }
                }
            }


        }





    }
}
