using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallTransformController : MonoBehaviour
{
    
    #region Variables

    [SerializeField] private List<Transform> ballList = new List<Transform>();

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        SetBallTransform();
    }
    
    #endregion

    #region SetBallTransform

    private void SetBallTransform()
    {
        var _index = Random.Range(0, ballList.Count);
        
        ballList[_index].gameObject.SetActive(true);
    }

    #endregion

}
