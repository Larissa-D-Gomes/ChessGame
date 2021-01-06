using System;

namespace GameBoard
{
    class GameBoardException: Exception
    {
        //Constructor
        public GameBoardException(string message): base(message)
        {
        }
    }
}
