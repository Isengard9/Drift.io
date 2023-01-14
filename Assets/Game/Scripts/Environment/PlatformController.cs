using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    #region Variables

    [SerializeField] private List<Transform> partList = new List<Transform>();

    private Action OnPartLost;

    private float _timer = 20;
    private float _timeInterval = 20;

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        InitPartList();
    }

    private void FixedUpdate()
    {
        if (!GameManager.IsGameStarted || GameManager.IsGameEnded) return;
        
        ControlTimer();
    }

    #endregion

    #region InitPartList

    private void InitPartList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var part = transform.GetChild(i).transform;
            partList.Add(part);
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
        var part = partList[0];
        var targetY = part.position.y - 10;
        part.DOMoveY(targetY, 1).OnComplete((() =>
        {
            partList.Remove(part);
            Destroy(part.gameObject, 1);
        }));

    }

    #endregion
}
