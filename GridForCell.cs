using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Match3
{
    public class GridForCell 
    {
        public int _width { get; private set;}
        public int _height { get; private set; }
        private float _cellSize;
        public float CellSize { get => _cellSize; set { } }
        private Vector3 _startPosition;
        private GameResources _gameResources;
        private Forma _forma;
        private Cell[,] _cells;
        public Cell[,] Cells { get => _cells; set { } }
        public GridForCell(int width, int height, float cellsize, Vector3 position)
        {
            _width = width;
            _height = height;
            _cellSize = cellsize;
            _startPosition = position;
            CreateCells();
        }

        public void CreateForm() => _forma = new Forma(_width, _height, _cellSize);
       
        public void GetResources(in int width,in int height,in float cellsize)
        {
            _width = width;
            _height = height;
            _cellSize = cellsize;
        }
        
        public void CreateCells()
        {
            CreateForm();
            _cells = new Cell[_width, _height];
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    int x = i;
                    int y = j;
                    Vector3 position = new Vector3(_cellSize * i + _startPosition.x + _cellSize / 2, _cellSize * j + _startPosition.y + _cellSize / 2, 0);
                    _cells[i, j] = new Cell(x, y, position);
                }
            }
        }
        
    }
}
