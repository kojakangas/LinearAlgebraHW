//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace LinearHomeworkInterface.components
//{
//    class MatrixBuilder
//    {

//        private static Random rand = new Random();
//        public static float offset = .001f; //used for float comparisons

//        //Can be considered augmented or not
//        public static float[,] generateRandomMatrix(int rows, int cols, int min, int max) {
//        float[,] matrix = new float[rows,cols];

//        for (int i = 0; i < rows; i++) {
//            for (int j = 0; j < cols; j++) {
//                matrix[i,j] = rand.Next(min,max);
//            }
//        }

//        return matrix;
//    }

//        //Is augmented
//        //This method has specific answers
//        public static float[,] generateUniqueSolutionMatrix(int rows, int cols, int min, int max, float[] answer) {
//        float[,] matrix = new float[rows,cols];
//        bool hasUniqueSolution = false;
//        while (!hasUniqueSolution) {
//            for (int i = 0; i < rows; i++) {
//                for (int j = 0; j < cols - 1; j++) {
//                    matrix[i,j] = rand.Next(min,max);
//                    matrix[i,cols - 1] = matrix[i,cols - 1] + matrix[i,j] * answer[j];
//                }
//            }
//            float[,] tempMatrix = new float[rows,cols];
//            Array.Copy(matrix,tempMatrix,matrix.Length);
//            tempMatrix = reduceMatrix(tempMatrix);
//            if (checkForRowOfZeros(tempMatrix) == -1 && !checkForInconsistentMatrix(tempMatrix)) {
//                hasUniqueSolution = true;
//            } else {
//                matrix = new float[rows,cols];
//            }
//        }

//        return matrix;
//    }

//        //Is augmented
//        //Will have non integer answers
//        public static float[,] generateUniqueSolutionMatrix(int rows, int cols,int min, int max) {
//        float[,] matrix = new float[rows,cols];
//        boolean hasUniqueSolution = false;
//        while (!hasUniqueSolution) {
//            for (int i = 0; i < rows; i++) {
//                for (int j = 0; j < cols; j++) {
//                    matrix[i][j] = rand.nextInt(max);
//                    if (rand.nextBoolean()) {
//                        matrix[i][j] = matrix[i][j] * (-1);
//                    }
//                }
//            }
//            float[][] tempMatrix = new float[rows][cols];
//            for (int p = 0; p < matrix.length; p++) {
//                tempMatrix[p] = Arrays.copyOf(matrix[p], cols);
//            }
//            tempMatrix = reduceMatrix(tempMatrix);
//            if (checkForRowOfZeros(tempMatrix) == -1 && !checkForInconsistentMatrix(tempMatrix)) {
//                hasUniqueSolution = true;
//            } else {
//                matrix = new float[rows][cols];
//            }
//        }

//        return matrix;
//    }

//        //Is augmented
//        public static float[][] generateInconsistentMatrix(int rows, int cols) {
//        float[][] matrix = new float[rows][cols];
//        boolean isInconsistent = false;
//        while (!isInconsistent) {
//            for (int i = 0; i < rows; i++) {
//                for (int j = 0; j < cols; j++) {
//                    matrix[i][j] = rand.nextInt(max);
//                    if (rand.nextBoolean()) {
//                        matrix[i][j] = matrix[i][j] * (-1);
//                    }
//                }
//            }
//            float[][] tempMatrix = new float[rows][cols];
//            for (int p = 0; p < n; p++) {
//                tempMatrix[p] = Arrays.copyOf(matrix[p], cols);
//            }
//            tempMatrix = reduceMatrix(tempMatrix);
//            if (checkForRowOfZeros(tempMatrix) == -1 && checkForInconsistentMatrix(tempMatrix)) {
//                isInconsistent = true;
//            } else {
//                matrix = new float[rows][cols];
//            }
//        }

//        return matrix;
//    }

