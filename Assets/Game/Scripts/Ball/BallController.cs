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
    private float defaultSmoothSpeed;
    public LineRenderer Rope;
    public Transform BallTransform;

    [SerializeField] private List<Transform> ballList = new List<Transform>();

    private Vector3 offsetTargetPosition;

    private bool isRotating = false;
    public Rigidbody rb;
    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        SetBallTransform();
        defaultSmoothSpeed = smoothSpeed;
    }

    void Start()
    {
        offsetTargetPosition = target.position - transform.position;
    }
    
    void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, target.position + offsetTargetPosition, smoothSpeed* Time.fixedDeltaTime);
        rb.angularVelocity += target.transform.eulerAngles * Time.fixedDeltaTime;
        rb.angularVelocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.fixedDeltaTime * smoothSpeed);
        rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, 2);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothSpeed* Time.fixedDeltaTime);
        return;
        var angle = Quaternion.Angle(transform.rotation, target.rotation);
        smoothSpeed = isRotating ? Mathf.Clamp(angle, 5, 100) : defaultSmoothSpeed;
        
        
        transform.position = Vector3.Slerp(transform.position, target.position + offsetTargetPosition, smoothSpeed* Time.fixedDeltaTime);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothSpeed* Time.fixedDeltaTime);
        
        Rope.SetPosition(0,target.position);
        Rope.SetPosition(1, BallTransform.position);
    }

    #endregion

    #region RotateTarget

    public void RotateAroundVehicle()
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }
}
