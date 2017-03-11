using System.Collections.Generic;
using Soduko.GameBoard;

namespace Soduko.GameHandlers
{
    public class GameSolver : IGameBoardHolder
    {
        private IGameBoard _gameBoard;
        private IDictionary<IGameBoard, IEnumerable<IGameBoard>> _targetSolutionGameBoard;

        public IGameBoard GameBoard => _gameBoard;
        public IDictionary<IGameBoard, IEnumerable<IGameBoard>> TargetSolutionGameBoards => _targetSolutionGameBoard;


        public GameSolver(IGameBoard gameBoard)
        {

        }
    }
}
