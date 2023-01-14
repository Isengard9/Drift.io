using System;
using Mono.Cecil.Rocks;
using UnityEngine;

namespace Game.Scripts.Vehicle
{
    public abstract class Vehicle : MonoBehaviour, IVehicle
    {

        #region Variables

        public Rigidbody vehicleRigidbody;
        public float MoveSpeed;
        public Vector2 MinMaxMoveSpeed = Vector2.zero;
        
        public Vector3 forwardDirection = Vector3.zero;
        public bool isForwardMoving = false;

        #endregion
        
        #region Start/Init

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            ControllerContainer.ControllerContainer.Instance.AddVehicle(this);
            OnInit();
        }
        

        #endregion

        #region Protected

        protected abstract void OnInit();

        protected abstract void Move();
        protected abstract void Rotate();
        

        #endregion
    }
}