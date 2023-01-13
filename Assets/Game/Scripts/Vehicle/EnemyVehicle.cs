using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Vehicle
{
    public class EnemyVehicle : Vehicle
    {
        #region Variables

        private Transform target;
        public Transform Target => target;

        [SerializeField] private float minDistanceFromTarget = 3;

        [SerializeField] private float movementSpeed = 2;
        [SerializeField] private float rotateSpeed = 5;
        #endregion

        protected override void OnInit()
        {
        }


        private void FindTarget()
        {
            target = ControllerContainer.ControllerContainer.Instance.Vehicles.Find(x => x != this
                && Vector3.Distance(transform.position, x.transform.position) > minDistanceFromTarget).transform;

            if (target == null)
            {
                var enemyCount = ControllerContainer.ControllerContainer.Instance.Vehicles.Count;
                
                if (enemyCount <= 1)
                {
                    Debug.Log("Game Failed");
                }

                else
                {
                    target = ControllerContainer.ControllerContainer.Instance.Vehicles.Find(x => x != this).transform;
                }
            }
        }

        #region DestroyAction

        public void DestroyAction()
        {
        }

        #endregion

        private float waitToRotate = 1;
        private bool isRotatingTime = false;
        private void Update()
        {
            if (!GameManager.IsGameStarted || GameManager.IsGameEnded) return;

            if (waitToRotate > 0)
            {
                if (isRotatingTime)
                {
                    ControlCheckTarget();
                    Rotate();
                }

                waitToRotate -= Time.deltaTime;
            }

            else
            {
                isRotatingTime = !isRotatingTime;
                waitToRotate = 1;
            }
            Move();
            
            
        }

        private void ControlCheckTarget()
        {
            if (target is null)
            {
                FindTarget();
                return;
            }
            
            var distance = Vector3.Distance(target.position, transform.position);

            if (distance < minDistanceFromTarget)
            {
                FindTarget();
            }

        }


        #region Physics

        protected override void Move()
        {
            vehicleRigidbody.position += transform.forward * Time.deltaTime * movementSpeed;
        }

        protected override void Rotate()
        {
            if(target is null)
                return;
            
            var targetPos = target.position;
            var targetRotation = Quaternion.LookRotation(targetPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }

        #endregion
    }
}