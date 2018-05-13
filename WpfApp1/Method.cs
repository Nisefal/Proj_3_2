using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    /// <summary>
    /// For calculation purposes
    /// </summary>
    static class Method
    {
        private static List<int[]> answers;
        private static int count = 0;

        /// <summary>
        /// Solves task with gentical algorythm. Returns only number of iterations. Easy to make to return answer to task (what, actually, has no sence).
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static int SolveWithGeneticAlgorythm(Field f)
        {
            count = 0;
            answers = new List<int[]>
            {
                new int[f.n*f.n],
                new int[f.n*f.n],
                new int[f.n*f.n],
                new int[f.n*f.n],
                new int[f.n*f.n],
                new int[f.n*f.n],
                new int[f.n*f.n],
                new int[f.n*f.n],
                new int[f.n*f.n],
                new int[f.n*f.n]
            };

            Field field = (Field) f.Clone();

            foreach (int[] item in answers)
                MakeAnswer(field, item);

            int loss = 0;
            int min_curr = 0, min_prev = 0;

            do
            {
                count++;
                // Backup copy
                List<int[]> temp = new List<int[]>();
                foreach (int[] item in answers)
                {
                    int[] i = new int[item.Length];
                    item.CopyTo(i, 0);
                    temp.Add(i);
                }

                MixAnswers(temp);

                min_curr = 0;
                int b = SelectBestIndex(field, answers);
                PlaceAnswerOnField(field, answers[b]);
                min_curr = field.AnswerSum();

                int w = SelectWorstIndex(field, answers);
                MakeAnswer(field, answers[w]);

                if (min_curr > min_prev)
                {
                    loss++;
                    temp.Clear();
                }
                else
                {
                    loss = 0;
                    min_prev = min_curr;
                    answers.Clear();
                    foreach (int[] item in temp)
                    {
                        int[] i = new int[item.Length];
                        item.CopyTo(i, 0);
                        answers.Add(i);
                    }
                }
            } while (loss < 10);
            return count;
        } 
        
        /// <summary>
        /// Generate new answer.
        /// </summary>
        /// <param name="f"></param>
        /// <param name="answ"></param>
        public static void MakeAnswer(Field f, int[] answ)
        {
            foreach (var pipe in f.pipes)
            {
                Coordinate c = pipe.GetCoordinate(Generator.RandomInt(1, pipe.Length()-2));
                answ[c.i * f.n + c.j] = 1;
            }
        }

        /// <summary>
        /// Choose pairs of answers to mix.
        /// </summary>
        /// <param name="list"></param>
        private static void MixAnswers(Field f, List<int[]> list)
        {
            bool[] flags = new bool[10]
            {
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false
            };

            int first = 0, second = 0;
            for (int i = 0; i < 10; i++)
            {
                if (i%2==0)
                {
                    first = SelectBestIndex(f, list, flags);
                }
                else
                {
                    second = SelectBestIndex(f, list, flags);
                    MixAnswers(list[first], list[second]);
                }
            }
        }

        /// <summary>
        /// Mix two int one-demensional arrays beginning from random position to the end.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        private static void MixAnswers(int[] first, int[] second)
        {
            int point = Generator.RandomInt(1, first.Length-2);
            int[] first_tmp = new int[first.Length];
            first.CopyTo(first_tmp,0);
            for (int i = point; i < first.Length; i++)
            {
                first[i] = second[i];
                second[i] = first_tmp[i];
            }
        }

        private static List<int[]> SelectPopulation(Field f, List<int[]> prevpop)
        {
            List<int[]> newpop = new List<int[]>();
            foreach (int[] item in prevpop)
            {
                PlaceAnswerOnField(f, item);
                if (f.ValidateAnswer())
                    newpop.Add(item);
            }

            while (newpop.Count()!=10)
            {
                newpop.Add(Generator.Ge)
            }

            return newpop;
        }

        /// <summary>
        /// Worst = with max sum
        /// </summary>
        /// <param name="f"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static int SelectWorstIndex(Field f, List<int[]> list)
        {
            int worst = 0, worst_val = 0;
            worst_val = int.MinValue;
            for (int i = 0; i < list[0].Length; i++)
            {
                PlaceAnswerOnField(f, list[i]);
                int val = f.AnswerSum();
                if (val > worst_val)
                {
                    worst_val = val;
                    worst = i;
                }
            }
            return worst;
        }

        /// <summary>
        /// Best = with min sum
        /// </summary>
        /// <param name="f"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static int SelectBestIndex(Field f, List<int[]> list)
        {
            int best = 0, best_val = 0;
            best_val = int.MaxValue;
            for (int i = 0; i < list[0].Length; i++)
            {
                PlaceAnswerOnField(f, list[i]);
                int val = f.AnswerSum();
                if (val < best_val)
                {
                    best_val = val;
                    best = i;
                }
            }
            return best;
        }

        private static int SelectBestIndex(Field f, List<int[]> list, bool[] check)
        {
            int best = 0, best_val = 0;
            best_val = int.MaxValue;
            for (int i = 0; i < list[0].Length; i++)
            {
                PlaceAnswerOnField(f, list[i]);
                int val = f.AnswerSum();
                if (val < best_val && !check[i])
                {
                    best_val = val;
                    best = i;
                }
            }
            check[best] = true;
            return best;
        }

        /// <summary>
        /// Mutation function
        /// </summary>
        /// <param name="answers"></param>
        private static void Mutate(List<int[]> answers)
        {
            foreach (int[] item in answers)
            {
                if (Generator.RandomInt(1, 100)==100)
                {
                    int pos = Generator.RandomInt(0, answers[0].Length-1);
                    if (item[pos] == 0)
                        item[pos] = 1;
                    else
                        item[pos] = 0;
                }
            }
        }

        /// <summary>
        /// Reflect one-dimensional array on Field
        /// </summary>
        /// <param name="f"></param>
        /// <param name="answer"></param>
        private static void PlaceAnswerOnField(Field f, int[] answer)
        {
            for (int i = 0; i < f.n; i++)
                for (int j = 0; j < f.n; j++)
                    f.LiftedPlates[i, j] = answer[f.n * i + j];
        }


    }
}