//        //Considered Augmented
//        public static float[][] generateMatrixWithFreeVariables(int rows, int cols, float[] answer, int numOfFreeVars) {
//        float[][] matrix = new float[rows][cols];
//        boolean hasFreeVariables = false;
//        while (!hasFreeVariables) {
//            for (int i = 0; i < matrix.length; i++) {
//                for (int j = 0; j < matrix[0].length - 1; j++) {
//                    matrix[i][j] = rand.nextInt(max);
//                    if (rand.nextBoolean()) {
//                        matrix[i][j] = matrix[i][j] * (-1);
//                    }
//                    matrix[i][matrix[0].length - 1] = matrix[i][matrix[0].length - 1] + matrix[i][j] * answer[j];
//                }
//            }
//            float[][] tempMatrix = new float[matrix.length][matrix[0].length];
//            for (int p = 0; p < n; p++) {
//                tempMatrix[p] = Arrays.copyOf(matrix[p], matrix[0].length);
//            }
//            tempMatrix = reduceMatrix(tempMatrix);
//            if (checkForRowOfZeros(tempMatrix) != -1 && checkForRowOfZeros(matrix) == -1
//                    && !checkForInconsistentMatrix(tempMatrix)) {
//                int counter = 0;
//                int row = checkForRowOfZeros(tempMatrix);
//                while (row != -1) {
//                    tempMatrix[row][0] = 1;
//                    row = checkForRowOfZeros(tempMatrix);
//                    counter++;
//                }
//                if (counter == numOfFreeVars) {
//                    hasFreeVariables = true;
//                }
//            } else {
//                matrix = new float[matrix.length][matrix[0].length];
//            }
//        }

//        return matrix;
//    }

//        public static float[][] generateRandomIdentityMatrix(int size) {
//        float[][] matrix = null;
//        boolean isIdentity = false;
//        while (!isIdentity) {
//            matrix = new float[size][size];
//            for (int i = 0; i < size; i++) {
//                for (int j = 0; j < size; j++) {
//                    matrix[i][j] = rand.nextInt(max);
//                    if (rand.nextBoolean()) {
//                        matrix[i][j] = matrix[i][j] * (-1);
//                    }
//                }
//            }
//            float[][] tempMatrix = new float[size][size];
//            for (int p = 0; p < size; p++) {
//                tempMatrix[p] = Arrays.copyOf(matrix[p], size);
//            }
//            tempMatrix = reduceMatrix(tempMatrix);
//            if (checkForRowOfZeros(tempMatrix) == -1) {
//                isIdentity = true;
//            }
//        }

//        return matrix;
//    }

//        public static float[,] reduceMatrix(float[,] matrix)
//        {
//            int numOfRows = matrix.Length;
//            int numOfCols = matrix[0].Length;
//            bool zeroColumn = false;

//            //For each column reduce the coefficients under it to 0
//            for (int col = 0; col < numOfRows; col++)
//            {
//                //Handles when n >= m
//                if (numOfCols <= col)
//                {
//                    break;
//                }

//                zeroColumn = false;
//                //if the current diagonal = 0 then swap rows until it doesn't
//                if (matrix[col,col] == 0)
//                {
//                    int index = col;
//                    while (matrix[col,col] == 0)
//                    {
//                        if (index < numOfRows - 1)
//                        {
//                            matrix = rowSwap(col, index + 1, matrix);
//                            index++;
//                        }
//                        else
//                        {
//                            int pivotRow = checkForPivotRowAbove(col, matrix);
//                            if (pivotRow != -1)
//                            {
//                                matrix = rowSwap(col, pivotRow, matrix);
//                            }
//                            else
//                            {
//                                zeroColumn = true;
//                            }
//                            break;
//                        }
//                    }
//                }

//                if (!zeroColumn)
//                {
//                    //Makes the current diagonal = 1
//                    if (matrix[col][col] != 1)
//                    {
//                        matrix = timesScalar(1 / matrix[col][col], col, matrix);
//                    }

