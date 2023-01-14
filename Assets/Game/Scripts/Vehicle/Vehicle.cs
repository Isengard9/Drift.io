using System;
using Mono.Cecil.Rocks;
using UnityEngine;

namespace Game.Scripts.Vehicle
{
    public abstract class Vehicle : MonoBehaviour, IVehicle
    {
        public Rigidbody vehicleRigidbody;
        public float MoveSpeed;
        public Vector2 MinMaxMoveSpeed = Vector2.zero;
        
        public Vector3 forwardDirection = Vector3.zero;
        public bool isForwardMoving = false;
        private void Start()
        {
            Init();
        }

        public void Init()
        {
            ControllerContainer.ControllerContainer.Instance.AddVehicle(this);
            OnInit();
        }

        protected abstract void OnInit();

        protected abstract void Move();
        protected abstract void Rotate();
        
    }
}