namespace Matrix
{
    using System;

    public class MatrixCalculator
    {
        public static void Main()
        {
            double[,] firstMatrix = { { 1, 3 }, { 5, 7 } };
            double[,] secondMatrix = { { 4, 2 }, { 1, 5 } };

            double[,] result = Multiply(firstMatrix, secondMatrix);

            for (int row = 0; row < result.GetLength(0); row++)
            {
                for (int col = 0; col < result.GetLength(1); col++)
                {
                    Console.Write(result[row, col] + " ");
                }

                Console.WriteLine();
            }
        }

        public static double[,] Multiply(double[,] firstMatrix, double[,] secondMatrix)
        {
            if (firstMatrix.GetLength(1) != secondMatrix.GetLength(0))
            {
                throw new InvalidOperationException("First matrix columns must be equal to second matrix rows.");
            }

            int elementsCount = firstMatrix.GetLength(1);

            int newMatrixRows = firstMatrix.GetLength(0);
            int newMatrixCols = secondMatrix.GetLength(1);

            double[,] multypliedMatrix = new double[newMatrixRows, newMatrixCols];

            for (int row = 0; row < newMatrixRows; row++)
            {
                for (int col = 0; col < newMatrixCols; col++)
                {
                    for (int current = 0; current < elementsCount; current++)
                    {
                        multypliedMatrix[row, col] += firstMatrix[row, current] * secondMatrix[current, col];
                    }
                }
            }

            return multypliedMatrix;
        }
    }
}