using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Match3 {
    public class MessageForWallVK
    {
        private Level _level;
        private YandexSDK _SDK;
        public MessageForWallVK(Level level, YandexSDK SDK)
        {
            _level = level;
            _SDK = SDK;
        }
        public void ShowMessage()
        {
            
            if (_level.MessageForWallVK)
            {
                string message;
                if (_level.number == 1) message = "я начал играть в новую игру: ‘–” “џ 3 в р€д!";
                else message = "я достиг " + _level.number.ToString() + " уровн€ в игре ‘–” “џ 3 в р€д!";
                _SDK.ShowMessageForWallVK(message);

            }
        }
    } 
}

