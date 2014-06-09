using EquationSolver.Contracts;
namespace EquationSolver.Implementation
{
    public class MatrixSolver : IMatrixSolver
    {
        #region IMatrixSolver

        bool IMatrixSolver.Solve(int[,] matrix)
        {
            int rowsCount = matrix.GetLength(0);
            int columnsCount = matrix.GetLength(1);

            for (int i = 0; i < rowsCount && i < columnsCount - 1; i++)
            {
                if (matrix[i, i] != 1)
                {
                    int rowIdx = SearchForFirstNonZero(matrix, i + 1, i, rowsCount);
                    if (rowIdx == -1)
                    {
                        return false;
                    }
                    SwapRows(matrix, i, rowIdx, columnsCount);
                }

                ForwardProcessing(matrix, i, rowsCount, columnsCount);
            }

            for (int i = columnsCount - 2; i > -1; i--)
            {
                BackwardProcessing(matrix, i, columnsCount);
            }

            return true;
        }

        #endregion

        #region Private Methods

        private int SearchForFirstNonZero(int[,] matrix, int rowIdx, int columnIdx, int rowsCount)
        {
            for (int i = rowIdx; i < rowsCount; i++)
            {
                if (matrix[i, columnIdx] == 1)
                {
                    return i;
                }
            }

            return -1;
        }

        private void SwapRows(int[,] matrix, int indexOne, int indexTwo, int columnsCount)
        {
            for (int i = 0; i < columnsCount; ++i)
            {
                var t = matrix[indexOne, i];
                matrix[indexOne, i] = matrix[indexTwo, i];
                matrix[indexTwo, i] = t;
            }
        }

        private void ForwardProcessing(int[,] matrix, int rowIdx, int rowsCount, int columnsCount)
        {
            for (int i = rowIdx + 1; i < rowsCount; i++)
            {
                if (matrix[i, rowIdx] != 0)
                {
                    for (int j = 0; j < columnsCount; j++)
                    {
                        matrix[i, j] = (matrix[i, j] ^ matrix[rowIdx, j]);
                    }
                }
            }
        }

        private void BackwardProcessing(int[,] matrix, int rowIdx, int columnsCount)
        {
            for (int i = rowIdx - 1; i > -1; i--)
            {
                if (matrix[i, rowIdx] != 0)
                {
                    for (int j = 0; j < columnsCount; j++)
                    {
                        matrix[i, j] = (matrix[i, j] ^ matrix[rowIdx, j]);
                    }
                }
            }
        }

        #endregion
    }
}