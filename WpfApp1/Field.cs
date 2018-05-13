using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Field : ICloneable, IDisposable
    {
        public int n;
        public int[,] PlatesField;
        public int[,] LiftedPlates; //0 || 1
        public List<Pipe> pipes; //list of list of indexes of x = list of pipes


        public Pipe PipeByNumber(int n)
        {
            return pipes[n];
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int SumPipe(Pipe p)
        {
            int sum = 0;
            foreach (Coordinate C in p.GetCoordinates())
            {
                sum += LiftedPlates[C.i, C.j];
            }
            return sum;
        }

        public bool PipeLocked(Pipe p)
        {
            if (SumPipe(p) > 0)
                return true;
            else
                return false;
        }

        public int AnswerSum()
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    sum+=PlatesField[i, j] * LiftedPlates[i, j];
            return sum;
        }

        public void AddPipe(Pipe p)
        {
            if (pipes == null)
                pipes = new List<Pipe>();
            pipes.Add(p);
        }

        public void Dispose()
        {
            n = 0;
            PlatesField = null;
            LiftedPlates = null;
            pipes = null;
        }
        
        public bool ValidateAnswer()
        {
            foreach (Pipe item in pipes)
                if (!PipeLocked(item))
                return false;
            return true;
        }
    }

    class Coordinate
    {
        public int i;
        public int j;

        public Coordinate(int _i, int _j)
        {
            i = _i;
            j = _j;
        }

        public static bool CoordinateExits(Coordinate c, int n)
        {
            if (c.i < 0 || c.j < 0 || c.i >= n || c.j >= n)
                return false;
            return true;
        }
    }

    class Pipe
    {
        private List<Coordinate> pipe;

        public Pipe()
        {
            pipe = new List<Coordinate>();
        }

        public Pipe(Coordinate c)
        {
            Add(c);
        }

        public Pipe(int i, int j)
        {
            Add(new Coordinate(i, j));
        }

        public void Add(Coordinate c)
        {
            if (pipe == null)
                pipe = new List<Coordinate>();
            pipe.Add(c);
        }

        public bool HasPoint(Coordinate c)
        {
            foreach (Coordinate coord in pipe)
                if (c.i == coord.i && c.j==coord.j)
                    return true;
            return false;
        }

        public int Length()
        {
            return pipe.Count;
        }

        public Coordinate GetLast()
        {
            return pipe.Last();
        }

        public List<Coordinate> GetCoordinates()
        {
            return pipe;
        }

        public Coordinate GetCoordinate(int i)
        {
            if (i >= 0 && i < Length())
                return pipe[i];
            else return null;
        }
    }
}
