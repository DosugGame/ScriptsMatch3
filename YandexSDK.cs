using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexSDK : MonoBehaviour
{
    // Создание SINGLETON
    protected static YandexSDK _instance;
    public static YandexSDK Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<YandexSDK>();

            }
            
            return _instance;
        }
    }

    
    
    public  void Init()
    {
        DontDestroyOnLoad(gameObject);
        AuthSuccess += AuthSuccessDel;
        DataGet += DataGetDel;
        RewardGet += RewardGetDel;
        Debug.Log(RewardGet + "   ивент рекламы");
        




    }
    
    public void Clear()
    {
        AuthSuccess -= AuthSuccessDel;
        DataGet -= DataGetDel;
        RewardGet -= RewardGetDel;
        Open -= OpenDel;
        CloseOrError -= CloseOrErrorDel;
        AuthSuccessDel = null;
        DataGetDel = null;
        RewardGetDel = null;
        OpenDel = null;
        CloseOrErrorDel = null;
    }
    public void SoundOff()
    {
        if (Open != null && OpenDel != null) Open();
    }
    public void SoundOn()
    {
        if (CloseOrError != null && CloseOrErrorDel != null) CloseOrError();
    }

    public void SoundInit()
    {
        Open += OpenDel;
        CloseOrError += CloseOrErrorDel;
    }

    UserGameData UGD;
    private UserData UD;

    public UserGameData GetUserGameData => UGD;

    public UserData GetUserData => UD;
    //
    
    [DllImport("__Internal")]
    protected static extern void Auth();    // Авторизация - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    protected static extern void SendToTheWallVK(string message);    
    [DllImport("__Internal")]
    protected static extern void ShowCommonADV();    // Показ обычной рекламы - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    protected static extern void GetData();    // Получение данных - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    protected static extern void SetData(string data);    // Отправка данных - внешняя функция для связи с плагином
    [DllImport("__Internal")]
    protected static extern void ShowRewardADV();    // Показ рекламы с наградой - внешняя функция для связи с плагином
    
    public event Action AuthSuccess;    //События
    public event Action DataGet;    //События
    public event Action RewardGet;  //События
    public event Action Open;    //Делегат
    public event Action CloseOrError;  //Делегат
    public  Action AuthSuccessDel;    //Делегат
    public  Action DataGetDel;    //Делегат
    public  Action RewardGetDel;  //Делегат
    public Action OpenDel;    //Делегат
    public Action CloseOrErrorDel;  //Делегат
  
    public void Authenticate()    //    Авторизация
    {
        Auth();
    }
    public void ShowMessageForWallVK(string message)
    {
        SendToTheWallVK(message);
    }
    public void GettingData()    // Получение данных
    {
        GetData();
    }

    public void SettingData(string data)    // Сохранение данных
    {
        SetData(data);
    }

    public void ShowCommonAdvertisment()    // Показ обычной рекламы
    {
        ShowCommonADV();
    }

    public void ShowRewardAdvertisment()    // Показ рекламы с наградой
    {
        ShowRewardADV();
    }

    
    public void AuthenticateSuccess(string data)    // Авторизация успешно пройдена
    {
        UD.Name = data;
        if (AuthSuccess != null) AuthSuccess();
        
    }
    
    public void DataGetting(string data) // Данные получены
    {
        UserDataSaving UDS = new UserDataSaving();
        UDS = JsonUtility.FromJson<UserDataSaving>(data);
        UGD = JsonUtility.FromJson<UserGameData>(UDS.data);
        if (DataGet != null) DataGet();
       
    }
    
    public void RewardGetting() // Реклама просмотрена
    {
        
        if (RewardGet != null) RewardGet();
        print("Реклама просмотрена");
        print(RewardGet);
       
    }



}

[Serializable]
public class UserData
{
    public string Name;
}

[Serializable]
public class UserGameData
{
    public UserGameData(int coin)
    {
        Coin = coin;
    }
    public int Coin;
}
[Serializable]
public class UserDataSaving
{
    public string data;
}
