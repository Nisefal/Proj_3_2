using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Field
    {
        public int[,] PlatesFiel;
        public int[,] LiftedPlates;
        public List<Dictionary<int, int>> pipes; //list of list of indexes of x = list of pipes

        public Dictionary<int,int> PipeByNumber(int n)
        {
            return pipes[n];
        }

        public double GetPipeSum(int n)
        {
            double sum = 0;

            /// pipes[1][1][1] - вторая труба, втрой индекс.

            return sum;
        }
    }
}
