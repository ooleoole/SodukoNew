using System.Collections.Generic;
using Soduko.GameBoard;
using Soduko.Interfaces;

namespace Soduko.GameHandlers
{
    public class GameCreator : IGameCreator, IGameBoardHolder
    {
        private const int GameBoardTagBase = 25;

        private readonly IGameBoard _gameBoard;
        private IDictionary<IGameBoard, IGameBoard> _gameBoardGameKeysPair;
        private readonly int _difficultyLevel;
        public IGameBoard GameBoard => _gameBoard;
        private readonly GameTagDistributor _gameTagDistributor;

        public IDictionary<IGameBoard, IGameBoard> GameBoardGameKeysPair => _gameBoardGameKeysPair;

        public GameCreator(IGameBoard gameBoard, int difficultylevel)
        {
            _gameBoard = gameBoard;
            _difficultyLevel = difficultylevel;
            _gameBoardGameKeysPair = new Dictionary<IGameBoard, IGameBoard>();
            _gameTagDistributor = new GameTagDistributor(this);
        }

        private void ClearRandomValuesBasedOnDifficulty()
        {
            var removeAmount = GameBoardTagBase - _difficultyLevel;
            _gameTagDistributor.RemoveRandomGameTagValues(removeAmount);

        }

        public void GenerateGame()
        {
            do
            {
                var tag = _gameTagDistributor.GetGameTag();
                _gameBoard.Add(tag);

            } while (_gameBoard.Count < _gameBoard.GameBoardSize);

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
