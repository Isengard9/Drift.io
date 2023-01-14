using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Scripts.Interface;
using UnityEngine;

public class PowerUpItem : MonoBehaviour, IDestroyable
{

    #region Variables

    [SerializeField] private GameObject parachute;
    
    private Rigidbody rb;

    #endregion

    #region MonoBehaviour

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            rb.isKinematic = true;
            ScaleDownParachute();
        }
    }

    #endregion

    #region Parachute

    private void ScaleDownParachute()
    {
        parachute.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 1)
            .OnComplete((() =>
            {
                parachute.SetActive(false);
            }));
    }

    #endregion

    #region DestroyAction

    public void DestroyAction()
    {
        Destroy(gameObject, 0.1f);
    }

    #endregion
}
