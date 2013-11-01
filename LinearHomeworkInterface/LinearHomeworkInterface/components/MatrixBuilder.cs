﻿using System;
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

        public MatrixOperations() {

        }

        //Can be considered augmented or not
        public float[,] generateRandomMatrix(int rows, int cols, int min, int max) {
        float[,] matrix = new float[rows,cols];

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                matrix[i,j] = rand.Next(min,max);
            }
        }

        return matrix;
    }

        //Is augmented
        //This method has specific answers
        public float[,] generateUniqueSolutionMatrix(int rows, int cols, int min, int max, float[] answer) {
        float[,] matrix = new float[rows,cols];
        bool hasUniqueSolution = false;
        while (!hasUniqueSolution) {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols - 1; j++) {
                    matrix[i,j] = rand.Next(min,max);
                    matrix[i,cols - 1] = matrix[i,cols - 1] + matrix[i,j] * answer[j];
                }
            }
            float[,] tempMatrix = this.copyMatrix(matrix);
            tempMatrix = reduceMatrix(tempMatrix);
            if (checkForRowOfZeros(tempMatrix) == -1 && !checkForInconsistentMatrix(tempMatrix)) {
                hasUniqueSolution = true;
            } else {
                matrix = new float[rows,cols];
            }
        }

        return matrix;
    }

        //Is augmented
        //Will have non integer answers
        public float[,] generateUniqueSolutionMatrix(int rows, int cols,int min, int max) {
        float[,] matrix = new float[rows,cols];
        bool hasUniqueSolution = false;
        while (!hasUniqueSolution) {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    matrix[i,j] = rand.Next(min,max);
                }
            }
            float[,] tempMatrix = this.copyMatrix(matrix);
            tempMatrix = reduceMatrix(tempMatrix);
            this.copyMatrix(matrix);
            if (checkForRowOfZeros(tempMatrix) == -1 && !checkForInconsistentMatrix(tempMatrix)) {
                hasUniqueSolution = true;
            } else {
                matrix = new float[rows,cols];
            }
        }

        return matrix;
    }

        //Is augmented
        public float[,] generateInconsistentMatrix(int rows, int cols, int min, int max) {
        float[,] matrix = new float[rows,cols];
        bool isInconsistent = false;
        while (!isInconsistent) {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    matrix[i,j] = rand.Next(min,max);
                }
            }
            float[,] tempMatrix = this.copyMatrix(matrix);
            tempMatrix = reduceMatrix(tempMatrix);
            if (checkForRowOfZeros(tempMatrix) == -1 && checkForInconsistentMatrix(tempMatrix)) {
                isInconsistent = true;
            } else {
                matrix = new float[rows,cols];
            }
        }

        return matrix;
    }

        //Considered Augmented
        public float[,] generateMatrixWithFreeVariables(int rows, int cols, int min, int max, float[] answer, int numOfFreeVars) {
        float[,] matrix = new float[rows,cols];
        bool hasFreeVariables = false;
        while (!hasFreeVariables) {
            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1) - 1; j++) {
                    matrix[i,j] = rand.Next(min,max);
                    matrix[i,matrix.GetLength(1) - 1] = matrix[i,matrix.GetLength(1) - 1] + matrix[i,j] * answer[j];
                }
            }
            float[,] tempMatrix = this.copyMatrix(matrix);
            tempMatrix = reduceMatrix(tempMatrix);
            if (checkForRowOfZeros(tempMatrix) != -1 && checkForRowOfZeros(matrix) == -1
                    && !checkForInconsistentMatrix(tempMatrix)) {
                int counter = 0;
                int row = checkForRowOfZeros(tempMatrix);
                while (row != -1) {
                    tempMatrix[row,0] = 1;
                    row = checkForRowOfZeros(tempMatrix);
                    counter++;
                }
                if (counter == numOfFreeVars) {
                    hasFreeVariables = true;
                }
            } else {
                matrix = new float[matrix.GetLength(0),matrix.GetLength(1)];
            }
        }

        return matrix;
    }

        public float[,] generateRandomIdentityMatrix(int size, int min, int max) {
        float[,] matrix = null;
        bool isIdentity = false;
        while (!isIdentity) {
            matrix = new float[size,size];
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    matrix[i,j] = rand.Next(min,max);
                }
            }
            float[,] tempMatrix = this.copyMatrix(matrix);
            tempMatrix = reduceMatrix(tempMatrix);
            if (checkForRowOfZeros(tempMatrix) == -1) {
                isIdentity = true;
            }
        }

        return matrix;
    }

        public float[,] reduceMatrix(float[,] matrix)
        {
            int numOfRows = matrix.GetLength(0);
            int numOfCols = matrix.GetLength(1);
            bool zeroColumn = false;

            //For each column reduce the coefficients under it to 0
            for (int col = 0; col < numOfRows; col++)
            {
                //Handles when n >= m
                if (numOfCols <= col)
                {
                    break;
                }

                zeroColumn = false;
                //if the current diagonal = 0 then swap rows until it doesn't
                if (matrix[col,col] == 0)
                {
                    int index = col;
                    while (matrix[col,col] == 0)
                    {
                        if (index < numOfRows - 1)
                        {
                            matrix = rowSwap(col, index + 1, matrix);
                            index++;
                        }
                        else
                        {
                            int pivotRow = checkForPivotRowAbove(col, matrix);
                            if (pivotRow != -1)
                            {
                                matrix = rowSwap(col, pivotRow, matrix);
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
                    if (matrix[col,col] != 1)
                    {
                        matrix = timesScalar(1 / matrix[col,col], col, matrix);
                    }

                    //This is what reduces the coefficients under the current column to 0
                    //by multiplying the current row by the first coefficient of the next
                    //row and changing the sign.
                    for (int row = 0; row < numOfRows; row++)
                    {
                        if (!(Math.Abs(matrix[row,col]) < offset) && row != col)
                        {
                            float scalar = matrix[row,col] * (-1);
                            matrix = addMultipleOfRow(scalar, col, row, matrix);
                            for (int i = 0; i < numOfCols; i++)
                            {
                                if (Math.Abs(matrix[row,i]) < offset)
                                {
                                    matrix[row,i] = 0;
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
                if (matrix[i,currentCol] != 0)
                {
                    for (int j = 0; j < currentCol; j++)
                    {
                        if (Math.Abs(matrix[i,j]) < offset)
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
                    if (!(Math.Abs(matrix[i,j]) < offset))
                    {
                        break;
                    }
                    counter++;
                }
                if (counter == matrix.GetLength(1) - 1 && !(Math.Abs(matrix[i,matrix.GetLength(1) - 1]) < offset))
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
                    if (!(Math.Abs(matrix[i,j]) < offset))
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
                    if (matrix[i,j] != matrix2[i,j])
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
                matrix[row,i] = matrix[row,i] * scalar;
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
                int row = changedRows.IndexOf(0);
                while (index < oldMatrix.GetLength(1))
                {
                    if (oldMatrix[row,index] != 0)
                    {
                        constant = newMatrix[row,index] / oldMatrix[row,index];
                        break;
                    }
                    index++;
                }
                for (int i = 0; i < oldMatrix.GetLength(1); i++)
                {
                    if (Math.Abs(constant * oldMatrix[row,i] - newMatrix[row,i]) > floatOffset)
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
                int row1 = changedRows.IndexOf(0);
                int row2 = changedRows.IndexOf(1);
                for (int i = 0; i < oldMatrix.GetLength(0); i++)
                {
                    if (oldMatrix[row1,i] != newMatrix[row2,i] || oldMatrix[row2,i] != newMatrix[row1,i])
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
                rowHolder[i] = matrix[actionRow,i];
            }
            matrix = timesScalar(scalar, actionRow, matrix);
            for (int i = 0; i < matrix.GetLength(1); i++)
                matrix[targetRow,i] += matrix[actionRow,i];

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                matrix[actionRow,i] = rowHolder[i];
            }
            return matrix;
        }

        public bool checkAddMultipleOfRow(float[,] oldMatrix, float[,] newMatrix) {
        bool isAddMultipleOfRow = false;
        ArrayList changedRows = findChangedRows(oldMatrix, newMatrix);
        if (changedRows.Count == 1) {
            int changedRow = changedRows.IndexOf(0);
            float[,] rowDifference = this.copyMatrix(oldMatrix);
            for (int i = 0; i < oldMatrix.GetLength(1); i++) {
                rowDifference[0,i] = oldMatrix[changedRow,i] - newMatrix[changedRow,i];
            }

            for (int j = 0; j < oldMatrix.GetLength(0); j++) {
                if (j != changedRow) {
                    if (checkTimesScalar(oldMatrix, rowDifference)) {
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
        public static float[,] matrixMultiplication(float[,] matrix, float[,] matrix2) {
        float[,] product = null;
        if (matrix.GetLength(1) == matrix2.GetLength(0)) {
            product = new float[matrix.GetLength(0),matrix2.GetLength(1)];
            for (int i = 0; i < product.GetLength(0); i++) {
                float[] vector = new float[matrix2.GetLength(0)];
                for (int k = 0; k < matrix2.GetLength(0); k++) {
                    vector[k] = matrix2[k,i];
                }
                for (int j = 0; j < product.GetLength(1); j++) {
                    float[] row = new float[matrix.GetLength(1)];
                    for (int k = 0; k < row.GetLength(1); k++)
                    {
                        row[k] = matrix[j, k];
                    }
                    product[j,i] = dotProduct(row, vector);
                }
            }

        }

        return product;
    }

        public static float[,] matrixScalarMultiplication(float[,] matrix, float scalar)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i,j] = scalar * matrix[i,j];
                }
            }
            return matrix;
        }

        public static float dotProduct(float[] vector1, float[] vector2)
        {
            float result = 0;
            for (int i = 0; i < vector1.GetLength(0); i++)
            {
                result += vector1[i] * vector2[i];
            }
            return result;
        }

        public float findDeterminant(float[,] matrix) {
        float determinant = 0;
        if (matrix.GetLength(0) == 2)
        {
            determinant = (matrix[0,0] * matrix[1,1]) - (matrix[0,1] * matrix[1,0]);
        } else if (matrix.GetLength(0) > 2) {
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
                        if (i != 0 && j != curColIndex) {
                            detMatrix[detRow,detCol] = matrix[i,j];
                            detCol++;
                        }
                    }
                    if (i != 0) {
                        detRow++;
                    }
                    detCol = 0;
                }
                float scalar = matrix[0,curColIndex];
                if (Math.Abs(scalar) != 0) {
                    if ((curColIndex % 2) == 0) {
                        determinant += scalar * findDeterminant(detMatrix);
                    } else if ((curColIndex % 2) == 1) {
                        determinant -= scalar * findDeterminant(detMatrix);
                    }
                }
                curColIndex++;
            }
        }
        return determinant;
    }

        public float[,] findInverse(float[,] matrix) {
        float[,] inverse = null;
        if (matrix.GetLength(0) == matrix.GetLength(1))
        {
            inverse = new float[matrix.GetLength(0), matrix.GetLength(1)];
            float[,] matrixWithInverse = new float[matrix.GetLength(0), matrix.GetLength(1) * 2];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrixWithInverse[i,j] = matrix[i,j];
                }
            }
            int row = 0;
            for (int j = matrix.GetLength(1); j < matrixWithInverse.GetLength(1); j++)
            {
                matrixWithInverse[row,j] = 1;
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

        public static bool checkMatrixEquality(float[,] matrix, float[,] matrix2)
        {
            bool isEqual = true;

            if (matrix.GetLength(0) == matrix2.GetLength(0) && matrix.GetLength(1) == matrix2.GetLength(1))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (Math.Abs(matrix[i,j] - matrix2[i,j]) >= offset)
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

        protected float[,] copyMatrix(float[,] matrix)
        {
            float[,] newArray = new float[matrix.GetLength(0),matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newArray[i, j] = matrix[i, j];
                }
            }

            return newArray;
        }
    }
}