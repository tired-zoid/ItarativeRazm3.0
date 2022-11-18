using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItarativeRazm
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Connections Matrix"); // ввод матрицы смежности 
            Console.Write("Columns and lines: ");
            int x = int.Parse(Console.ReadLine());
            int[,] masC = new int[x, x];
            Console.WriteLine();

            Console.WriteLine("Fill matrix");

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Console.Write("C[" + i + "," + j + "]: ");
                    masC[i, j] = int.Parse(Console.ReadLine());
                }
            }
            Console.WriteLine();

            for (int i = 0; i < x; i++)  // вывод матрицы смежности 
            {
                for (int j = 0; j < x; j++)
                {
                    Console.Write("C[" + i + "," + j + "]: " + masC[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.ReadLine();


            Console.WriteLine("Distance Matrix"); // ввод матрицы расстояний
            Console.Write("Columns and lines: ");
            int y = int.Parse(Console.ReadLine());
            int[,] masD = new int[y, y];
            Console.WriteLine();

            Console.WriteLine("Fill matrix");

            for (int i = 0; i < y; i++) 
            {
                for (int j = 0; j < y; j++)
                {
                    Console.Write("D[" + i + "," + j + "]: ");
                    masD[i, j] = int.Parse(Console.ReadLine());
                }
            }
            Console.WriteLine();

            for (int i = 0; i < y; i++) // вывод матрицы расстояний
            {
                for (int j = 0; j < y; j++)
                {
                    Console.Write("D[" + i + "," + j + "]: " + masD[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.ReadLine();


            int[] degD = new int[y];  // считаем степени позиций 
            int sumDdeg = 0;

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    sumDdeg += masD[i, j];
                }

                degD[i] = sumDdeg;
                sumDdeg = 0;
            }

            int[] degC = new int[x];   // считаем степени вершин 
            int sumCdeg = 0;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    sumCdeg += masC[i, j];
                }

                degC[i] = sumCdeg;
                sumCdeg = 0;
            }

            int[] degCsorted = new int[x]; // сортируем степени вершин 
            Array.Copy(degC, degCsorted, x);
            Array.Sort(degCsorted);
            Array.Reverse(degCsorted);

            for (int j = 0; j < degCsorted.Length; j++)
            {
                Console.Write("  {0} ", degCsorted[j]);
            }
            Console.ReadLine();

            int[] degDsorted = new int[y]; // сортируем степени позиций 
            Array.Copy(degD, degDsorted, y);
            Array.Sort(degDsorted);

            for (int j = 0; j < degDsorted.Length; j++)
            {
                Console.Write("  {0} ", degDsorted[j]);
            }
            Console.ReadLine();


            int[] conSort = new int[x];
            int kC = 0;
            int[] disSort = new int[y];
            int kD = 0;


            for (int i = 0; i < degCsorted.Length; i++) // заносим сортированные вершины в строку для вывода 
            {
                for (int j = 0; j < degC.Length; j++) 
                {
                    if (degCsorted[i] == degC[j])
                    {
                        degC[j] = 9999;
                        conSort[kC] = j + 1;
                        kC++;

                    }
                }
              
            }

            Console.WriteLine("Sorted elements:");

            for (int j = 0; j < conSort.Length; j++)
            {
                Console.Write("  {0} ", conSort[j]);
            }
            Console.ReadLine();



            for (int i = 0; i < degDsorted.Length; i++) // заносим сортированные позиции в строку для вывода 
            {
                for (int j = 0; j < degD.Length; j++)
                {
                    if (degDsorted[i] == degD[j])
                    {
                        degD[j] = 9999;
                        disSort[kD] = j + 1;
                        kD++;
                    }
                }

            }

            Console.WriteLine("Sorted positions:");

            for (int j = 0; j < disSort.Length; j++)
            {
                Console.Write("  {0} ", disSort[j]);
            }
            Console.ReadLine();


            int[,] BEGINRAZM = new int[2, x];

                for (int j = 0; j < x; j++)
                {
                BEGINRAZM[0, j] = conSort[j];
                BEGINRAZM[1, j] = disSort[j];
                }

                for (int j = 0; j < x; j++)
            Console.WriteLine(" {0}  {1} ", BEGINRAZM[0, j], BEGINRAZM[1, j]);

            // меняем строки в таблице позиций для полученного размещения 

            int[,] bRDis = new int[y, y];
            int[,] begRazDis = new int[y, y];


            int k = 0;
            int l = 0;
            int m = 0;
            int n = 0;
            int tem = 0;

            while(m != conSort.Length)
            {
                for (int p = 0; p < bRDis.GetLength(1); p++)
                {
                    k = conSort[m] - 1; // переписываем строки в нужном порядке 
                    l = disSort[n] - 1; // 
                    bRDis[k, p] = masD[l, p];

                }

                m++;
                n++;
            }

            k = 0;
            l = 0;
            m = 0;
            n = 0;


            for (int i = 0; i < BEGINRAZM.GetLength(1); i++) // переставляем столбцы в матрице расстояний 
            {
                for (int j = 0; j < begRazDis.GetLength(0); j++)
                {
                    k = BEGINRAZM[0, i] - 1;
                    l = BEGINRAZM[1, i] - 1;
                    begRazDis[j, k] = bRDis[j, l];
                }

            }


            Console.WriteLine("New matrix distance:"); // вывод новой матрицы расстояний 

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Console.Write(" newD[" + i + "," + j + "]: " + begRazDis[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.ReadLine();


            // подсчет всех дельта 

            int delF = 0;
            int[] iteration = new int[x * x];
            int e1 = 0;
            int e2 = 0;
            int maxDelF = 0;
            int g = 0;
            bool stillHave = true;
            int topForCh1 = 0;
            int topForCh2 = 0;
            int[] v1 = new int[x * x];
            int[] v2 = new int[x * x];
             

            while (stillHave == true)
            {
                stillHave = false;

                for (int i = 0; i < conSort.Length; i++)
                {
                    e1 = conSort[i] - 1; 

                    for (int f = i + 1; f < conSort.Length; f++)
                    {
                        e2 = conSort[f] - 1;
                        for (int j = 0; j < masC.GetLength(1); j++)
                        {
                            if (j == e1 || j == e2)
                            {
                                delF += 0;
                            }
                            else 
                            delF += ( (masC[e1, j] - masC[e2, j]) *   (begRazDis[e1, j] - begRazDis[e2, j] ));
                        }

                        Console.WriteLine("Elements for change: delF = {0}  v1 = {1}, v2 = {2}", delF, e1 + 1, e2 + 1);
                        if (delF > 0)
                        {
                            stillHave = true;
                            Console.WriteLine("Succes!");
                            Console.ReadLine();
                            iteration[g] = delF;
                            v1[g] = e1;
                            v2[g] = e2;
                            g++;
                        }
                        else
                        {
                            Console.WriteLine("Not for us...");
                            Console.ReadLine();
                        }
                        delF = 0;
                    }

                    if (stillHave == true) 
                    {
                        maxDelF = iteration[0];

                        for (int w = 0; w < iteration.Length; w++)
                        {
                            if (iteration[w] > maxDelF)
                            {
                                maxDelF = iteration[w];
                                topForCh1 = v1[w];
                                topForCh2 = v2[w];
                            }
                        }

                    }


                }

                for (int j = 0; j < iteration.Length; j++)
                {
                    if (iteration[j] > 0)
                        Console.Write("  {0} ", iteration[j]);
                }
                Console.ReadLine();

                int temp = 0;
                 // перестановка вершин 
                for (int p = 0; p < masC.GetLength(1); p++)
                {
                    for (int q = 0; q < masC.GetLength(1); q++)

                    {
                        temp = 0;
                        temp = masC[topForCh1, p];
                        masC[topForCh1, p] = masC[topForCh2, p];
                        masC[topForCh2, p] = temp;

                        temp = 0;
                        temp = masC[q,topForCh1];
                        masC[q,topForCh1] = masC[q, topForCh2];
                        masC[q, topForCh2] = temp;
                    }
                }
                    for (int q = 0; q < iteration.Length; q++)
                {
                    iteration[q] = 0;
                }

                Console.Write("My choice for change: delF = {0}  v1 = {1}, v2 = {2}", maxDelF, topForCh1, topForCh2);
                Console.ReadLine();

            }


            int function = 0; // подсчет целевой функции 
             for (int i = 0; i < masC.GetLength(0); i++)
            {
                for (int j = 0; j < masC.GetLength(0); j ++)
                {
                    function += masC[i, j] * begRazDis[i, j];
                }
            }

            Console.WriteLine("All the reshuffles were made!!!");
            Console.WriteLine("Final function: {0}", function);
            Console.ReadLine();


        }
    }
}
