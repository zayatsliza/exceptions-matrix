using System;

namespace Exceptions
{
    
    [Serializable]
    public class MatrixException : Exception
    {
        
        /// <summary>  
        /// Initializes a new instance of the <see cref="T:MyException"/> class
        /// </summary>
        public MatrixException()
        {
            
        }

        public override string Message
        {
            get
            {
                return "MatrixException";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MyException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        public MatrixException(string message) : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MyException"/> class
        /// </summary>
        /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
        /// <param name="inner">The exception that is the cause of the current exception. </param>
        public MatrixException(string message, Exception inner) : base(message, inner)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MyException"/> class
        /// </summary>
        /// <param name="context">The contextual information about the source or destination.</param>
        /// <param name="info">The object that holds the serialized object data.</param>
        protected MatrixException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {

        }
    }
    //TODO: Create custom exception "MatrixException"

    public class Matrix
    {
        private readonly int rows;
        private readonly int columns;
        private readonly double[,] array;
        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows
        {
            get => rows;
        }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns
        {
            get => columns;
        }

        /// <summary>
        /// An array of floating-point values that represents the elements of this Matrix.
        /// </summary>
        public double[,] Array
        {
            get => array;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when row or column is zero or negative.</exception>
        public Matrix(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.rows = rows;
            this.columns = columns;

            array = new double[rows, columns];

            for (int i = 0; i < rows ; i++){
                for(int j = 0; j < columns; j++)
                {
                    array[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class with the specified elements.
        /// </summary>
        /// <param name="array">An array of floating-point values that represents the elements of this Matrix.</param>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        public Matrix(double[,] array)
        {
            if (array == null) throw new ArgumentNullException();

            rows = array.GetLength(0);
            columns = array.GetLength(1);

            this.array = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    this.array[i, j] = array[i, j];
                }
            }
        }

        /// <summary>
        /// Allows instances of a <see cref="Matrix"/> to be indexed just like arrays.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <exception cref="ArgumentException">Thrown when index is out of range.</exception>
        public double this[int row, int column]
        {
            get
            {
                if (row < 0 || column < 0 || row >= rows || column >= columns) throw new ArgumentException(); 
                return array[row, column];
            }
            set
            {
                if (row < 0 || column < 0 || row >= rows || column >= columns) throw new ArgumentException();
                array[row, column] = value;
            }
        }

        /// <summary>
        /// Adds <see cref="Matrix"/> to the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for adding.</param>
        /// <exception cref="ArgumentNullException">Thrown when parameter is null.</exception>
        /// <exception cref="MatrixException">Thrown when the matrix has the wrong dimensions for the operation.</exception>
        /// <returns><see cref="Matrix"/></returns>
        public Matrix Add(Matrix matrix)
        {
        
            if (matrix == null) throw new ArgumentNullException();
            if (rows != matrix.rows || columns != matrix.columns) throw new MatrixException();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    array[i, j] += matrix.array[i, j];
                }
            }
            return this;
        }

        /// <summary>
        /// Subtracts <see cref="Matrix"/> from the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for subtracting.</param>
        /// <exception cref="ArgumentNullException">Thrown when parameter is null.</exception>
        /// <exception cref="MatrixException">Thrown when the matrix has the wrong dimensions for the operation.</exception>
        /// <returns><see cref="Matrix"/></returns>
        public Matrix Subtract(Matrix matrix)
        {
            if (matrix == null) throw new ArgumentNullException();
            if (rows != matrix.rows || columns != matrix.columns) throw new MatrixException();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    array[i, j] -= matrix.array[i, j];
                }
            }
            return this;
        }

        /// <summary>
        /// Multiplies <see cref="Matrix"/> on the current matrix.
        /// </summary>
        /// <param name="matrix"><see cref="Matrix"/> for multiplying.</param>
        /// <exception cref="ArgumentNullException">Thrown when parameter is null.</exception>
        /// <exception cref="MatrixException">Thrown when the matrix has the wrong dimensions for the operation.</exception>
        /// <returns><see cref="Matrix"/></returns>
        public Matrix Multiply(Matrix matrix)
        {
            if (matrix == null) throw new ArgumentNullException(); 
            if (columns != matrix.rows) throw new MatrixException();

            double[,] result = new double[rows, matrix.columns];

            for(int i = 0; i < result.GetLength(0); i++)
            {
                for( int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = 0;
                    for( int k = 0; k < columns; k++)
                    {
                        result[i, j] += array[i, k] * matrix.array[k, j];
                    }
                }
            }
            return new Matrix(result);
        }
    }
}
