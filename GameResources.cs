using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3 {
    public class GameResources : MonoBehaviour
    {
        [SerializeField] private GameSetting _gameSetting;
        [SerializeField] private UIProgressMain _ui;
        private GridForCell _grid;
        [SerializeField] private Pool _pool;
        [SerializeField] private CellDrawer _cellDrawer;
        [SerializeField] private Board _board;
        [SerializeField] private BonusItemSetting _bonus;
        [SerializeField] private LevelsSystem _levelsSystem;
        [SerializeField] private Background _background;
        [SerializeField] SoundForYandex sound;


        private void Awake()
        {
            Level current = _levelsSystem.Current();
            _gameSetting.GiveResourse(current,out DataSetting data);
            Centering(data);
            _background.GetBackground(_levelsSystem.Current().NumberBackgroung);
            DataHandlerForLogic dataHandlerForLogic = new DataHandlerForLogic();
            BoardConstroctor boardConstroctor = new BoardConstroctor(data);
            ITargetsContent[] content = new ITargetsContent[3] { _ui, boardConstroctor, dataHandlerForLogic };
            TargetsConstructor constructortargets = new TargetsConstructor(data.Counteiner, content, data.GameMode, data.CurrentLevel.Target);
            _ui.GetNumberOfStep(_levelsSystem.Current());

            Spawn spawn = new Spawn(transform.position, transform.rotation, data.Counteiner._forCollectTiles);
            _pool = new Pool(data.PoolCapacity, spawn);
            _grid = new GridForCell(data.Wight, data.Height, data.Cellsize, transform.position);
            if (_cellDrawer != null) _cellDrawer.Draw(_grid);
            BonusSystem bonus = new BonusSystem(_bonus);
            boardConstroctor.CreateBoard(data, _pool, _grid, spawn, _board, dataHandlerForLogic, bonus);

            Ads ads = new Ads(AdsVariant.VK, sound);
            List<IPauseContent> pausecontent = new List<IPauseContent>();
            pausecontent.Add(_board);

            MessageForWallVK message = new MessageForWallVK(current, ads.SDK );
            MainGameLogic main = new MainGameLogic(_ui, _ui, _board, dataHandlerForLogic, _levelsSystem, message);
            
            
            
            sound.init(ads);
            ButtonContent buttonContent = new ButtonContent(_levelsSystem, pausecontent, ads);
            _ui.ButtonContent = buttonContent;




        }
       
        private void Centering(DataSetting data)
        {
            float x = Camera.main.transform.position.x;
            float y = Camera.main.transform.position.y;
            float centerX = transform.position.x - data.Wight * data.Cellsize/2;
            float centerY = transform.position.y - data.Height * data.Cellsize / 2;
            transform.position = new Vector3(x - data.Wight * data.Cellsize / 2 + 1.5f, y - data.Height * data.Cellsize / 2, transform.position.z);
        }
    }
}



