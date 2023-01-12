using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Instance

    public static TimeManager instance;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    #region Actions

    

    #endregion
}
