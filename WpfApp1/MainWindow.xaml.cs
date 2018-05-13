using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int n_start;
        public static int n_end;
        public static int step;
        public static int percent;
        public static int min_lenght;

        public MainWindow()
        {
            InitializeComponent();
            Method.SolveWithGeneticAlgorythm(Generator.GenerateField(5, 50));
        }

        public void SetProgressValue(double value)
        {
            if(Dispatcher.CheckAccess())
            {
                Progress.Value = value;
            }
            else
            {
                Dispatcher.Invoke(() => { Progress.Value = value; });
            }
        }

        private void StartNTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            int n = 0;
            if (!int.TryParse(((TextBox)sender).Text, out n))
                ((TextBox)sender).Text = "";
            //if (n < 5)
            //    ((TextBox)sender).Text = "5";
            //MakeOk();
        }

        private void EndNTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            int n = 0;
            if (!int.TryParse(((TextBox)sender).Text, out n))
                ((TextBox)sender).Text = "";
            //if (n > 20)
            //    ((TextBox)sender).Text = "20";
            //MakeOk();
        }

        private void StepSizeTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            int n = 0;
            if (!int.TryParse(((TextBox)sender).Text, out n))
                ((TextBox)sender).Text = "";
            if (n > 5)
                ((TextBox)sender).Text = "5";
        }

        private void TriesTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            int n = 0;
            if (!int.TryParse(((TextBox)sender).Text, out n))
                ((TextBox)sender).Text = "";
            if(n>5)
                ((TextBox)sender).Text = "5";
        }
        private void MakeOk()
        {
            try
            {
                if (int.Parse(StartNTB.Text) > int.Parse(EndNTB.Text))
                {
                    string i = StartNTB.Text;
                    StartNTB.Text = EndNTB.Text;
                    EndNTB.Text = i;
                }
            }
            catch { }
        }

        private void PercentS_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.IsInitialized)
                PercentValueView.Text = ((int)PercentS.Value).ToString()+" %";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = "";
            Field f = Generator.GenerateField(10, 35);
            var lst = f.pipes;
            foreach (var m in lst)
            {
                foreach (var n in m.GetCoordinates())
                {
                    s += $"{n.i} {n.j}\n";
                }
            }
        }
    }
}
