using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
  public enum GameMode
    {
        CollectTiles,
        DeliverTheItem,
        ClearCells
    }
    public class DataSetting
    {
        public int Wight { get { return CurrentLevel.Width; } set { } } 
        public int Height { get { return CurrentLevel.Height; } set { } }
        public float Cellsize;
        public Tile[] Tiles;
        public float Speed;
        public int PoolCapacity;
        public GameMode GameMode { get { return CurrentLevel.GameMode; } set { } }
        public ConstructorContainer Counteiner;
        public ItemToCollect ItemToCollectBlank;
        public DirtyCell DirtyCell;
        public Icicle Icicle;
        public List<Coordinate> Icicles { get { return CurrentLevel.Icicles; } set { } }
        public Level CurrentLevel;
    }
    public sealed class GameSetting : MonoBehaviour
    {

        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private float _cellSize;
        [SerializeField] private Tile[] _tiles;
        [SerializeField] private float _speedTiles;
        [SerializeField] private int _poolCapacity;
        [SerializeField] private Sprite[] _spriteItem;
        [SerializeField] private Sprite _spritelCeaningCell;
        [SerializeField] private ItemToCollect _itemToCollectBlank;
        [SerializeField] private DirtyCell _dirtyCell;
        [SerializeField] private Icicle _icicle;



        public void GiveResourse(in Level level , out DataSetting data)
        {
            data = new DataSetting();
            data.CurrentLevel = level;
            data.Cellsize = _cellSize;
            data.Tiles = new Tile[level.CountTiles];
            for (int i = 0; i < level.CountTiles; i++)
            {
                data.Tiles[i] = _tiles[i];
            }
            
            data.Speed = _speedTiles;
            data.PoolCapacity = _poolCapacity;
            if (data.GameMode == GameMode.ClearCells) 
            {
                Color color = _dirtyCell.GetComponentInChildren<SpriteRenderer>().color;
                data.Counteiner = new ConstructorContainer(data.Tiles, _spriteItem, _spritelCeaningCell,color);
            }
            else data.Counteiner = new ConstructorContainer(data.Tiles, _spriteItem, _spritelCeaningCell);
            data.ItemToCollectBlank = _itemToCollectBlank;
            data.DirtyCell = _dirtyCell;
            data.Icicle = _icicle;

        }
        private void OnDrawGizmos()
        {
            if (_width > 0 && _height > 0 && _cellSize > 0)
            {
                Vector3 start;
                Vector3 startposition = transform.position;
                for (int i = 0; i < _height + 1; i++)
                {
                    start = new Vector3(startposition.x, i * _cellSize + startposition.y, 0);
                    Gizmos.DrawLine(start, new Vector3(_width * _cellSize + startposition.x, i * _cellSize + startposition.y, 0));
                }
                for (int i = 0; i < _width + 1; i++)
                {
                    start = new Vector3(startposition.x + i * _cellSize, startposition.y, 0);
                    Gizmos.DrawLine(start, new Vector3(i * _cellSize + startposition.x, _height * _cellSize + startposition.y, 0));
                }
            }



        }
    }
}
