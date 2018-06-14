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
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Komiwojażer_genetyczny
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string[] Miasto;
        int Posobnicy;
        int Krzyżowania;
        int Rozmiarmax;
        int Liniepliku;
        int[,] Dystans;
        int[,] Populacja;
        int[,] Populacjapomocnicza;
        int[] DystansPopulacji;
        int[] DystansPopulacjipomocniczy;
        public int WybraneMiasto;
        int[] Liczbylosowe;
        int[] Korkinajleszego;

        public MainWindow()
        {
            InitializeComponent();
            iniciujlisboxy();
        }

        void iniciujzmienne()
        {
            Liniepliku = 0;
            foreach (string line in File.ReadLines(listboxobszar.SelectedItem.ToString() + @"\miasta.txt"))
            {
                if (line != String.Empty) ++Liniepliku;
            }
            Dystans = new int[Liniepliku, Liniepliku];
            Korkinajleszego = new int[Liniepliku + 1];
        }

        void iniciujlisboxy()
        {
            listboxobszar.Items.Add("pl");
            listboxobszar.Items.Add("usa");
            

            listboxalgorytm.Items.Add("Tsp");
            listboxalgorytm.Items.Add("Erc");
        }

        void wczytajdane()
        {

            iniciujzmienne();


            Miasto = new string[Liniepliku];
            int i = 0;
            const Int32 BufferSize = 128;

            

            using (var fileStream = File.OpenRead(listboxobszar.SelectedItem.ToString() + "/miasta.txt"))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    Miasto[i] = line;
                    i++;
                }
                streamReader.Dispose();
            }




            using (var fileStream = File.OpenRead(listboxobszar.SelectedItem.ToString() + "/odl.txt"))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;



                i = 0;
                while ((line = streamReader.ReadLine()) != null)
                {

                    for (int j = 0; j < Liniepliku; j++)
                    {
                        string[] split = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        Dystans[i, j] = Convert.ToInt32(split[j]);


                    }

                    i++;

                }
                streamReader.Dispose();
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            wczytajdane();

            listamiast.Items.Clear();
            for (int i = 0; i < Miasto.Length; i++)
            {
                listamiast.Items.Add(Miasto[i]);
            }


        }

        void Losowanie(int n, int k)
        {
            Random rand = new Random();
            // wypełnianie tablicy liczbami 1,2...n
            int[] numbers = new int[n];

            int nk = 0;
            for (int i = 0; i < n + 1; i++)
            {
                if (i != WybraneMiasto)
                {
                    numbers[nk] = i;
                    nk++;
                }

            }


            // losowanie k liczb

            for (int i = 0; i < k; i++)
            {

                // tworzenie losowego indeksu pomiędzy 0 i n - 1
                int r = rand.Next(0, n);



                // wybieramy element z losowego miejsca

                Liczbylosowe[i] = 0;
                Liczbylosowe[i] = numbers[r];

                // przeniesienia ostatniego elementu do miejsca z którego wzięliśmy
                numbers[r] = numbers[n - 1];
                n--;
            }
        }


        void FillPopulacja()
        {

            for (int i = 0; i < Posobnicy; i++)
            {
                Liczbylosowe = new int[Liniepliku-1];
                for (int z = 0; z < 10000; z++)
                {
                    Losowanie(Liniepliku - 1, Liniepliku - 1);
                }


                for (int j = 0; j < Liniepliku - 1; j++)
                {

                    //Text.Text += Liczbylosowe[j] + " ";
                    Populacja[i, j] = Liczbylosowe[j];

                }
                //Text.Text += System.Environment.NewLine;
            }
        }

        void SumaDystans()
        {
            for (int i = 0; i < Rozmiarmax; i++)
            {
                DystansPopulacji[i] = 0;
                int a = WybraneMiasto;
                int b = 0;

                for (int j = 0; j < Liniepliku - 1; j++)
                {
                    b = Populacja[i, j];
                    DystansPopulacji[i] += Dystans[a, b];

                    a = b;

                }
                b = WybraneMiasto;
                DystansPopulacji[i] += Dystans[a, b];
            }

        }

        void Krokinajlepszegosobnika()
        {
            for (int i = 0; i < 1; i++)
            {
                DystansPopulacji[i] = 0;
                int a = WybraneMiasto;
                int b = 0;

                for (int j = 0; j < Liniepliku - 1; j++)
                {
                    b = Populacja[i, j];
                    Korkinajleszego[j] = Dystans[a, b];

                    a = b;

                }
                b = WybraneMiasto;
                Korkinajleszego[Liniepliku - 1] = Dystans[a, b];
            }
        }




        void Obliczenia()
        {

            switch (listboxalgorytm.SelectedItem.ToString())
            {
                case "Tsp":
                    {
                        Tsp();
                        break;
                    }
                case "Erc":
                    {
                        Erc();
                        break;
                    }
            }
                


            SumaDystans();

            for (int i = 0; i < DystansPopulacji.Length; i++)
            {
                DystansPopulacjipomocniczy[i] = DystansPopulacji[i];
            }

            int h = Convert.ToInt32(ilośćmutacjibox.Text);
            for (int i = 0; i < h; i++)
            {
                Mutacja();
            }



            //QuickSort(DystansPopulacji, 0, DystansPopulacji.Length - 1);

            Array.Sort(DystansPopulacji);

            Sort();

            for (int i = Posobnicy; i < Rozmiarmax; i++)
            {
                for (int j = 0; j < Liniepliku - 1; j++)
                {
                    Populacja[i, j] = 0;
                }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (listamiast.SelectedItem != null)
            {

                WybraneMiasto = listamiast.SelectedIndex;
                Posobnicy = Convert.ToInt32(osobnicysestartowi.Text);
                Krzyżowania = Convert.ToInt32(krzyżowaniatext.Text);
                Rozmiarmax = Posobnicy + Krzyżowania + Krzyżowania;

                Populacja = new int[Rozmiarmax , Liniepliku - 1];
                Populacjapomocnicza = new int[Rozmiarmax , Liniepliku - 1];
                DystansPopulacji = new int[Rozmiarmax ];
                DystansPopulacjipomocniczy = new int[Rozmiarmax ];


                

                FillPopulacja();
                int k;
                k = Convert.ToInt32(Iteracjetextbox.Text);

                for (int z = 0; z < k; z++)
                {
                    Obliczenia();
                }
              

                Krokinajlepszegosobnika();

                Wyswietlanie();


                //for (int i = 0; i < Rozmiarmax; i++)
                //{
                //    for (int j = 0; j < Liniepliku-1; j++)
                //    {
                //        Text.Text += Populacja[i, j] + System.Environment.NewLine;
                //    }
                //}


            }
            else
            {
                MessageBox.Show("Wybierz miasto");
            }
        }


        void Wyswietlanie()
        {
            Text.Text = "";
            Text.Text += listamiast.SelectedItem.ToString() + System.Environment.NewLine;

            Text.Text += "Wybrana trasa " + DystansPopulacji[1] + "km " + System.Environment.NewLine;
            Text.Text += "Start w " + listamiast.SelectedItem.ToString();
            Text.Text += System.Environment.NewLine;

            int Sumapokroku = 0;
            for (int i = 0; i < Liniepliku - 1; i++)
            {
                int ner = Populacja[1, i];
                Sumapokroku += Korkinajleszego[i];

                Text.Text += "Przebyto " + Korkinajleszego[i] + "km do " + Miasto[ner] + " (" + Sumapokroku + "km) ";

                Text.Text += System.Environment.NewLine;
            }
            Sumapokroku += Korkinajleszego[Liniepliku-1];
            Text.Text += "Przebyto " + Korkinajleszego[Liniepliku-1] + "km do " + listamiast.SelectedItem.ToString() + " (" + Sumapokroku + "km ) ";
        }

        void Mutacja()
        {
            Random rand = new Random();
            int r1;
            int r2;
            int r3;
            int pomocnicza = 0;
            r1 = rand.Next(0, Liniepliku-1);
            r2 = rand.Next(0, Liniepliku-1);
            r3 = rand.Next(3, Rozmiarmax-1);

            pomocnicza = Populacja[r3, r1];
            Populacja[r3, r1] = Populacja[r3, r2];
            Populacja[r3, r2] = pomocnicza;
        }

        public int[] Crossover(int[] orginalParent1, int[] orginalParent2)
        {

            int[] parent1 = (int[])orginalParent1.Clone();
            int[] parent2 = (int[])orginalParent2.Clone();

            int L = parent1.Length;
            int[] child = new int[L];


            Random R = new Random();
            int p = R.Next(L);
            int startNumber = parent1[p];


            int[] numNeighbors = new int[L+1];
            for (int i = 0; i < L; i++)
            {
                numNeighbors[i] = 4;
                child[i] = -2;
            }

            child[0] = startNumber;


            for (int n = 1; n < L; n++)
            {


                //cross out the selected point by setting it to -1
                //update the number of neighbors of its neighbors

                int n1 = p > 0 ? parent1[p - 1] : parent1[L - 1];
                int n2 = p < L - 1 ? parent1[p + 1] : parent1[0];

                if (n1 > -1 && parent1[n1] > -1)
                    numNeighbors[n1]--;
                if (n2 > -1 && parent1[n2] > -1)
                    numNeighbors[n2]--;

                parent1[p] = -1;

                for (int i = 0; i < L; i++)
                {
                    if (parent2[i] == startNumber)
                    {
                        parent2[i] = -1;
                        int n3 = i > 0 ? parent2[i - 1] : parent2[L - 1];
                        int n4 = i < L - 1 ? parent2[i + 1] : parent2[0];

                        if (n3 > -1 && parent2[n3] > -1)
                            numNeighbors[n3]--;
                        if (n4 > -1 && parent2[n4] > -1)
                            numNeighbors[n4]--;

                        break;
                    }
                }

                int cos;
                int innecos;
                //look for the point with the lowest numNeighbors
                int minNumNeighbors = 5;
                List<int> minI = new List<int>();
                for (int i = 0; i < L; i++)
                {
                   
                    if (parent1[i] > -1)
                    {
                        cos = parent1[i];
                        if (numNeighbors[parent1[i]] < minNumNeighbors)
                        {
                            minNumNeighbors = numNeighbors[parent1[i]];
                            if (minNumNeighbors == 0)
                                break;
                        }
                    }
                        
                        
                    
                }

                for (int i = 0; i < L; i++)
                {
                    if (parent1[i] > -1)
                        if (numNeighbors[parent1[i]] == minNumNeighbors)
                        {
                            minI.Add(i);
                        }
                }


                int x = R.Next(minI.Count);
                p = minI[x];


                startNumber = parent1[p];

                child[n] = startNumber;


            }

            return child;
        }

        


        void Erc()
        {
            Random rand = new Random();
            int r1;
            int r2;
            
            int[] tab1 = new int[Liniepliku - 1];
            int[] tab2 = new int[Liniepliku - 1];
            int[] tab3 = new int[Liniepliku - 1];

            int[] ra = new int[6];
            int[] pomoc = new int[6];


            for (int i = 0; i < Krzyżowania*2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    ra[j] = rand.Next(0, Posobnicy);
                    pomoc[j] = DystansPopulacji[ra[j]];
                }

                Array.Sort(pomoc,ra);


                for (int m = 0; m < Liniepliku - 1; m++)
                {
                    tab1[m] = Populacja[ra[0], m];
                    tab2[m] = Populacja[ra[1], m];
                }


                //r1 = rand.Next(0, Posobnicy);
                //L:
                //r2 = rand.Next(0, Posobnicy);


                //for (int m = 0; m < Liniepliku -1; m++)
                //{
                //    tab1[m] = Populacja[r1, m];
                //    tab2[m] = Populacja[r2, m];
                //}

                tab3 = Crossover(tab1, tab2);

                for (int n = 0; n < Liniepliku -1; n++)
                {
                    Populacja[Posobnicy + i, n] = tab3[n];
                }
            }
        }



        void Tsp()
        {
            Random rand = new Random();

            int pierwszapołowa = (Liniepliku-1)/2;
            int r1;
            int r2;
            int[] ra = new int[6];
            int[] pomoc = new int[6];


            int osobnicy =Posobnicy;
            int[] byl = new int[pierwszapołowa+1];
            int[] pomocnicza = new int[pierwszapołowa+1];

            int[] byl2 = new int[pierwszapołowa+1];
            int[] pomocnicza2 = new int[pierwszapołowa + 1];
            int warunek = 0;

            for (int i = 0; i < Krzyżowania; i++)
            {

                for (int j = 0; j < 6; j++)
                {
                    ra[j] = rand.Next(0, Posobnicy);
                    pomoc[j] = DystansPopulacji[ra[j]];
                }

                Array.Sort(pomoc, ra);

                r1 = ra[0];
                
                r2 = ra[1];

                //r1 = rand.Next(0, osobnicy-2);
                //L:
                //r2 = rand.Next(0, osobnicy-2);
                //if (r1 == r2) goto L;

                //wypełnienie 1 części
                for (int j = 0; j < pierwszapołowa; j++)
                {
                    Populacja[osobnicy, j] = Populacja[r1, j];
                    byl[j] = Populacja[r1, j];
                    
                }

                //wypełnienie 1 częsci 2 osobnika
                
                for (int j = 0; j < pierwszapołowa; j++)
                {
                    Populacja[osobnicy + Krzyżowania, j] = Populacja[r2, j];
                    byl2[j] = Populacja[r2, j];

                }



                int nr = 0;

                for (int h = 0; h < Liniepliku - 1; h++)
                {
                    
                    for (int p = 0; p < pierwszapołowa; p++)
                    {
                        if(Populacja[r2,h]!=byl[p] )
                        {
                            warunek = 0;                                                        
                        }  
                        else
                        {
                            warunek = 1;
                            break;
                        }
                    }
                    if (warunek == 0)
                    {
                        pomocnicza[nr] = Populacja[r2, h];
                        nr++;
                    }                                           
                }

                nr = 0;

                for (int h = 0; h < Liniepliku - 1; h++)
                {

                    for (int p = 0; p < pierwszapołowa; p++)
                    {
                        if (Populacja[r1, h] != byl2[p])
                        {
                            warunek = 0;
                        }
                        else
                        {
                            warunek = 1;
                            break;
                        }
                    }
                    if (warunek == 0)
                    {
                        pomocnicza2[nr] = Populacja[r1, h];
                        nr++;
                    }
                }


                //wypełnienie 2 częsci
                nr = 0;

                for (int k = pierwszapołowa; k < Liniepliku-1 ; k++)
                {
                    Populacja[osobnicy, k] = pomocnicza[nr];
                    Populacja[osobnicy+Krzyżowania, k] = pomocnicza2[nr];
                    nr++;
                }
                osobnicy++;
            }

            
        }

        private void Iteracjetextbox_PeviewTextUnput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        public static void QuickSort(int[] array, int left, int right)
        {
            var i = left;
            var j = right;
            var pivot = array[(left + right) / 2];
            while (i < j)
            {
                while (array[i] < pivot) i++;
                while (array[j] > pivot) j--;
                if (i <= j)
                {
                    // swap
                    var tmp = array[i];
                    array[i++] = array[j];  // ++ and -- inside array braces for shorter code
                    array[j--] = tmp;
                }
            }
            if (left < j) QuickSort(array, left, j);
            if (i < right) QuickSort(array, i, right);
        }

        void Sort()
        {
            int[] kolejnosc = new int[Rozmiarmax];


            for (int i = 0; i < kolejnosc.Length; i++)
            {
                kolejnosc[i] = i;
            }

            for (int i = 0; i < kolejnosc.Length; i++)
            {

                for (int j = 0; j < kolejnosc.Length; j++)
                {
                    if (DystansPopulacji[i] == DystansPopulacjipomocniczy[j])
                    {
                        kolejnosc[i] = j;
                        break;
                    }

                }
                //Text.Text += kolejnosc[i] + " ";

            }

            for (int i = 0; i < Rozmiarmax; i++)
            {
                for (int j = 0; j < Liniepliku -1; j++)
                {
                    Populacjapomocnicza[i, j] = Populacja[kolejnosc[i], j];
                }
            }

            for (int i = 0; i < Rozmiarmax; i++)
            {
                for (int j = 0; j < Liniepliku -1; j++)
                {
                    Populacja[i, j] = Populacjapomocnicza[i, j];
                }
            }

        }


    }
}
