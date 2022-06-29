using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Match3
{ public interface ISpawn<T>
    {
        public T Create(Vector3 position);
        public T Create();
        public void CreateWithExclude(in Vector3 position, out T tile, out int id, params int[] exclude);
    }
    public class Spawn : ISpawn<Tile>
    {
        private Tile[] _tilesType;
        private  Vector3 _startPosition = new Vector3(0, 0, 0);
        private Quaternion _angle;
        public Spawn(Vector3 position, Quaternion angle, Tile[] tilesType)
        {
            _startPosition = position;
            _angle = angle;
            _tilesType = tilesType;
        }
        public void GetResourse(Tile[] tiles)
        {
            _tilesType = tiles;
            for (int i = 0; i < 30; i++)
            {
                Tile tile = Object.Instantiate(RandomTile(out int id), _startPosition, _angle);
                tile.gameObject.SetActive(false);
            }
        }
        public void CreateWithExclude(in Vector3 position, out Tile tile, out int id, params int[] exclude)
        {
            tile = Object.Instantiate(RandomTile(out id, exclude), position, _angle);
        }
        public Tile Create( Vector3 position)
        {
            Tile tile = Object.Instantiate(RandomTile(out int id), _startPosition, _angle);
            return tile;
        }
        public Tile Create()
        {
            Tile tile = Object.Instantiate(RandomTile(out int id), _startPosition, _angle);
            tile.ID = id;
            return tile;
        }
        public Tile RandomTile(out int id)
        {
            int index = Random.Range(0, _tilesType.Length);
            id = index;
            return _tilesType[index];
        }
        public Tile RandomTile(out int id, params int[] exclude)
        {
            int x = RandomFromRangeWithExceptions(0, _tilesType.Length, exclude);
            id = x;
            return _tilesType[x];
        }
        private int RandomFromRangeWithExceptions(int rangeMin, int rangeMax, params int[] exclude)
        {
            List<int> exclude2 = new List<int>();
            for (int i = 0; i < exclude.Length; i++)
            {
                if (exclude[i] >= 0) exclude2.Add(exclude[i]);
            }
            
            var range = Enumerable.Range(rangeMin, rangeMax).Where(i => !exclude2.Contains(i));
            int index = Random.Range(rangeMin, rangeMax - exclude2.Count);
            return range.ElementAt(index);
        }
    }
}
