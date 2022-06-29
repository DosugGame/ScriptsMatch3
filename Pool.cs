using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Linq;

namespace Match3
{
    public interface IPool<T>
    {
        public void PutIntoThePool(T obj);
        public T TakeFromThePool();
        public T TakeFromThePool(Vector3 position);
    }
   
    public class Pool : IPool<Tile>
    {
        private Vector3 _startPosition;
        private List<Tile> _tiles;
        private int _count;
        
        public Pool(int number, ISpawn<Tile> spawn)
        {
            _tiles = new List<Tile>(number * 2);
            _count = number;
            Fill(spawn);
        }
        public void Fill(ISpawn<Tile> spawn)
        {
            for (int i = 0; i < _count; i++)
            {
               Tile tile =  spawn.Create();
               _tiles.Add(tile);
                tile.gameObject.SetActive(false);
                
            }
          
        }
        
        public void PutIntoThePool(Tile tile)
        {
            
            _tiles.Add(tile);
           
        }
        public Tile TakeFromThePool()
        {
            int index = Random.Range(0, _tiles.Count /3);
            Tile tile = _tiles[index];
            tile.gameObject.SetActive(true);
           _tiles.Remove(tile);
            return tile;
        }
        public Tile TakeFromThePool(Vector3 position)
        {
            int index = Random.Range(0, _tiles.Count/3);
            Tile tile = _tiles[index];
            tile.gameObject.transform.position = position;
            tile.gameObject.SetActive(true);
            _tiles.Remove(tile);
            return tile;
        }
    }
}
