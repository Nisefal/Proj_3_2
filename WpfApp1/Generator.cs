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
        public static Dictionary<int, int> GeneretePipe(int min, int max, int current_n, int min_lenght)
        {
            int length = RandomInt(min, max);
            Dictionary<int, int> pipe = new Dictionary<int, int>() { {RandomInt(0, current_n), RandomInt(0, current_n) }};
            bool path_exists = true;
            int side = 0;
            while (path_exists && pipe.Count< min_lenght)
            {
                side = RandomInt(0, 3);
                if (side==0)
                {
                    
                }
                else
                {
                    if (side==1)
                    {

                    }
                    else
                    {
                        if(side==3)
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }
            
            //pipe.Add(RandomInt(0, current_n), RandomInt(0, current_n));


            return pipe;
        }

        public static Field GenerateField(int current_n, int percent_max)
        {
            return new Field();
        }
        

    }
}
