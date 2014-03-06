using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatrixBuilder
{
    class MatrixOperations
    {

        private static Random rand = new Random();
        public static float offset = .001f; //used for float comparisons

        public MatrixOperations()
        {

        }

        //Can be considered augmented or not
        public float[,] generateRandomMatrix(int rows, int cols, int min, int max)
        {
            float[,] matrix = new float[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rand.Next(min, max);
                }
            }

            return matrix;
        }

        //Is augmented
        //This method has specific answers
        public float[,] generateUniqueSolutionMatrix(int rows, int cols, int min, int max, float[] answer)
        {
            float[,] matrix = new float[rows, cols];
            bool hasUniqueSolution = false;
            while (!hasUniqueSolution)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols - 1; j++)
                    {
                        matrix[i, j] = rand.Next(min, max);
                        matrix[i, cols - 1] = matrix[i, cols - 1] + matrix[i, j] * answer[j];
                    }
                }
                float[,] tempMatrix = this.copyMatrix(matrix);
                tempMatrix = reduceMatrix(tempMatrix);
                if (checkForRowOfZeros(tempMatrix) == -1 && !checkForInconsistentMatrix(tempMatrix))
                {
                    hasUniqueSolution = true;
                }
                else
                {
                    matrix = new float[rows, cols];
                }
            }

            return matrix;
        }

        //Is augmented
        //Will have non integer answers
        public float[,] generateUniqueSolutionMatrix(int rows, int cols, int min, int max)
        {
            float[,] matrix = new float[rows, cols];
            bool hasUniqueSolution = false;
            while (!hasUniqueSolution)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] = rand.Next(min, max);
                    }
                }
                float[,] tempMatrix = this.copyMatrix(matrix);
                tempMatrix = reduceMatrix(tempMatrix);
                this.copyMatrix(matrix);
                if (checkForRowOfZeros(tempMatrix) == -1 && !checkForInconsistentMatrix(tempMatrix))
                {
                    hasUniqueSolution = true;
                }
                else
                {
                    matrix = new float[rows, cols];
                }
            }

            return matrix;
        }

        //Is augmented
        public float[,] generateInconsistentMatrix(int rows, int cols, int min, int max)
        {
            float[,] matrix = new float[rows, cols];
            bool isInconsistent = false;
            while (!isInconsistent)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] = rand.Next(min, max);
                    }
                }
                float[,] tempMatrix = this.copyMatrix(matrix);
                tempMatrix = reduceMatrix(tempMatrix);
                if (checkForRowOfZeros(tempMatrix) == -1 && checkForInconsistentMatrix(tempMatrix))
                {
                    isInconsistent = true;
                }
                else
                {
                    matrix = new float[rows, cols];
                }
            }

            return matrix;
        }

        //Considered Augmented
        public float[,] generateMatrixWithFreeVariables(int rows, int cols, int min, int max, float[] answer, int numOfFreeVars)
        {
            float[,] matrix = new float[rows, cols];
            bool hasFreeVariables = false;
            while (!hasFreeVariables)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                    {
                        matrix[i, j] = rand.Next(min, max);
                        matrix[i, matrix.GetLength(1) - 1] = matrix[i, matrix.GetLength(1) - 1] + matrix[i, j] * answer[j];
                    }
                }
                float[,] tempMatrix = this.copyMatrix(matrix);
                tempMatrix = reduceMatrix(tempMatrix);
                if (checkForRowOfZeros(tempMatrix) != -1 && checkForRowOfZeros(matrix) == -1
                        && !checkForInconsistentMatrix(tempMatrix))
                {
                    int counter = 0;
                    int row = checkForRowOfZeros(tempMatrix);
                    while (row != -1)
                    {
                        tempMatrix[row, 0] = 1;
                        row = checkForRowOfZeros(tempMatrix);
                        counter++;
                    }
                    if (counter == numOfFreeVars)
                    {
                        hasFreeVariables = true;
                    }
                }
                else
                {
                    matrix = new float[matrix.GetLength(0), matrix.GetLength(1)];
                }
            }

            return matrix;
        }

        public float[,] generateRandomIdentityMatrix(int size, int min, int max)
        {
            float[,] matrix = null;
            bool isIdentity = false;
            while (!isIdentity)
            {
                matrix = new float[size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        matrix[i, j] = rand.Next(min, max);
                    }
                }
                float[,] tempMatrix = this.copyMatrix(matrix);
                tempMatrix = reduceMatrix(tempMatrix);
                if (checkForRowOfZeros(tempMatrix) == -1)
                {
                    isIdentity = true;
                }
            }

            return matrix;
        }

        public float[] generateRandomVector(int size, int min, int max)
        {
            float[] vector = new float[size];
            for (int i = 0; i < size; i++)
                vector[i] = rand.Next(min, max);
            return vector;
        }

        //When reduced, has a pivot row for every column. For use when rows>=cols

        public float[,] generateIndependentMatrix(int rows, int cols, int min, int max)

        {

            bool independent = false;

            float[,] matrix = new float[rows,cols];

            if (rows >= cols)

            {

                while (!independent)

                {

                    matrix = generateRandomMatrix(rows, cols, min, max);

                    float[,] reduced = new float[rows,cols];

                    for (int i = 0; i < rows; i++)

                    {

                        for (int j = 0; j < cols; j++)

                        {

                            reduced[i, j] = matrix[i, j];

                        }

                    }

                    reduced = reduceMatrix(reduced);

                    independent = checkMainDiagonalForOnes(reduced, cols);

                }

            }

            return matrix;

        }



        //When reduced, doesn't have a pivot row for every column.

        public float[,] generateDependentMatrix(int rows, int cols, int min, int max)

        {

            bool independent = true;

            float[,] matrix = new float[rows, cols];

            if (rows >= cols)

            {

                while (independent)

                {

                    matrix = generateRandomMatrix(rows, cols, min, max);

                    float[,] reduced = new float[rows, cols];

                    for (int i = 0; i < rows; i++)

                    {

                        for (int j = 0; j < cols; j++)

                        {

                            reduced[i, j] = matrix[i, j];

                        }

                    }

                    reduced = reduceMatrix(reduced);

                    independent = checkMainDiagonalForOnes(reduced, cols);

                }

            }

            else matrix = generateRandomMatrix(rows, cols, min, max);

            return matrix;

        }

        //Checks every column for a 1 along the main diagonal (for use with independence/dependence, will break if rows>cols)

        public bool checkMainDiagonalForOnes(float[,] matrix, int columns)

        {

            bool onlyones = true;

            float[,] tempMatrix = matrix;

            float[,] reducedMatrix = reduceMatrix(tempMatrix);

            for (int columnpointer = 0; columnpointer < columns; columnpointer++)

            {

                if (reducedMatrix[columnpointer, columnpointer] != 1)

                {

                    onlyones = false;

                    break;

                }

            }

            return onlyones;

        }

        public bool checkColumnEquality(float[,] matrixBase, float[,] matrixCompare)
        {
            bool columnEquality = true;

            if (matrixBase.GetLength(0) == matrixCompare.GetLength(0) && matrixBase.GetLength(1) == matrixCompare.GetLength(1))
            {
                List<float[]> compareColumns= new List<float[]>();
                for (int compareColumn = 0; compareColumn < matrixCompare.GetLength(1); compareColumn++)
                {
                    float[] column = new float[matrixCompare.GetLength(0)];
                    for (int compareRow = 0; compareRow < matrixCompare.GetLength(0); compareRow++)
                    {
                        column[compareRow] = matrixCompare[compareRow, compareColumn];
                    }
                    compareColumns.Add(column);
                }

                List<float[]> baseColumns = new List<float[]>();
                for (int baseColumn = 0; baseColumn < matrixBase.GetLength(1); baseColumn++)
                {
                    float[] column = new float[matrixBase.GetLength(0)];
                    for (int baseRow = 0; baseRow < matrixBase.GetLength(0); baseRow++)
                    {
                        column[baseRow] = matrixBase[baseRow, baseColumn];
                    }
                    baseColumns.Add(column);
                }

                for (int i = 0; i < baseColumns.Count; i++)
                {
                    bool match = true;
                    float[] baseArray = baseColumns[i];
                    for (int curColumn = 0; curColumn < compareColumns.Count; curColumn++)
                    {
                        float[] compareArray = compareColumns[curColumn];
                        match = true;
                        for (int j = 0; j < baseArray.Length; j++)
                        {
                            if (baseArray[j] != compareArray[j])
                            {
                                match = false;
                                break;
                            }
                        }
                        if (match)
                        {
                            compareColumns.Remove(compareColumns[curColumn]);
                            break;
                        }
                    }
                    if (!match)
                    {
                        columnEquality = false;
                        break;
                    }
                }
            }
            else columnEquality = false;
            return columnEquality;
        }

        public float[,] reduceMatrix(float[,] matrix)
        {
            int numOfRows = matrix.GetLength(0);
            int numOfCols = matrix.GetLength(1);
            bool zeroColumn = false;

            //For each column reduce the coefficients under it to 0
            for (int col = 0; col < numOfRows; col++)
            {
                int tRow = col;
                //Handles when n >= m
                if (numOfCols <= col)
                {
                    break;
                }

                if (zeroColumn == true && numOfRows + 1 < numOfCols)
                {
                    tRow -= 1;
                }
                else if (tRow == numOfRows && numOfRows + 1 < numOfCols)
                {
                    break;
                }

                zeroColumn = false;
                //if the current diagonal = 0 then swap rows until it doesn't
                if (matrix[tRow, col] == 0)
                {
                    int index = col;
                    while (matrix[tRow, col] == 0)
                    {
                        if (index < numOfRows - 1)
                        {
                            matrix = rowSwap(tRow, index + 1, matrix);
                            index++;
                        }
                        else
                        {
                            int pivotRow = checkForPivotRowAbove(col, matrix);
                            if (pivotRow != -1)
                            {
                                matrix = rowSwap(tRow, pivotRow, matrix);
                            }
                            else
                            {
                                zeroColumn = true;
                            }
                            break;
                        }
                    }
                }

                if (!zeroColumn)
                {
                    //Makes the current diagonal = 1
                    if (matrix[tRow, col] != 1)
                    {
                        matrix = timesScalar(1 / matrix[tRow, col], tRow, matrix);
                    }

                    //This is what reduces the coefficients under the current column to 0
                    //by multiplying the current row by the first coefficient of the next
                    //row and changing the sign.
                    for (int row = 0; row < numOfRows; row++)
                    {
                        if (!(Math.Abs(matrix[row, col]) < offset) && row != col)
                        {
                            float scalar = matrix[row, col] * (-1);
                            matrix = addMultipleOfRow(scalar, tRow, row, matrix);
                            for (int i = 0; i < numOfCols; i++)
                            {
                                if (Math.Abs(matrix[row, i]) < offset)
                                {
                                    matrix[row, i] = 0;
                                }
                            }
                        }
                    }
                }
            }

            return matrix;
        }

        //Used only during matrix reduction
        private int checkForPivotRowAbove(int currentCol, float[,] matrix)
        {
            int pivotRow = -1;
            int counter = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, currentCol] != 0)
                {
                    for (int j = 0; j < currentCol; j++)
                    {
                        if (Math.Abs(matrix[i, j]) < offset)
                        {
                            counter++;
                        }
                    }
                    if (counter == currentCol)
                    {
                        pivotRow = i;
                        break;
                    }
                    counter = 0;
                }
            }

            return pivotRow;
        }

        //Used during matrix generation
        public static bool checkForInconsistentMatrix(float[,] matrix)
        {
            bool inconsistent = false;
            int counter = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    if (!(Math.Abs(matrix[i, j]) < offset))
                    {
                        break;
                    }
                    counter++;
                }
                if (counter == matrix.GetLength(1) - 1 && !(Math.Abs(matrix[i, matrix.GetLength(1) - 1]) < offset))
                {
                    inconsistent = true;
                    break;
                }
                counter = 0;
            }

            return inconsistent;
        }

        //Used during matrix generation
        public static int checkForRowOfZeros(float[,] matrix)
        {
            int freeRow = -1;
            int counter = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!(Math.Abs(matrix[i, j]) < offset))
                    {
                        break;
                    }
                    counter++;
                }
                if (counter == matrix.GetLength(1))
                {
                    freeRow = i;
                    break;
                }
                counter = 0;
            }

            return freeRow;
        }

        //Used when checking row operations
        public ArrayList findChangedRows(float[,] matrix, float[,] matrix2)
        {
            ArrayList changedRowsIndex = new ArrayList();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] != matrix2[i, j])
                    {
                        changedRowsIndex.Add(i);
                        break;
                    }
                }
            }

            return changedRowsIndex;
        }

        public float[,] timesScalar(float scalar, int row, float[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
                matrix[row, i] = matrix[row, i] * scalar;
            return matrix;
        }

        public bool checkTimesScalar(float[,] oldMatrix, float[,] newMatrix)
        {
            bool isTimesScalar = true;
            float floatOffset = .001f;
            float constant = 0;
            int index = 0;
            ArrayList changedRows = findChangedRows(oldMatrix, newMatrix);
            if (changedRows.Count == 1)
            {
                int[] rows = (int[])changedRows.ToArray(typeof(int));
                int row = rows[0];
                while (index < oldMatrix.GetLength(1))
                {
                    if (oldMatrix[row, index] != 0)
                    {
                        constant = newMatrix[row, index] / oldMatrix[row, index];
                        break;
                    }
                    index++;
                }
                for (int i = 0; i < oldMatrix.GetLength(1); i++)
                {
                    if (Math.Abs(constant * oldMatrix[row, i] - newMatrix[row, i]) > floatOffset)
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

        public float[,] rowSwap(int row1, int row2, float[,] matrix)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                float temp = matrix[row1, j];
                matrix[row1, j] = matrix[row2, j];
                matrix[row2, j] = temp;
            }

            return matrix;
        }

        public bool checkRowSwap(float[,] oldMatrix, float[,] newMatrix)
        {
            bool isRowSwap = true;
            ArrayList changedRows = findChangedRows(oldMatrix, newMatrix);
            if (changedRows.Count == 2)
            {
                int[] rows = (int[])changedRows.ToArray(typeof(int));
                int row1 = rows[0];
                int row2 = rows[1];
                for (int i = 0; i < oldMatrix.GetLength(1); i++)
                {
                    if (oldMatrix[row1, i] != newMatrix[row2, i] || oldMatrix[row2, i] != newMatrix[row1, i])
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

        public float[,] addMultipleOfRow(float scalar, int actionRow, int targetRow, float[,] matrix)
        {
            float[] rowHolder = new float[matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                rowHolder[i] = matrix[actionRow, i];
            }
            matrix = timesScalar(scalar, actionRow, matrix);
            for (int i = 0; i < matrix.GetLength(1); i++)
                matrix[targetRow, i] += matrix[actionRow, i];

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                matrix[actionRow, i] = rowHolder[i];
            }
            return matrix;
        }

        public bool checkAddMultipleOfRow(float[,] oldMatrix, float[,] newMatrix)
        {
            bool isAddMultipleOfRow = false;
            ArrayList changedRows = findChangedRows(oldMatrix, newMatrix);
            if (changedRows.Count == 1)
            {
                int[] rows = (int[])changedRows.ToArray(typeof(int));
                int changedRow = rows[0];
                float[,] rowDifference = this.copyMatrix(oldMatrix);
                for (int i = 0; i < oldMatrix.GetLength(1); i++)
                {
                    rowDifference[0, i] = oldMatrix[changedRow, i] - newMatrix[changedRow, i];
                }

                for (int j = 0; j < oldMatrix.GetLength(0); j++)
                {
                    if (j != changedRow)
                    {
                        if (checkTimesScalar(oldMatrix, rowDifference))
                        {
                            isAddMultipleOfRow = true;
                            break;
                        }
                    }
                    if ((j + 1) != oldMatrix.GetLength(0))
                        rowSwap(j, j + 1, rowDifference);
                    for (int k = 0; k < oldMatrix.GetLength(1); k++)
                    {
                        rowDifference[j, k] = oldMatrix[j, k];
                    }
                }
            }

            return isAddMultipleOfRow;
        }

        //Will error with incorrect size matrices
        public float[,] matrixMultiplication(float[,] matrix, float[,] matrix2)
        {
            float[,] product = null;
            if (matrix.GetLength(1) == matrix2.GetLength(0))
            {
                product = new float[matrix.GetLength(0), matrix2.GetLength(1)];
                for (int i = 0; i < product.GetLength(0); i++)
                {
                    float[] vector = new float[matrix2.GetLength(0)];
                    for (int k = 0; k < matrix2.GetLength(0); k++)
                    {
                        vector[k] = matrix2[k, i];
                    }
                    for (int j = 0; j < product.GetLength(1); j++)
                    {
                        float[] row = new float[matrix.GetLength(1)];
                        for (int k = 0; k < row.GetLength(1); k++)
                        {
                            row[k] = matrix[j, k];
                        }
                        product[j, i] = dotProduct(row, vector);
                    }
                }

            }

            return product;
        }

        public float[,] matrixScalarMultiplication(float[,] matrix, float scalar)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = scalar * matrix[i, j];
                }
            }
            return matrix;
        }

        public float dotProduct(float[] vector1, float[] vector2)
        {
            float result = 0;
            for (int i = 0; i < vector1.GetLength(0); i++)
            {
                result += vector1[i] * vector2[i];
            }
            return result;
        }

        public float findDeterminant(float[,] matrix)
        {
            float determinant = 0;
            if (matrix.GetLength(0) == 2)
            {
                determinant = (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);
            }
            else if (matrix.GetLength(0) > 2)
            {
                int curColIndex = 0;
                while (curColIndex < matrix.GetLength(0))
                {
                    float[,] detMatrix = new float[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
                    int detRow = 0;
                    int detCol = 0;
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (i != 0 && j != curColIndex)
                            {
                                detMatrix[detRow, detCol] = matrix[i, j];
                                detCol++;
                            }
                        }
                        if (i != 0)
                        {
                            detRow++;
                        }
                        detCol = 0;
                    }
                    float scalar = matrix[0, curColIndex];
                    if (Math.Abs(scalar) != 0)
                    {
                        if ((curColIndex % 2) == 0)
                        {
                            determinant += scalar * findDeterminant(detMatrix);
                        }
                        else if ((curColIndex % 2) == 1)
                        {
                            determinant -= scalar * findDeterminant(detMatrix);
                        }
                    }
                    curColIndex++;
                }
            }
            return determinant;
        }

        public float[,] findInverse(float[,] matrix)
        {
            float[,] inverse = null;
            if (matrix.GetLength(0) == matrix.GetLength(1))
            {
                inverse = new float[matrix.GetLength(0), matrix.GetLength(1)];
                float[,] matrixWithInverse = new float[matrix.GetLength(0), matrix.GetLength(1) * 2];
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrixWithInverse[i, j] = matrix[i, j];
                    }
                }
                int row = 0;
                for (int j = matrix.GetLength(1); j < matrixWithInverse.GetLength(1); j++)
                {
                    matrixWithInverse[row, j] = 1;
                    row++;
                }

                matrixWithInverse = reduceMatrix(matrixWithInverse);

                for (int i = 0; i < inverse.GetLength(0); i++)
                {
                    for (int j = 0; j < inverse.GetLength(1); j++)
                    {
                        inverse[i, j] = matrixWithInverse[i, j + matrix.GetLength(1)];
                    }
                }
            }

            return inverse;
        }

        public bool checkMatrixEquality(float[,] matrix, float[,] matrix2)
        {
            bool isEqual = true;

            if (matrix.GetLength(0) == matrix2.GetLength(0) && matrix.GetLength(1) == matrix2.GetLength(1))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (Math.Abs(matrix[i, j] - matrix2[i, j]) >= offset)
                        {
                            isEqual = false;
                            break;
                        }
                    }
                    if (!isEqual)
                    {
                        break;
                    }
                }
            }
            else
            {
                isEqual = false;
            }
            return isEqual;
        }

        public String checkForIdentity(float[,] matrix, float[,] sesmatrix)
        {

            if (matrix.GetLength(0) == sesmatrix.GetLength(0) && matrix.GetLength(1) == sesmatrix.GetLength(1))
            {
                sesmatrix = reduceMatrix(sesmatrix);
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] != sesmatrix[i,j])
                        {
                            return "<div>You have not successfully reduced your matrix.<div>\n";
                        }
                    }
                }
            }
            else
            {
                return "<div>You have an incorrect size for your answer matrix.<div>\n";
            }
            return "";
        }

        protected float[,] copyMatrix(float[,] matrix)
        {
            float[,] newArray = new float[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newArray[i, j] = matrix[i, j];
                }
            }

            return newArray;
        }

        public String checkSingleRowOperation(Dictionary<int, float[,]> matrixMap)
        {
            String feedback = "";
            for (int i = 1; i < matrixMap.Count(); i++)
            {
                float[,] matrix1 = null;
                matrixMap.TryGetValue(i - 1, out matrix1);
                float[,] matrix2 = null;
                matrixMap.TryGetValue(i, out matrix2);
                if (checkMatrixEquality(matrix1, matrix2))
                {
                    feedback += "<div>No row operation between matrix " + i + " and matrix " + (i + 1) + ".<div>";
                } else if (!checkAddMultipleOfRow(matrix1, matrix2) && !checkTimesScalar(matrix1, matrix2)
                        && !checkRowSwap(matrix1, matrix2))
                {
                    feedback += "<div>Mistake between matrix " + i + " and matrix " + (i + 1) + ".<div>";
                }
            }
            return feedback.Equals("") ? null : feedback;
        }

        public String checkSingleRowOperationInverseQuestion(Dictionary<int, float[,]> matrixMap)
        {
            String feedback = "";
            for (int i = 1; i < matrixMap.Count(); i++)
            {
                float[,] matrix1 = null;
                matrixMap.TryGetValue(i, out matrix1);
                float[,] matrix2 = null;
                matrixMap.TryGetValue(i + 1, out matrix2);
                if (checkMatrixEquality(matrix1, matrix2))
                {
                    feedback += "<div>No row operation between matrix " + i + " and matrix " + (i + 1) + ".<div>";
                }
                else if (!checkAddMultipleOfRow(matrix1, matrix2) && !checkTimesScalar(matrix1, matrix2)
                      && !checkRowSwap(matrix1, matrix2))
                {
                    feedback += "<div>Mistake between matrix " + i + " and matrix " + (i + 1) + ".<div>";
                }
            }
            return feedback.Equals("") ? null : feedback;
        }

        public String checkAnswers(float[] correctAnswers, float[] studentAnswers)
        {
            String feedback = "";
            if (correctAnswers.Length == studentAnswers.Length)
            {
                for (int i = 0; i < correctAnswers.Length; i++)
                {
                    float correctNumber = correctAnswers[i];
                    float studentNumber = studentAnswers[i];
                    if (Math.Abs(correctNumber - studentNumber) > offset)
                    {
                        feedback += "<div>Answer for x<sub>" + (i + 1) + " </sub>  is incorrect.<div>\n";
                    }
                }
            }
            else
            {
                feedback = "Your answer has an incorrect number of variables.";
            }
            return feedback.Equals("") ? null : feedback;
        }

        //overload method for checking answers as matrices
        public String checkAnswers(Dictionary<int, float[,]> correctAnswers, Dictionary<int, float[,]> studentAnswers)
        {
            String feedback = "";
            float[,] matrix1 = null;
            correctAnswers.TryGetValue(0, out matrix1);
            float[,] matrix2 = null;
            studentAnswers.TryGetValue(0, out matrix2);
            if (!checkMatrixEquality(matrix1, matrix2))
            {
                feedback += "<div>Your reduced matrix is incorrect.<div>\n";
            }
            return feedback.Equals("") ? null : feedback;
        }

        public String checkFreeVariableAnswers(float[,] startingMatrix, String[] studentAnswers)
        {
            String feedback = "";
            if (startingMatrix.GetLength(0) == studentAnswers.Length)
            {
                startingMatrix = reduceMatrix(startingMatrix);
                List<int> rowsToSolve = findRowsToSolve(startingMatrix);
                float[,] solvedMatrix = solveForFreeVariables(startingMatrix, rowsToSolve);
                float[,] studentMatrix = parseStudentAnswer(studentAnswers);
                bool t = checkMatrixEquality(solvedMatrix, studentMatrix);
                bool c = !checkMatrixEquality(solvedMatrix, studentMatrix);
                if (!checkMatrixEquality(solvedMatrix, studentMatrix))
                {
                    feedback += "<div>Incorrect solution provided, check your answers.</div>";
                }
            }
            else
            {
                feedback = "Your answer has an incorrect number of variables.";
            }
            return feedback.Equals("") ? null : feedback;
        }

        private float[,] parseStudentAnswer(String[] studentAnswers) {
        float[,] studentMatrix = new float[studentAnswers.Length,studentAnswers.Length + 1];
        for (int row = 0; row < studentAnswers.Length; row++) {
            if (!studentAnswers[row].Contains("F")) {
                String stringRow = studentAnswers[row];
                String[] variables = stringRow.Split(',');
                foreach (String variable in variables) {
                    if (variable.Contains("@")) {
                        String[] valueAndIndex = variable.Split('@');
                        float value = float.Parse(valueAndIndex[0]);
                        int col = int.Parse(valueAndIndex[1]);
                        studentMatrix[row,col] = value;
                    } else {
                        studentMatrix[row,studentAnswers.Length] = float.Parse(variable);
                    }
                }
                studentMatrix[row,row] = 1f;
            }
        }
        return studentMatrix;
    }

        private float[,] solveForFreeVariables(float[,] matrix, List<int> rowsToSolve) {
        foreach (int row in rowsToSolve) {
            
            for (int i = 0; i < matrix.GetLength(1); i++) {
                if (i != row) {
                    if (i == matrix.GetLength(1) - 1)
                    {
                        matrix[row, i] = matrix[row, i] / matrix[row, row];
                    } else {
                        if (matrix[row, i] != 0)
                        {
                            matrix[row, i] = (matrix[row, i] / matrix[row, row]) * (-1);
                        }
                    }
                }
            }
        }
        return matrix;
    }

        private List<int> findRowsToSolve(float[,] matrix)
        {
            List<int> freeRows = new List<int>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i,i] != 0)
                {
                    freeRows.Add(i);
                }
            }

            return freeRows;
        }

        public int countOperationsNeeded(float[,] origMatrix)
        {
            float[,] matrix = copyMatrix(origMatrix);
            int numOfRows = matrix.GetLength(0);
            int numOfCols = matrix.GetLength(1);
            int numOfRowOps = 0;
            bool zeroColumn = false;

            //For each column reduce the coefficients under it to 0
            for (int col = 0; col < numOfCols - 1; col++)
            {
                //Handles when n >= m
                int tRow = col;
                if (numOfCols <= col)
                {
                    break;
                }
                if (zeroColumn == true && numOfRows + 1 < numOfCols)
                {
                    tRow -= 1;
                }
                else if (tRow == numOfRows && numOfRows + 1 < numOfCols)
                {
                    break;
                }

                zeroColumn = false;
                //if the current diagonal = 0 then swap rows until it doesn't
                if (matrix[tRow, col] == 0)
                {
                    int index = col;
                    while (matrix[tRow, col] == 0)
                    {
                        if (index < numOfRows - 1)
                        {
                            matrix = rowSwap(tRow, index + 1, matrix);
                            numOfRowOps++;
                            index++;
                        }
                        else
                        {
                            int pivotRow = checkForPivotRowAbove(col, matrix);
                            if (pivotRow != -1)
                            {
                                matrix = rowSwap(tRow, pivotRow, matrix);
                                numOfRowOps++;
                            }
                            else
                            {
                                zeroColumn = true;
                            }
                            break;
                        }
                    }
                }

                if (!zeroColumn)
                {
                    //Makes the current diagonal = 1
                    if (matrix[tRow, col] != 1)
                    {
                        matrix = timesScalar(1 / matrix[tRow, col], tRow, matrix);
                        numOfRowOps++;
                    }

                    //This is what reduces the coefficients under the current column to 0
                    //by multiplying the current row by the first coefficient of the next
                    //row and changing the sign.
                    for (int row = 0; row < numOfRows; row++)
                    {
                        if (!(Math.Abs(matrix[row, col]) <= offset) && row != col)
                        {
                            float scalar = matrix[row, col] * (-1);
                            matrix = addMultipleOfRow(scalar, tRow, row, matrix);
                            numOfRowOps++;
                            for (int i = 0; i < numOfCols; i++)
                            {
                                if (Math.Abs(matrix[row, i]) < offset)
                                {
                                    matrix[row, i] = 0;
                                }
                            }
                        }
                    }
                }
            }

            return numOfRowOps;
        }

        public bool checkInverse(float[,] matrix, float[,] inverse)
        {
            bool isInverse = true;
            int numOfRows = matrix.GetLength(0);
            int numOfCols = matrix.GetLength(1);
            if (matrix.GetLength(0) == inverse.GetLength(0) && matrix.GetLength(1) == inverse.GetLength(1))
            {
                float[,] product = crossProduct(matrix, inverse);
                for (int i = 0; i < numOfRows; i++)
                {
                    for (int j = 0; j < numOfCols; j++)
                    {
                        if (i == j && Math.Abs(1 - product[i,j]) > offset)
                        {
                            isInverse = false;
                        }
                        else if (i != j && Math.Abs(product[i,j]) > offset)
                        {
                            isInverse = false;
                        }
                    }
                }
            }
            else
            {
                isInverse = false;
            }
            return isInverse;
        }

        public float[,] crossProduct(float[,] matrix, float[,] matrix2)
        {
            float[,] product = null;
            if (matrix.GetLength(1) == matrix2.GetLength(0))
            {
                product = new float[matrix.GetLength(0), matrix2.GetLength(1)];
                for (int i = 0; i < product.GetLength(0); i++)
                {
                    float[] vector = new float[matrix2.GetLength(0)];
                    for (int k = 0; k < matrix2.GetLength(0); k++)
                    {
                        vector[k] = matrix2[k, i];
                    }
                    for (int j = 0; j < product.GetLength(1); j++)
                    {
                        float[] row = new float[matrix.GetLength(1)];
                        for (int k = 0; k < row.GetLength(0); k++)
                        {
                            row[k] = matrix[j, k];
                        }
                        product[j, i] = dotProduct(row, vector);
                    }
                }

            }

            return product;
        }
    }
}
