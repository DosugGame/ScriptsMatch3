using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKSDK : YandexSDK
{
    public static YandexSDK OKInstance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<OKSDK>();

            }

            return _instance;
        }
        
    }
}
