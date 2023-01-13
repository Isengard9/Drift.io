using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    
    #region Variables

    public Transform target;
    public float smoothSpeed = 0.125f;

    public LineRenderer Rope;
    public Transform BallTransform;

    [SerializeField] private List<Transform> ballList = new List<Transform>();

    private Vector3 offsetTargetPosition;

    private bool isRotating = false;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        SetBallTransform();
    }

    void Start()
    {
        offsetTargetPosition = target.position - transform.position;
    }
    
    void FixedUpdate()
    {

        var angle = Quaternion.Angle(transform.rotation, target.rotation);
        smoothSpeed = isRotating ? Mathf.Clamp(angle, 5, 100) : 5;
        
        
        transform.position = Vector3.Slerp(transform.position, target.position + offsetTargetPosition, smoothSpeed* Time.fixedDeltaTime);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothSpeed* Time.fixedDeltaTime);
        
        Rope.SetPosition(0,target.position);
        Rope.SetPosition(1, BallTransform.position);
    }

    #endregion

    #region RotateTarget

    public void RotateTarget()
    {
        if(isRotating)
            return;
        
        isRotating = true;

        var targetRotation = 0;
        Rope.enabled = false;

        DOTween.To(() => targetRotation, x => targetRotation = x, 360 * 6, 3).OnUpdate(() =>
        {
            target.localEulerAngles = Vector3.up * targetRotation;
        }).OnComplete(() =>
        {
            target.localEulerAngles = Vector3.zero;
            isRotating = false;
            Rope.enabled = true;
        });

    }

    #endregion

    #region SetBallTransform

    private void SetBallTransform()
    {
        var _index = Random.Range(0, ballList.Count);
        
        ballList[_index].gameObject.SetActive(true);
        BallTransform = ballList[_index];
    }

    #endregion

}
