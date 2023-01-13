using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Variables

    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField]
    private Vector3 direction = Vector3.zero;

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!GameManager.isGameStarted || GameManager.isGameEnded) return;
        
        if (Input.GetMouseButton(0))
        {
            Rotation();
        }
        
        Move();
    }

    #endregion

    #region Move

    private void Move()
    {
        rb.velocity = transform.forward * moveSpeed;
    }
    
    private void Rotation()
    {
        var horizontal = joystick.Horizontal;
        var vertical = joystick.Vertical;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,Mathf.Atan2(horizontal,vertical) * Mathf.Rad2Deg, 0), Time.deltaTime * rotationSpeed);
    }

    #endregion
    

    
}