//                    //This is what reduces the coefficients under the current column to 0
//                    //by multiplying the current row by the first coefficient of the next
//                    //row and changing the sign.
//                    for (int row = 0; row < numOfRows; row++)
//                    {
//                        if (!(Math.abs(matrix[row][col]) < offset) && row != col)
//                        {
//                            float scalar = matrix[row][col] * (-1);
//                            matrix = addMultipleOfRow(scalar, col, row, matrix);
//                            for (int i = 0; i < numOfCols; i++)
//                            {
//                                if (Math.abs(matrix[row][i]) < offset)
//                                {
//                                    matrix[row][i] = 0;
//                                }
//                            }
//                        }
//                    }
//                }
//            }

//            return matrix;
//        }

//        //Used only during matrix reduction
//        private static int checkForPivotRowAbove(int currentCol, float[][] matrix)
//        {
//            int pivotRow = -1;
//            int counter = 0;

//            for (int i = 0; i < matrix.length; i++)
//            {
//                if (matrix[i][currentCol] != 0)
//                {
//                    for (int j = 0; j < currentCol; j++)
//                    {
//                        if (Math.abs(matrix[i][j]) < offset)
//                        {
//                            counter++;
//                        }
//                    }
//                    if (counter == currentCol)
//                    {
//                        pivotRow = i;
//                        break;
//                    }
//                    counter = 0;
//                }
//            }

//            return pivotRow;
//        }

//        //Used during matrix generation
//        public static bool checkForInconsistentMatrix(float[,] matrix)
//        {
//            bool inconsistent = false;
//            int counter = 0;

//            for (int i = 0; i < matrix.Length; i++)
//            {
//                for (int j = 0; j < matrix[0].length - 1; j++)
//                {
//                    if (!(Math.abs(matrix[i,j]) < offset))
//                    {
//                        break;
//                    }
//                    counter++;
//                }
//                if (counter == matrix[0].length - 1 && !(Math.abs(matrix[i,matrix[0].length - 1]) < offset))
//                {
//                    inconsistent = true;
//                    break;
//                }
//                counter = 0;
//            }

//            return inconsistent;
//        }

//        //Used during matrix generation
//        public static int checkForRowOfZeros(float[,] matrix)
//        {
//            int freeRow = -1;
//            int counter = 0;

//            for (int i = 0; i < matrix.Length; i++)
//            {
//                for (int j = 0; j < matrix[0].length; j++)
//                {
//                    if (!(Math.Abs(matrix[i,j]) < offset))
//                    {
//                        break;
//                    }
//                    counter++;
//                }
//                if (counter == matrix[0].length)
//                {
//                    freeRow = i;
//                    break;
//                }
//                counter = 0;
//            }

//            return freeRow;
//        }

//        //Used when checking row operations
//        public static ArrayList<Integer> findChangedRows(float[][] matrix, float[][] matrix2)
//        {
//            ArrayList<Integer> changedRowsIndex = new ArrayList<Integer>();

//            for (int i = 0; i < matrix.length; i++)
//            {
//                for (int j = 0; j < matrix[i].length; j++)
//                {
//                    if (matrix[i][j] != matrix2[i][j])
//                    {
//                        changedRowsIndex.add(i);
//                        break;
//                    }
//                }
//            }

//            return changedRowsIndex;
//        }

//        public static float[][] timesScalar(float scalar, int row, float[][] matrix)
//        {
//            for (int i = 0; i < matrix[0].length; i++)
//                matrix[row][i] = matrix[row][i] * scalar;
//            return matrix;
//        }

