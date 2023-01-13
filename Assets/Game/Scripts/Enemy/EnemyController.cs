using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Interface;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, IDestroyable
{

    #region Variables

    public BallController ballController;

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

    #region DestroyAction

    public void DestroyAction()
    {
        
    }

    #endregion
}
