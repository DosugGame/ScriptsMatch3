using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Match3 {
    public class TargetInfo
    {
        public int ID;
        public int Value;
        
        public TargetInfo(int id)
        {
            ID = id;
            Value = 0;
        }
    }
    public class PackageInfo
    {
        public TargetInfo[] _targetsInfo;
        public int NumberOfTileMatches;
        public bool Move = false;
        public PackageInfo(TargetContainer[] targets)
        {
            _targetsInfo = new TargetInfo[targets.Length];
            int i = 0;
            foreach (TargetContainer target in targets)
            {
                _targetsInfo[i] = new TargetInfo(target.ID);
                i++;
            }
        }
    }
    public class DataHandlerForLogic : ITargetsContent
    {
        private PackageInfo _package;
        private bool _transferATile = true;
        public delegate void PackageHandler(PackageInfo info);
        public event PackageHandler Send;
        private IAmAGoalPackageHandler _ui;


        public DeadHandler DeadHandler;
        public void GetPackageHandler(IAmAGoalPackageHandler ui)
        {
            _ui = ui;
            Send += ui.PackageProcessing;
        }
        public void Init(GameMode mode, BoardData data)
        {
            DeadHandler = data.DeadHandler;
            if (mode == GameMode.ClearCells)
            {
                _transferATile = false;
                data.TargetDeadHandler += CatchTheTarget;
            }
            else  DeadHandler.DeadTile += CatchTheTarget;
            DeadHandler.DeadTile += TileMatches;
            data.Step += SendPackage;
        }
        public void SetTarget(TargetContainer[] targets)
        {
            _package = new PackageInfo(targets);
        }
        private void Clear(PackageInfo package)
        {
            foreach (TargetInfo target in package._targetsInfo)
            {
                target.Value = 0;
            }
            package.NumberOfTileMatches = 0;
        }
        public void SendPackage(bool move = false)
        {
            _package.Move = move;
            if (Send != null) Send(_package);
            Clear(_package);
        }
        public void CatchTheTarget(Tile tile = null) 
        { 
            if (_transferATile)
            {
                foreach (TargetInfo targetInfo in _package._targetsInfo)
                {
                   
                    if (tile.ID == targetInfo.ID)  targetInfo.Value++; 
                }
            }
            else _package._targetsInfo[0].Value++;
            
        }
        public void TileMatches(Tile tile)
        {
            if(tile.ID >= 0 && tile.ID < 10) _package.NumberOfTileMatches++;
        }
    } 
}
