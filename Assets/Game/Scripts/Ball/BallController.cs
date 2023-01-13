using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = 0.125f;

    public LineRenderer Rope;
    public Transform BallTransform;

    private Vector3 offsetTargetPosition;

    private bool isRotating = false;
    void Start()
    {
        offsetTargetPosition = target.position - transform.position;
    }

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

    void FixedUpdate()
    {

        var angle = Quaternion.Angle(transform.rotation, target.rotation);
        smoothSpeed = isRotating ? Mathf.Clamp(angle, 5, 100) : 5;
        
        
        transform.position = Vector3.Slerp(transform.position, target.position + offsetTargetPosition, smoothSpeed* Time.fixedDeltaTime);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothSpeed* Time.fixedDeltaTime);
        
        Rope.SetPosition(0,target.position);
        Rope.SetPosition(1, BallTransform.position);
    }
}
