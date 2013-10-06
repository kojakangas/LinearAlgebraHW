using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinearHomeworkInterface.components
{
    public class MatrixBuilder
    {

        public float[][] createIdentity(int rows, int cols, List<int> solution) {
        float[][] matrix = new float[rows][];

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (j != cols - 1) {
                    if (i == j) {
                        matrix[i][j] = 1;
                    } else {
                        matrix[i][j] = 0;
                    }
                } else {
                    matrix[i][j] = solution.ElementAt(i);
                }
            }
        }

        return matrix;
    }

        public List<int> findChangedRows(float[][] matrix, float[][] matrix2)
        {
            List<int> changedRowsIndex = new List<int>();

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i][j] != matrix2[i][j])
                    {
                        changedRowsIndex.Add(i);
                        break;
                    }
                }
            }

            return changedRowsIndex;
        }

        public float[][] timesScalar(float scalar, int row, float[][] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                matrix[row][i] = matrix[row][i] * scalar;
            return matrix;
        }

        public bool checkTimesScalar(float[][] oldMatrix, float[][] newMatrix)
        {
            bool isTimesScalar = true;
            float floatOffset = .001f;
            float constant = 0;
            int index = 0;
            List<int> changedRows = findChangedRows(oldMatrix, newMatrix);
            if (changedRows.Count() == 1)
            {
                int row = changedRows.ElementAt(0);
                while (index < oldMatrix.GetLength(0))
                {
                    if (oldMatrix[row][index] != 0)
                    {
                        constant = newMatrix[row][index] / oldMatrix[row][index];
                        break;
                    }
                    index++;
                }
                for (int i = 0; i < oldMatrix.GetLength(0); i++)
                {
                    if (Math.Abs(constant * oldMatrix[row][i] - newMatrix[row][i]) > floatOffset)
                    {
                        isTimesScalar = false;
                        break;
                    }
                }
            }
            else if (changedRows.Count != 0)
            {
                isTimesScalar = false;
            }
            return isTimesScalar;
        }

        public float[][] rowSwap(int row1, int row2, float[][] matrix)
        {
            float[] temp = matrix[row1];
            matrix[row1] = matrix[row2];
            matrix[row2] = temp;

            return matrix;
        }

        public bool checkRowSwap(float[][] oldMatrix, float[][] newMatrix)
        {
            bool isRowSwap = true;
            List<int> changedRows = findChangedRows(oldMatrix, newMatrix);
            if (changedRows.Count == 2)
            {
                int row1 = changedRows.ElementAt(0);
                int row2 = changedRows.ElementAt(1);
                for (int i = 0; i < oldMatrix.Length; i++)
                {
                    if (oldMatrix[row1][i] != newMatrix[row2][i] || oldMatrix[row2][i] != newMatrix[row1][i])
                    {
                        isRowSwap = false;
                        break;
                    }
                }
            }
            else
            {
                isRowSwap = false;
            }

            return isRowSwap;
        }

        public float[][] addMultipleOfRow(float scalar, int actionRow, int targetRow, float[][] matrix)
        {
            float[] rowHolder = matrix[actionRow];
            matrix = timesScalar(scalar, actionRow, matrix);
            for (int i = 0; i < matrix.GetLength(0); i++)
                matrix[targetRow][i] += matrix[actionRow][i];

            matrix[actionRow] = rowHolder;
            return matrix;
        }

        public bool checkAddMultipleOfRow(float[][] oldMatrix, float[][] newMatrix) {
        bool isAddMultipleOfRow = false;
        List<int> changedRows = findChangedRows(oldMatrix, newMatrix);
        if (changedRows.Count == 1) {
            int changedRow = changedRows.ElementAt(0);
            float[][] rowDifference = new float[oldMatrix.Length][];
            for (int i = 0; i < oldMatrix.Length; i++) {
                for (int j = 0; j < oldMatrix.GetLength(0); j++)
                {
                    rowDifference[i][j] = oldMatrix[i][j];
                }
            }
            for (int i = 0; i < oldMatrix.GetLength(0); i++) {
                rowDifference[0][i] = oldMatrix[changedRow][i] - newMatrix[changedRow][i];
            }

            for (int j = 0; j < oldMatrix.Length; j++) {
                if (j != changedRow) {
                    if (checkTimesScalar(oldMatrix, rowDifference)) {
                        isAddMultipleOfRow = true;
                        break;
                    }
                }
                if ((j + 1) != oldMatrix.Length)
                    rowSwap(j, j + 1, rowDifference);
                rowDifference[j] = oldMatrix[j];
            }
        }

        return isAddMultipleOfRow;
    }
    }
}