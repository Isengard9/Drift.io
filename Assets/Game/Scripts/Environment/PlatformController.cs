using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    #region Variables

    [SerializeField] private List<Rigidbody> rbList = new List<Rigidbody>();

    private Action OnPartLost;

    private float _timer = 20;
    private float _timeInterval = 20;

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        InitRbList();
    }

    private void FixedUpdate()
    {
        if (!GameManager.IsGameStarted || GameManager.IsGameEnded) return;
        
        ControlTimer();
    }

    #endregion

    #region InitRbList

    private void InitRbList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var rb = transform.GetChild(i).GetComponent<Rigidbody>();
            rbList.Add(rb);
        }
    }

    #endregion

    #region LosePart

    private void ControlTimer()
    {
        if (_timer <= 0)
        {
            LosePart();
            _timer = _timeInterval;
        }

        else
        {
            _timer -= Time.fixedDeltaTime;
        }
    }
    
    private void LosePart()
    {
        var rb = rbList[0];
        rb.isKinematic = false;
        rb.useGravity = true;
        rbList.Remove(rb);
        Destroy(rb.gameObject, 5);
    }

    #endregion
}
