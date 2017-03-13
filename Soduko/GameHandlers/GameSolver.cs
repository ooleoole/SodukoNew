using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Soduko.GameBoard;

namespace Soduko.GameHandlers
{
    public class GameSolver : IGameBoardHolder
    {
        private IGameBoard _gameBoard;
        private IDictionary<IGameBoard, ICollection<IGameBoard>> _targetSolutionGameBoard;
        private readonly GameTagDistributor _gameTagDistributor;

        public IGameBoard GameBoard => _gameBoard;
        public IDictionary<IGameBoard, ICollection<IGameBoard>> TargetSolutionGameBoards => _targetSolutionGameBoard;


        public GameSolver(IGameBoard gameBoard)
        {
            _gameBoard = gameBoard;
            _targetSolutionGameBoard = new Dictionary<IGameBoard, ICollection<IGameBoard>>();
            _gameTagDistributor = new GameTagDistributor(this);
            _gameBoard.LoadFreeCoordinatesSeed();
        }

        public void SolveBoard()
        {

            SaveTarget();
            _gameBoard.LoadFreeCoordinatesSeed();
            _gameTagDistributor.PlaceGameTags();
            SaveSolution();
        }

        private void SaveSolution()
        {
            var gameBoardSolution = _gameBoard.Clone();
            var values=_targetSolutionGameBoard.Values;
            var value = values.FirstOrDefault();
            value.Add(gameBoardSolution);
        }

        private void SaveTarget()
        {
            var gameBoardTarget = _gameBoard.Clone();
            var targetEmptySolutionListPair = new KeyValuePair<IGameBoard, ICollection<IGameBoard>>(gameBoardTarget, new Collection<IGameBoard>());
            _targetSolutionGameBoard.Add(targetEmptySolutionListPair);
        }
    }
}
