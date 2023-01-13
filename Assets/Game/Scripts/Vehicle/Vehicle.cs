using System;
using Mono.Cecil.Rocks;
using UnityEngine;

namespace Game.Scripts.Vehicle
{
    public abstract class Vehicle : MonoBehaviour, IVehicle
    {
        public Rigidbody vehicleRigidbody;
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