using System;
using System.Collections.Generic;

namespace WpfApp1
{
    /// <summary>
    /// Generates data for solvers(Methods)
    /// </summary>
    static class Generator
    {
        public static Random rand;
        private static int n;
        private static List<Coordinate> indexes;
        
        /// <summary>
        /// Integer randomizator
        /// </summary>
        /// <param name="st"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int RandomInt(int st=1, int end=10)
        {
            if (rand == null)
                rand = new Random();
            return rand.Next(st,end);
        }
        
        /// <summary>
        /// max = n
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Pipe GeneretePipe()
        {
            // to make pipes more direct
            int first = 10;
            int second = 20;
            int third = 30;
            int fourth = 39;
            //

            //randomly generate pipe's length
            int length = RandomInt(2, n);

            //initialize new pipe's start pozition
            Pipe pipe = new Pipe(RandomInt(0, n), RandomInt(0, n));

            //pipe has space to 'grow'
            bool path_exists = true;

            //direction to 'grow'
            int side = 0;
            
            //new and last coordinates
            Coordinate last_c = pipe.GetLast();
            Coordinate new_c = null;

            while (path_exists && pipe.Length()< length)
            {
                //0-9 - to left; 10-19 - down; 20-29 - to right; 30-39 - up
                side = RandomInt(0, fourth);
                if (side>=0 && side <first)
                {
                    new_c = new Coordinate(last_c.i-1, last_c.j);
                    if (Coordinate.CoordinateExits(new_c, n) && !pipe.HasPoint(new_c))
                    {
                        pipe.Add(new_c);
                        last_c = new_c;
                    }
                }
                else
                {
                    if (side>=first+1 && side <second)
                    {
                        new_c = new Coordinate(last_c.i, last_c.j+1);
                        if (Coordinate.CoordinateExits(new_c, n) && !pipe.HasPoint(new_c))
                        {
                            pipe.Add(new_c);
                            last_c = new_c;
                        }
                    }
                    else
                    {
                        if(side>=second+1 && side < third)
                        {
                            new_c = new Coordinate(last_c.i + 1, last_c.j);
                            if (Coordinate.CoordinateExits(new_c, n) && !pipe.HasPoint(new_c))
                            {
                                pipe.Add(new_c);
                                last_c = new_c;
                            }
                        }
                        else
                        {
                            new_c = new Coordinate(last_c.i, last_c.j-1);
                            if (Coordinate.CoordinateExits(new_c, n) && !pipe.HasPoint(new_c))
                            {
                                pipe.Add(new_c);
                                last_c = new_c;
                            }
                        }
                    }
                }
                path_exists = PathExists(pipe.GetCoordinates()[pipe.Length()-1],pipe);
            }
            return pipe;
        }

        /// <summary>
        /// Check if pipe has space to 'grow'
        /// </summary>
        /// <param name="c"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool PathExists(Coordinate c, Pipe p)
        {
            if (LeftExists(c, p) || RightExists(c, p) || DownExists(c, p) || UpExists(c, p))
                return true;
            else
                return false;
        }

        /// <summary>
        /// pipe has space in 'left' direction
        /// </summary>
        /// <param name="c"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool LeftExists(Coordinate c, Pipe p)
        {
            Coordinate new_c = new Coordinate(c.i - 1, c.j);
            if (Coordinate.CoordinateExits(new_c, n) && !p.HasPoint(new_c))
                return true;
            return false;
        }

        /// <summary>
        /// pipe has space in 'down' direction
        /// </summary>
        /// <param name="c"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool DownExists(Coordinate c, Pipe p)
        {
            Coordinate new_c = new Coordinate(c.i, c.j+1);
            if (Coordinate.CoordinateExits(new_c, n) && !p.HasPoint(new_c))
                return true;
            return false;
        }

        /// <summary>
        /// pipe has space in 'right' direction
        /// </summary>
        /// <param name="c"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool RightExists(Coordinate c, Pipe p)
        {
            Coordinate new_c = new Coordinate(c.i + 1, c.j);
            if (Coordinate.CoordinateExits(new_c, n) && !p.HasPoint(new_c))
                return true;
            return false;
        }

        /// <summary>
        /// pipe has space in 'up' direction
        /// </summary>
        /// <param name="c"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool UpExists(Coordinate c, Pipe p)
        {
            Coordinate new_c = new Coordinate(c.i, c.j-1);
            if (Coordinate.CoordinateExits(new_c, n) && !p.HasPoint(new_c))
                return true;
            return false;
        }

        /// <summary>
        /// Pipes cover minimum percent of field
        /// </summary>
        /// <param name="percent"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool SpaceFilledWithPipes(int percent, Field field)
        {
            //indexes = new Dictionary<int, int>();
            foreach (Pipe pipe in field.pipes)
                foreach (Coordinate c in pipe.GetCoordinates())
                    if(!pipe.HasPoint(c))
                        indexes.Add(c);

            if (field.PlatesFiel.Length * field.PlatesFiel.Length / 100 * indexes.Count < percent)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Generte field with pipes etc.
        /// </summary>
        /// <param name="current_n"></param>
        /// <param name="percent_max"></param>
        /// <returns></returns>
        public static Field GenerateField(int current_n, int percent_max)
        {
            Field f = new Field();
            n = current_n;

            //fill with plates
            f.PlatesFiel = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    f.PlatesFiel[i, j] = RandomInt(1, 10);

            //fill with pipes
            if (indexes == null)
                indexes = new List<Coordinate>();
            while (!SpaceFilledWithPipes(percent_max, f))
            {
                f.pipes.Add(GeneretePipe());
            }

            indexes.Clear();
            n = 0;
            return f;
        }
    }
}
