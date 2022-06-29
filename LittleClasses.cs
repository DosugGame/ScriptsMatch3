using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace Match3
{
    public  class Forma
    {
        public Size <int> _size;
        public Size <float> _cellSize;
        public Forma(int width = 10, int height = 10, float cellSize = 1)
        {
            _size = new Size <int> (width, height); 
            _cellSize = new Size <float> (cellSize); 

        }
       
    } 
    
   
    public class Size <T>
    {
        public T _width { get => _width; set { } }
        public T _height { get => _height; set { } }
        public Size ( T  width, T  height)
        {
            _width = width;
            _height = height;
        }
        public Size(T width)
        {
            _width = width;
            _height = width;
        }

    }
    public class Cell
    {
        public int x;
        public int y;
        public Vector3 position;
        public Cell(int x, int y, Vector3 position)
        {
            this.x = x;
            this.y = y;
            this.position = position;
        }
    }
    public class Coordinate
    {
        public int x;
        public int y;
        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class StateContext
    {
        
        public StateContext(Animator anim, Tile mono, List<SpriteRenderer> sprites, ParticleSystem dead, IListenerTiles messenger, AudioSource sound)
        {
            Anim = anim;
            Mono = mono;
            Sprites = sprites;
            Dead = dead;
            Messenger = messenger;
            _numberLayers = new int[sprites.Count];
            for (int i = 0; i < sprites.Count; i++)
            {
                _numberLayers[i] = sprites[i].sortingOrder;
            }
            Sound = sound;
        }
        public StateContext(Animator anim, Tile mono, List<SpriteRenderer> sprites, ParticleSystem dead, IListenerTiles messenger)
        {
            Anim = anim;
            Mono = mono;
            Sprites = sprites;
            Dead = dead;
            Messenger = messenger;
            _numberLayers = new int[sprites.Count];
            for (int i = 0; i < sprites.Count; i++)
            {
                _numberLayers[i] = sprites[i].sortingOrder;
            }
            
        }

        public Animator Anim;
        public IListenerTiles Messenger;
        public ParticleSystem Dead;
        public Tile Mono;
        public List<SpriteRenderer> Sprites = new List<SpriteRenderer>();
        private int[] _numberLayers;
        public AudioSource Sound;
        public void DefoultLayers()
        {
            for (int i = 0; i < Sprites.Count; i++)
            {
                Sprites[i].sortingOrder = _numberLayers[i];
            }
        }
    }
    public enum Movement { NewCell, Home, Fail, Fall }
    
    

    public interface IListenerTiles
    {
       
        public void Message(in MessengerTiles.Messages message, Tile tile = null);

    }
    public interface ICommandHandler
    {
        public delegate void MessageDelegate(Tile tile);
        public delegate void MessageDelegateEmpty();
        public event MessageDelegate IAmpressed;
        public event MessageDelegate IAmReleased;
        public event MessageDelegateEmpty Swap;
        public event MessageDelegateEmpty AllTilesAreDestroyed;
        public event MessageDelegateEmpty Filed;
        public event ICommandHandler.MessageDelegate Dead;
        public event Action IAMReady;
        public void PauseOn();
        public void PauseOff();

        public void CommandHandler(MessengerTiles.Commands commands, int count = 0);
    }
    public class MessengerTiles: ICommandHandler, IListenerTiles
    {
        public enum Messages { IAmpressed, IAmReleased, IAmInPosition, iAmDestroyed, IWasBorn };
        public enum Commands { ICanPush, ICanRelease, GoHome, Swap, Revers, Destroy, FillGrid, Birth };
       
        private ICommandHandler.MessageDelegate _current;
        private ICommandHandler.MessageDelegateEmpty _currentEmpty;
        public ICommandHandler.MessageDelegateEmpty _current2;
        public event ICommandHandler.MessageDelegate IAmpressed;
        public event ICommandHandler.MessageDelegate IAmReleased;
        public event ICommandHandler.MessageDelegateEmpty Swap;
        public event ICommandHandler.MessageDelegateEmpty AllTilesAreDestroyed;
        public event ICommandHandler.MessageDelegateEmpty Filed;
        public event ICommandHandler.MessageDelegate Dead;
        private bool _pause = false;
        public event Action IAMReady;
        private bool _withTile = true;
        private int _counter = 0;
        private Messages _expectedMessages;
        public void PauseOn() { _pause = true; }
        public void PauseOff() { _pause = false; }
        public  void Message(in Messages message, Tile tile = null)
        {
            if (!_pause)
            {
                if (message == Messages.iAmDestroyed && Dead != null) { Dead(tile); }
                CountTheMessages(message, tile);
            }
            
            
        }
        public DeadHandler InitializationDeadHandler()
        {
            return new DeadHandler(this);
        }


        private void CountTheMessages(in Messages message, Tile tile = null)
        {
            if (message == _expectedMessages)
            {
                _counter--;
                if (_counter == 0)
                {
                    if (_withTile) _current(tile); else { _currentEmpty();  }
                    if(message == Messages.IAmInPosition) Debug.Log("готово");
                }
            }
        }
        
        public void CommandHandler(Commands commands, int count = 0)
        {
            switch (commands)
            {
                case Commands.ICanPush:
                    _withTile = true;
                    Debug.Log("можешь брать");
                    if (IAMReady != null) IAMReady();
                    WaitMessages(Messages.IAmpressed, 1);
                   _current = IAmpressed;
                    break;
                case Commands.ICanRelease:
                    _withTile = true;
                    WaitMessages(Messages.IAmReleased, 1);
                    _current = IAmReleased;
                    break;
                case Commands.GoHome:
                    _withTile = false;
                    WaitMessages(Messages.IAmInPosition, 1);
                    _currentEmpty = MovementIsOver; 
                    break;
                case Commands.Swap:
                    _withTile = false;
                    WaitMessages(Messages.IAmInPosition, 2);
                    _currentEmpty = Swap;
                    break;
                case Commands.Revers:
                    _withTile = false;
                    WaitMessages(Messages.IAmInPosition, 2);
                    _currentEmpty = MovementIsOver;
                    break;
                case Commands.Destroy:
                    _withTile = false;
                    WaitMessages(Messages.iAmDestroyed, count);
                    _currentEmpty = AllTilesAreDestroyed;
                    break;
                case Commands.FillGrid:
                    _withTile = false;
                    WaitMessages(Messages.IAmInPosition, count);
                    Debug.Log("жду новых " + count);
                    _currentEmpty = Filed;
                    break;
                case Commands.Birth:
                    _withTile = false;
                    WaitMessages(Messages.IWasBorn, count);
                    
                    _currentEmpty = Filed;
                    break;

                default:
                    break;
            }
        }
        private void MovementIsOver()
        {
            CommandHandler(Commands.ICanPush); 
        }
       

        public void WaitMessages(in Messages message, in int count)
        {
            if (message == Messages.IAmInPosition && count == 0) MovementIsOver();
            else
            {
                _counter = count;
                _expectedMessages = message;
            }
            
            



        }
       

    }
    public interface ITakeTheValue
    {
        public void Add(int value);
        public void Clear();
        public void NewValue(int value);

    }
    public class UICounter : MonoBehaviour, ITakeTheValue
    {
        public int _value = 0;
        public int _price;
        public int _defaultPrice = 0;
        public Text _text;
        public virtual void GetResources(int price = 1)
        {
            _price = price;
            foreach (Text text in GetComponentsInChildren<Text>())
            {
                if (text.name == "Value") _text = text;
            }
            UpdateValue();
        }
        public virtual void Add(int value)
        {
            _value += value * _price;
            UpdateValue();
        }
        public virtual void Clear()
        {
            _price = _defaultPrice;
            UpdateValue();
        }
        public virtual void NewValue(int value)
        {
            _value = value;
            UpdateValue();
        }
        public virtual void UpdateValue()
        {
            _text.text = _value.ToString();
        }

    }
    
    public class CheckInfo
    {
        public bool NeedCheck;
        public bool AddedToArray;
        public bool DoNotTouch;
        public CheckInfo()
        {
            NeedCheck = false;
            AddedToArray = false;
            DoNotTouch = false;
        }
        public CheckInfo(bool needcheck)
        {
            if (needcheck) NeedCheck = true;
            else NeedCheck = false;
            AddedToArray = false;
            DoNotTouch = false;
        }
    }
    public class TableCheck
    {
        public int Width;
        public int Height;
        public CheckInfo[,] Table;
        public TableCheck(int width, int height)
        {
            Width = width;
            Height = height;
            CreateTable();
        }
        public TableCheck(int width, int height, ItemsToCollectSystem Items)
        {
            Width = width;
            Height = height;
            CreateTable();
            if (Items.HaveItems())
            {
                List<Coordinate> exceptions = Items.CoordinateItems();
                foreach (Coordinate coordinate in exceptions)
                {
                    Table[coordinate.x, coordinate.y].DoNotTouch = true;
                    Table[coordinate.x, coordinate.y].NeedCheck = false;
                }
            }
            
        }
        public void CreateTable()
        {
            Table = new CheckInfo[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Table[i, j] = new CheckInfo();
                }
            }
        }
        public void AddTilesForCheck(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                Table[tile._myCell.x, tile._myCell.y] = new CheckInfo(true);
            }
        }
        public void ExcludeTilesForBonus(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                Table[tile._myCell.x, tile._myCell.y].DoNotTouch = true;
            }
        }
        public void AddToArray(Tile tile)
        {
            CheckInfo info = Table[tile._myCell.x, tile._myCell.y];
            info.AddedToArray = true;
            if (!info.NeedCheck) info.DoNotTouch = true;
        }
        public void CheckCompleted(Tile tile)
        {
            CheckInfo info = Table[tile._myCell.x, tile._myCell.y];
            info.NeedCheck = false;
        }
        public void CheckCompleted(List<Tile> tiles)
        {
            
            foreach (Tile tile in tiles)
            {
                CheckInfo info = Table[tile._myCell.x, tile._myCell.y];
                info.NeedCheck = false;
                info.DoNotTouch = true;
            }
        }
        public bool NeedCkeck(Tile tile)
        {
            return Table[tile._myCell.x, tile._myCell.y].NeedCheck;
        }
        public bool AddedToArray(Tile tile)
        {
            return Table[tile._myCell.x, tile._myCell.y].AddedToArray;
        }
        public bool NeedCkeck(List<Tile> tiles, out Tile tileforcheck)
        {
            bool needcheck = false;
            tileforcheck = null;
            foreach (Tile tile in tiles)
            {
                if (Table[tile._myCell.x, tile._myCell.y].NeedCheck) { needcheck = true; tileforcheck = tile; break; }
            }
            return needcheck;
        }
    }
    public enum Result { Win, Lose }
    public class LevelResults
    {
        public Result Result;
        public int NumberOfMovesPerLevel;
        public int NumberOfMovesLeft;
        public int Score;
        public int ScorePrice;
        public LevelResults(int numbermoves, int scorePrice)
        {
            NumberOfMovesPerLevel = numbermoves;
            ScorePrice = scorePrice;
        }
        public void TheMovesAreOver()
        {
            Result = Result.Lose;
        }
        public void GoalAchieved(int numberOfMovesLeft, int score)
        {
            Result = Result.Win;
            NumberOfMovesLeft = numberOfMovesLeft;
            Score = score;
        }
    }
}