//        public static boolean checkTimesScalar(float[][] oldMatrix, float[][] newMatrix)
//        {
//            boolean isTimesScalar = true;
//            float floatOffset = .001f;
//            float constant = 0;
//            int index = 0;
//            ArrayList<Integer> changedRows = findChangedRows(oldMatrix, newMatrix);
//            if (changedRows.size() == 1)
//            {
//                int row = changedRows.get(0);
//                while (index < oldMatrix[row].length)
//                {
//                    if (oldMatrix[row][index] != 0)
//                    {
//                        constant = newMatrix[row][index] / oldMatrix[row][index];
//                        break;
//                    }
//                    index++;
//                }
//                for (int i = 0; i < oldMatrix[0].length; i++)
//                {
//                    if (Math.abs(constant * oldMatrix[row][i] - newMatrix[row][i]) > floatOffset)
//                    {
//                        isTimesScalar = false;
//                        break;
//                    }
//                }
//            }
//            else if (changedRows.size() != 0)
//            {
//                isTimesScalar = false;
//            }
//            return isTimesScalar;
//        }

//        public static float[][] rowSwap(int row1, int row2, float[][] matrix)
//        {
//            float[] temp = matrix[row1];
//            matrix[row1] = matrix[row2];
//            matrix[row2] = temp;

//            return matrix;
//        }

//        public static boolean checkRowSwap(float[][] oldMatrix, float[][] newMatrix)
//        {
//            boolean isRowSwap = true;
//            ArrayList<Integer> changedRows = findChangedRows(oldMatrix, newMatrix);
//            if (changedRows.size() == 2)
//            {
//                int row1 = changedRows.get(0);
//                int row2 = changedRows.get(1);
//                for (int i = 0; i < oldMatrix.length; i++)
//                {
//                    if (oldMatrix[row1][i] != newMatrix[row2][i] || oldMatrix[row2][i] != newMatrix[row1][i])
//                    {
//                        isRowSwap = false;
//                        break;
//                    }
//                }
//            }
//            else
//            {
//                isRowSwap = false;
//            }

//            return isRowSwap;
//        }

//        public static float[][] addMultipleOfRow(float scalar, int actionRow, int targetRow, float[][] matrix)
//        {
//            float[] rowHolder = matrix[actionRow].clone();
//            matrix = timesScalar(scalar, actionRow, matrix);
//            for (int i = 0; i < matrix[0].length; i++)
//                matrix[targetRow][i] += matrix[actionRow][i];

//            matrix[actionRow] = rowHolder;
//            return matrix;
//        }

//        public static boolean checkAddMultipleOfRow(float[][] oldMatrix, float[][] newMatrix) {
//        boolean isAddMultipleOfRow = false;
//        ArrayList<Integer> changedRows = findChangedRows(oldMatrix, newMatrix);
//        if (changedRows.size() == 1) {
//            int changedRow = changedRows.get(0);
//            float[][] rowDifference = new float[oldMatrix.length][oldMatrix[0].length];
//            for (int i = 0; i < n; i++) {
//                rowDifference[i] = Arrays.copyOf(oldMatrix[i], oldMatrix[i].length);
//            }
//            for (int i = 0; i < oldMatrix[changedRow].length; i++) {
//                rowDifference[0][i] = oldMatrix[changedRow][i] - newMatrix[changedRow][i];
//            }

//            for (int j = 0; j < oldMatrix.length; j++) {
//                if (j != changedRow) {
//                    if (checkTimesScalar(oldMatrix, rowDifference)) {
//                        isAddMultipleOfRow = true;
//                        break;
//                    }
//                }
//                if ((j + 1) != oldMatrix.length)
//                    rowSwap(j, j + 1, rowDifference);
//                rowDifference[j] = oldMatrix[j];
//            }
//        }

//        return isAddMultipleOfRow;
//    }

//        //Will error with incorrect size matrices
//        public static float[][] matrixMultiplication(float[][] matrix, float[][] matrix2) {
//        float[][] product = null;
//        if (matrix[0].length == matrix2.length) {
//            product = new float[matrix.length][matrix2[0].length];
//            for (int i = 0; i < product.length; i++) {
//                float[] vector = new float[matrix2.length];
//                for (int k = 0; k < matrix2.length; k++) {
//                    vector[k] = matrix2[k][i];
//                }
//                for (int j = 0; j < product[0].length; j++) {
//                    product[j][i] = dotProduct(matrix[j], vector);
//                }
//            }

