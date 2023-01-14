using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Ball;
using Game.Scripts.Interface;
using Game.Scripts.Vehicle;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, IDestroyable
{

    #region Variables

    [SerializeField] private WreckingBallController wreckingBallController;
    [SerializeField] private EnemyVehicle enemyVehicle;
    
    [SerializeField] private MeshRenderer carMesh;
    [SerializeField] private SkinnedMeshRenderer driverMesh;
    
    [SerializeField] private List<Material> colorList = new List<Material>();
    

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        SetCarColor();
        SetDriverColor();
    }

    #endregion

    #region SetMaterials

    private void SetCarColor()
    {
        var random = Random.Range(0, colorList.Count);
        carMesh.material = colorList[random];
    }

    private void SetDriverColor()
    {
        var random = Random.Range(0, colorList.Count);
        driverMesh.material = colorList[random];
    }

    #endregion

    #region Trigger/Collision

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Space"))
        {
            DestroyAction();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            enemyVehicle.isForwardMoving = true;
        }
    }

    #endregion
    
    #region DestroyAction

    public void DestroyAction()
    {
        Destroy(gameObject,0.1f);
    }

    #endregion
}
