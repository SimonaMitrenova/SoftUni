namespace BadCodeReformatted
{
    using System;

    public static class Matrix
    {
        public static int SumMatrixArea(
            int[,] matrix,
            int starterXPoint,
            int starterYPoint,
            int endXPoint,
            int endYPoint)
        {
            int sum = 0;

            for (int row = starterXPoint; row <= endXPoint; row++)
            {
                for (int y = starterYPoint; y <= endYPoint; y++)
                {
                    sum += matrix[row, y];
                }
            }

            return sum;
        }

        public static void PrintMatrix(int[,] matrix, int offset = 0)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col].ToString().PadLeft(offset));
                }

                Console.WriteLine();
            }
        }

        public static void Fill(int[,] matrix, int x = 0, int y = 0)
        {
            int rows = matrix.GetLength(0) - 1;
            int columns = matrix.GetLength(1) - 1;

            while (x < rows && y < columns)
            {
                int a = matrix[x, columns] - SumMatrixArea(matrix, x, 0, x, y);
                int b = matrix[rows, y] - SumMatrixArea(matrix, 0, y, x, y);
                if (a < b)
                {
                    matrix[x++, y] = a;
                }
                else
                {
                    matrix[x, y++] = b;
                }
            }
        }

        public static void Main()
        {
            int[,] numbersMatrix =
                {
                    { 0, 0, 0, 0, 15 }, 
                    { 0, 0, 0, 0, 5 }, 
                    { 0, 0, 0, 0, 5 }, 
                    { 10, 7, 5, 3, 25 }
                };

            PrintMatrix(numbersMatrix, 3);

            Console.WriteLine();
            Fill(numbersMatrix);
            PrintMatrix(numbersMatrix, 3);

            Console.WriteLine();
            Fill(numbersMatrix);
            PrintMatrix(numbersMatrix, 3);
        }
    }
}