//        }

//        return product;
//    }

//        public static float[][] matrixScalarMultiplication(float[][] matrix, float scalar)
//        {
//            for (int i = 0; i < matrix.length; i++)
//            {
//                for (int j = 0; j < matrix[0].length; j++)
//                {
//                    matrix[i][j] = scalar * matrix[i][j];
//                }
//            }
//            return matrix;
//        }

//        public static float dotProduct(float[] vector1, float[] vector2)
//        {
//            float result = 0;
//            for (int i = 0; i < vector1.length; i++)
//            {
//                result += vector1[i] * vector2[i];
//            }
//            return result;
//        }

//        public static float findDeterminant(float[][] matrix) {
//        float determinant = 0;
//        if (matrix.length == 2) {
//            determinant = (matrix[0][0] * matrix[1][1]) - (matrix[0][1] * matrix[1][0]);
//        } else if (matrix.length > 2) {
//            int curColIndex = 0;
//            while (curColIndex < matrix.length) {
//                float[][] detMatrix = new float[matrix.length - 1][matrix[0].length - 1];
//                int detRow = 0;
//                int detCol = 0;
//                for (int i = 0; i < matrix.length; i++) {
//                    for (int j = 0; j < matrix[0].length; j++) {
//                        if (i != 0 && j != curColIndex) {
//                            detMatrix[detRow][detCol] = matrix[i][j];
//                            detCol++;
//                        }
//                    }
//                    if (i != 0) {
//                        detRow++;
//                    }
//                    detCol = 0;
//                }
//                float scalar = matrix[0][curColIndex];
//                if (Math.abs(scalar) != 0) {
//                    if ((curColIndex % 2) == 0) {
//                        determinant += scalar * findDeterminant(detMatrix);
//                    } else if ((curColIndex % 2) == 1) {
//                        determinant -= scalar * findDeterminant(detMatrix);
//                    }
//                }
//                curColIndex++;
//            }
//        }
//        return determinant;
//    }

//        public static float[][] findInverse(float[][] matrix) {
//        float[][] inverse = null;
//        if (matrix.length == matrix[0].length) {
//            inverse = new float[matrix.length][matrix[0].length];
//            float[][] matrixWithInverse = new float[matrix.length][matrix[0].length * 2];
//            for (int i = 0; i < matrix.length; i++) {
//                for (int j = 0; j < matrix[0].length; j++) {
//                    matrixWithInverse[i][j] = matrix[i][j];
//                }
//            }
//            int row = 0;
//            for (int j = matrix[0].length; j < matrixWithInverse[0].length; j++) {
//                matrixWithInverse[row][j] = 1;
//                row++;
//            }

//            matrixWithInverse = reduceMatrix(matrixWithInverse);

//            for (int i = 0; i < inverse.length; i++) {
//                for (int j = 0; j < inverse[0].length; j++) {
//                    inverse[i][j] = matrixWithInverse[i][j + matrix[0].length];
//                }
//            }
//        }

//        return inverse;
//    }

//        public static boolean checkMatrixEquality(float[][] matrix, float[][] matrix2)
//        {
//            boolean isEqual = true;

//            if (matrix.length == matrix2.length && matrix[0].length == matrix2[0].length)
//            {
//                for (int i = 0; i < matrix.length; i++)
//                {
//                    for (int j = 0; j < matrix[0].length; j++)
//                    {
//                        if (Math.abs(matrix[i][j] - matrix2[i][j]) >= offset)
//                        {
//                            isEqual = false;
//                            break;
//                        }
//                    }
//                    if (!isEqual)
//                    {
//                        break;
//                    }
//                }
//            }
//            else
//            {
//                isEqual = false;
//            }
//            return isEqual;
//        }
//    }
//}