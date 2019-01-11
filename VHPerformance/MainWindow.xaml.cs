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
using System.Threading;
using System.Diagnostics;

namespace VHPerformance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int Square(int getal)
        {
            Thread.Sleep(1000 * getal);
            return getal * getal;
        }

        private void Kwadraat_Click(object sender, RoutedEventArgs e)
        {
            var getallen = new[] { 5, 3, 2, 8, 7, 4, 3, 9 };

            var sw = Stopwatch.StartNew();

            var query = from g in getallen.AsParallel()
                        where Square(g) > 10
                        select g;

            var resultaat = query.ToList();
            sw.Stop();

            MessageBox.Show(sw.ElapsedMilliseconds.ToString());
        }

        private async void BerekenKwadraat_Click(object sender, RoutedEventArgs e)
        {
            int getal = int.Parse(txt.Text);
            var taak = new Task<int>(() =>
            {
                return Square(getal);
                
            });

            taak.Start();
            await taak;
            lbx.Items.Add($"Het kwadraat van {getal} is {taak.Result}");
        }

        Task<int> SquareAsync(int getal)
        {
            var taak = new Task<int>(() => Square(getal));
            taak.Start();
            return taak;
        }

        private async void BerekenKwadraatAsync_Click(object sender, RoutedEventArgs e)
        {
            int getal = int.Parse(txt.Text);
            int antwoord = await SquareAsync(getal);
            lbx.Items.Add($"Het kwadraat van {getal} is {antwoord}");
        }
    }
}
