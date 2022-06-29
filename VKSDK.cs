using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VKSDK : YandexSDK
{
    public static YandexSDK VKInstance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<VKSDK>();

            }

            return _instance;
        }
    }
    
}
