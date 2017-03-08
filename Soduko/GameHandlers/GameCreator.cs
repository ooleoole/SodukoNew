using System;
using System.Collections.Generic;
using System.Linq;
using Soduko.GameBoard;
using Soduko.Interfaces;
using Soduko.Utilitys;

namespace Soduko.GameHandlers
{
    public class GameCreator : IGameCreator, IGameBoardHolder
    {
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

        //private void ClearRandomValuesBasedOnDifficulty()
        //{
        //    var removeAmount = 25 - _difficultyLevel;
        //    var removeSeed = GetCoordinatsSeed();
        //    int index;

        //    for (var i = 0; i < removeAmount; i++)
        //    {
        //        index = _random.Next(0, removeSeed.Count);
        //        removeSeed.RemoveAt(index);
        //    }

        //    do
        //    {
        //        index = _random.Next(0, removeSeed.Count);
        //        var randomCoordinate = removeSeed.ElementAt(index);
        //        removeSeed.Remove(randomCoordinate);
        //        var gameTag = new GameBoardTag(randomCoordinate);
        //        _gameBoard.Replace(gameTag);
        //    } while (removeSeed.Count != 0);

        //}

        public void GenerateGame()
        {
            do
            {
                var tag = _gameTagDistributor.PlaceGameTag();
                _gameBoard.Add(tag);

            } while (_gameBoard.Count < _gameBoard.GameBoardSize);

            AddKeyGamePairToDic();
            //ClearRandomValuesBasedOnDifficulty();
        }

        private void AddKeyGamePairToDic()
        {
            var gameBoardKey = _gameBoard.Clone();
            var gameBoard = _gameBoard;
            var keyGamePair = new KeyValuePair<IGameBoard, IGameBoard>(gameBoardKey, gameBoard);
            _gameBoardGameKeysPair.Add(keyGamePair);
        }
    }
}
