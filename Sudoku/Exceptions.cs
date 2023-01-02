using System.Runtime.Serialization;

namespace Sudoku
{
    [Serializable]
    internal class NotLegalStringException : Exception
    {
        // Not Legal String Exception - thrown when user give ilegal data string to the board 
        public NotLegalStringException(string? message) : base(message)
        {
          
        }
    
    }

    internal class NotLegalDataSizeException : Exception
    {
        // Not Legal Date Size Exception - thrown when user give board that have more or less numbers then it should 
        public NotLegalDataSizeException(string? message) : base(message)
        {

        }

    }
}