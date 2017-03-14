using System.Collections.Generic;
using Soduko.GameBoard;
using Soduko.Interfaces;

namespace Soduko.GameHandlers
{
    public class GameHolder : IGameHolder, IGameBoardHolder
    {
        private const int RemoveBase = 50;

        private readonly IGameBoard _gameBoard;
        private readonly IDictionary<IGameBoard, IGameBoard> _gameBoardGameKeysPair;
        private readonly int _difficultyLevel;
        private readonly GameTagDistributor _gameTagDistributor;
        private IGameBoardRules _boardRules;
        

        public IGameBoardRules BoardRules => _boardRules;
        public IGameBoard GameBoard => _gameBoard;
        public IDictionary<IGameBoard, IGameBoard> GameBoardGameKeysPair => _gameBoardGameKeysPair;

        public GameHolder(IGameBoard gameBoard, int difficultylevel, IGameBoardRules boardRules)
        {
            _gameBoard = gameBoard;
            _difficultyLevel = difficultylevel;
            _boardRules = boardRules;
            _gameBoardGameKeysPair = new Dictionary<IGameBoard, IGameBoard>();
            _gameTagDistributor = new GameTagDistributor(this);
        }

        private void ClearRandomValuesBasedOnDifficulty()
        {
            var removeAmount = RemoveBase + _difficultyLevel;
            _gameBoard.LoadCoordinatesSeed();

            for (var i = 0; i < removeAmount; i++)
            {
                _gameTagDistributor.RemoveRandomGameTagValue();

            }

        }


        public void LoadGame()
        {
            _gameBoard.LoadFreeCoordinatesSeed();
            _gameTagDistributor.PlaceGameTags();

            AddKeyGamePairToDic();
            _gameBoard.Clear();
        }

        private void AddKeyGamePairToDic()
        {
            var gameBoardKey = _gameBoard.Clone();
            ClearRandomValuesBasedOnDifficulty();
            var gameBoard = _gameBoard.Clone();
            var keyGamePair = new KeyValuePair<IGameBoard, IGameBoard>(gameBoardKey, gameBoard);
            _gameBoardGameKeysPair.Add(keyGamePair);
        }
    }
}
