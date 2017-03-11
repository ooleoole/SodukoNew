using System.Collections.Generic;
using Soduko.GameBoard;
using Soduko.Utilitys;

namespace Soduko.GameHandlers
{
    public interface IGameBoardHolder
    {
        IGameBoard GameBoard { get;}
        
    }
}