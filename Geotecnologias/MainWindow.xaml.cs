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

namespace Geotecnologias
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double a = 6378137.0000;
        double b = 6356752.3141;
        double f;
        double e2;
        double elinha2;
        double n;
        double h;
        double P;
        double fi;

        double LatiG;
        double LatiM;
        double LatiS;
        double LatitudeG;
        double LatitudeR;

        double LongG;
        double LongM;
        double LongS;
        double LongitudeG;
        double LongitudeR;

        double XX;
        double YY;
        double ZZ;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalcularUTM_Click(object sender, RoutedEventArgs e)
        {

            //Captura de dados
            h = Convert.ToDouble(Altgeo.Text);
            LatiG = Convert.ToDouble(LatG.Text);
            LatiM = Convert.ToDouble(LatM.Text);
            LatiS = Convert.ToDouble(LatS.Text);
            LongG = Convert.ToDouble(LonG.Text);
            LongM = Convert.ToDouble(LonM.Text);
            LongS = Convert.ToDouble(LonS.Text);

            //Latitude em graus e rad
            if (LatiG >= 0)
            {
                LatitudeG = LatiG + (LatiM / 60) + (LatiS / 3600);
                LatitudeR = (LatitudeG * Math.PI) / 180;
            }
            else
            {
                LatitudeG = LatiG - (LatiM / 60) - (LatiS / 3600);
                LatitudeR = (LatitudeG * Math.PI) / 180;
            }

            //Longitude em graus e rad

            if (LongG >= 0)
            {
                LongitudeG = LongG + (LongM / 60) + (LongS / 3600);
                LongitudeR = (LongitudeG * Math.PI) / 180;
            }
            else
            {
                LongitudeG = LongG - (LongM / 60) - (LongS / 3600);
                LongitudeR = (LongitudeG * Math.PI) / 180;
            }


            //Calculo das droga dos paremetro
            f = (a-b)/a;
            e2 = 2*f - Math.Pow(f,2);
            n = a/(Math.Sqrt(1-e2*Math.Pow(Math.Sin(LatitudeR),2)));


            //Final
            XX = (n+h)*Math.Cos(LatitudeR)*Math.Cos(LongitudeR); 
            YY = (n + h) * Math.Cos(LatitudeR) * Math.Sin(LongitudeR);
            ZZ = (n * (1 - e2) + h) * Math.Sin(LatitudeR) ;

            //Exibição dos Karai
            MessageBox.Show("Resultados" + Environment.NewLine + "X:  "  + Convert.ToString(XX) + Environment.NewLine + "Y:  "  + Convert.ToString(YY) + Environment.NewLine + "Z:  "  + Convert.ToString(ZZ) + Environment.NewLine, "top");
            //MessageBox.Show("LatitudeG  " + Convert.ToString(LatitudeG) + "LatitudeR  " + Convert.ToString(LatitudeR));
        }

        private void CalcularGeo_Click(object sender, RoutedEventArgs e)
        {
            //Captura de dados
            XX = Convert.ToDouble(X.Text);
            YY = Convert.ToDouble(Y.Text);
            ZZ = Convert.ToDouble(Z.Text);



            //Calculo das droga dos paremetro
            f = (a - b) / a;
            e2 = 2 * f - Math.Pow(f, 2);
            elinha2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(b, 2);
            P = Math.Sqrt(Math.Pow(XX,2)+Math.Pow(YY,2));
            fi = Math.Atan((ZZ*a)/(P*b));


            //Resultados e o n
            LatitudeR = Math.Atan((ZZ+(elinha2*b*Math.Pow(Math.Sin(fi),3)))/(P-(e2*a*Math.Pow(Math.Cos(fi),3))));
            LongitudeR = Math.Atan(YY/XX);
            n = a / Math.Sqrt(1 - (e2 * Math.Pow(Math.Sin(LatitudeR), 2)));
            h =(P/Math.Cos(LatitudeR))-n;

            //Conversoes Finais
            LatitudeG = LatitudeR * 180 / Math.PI;
            LatiG = Math.Truncate(LatitudeG);
  
                LatiM = Math.Truncate((Math.Abs(LatitudeG) - Math.Abs(LatiG)) * 60);
                LatiS = (Math.Abs(LatitudeG) - Math.Abs(LatiG) - (LatiM / 60)) * 3600;


            

            LongitudeG = LongitudeR * 180 / Math.PI;
            LongG = Math.Truncate(LongitudeG);

            LongM = Math.Truncate((Math.Abs(LongitudeG) - Math.Abs(LongG)) * 60);
                LongS = Math.Abs(LongitudeG) - Math.Abs(LongG) - (LongM / 60) * 3600;
            


            //Exibição dos Karai
            MessageBox.Show("Latitude:" + Environment.NewLine + Convert.ToString(LatiG)+ "°" + Convert.ToString(LatiM) + "'" + Convert.ToString(LatiS) + "'" + Environment.NewLine + "Longitude:" + Environment.NewLine + Convert.ToString(LongG) + "°" + Convert.ToString(LongM) + "'" + Convert.ToString(LongS) + "'", "Top");
            //MessageBox.Show("n " + Convert.ToString(n) + " h " + Convert.ToString(h) + " P " + Convert.ToString(P) +  " fi " + Convert.ToString(fi) + " XX " + Convert.ToString(XX) + " YY " + Convert.ToString(YY));
        }

        private void SalvarDatum_Click(object sender, RoutedEventArgs e)
        {
            a = Convert.ToDouble(aa.Text);
            b = Convert.ToDouble(bb.Text);
            MessageBox.Show("Salvo!", "Deu TOP", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }
    }
}