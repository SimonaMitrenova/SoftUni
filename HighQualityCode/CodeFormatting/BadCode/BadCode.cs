using System;

namespace useless
{
    static class СевероЗапад
    {
        static int sum(int[,] arr, int x0, int y0, int x1, int y1)
        {
            int sum = 0, x, y;
            for (x = x0; x <= x1; x++)
                for (y = y0; y <= y1; y++)
                    sum += arr[x, y];
            return sum;
        }
        static void write(int[,] arr, int w = 0)
        {
            for (int x = 0; x < arr.GetLength(0); x++)
            {
                for (int y = 0; y < arr.GetLength(1); y++)
                    Console.Write(arr[x, y].ToString().PadLeft(w));
                Console.WriteLine();
            }
        }
        static void fill(int[,] arr, int x = 0, int y = 0)
        {
            int a, b,
                n = arr.GetLength(0) - 1,
                m = arr.GetLength(1) - 1;
            while (x < n && y < m)
            {
                a = arr[x, m] - sum(arr, x, 0, x, y);
                b = arr[n, y] - sum(arr, 0, y, x, y);
                if (a < b)
                    arr[x++, y] = a;
                else
                    arr[x, y++] = b;
            }
        }
        static void Main(string[] args)//только для теста
        {
            int[,] arr ={ 
                {  0,  0,  0,  0, 15 },
                {  0,  0,  0,  0, 5 },
                {  0,  0,  0,  0, 5 },
                { 10,  7,  5,  3, 25 }
                        };
            write(arr, 3);
            Console.WriteLine();
            fill(arr);
            write(arr, 3);
            Console.Read();
        }
    }
}