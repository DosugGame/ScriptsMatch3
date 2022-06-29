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
                if (_level.number == 1) message = "� ����� ������ � ����� ����: ������ 3 � ���!";
                else message = "� ������ " + _level.number.ToString() + " ������ � ���� ������ 3 � ���!";
                _SDK.ShowMessageForWallVK(message);

            }
        }
    } 
}

