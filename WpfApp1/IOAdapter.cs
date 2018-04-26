using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    /// <summary>
    /// For input/output purposes
    /// </summary>
    static class IOAdapter
    {
        private static int Start;
        private static int End;
        private static int Step;
        private static int CurrentStepNumber;
        private static MainWindow win;

        public static void InitializeIOAdapter(int s, int e, int stp, MainWindow w)
        {
            Start = s;
            End = e;
            Step = stp;
            CurrentStepNumber = 0;
            win = w;
        }

        public static void NextStep()
        {
            CurrentStepNumber++;
            double val = 100 / (End - Start) * Step * CurrentStepNumber;
            win.SetProgressValue(val);
        }

    }
}
