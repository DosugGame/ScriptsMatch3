using System.Collections;
using System.Collections.Generic;
using UnityEngine;





namespace Match3
{
    
    public delegate void Handler(int value);
    public class MainGameLogic
    {
        public DataHandlerForLogic DataHandlerForLogic;
        public event Handler TargetHandler;
        private Board _board;
        private IGetTheResult _uipanels;
        private LevelsSystem _levelsSystem;
        private MessageForWallVK _messageForWallVK;

        public MainGameLogic(IAmAGoalPackageHandler ui, IGetTheResult uipanels, Board board, DataHandlerForLogic dataHandlerForLogic, LevelsSystem levelsSystem, MessageForWallVK message)
        {
            _uipanels = uipanels;
            DataHandlerForLogic = dataHandlerForLogic;
            DataHandlerForLogic.GetPackageHandler(ui);
            _uipanels.GetTheResult += FillWinLosePanel;
            _uipanels.TheMovesAreOver += TheMovesAreOver;
            _board = board;
            _levelsSystem = levelsSystem;
            _uipanels.MovesAdded += ResumeGame;
            _messageForWallVK = message;



        }
        public void TheMovesAreOver()
        {
            _board.IAmReady += GameIsOver;
        }
        public void GameIsOver()
        {
            _uipanels.ResultRequest();
        }
        public void FillWinLosePanel(LevelResults result, WinLosePanel panel)
        {
            _board.PauseOn();
            _board.StartCoroutine(EndLevel(panel, result));
            
        }
        private void ResumeGame()
        {
            _board.IAmReady -= GameIsOver;
            _board.PauseOff();
        }
        public void Win(LevelResults result, WinLosePanel panel)
        {
            int stars = CalkStars(result);
            panel.Win(stars);
            _levelsSystem._saveLoad.SaveStars(_levelsSystem.Current().number, stars);
            _levelsSystem.OpenNextLevel();
            
        }
        public void Lose( WinLosePanel panel)
        {
            panel.Lose();
        }
        private int CalkStars(LevelResults result)
        {
            int stars = 1;
            int numberofmoves = result.NumberOfMovesPerLevel - result.NumberOfMovesLeft;
            float percentnumberofmoves = 100/result.NumberOfMovesPerLevel  * numberofmoves;
            
            percentnumberofmoves = 100 - percentnumberofmoves;
            
            if (percentnumberofmoves >= 20) { stars++;}
            float x = 0;
            if (numberofmoves > 0)  x = (result.Score / result.ScorePrice)/ numberofmoves;
            if (x >= 5)  stars++; 
            return stars;
        }
        IEnumerator EndLevel(WinLosePanel panel, LevelResults result)
        {
            bool win = result.Result == Result.Win;
            panel.ShowResultImage(win);
            yield return new WaitForSeconds(1);
            _board.EndLevel(win);
            if (win)
            { 
                yield return new WaitForSeconds(2);
                if (_messageForWallVK != null) _messageForWallVK.ShowMessage(); 
                int stars = CalkStars(result);
                panel.Win(stars);
                _levelsSystem._saveLoad.SaveStars(_levelsSystem.Current().number, stars);
                _levelsSystem.OpenNextLevel();

            }
            else
            {
                yield return new WaitForSeconds(1);
                panel.Lose();
            }
        }

        
    }
}