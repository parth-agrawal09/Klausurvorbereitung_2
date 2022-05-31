using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Klausurvorbereitung_2
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int N = 2, count = 0;
        Rectangle[] RR;
        Ellipse[] CC;
        bool R_an = true;
        bool C_an = true;
        double h1 = 0, h2 = 0, h3 = 0;
        public MainWindow()
        {
            InitializeComponent();
            RasterErstellen(N);
            Figuren();
        }

        private void RasterErstellen(int n)
        {
            for (int i = 0; i < n; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                Feld.ColumnDefinitions.Add(cd);

                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(1, GridUnitType.Star);
                Feld.RowDefinitions.Add(rd);
            }
        }

        private void Figuren()
        {
            RR = new Rectangle[N];
            CC = new Ellipse[N];
            for (int i = 0; i < N; i++)
            {
                Rectangle r = new Rectangle();
                r.Fill = Brushes.Magenta;
                r.Visibility = Visibility.Hidden;
                RR[i] = r;
                Ellipse e = new Ellipse();
                e.Fill = Brushes.Green;
                e.Stroke = Brushes.Gray;
                e.Visibility = Visibility.Hidden;
                CC[i] = e;

                if (i % 3 == 0)
                {
                    Grid.SetColumn(r, i);
                    Grid.SetRow(r, i);
                    Feld.Children.Add(r);
                }
                else
                {
                    Grid.SetColumn(e, i);
                    Grid.SetRow(e, i);
                    Feld.Children.Add(e);
                }
            }

            

        }

        private void Btn_R_Click(object sender, RoutedEventArgs e)
        {
            if (R_an) for (int i = 0; i < N; i++) RR[i].Visibility = Visibility.Visible;
            else for (int i = 0; i < N; i++) RR[i].Visibility = Visibility.Hidden;
            R_an = !R_an;
        }

        private void Btn_C_Click(object sender, RoutedEventArgs e)
        {
            if (C_an) for (int i = 0; i < N; i++) CC[i].Visibility = Visibility.Visible;
            else for (int i = 0; i < N; i++) CC[i].Visibility = Visibility.Hidden;
            C_an = !C_an;
        }

        private void CB_C_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int x = CB_C.SelectedIndex;
            switch (x)
            {
                case 0:
                    for (int i = 0; i < N; i++)
                    {
                        CC[i].Fill = Brushes.Green;
                        RR[i].Fill = Brushes.Magenta;
                    }
                    break;
                case 1:
                    for (int i = 0; i < N; i++)
                    {
                        CC[i].Fill = Brushes.Blue;
                        RR[i].Fill = Brushes.Yellow;
                    }
                    break;
                case 2:
                    for (int i = 0; i < N; i++)
                    {
                        CC[i].Fill = Brushes.Red;
                        RR[i].Fill = Brushes.Cyan;
                    }
                    break;
            }
        }

        private void ZahlenEingabePrüfungD(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[+-]?[0-9]*(\,|\.)?[0-9]+$");
        }

        private void Btn_CM_MouseEnter(object sender, MouseEventArgs e)
        {
            Canvas.SetLeft(Btn_CM, Canvas.GetLeft(Btn_CM) + Btn_CM.ActualWidth);
        }

        private void ZahlenEingabePrüfungI(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[+-]?[0-9]*$");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double a, h;
            int n;
            if (TB_n.Text == "" || TB_a.Text == "" || TB_h.Text == "" ) return;
            if (Convert.ToInt32(TB_n.Text) < 3)
            {
                MessageBox.Show("n muss größer als 2 und eine ganze Zahl sein.", "Fehler");
                return;
            }
            n = Convert.ToInt32(TB_n.Text);
            a = Convert.ToDouble(TB_a.Text);
            h = Convert.ToDouble(TB_h.Text);

            double V, Ao, Ag, ri;
            ri = a / 2 * 1 / Math.Tan(Math.PI / n);
            Ag = n * a / 2 * ri;
            V = 1 / 3.0 * Ag * h;
            Ao = Ag + n * 0.5 * a * Math.Sqrt(ri * ri + h * h);

            TB_Ausgabe.Text = $"Oberflächeninhalt = {Math.Round(Ao,2)}\n Volumen = {Math.Round(V,2)}";
        }

        private void Btn_W_Click(object sender, RoutedEventArgs e)
        {
            string Ausgabe;
            Random rng = new Random();
            for (int i = 0; i < 1000; i++)
            {
                count++;
                double y = rng.NextDouble();
                if (y <= 0.1)
                {
                    TB_Z.Text = "2";
                    h2++;
                }
                else if (y <= 0.4)
                {
                    TB_Z.Text = "1";
                    h1++;
                }
                else
                {
                    TB_Z.Text = "3";
                    h3++;
                }
            }
            Ausgabe = "h_1 = " + Math.Round(h1 / count, 3) + "\nh_2 = " + Math.Round(h2 / count, 3) + "\nh_3 = " + Math.Round(h3 / count, 3);
            TB_H.Text = Ausgabe;
        }
    }
}
