using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Match3
{
    public enum AdsVariant {Yandex, VK, OK }
    public class Ads 
    {
        public event Action AdViewed;
        public YandexSDK SDK;
        private SoundForYandex sound;
        
        public Ads(AdsVariant variant, SoundForYandex s)
        {
            if (variant == AdsVariant.Yandex)
            {
                init(); 
            }
            else if (variant == AdsVariant.VK) initVK();
            else if (variant == AdsVariant.OK) initOK();
            sound = s;
        }

        

        private void init()
        {
            SDK = YandexSDK.Instance;
            SDK.Clear();
            SDK.AuthSuccessDel += SettingMame;
            SDK.DataGetDel += SettingData;
            SDK.RewardGetDel += AdViewedFunction;
            Debug.Log(SDK.RewardGetDel);
            SDK.Init();
            Debug.Log("YAndex");
        }
        private void initVK()
        {
            SDK = VKSDK.VKInstance;
            SDK.Clear();
            SDK.AuthSuccessDel += SettingMame;
            SDK.DataGetDel += SettingData;
            SDK.RewardGetDel += AdViewedFunction;
            SDK.Init();
            Debug.Log("VK");
        }
        private void initOK()
        {
            SDK = OKSDK.OKInstance;
            SDK.Clear();
            SDK.AuthSuccessDel += SettingMame;
            SDK.DataGetDel += SettingData;
            SDK.RewardGetDel += AdViewedFunction;
            SDK.Init();
            Debug.Log("OK");
        }
        private void AdViewedFunction()
        {
            if (AdViewed != null) AdViewed();
        }
        public void ShowCommon() //обычная реклама
        {
            SDK.ShowCommonAdvertisment();
        }
        public void SoundOff() 
        {
            if(sound != null)sound.SoundOff();
        }
        public void SoundOn() 
        {
            if (sound != null) sound.SoundOn();
        }
        public void ShowReward() //с вознаграждением
        {
            SDK.ShowRewardAdvertisment();
        }
        public void Auth()
        {
            SDK.Authenticate();
        }
        public void GetData()
        {
            SDK.GettingData();
        }
        public void SetData()
        {
            UserGameData UD = new UserGameData(5);
            SDK.SettingData(JsonUtility.ToJson(UD));
        }
        
       



       
        private void SettingData()
        {
            
        }
        private void SettingMame()
        {
            
           
        }
        

        
        

        
    }


}
