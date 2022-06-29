using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Match3 {
    public class LevelsSystem : MonoBehaviour
    {
        private static Level CurrentLevel;
        private bool _levelLoaded = false;
        public SaveSystem _saveLoad = new SaveSystem();
        public int NumberLevels { get; private set; } = 32;
       
        
        public void LoadLevel(int number)
        {
            CurrentLevel = Create(number);
            print(CurrentLevel.number);
           
        }
        public void LoadLevelAndScene(int number)
        {
            LoadLevel(number);
            LoadScene();
        }
        public Level Current()
        {
            if(CurrentLevel == null) LoadCurrentLevel();
            return CurrentLevel;
        }
        public void OpenNextLevel()
        {
            if(_saveLoad.Load("CurrentLevel") == CurrentLevel.number)
            _saveLoad.Save("CurrentLevel", _saveLoad.Load("CurrentLevel") + 1);
        }
        
        public void NextLevel()
        {
            int number = CurrentLevel.number + 1;
            LoadLevel(number);
            LoadScene();
        }
        public void LoadScene()
        {
            SceneManager.LoadScene(1);
        }
        public void LoadCurrentLevel()
        {
            LoadLevel(GetCurrentLevelInt());
            _levelLoaded = true;
        }
        public int GetCurrentLevelInt()
        {
            if (!PlayerPrefs.HasKey("CurrentLevel")) PlayerPrefs.SetInt("CurrentLevel",1);
            return PlayerPrefs.GetInt("CurrentLevel");
             
        }
        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void Home()
        {
            SceneManager.LoadScene(0);
        }
        public void SetCurrentLevelInt(int value)
        {
            PlayerPrefs.SetInt("CurrentLevel", value);
        }
        public Level Create(int number)
        {
            Level level;
            switch (number)
            {
                case 1: level = new Level_1(); break;
                case 2: level = new Level_2(); break;
                case 3: level = new Level_3(); break;
                case 4: level = new Level_4(); break;
                case 5: level = new Level_5(); break;
                case 6: level = new Level_6(); break;
                case 7: level = new Level_7(); break;
                case 8: level = new Level_8(); break;
                case 9: level = new Level_9(); break;
                case 10: level = new Level_10(); break;
                case 11: level = new Level_11(); break;
                case 12: level = new Level_12(); break;
                case 13: level = new Level_13(); break;
                case 14: level = new Level_14(); break;
                case 15: level = new Level_15(); break;
                case 16: level = new Level_16(); break;
                case 17: level = new Level_17(); break;
                case 18: level = new Level_18(); break;
                case 19: level = new Level_19(); break;
                case 20: level = new Level_20(); break;
                case 21: level = new Level_21(); break;
                case 22: level = new Level_22(); break;
                case 23: level = new Level_23(); break;
                case 24: level = new Level_24(); break;
                case 25: level = new Level_25(); break;
                case 26: level = new Level_26(); break;
                case 27: level = new Level_27(); break;
                case 28: level = new Level_28(); break;
                case 29: level = new Level_29(); break;
                case 30: level = new Level_30(); break;
                case 31: level = new Level_31(); break;
                case 32: level = new Level_32(); break;
                case 33: level = new Level_33(); break;
                case 34: level = new Level_34(); break;
                case 35: level = new Level_35(); break;
                case 36: level = new Level_36(); break;
                case 37: level = new Level_37(); break;
                case 38: level = new Level_38(); break;
                case 39: level = new Level_39(); break;
                case 40: level = new Level_40(); break;
                case 41: level = new Level_41(); break;
                case 42: level = new Level_42(); break;
                case 43: level = new Level_43(); break;
                case 44: level = new Level_44(); break;
                case 45: level = new Level_45(); break;
                case 46: level = new Level_46(); break;
                case 47: level = new Level_47(); break;
                case 48: level = new Level_48(); break;
                case 49: level = new Level_49(); break;
                case 50: level = new Level_50(); break;
                default:
                    level = new Level_1(); break;
            }
            return level;
        }
    }
}
