using System;
using System.Collections.Generic;
using System.Numerics;
using Game.Scripts.Ball;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Game.Scripts.Vehicle
{
    public class EnemyVehicle : Vehicle
    {
        #region Variables

        private Transform target;
        public Transform Target => target;

        [SerializeField] private WreckingBallController wreckingBallController;

        [SerializeField] private float minDistanceFromTarget = 3;
        [SerializeField] private float rotateSpeed = 5;
        
        #endregion

        #region Target

        private void FindTarget()
        {
            var newTarget = ControllerContainer.ControllerContainer.Instance.Vehicles.Find(x => x != this
                && Vector3.Distance(transform.position, x.transform.position) > minDistanceFromTarget);

            if(newTarget == null)
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
            else
            {
                target = newTarget.transform;
            }
        }
        
        private void ControlCheckTarget()
        {
            if (target == null)
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

        #endregion

        #region MonoBehaviour

        protected override void OnInit()
        {
            
        }
        
        private float waitToRotate = 1;
        private bool isRotatingTime = false;
        private bool randomRotateActive = false;
        private Vector3 randomRotationPoint = Vector3.zero;
        private void Update()
        {
            if (!GameManager.IsGameStarted || GameManager.IsGameEnded)
            {
                vehicleRigidbody.velocity -= Vector3.up * Time.deltaTime * MoveSpeed;
                return;
            }

            if (waitToRotate > 0)
            {
                if (isRotatingTime)
                {
                    ControlCheckTarget();

                    if (randomRotateActive)
                    {
                        Rotate(randomRotationPoint);
                    }
                    else
                    {
                        Rotate(target.position);
                    }
                    
                }
                wreckingBallController.EnemyEffect(isRotatingTime);
                waitToRotate -= Time.deltaTime;
            }
            
            else
            {
                randomRotateActive = Random.value > 0.7f;
                randomRotationPoint = (Vector3.forward + Vector3.right) * Random.Range(-10, 10);
                isRotatingTime = !isRotatingTime;
                waitToRotate = 1;
            }
            
            Move();
        }

        #endregion
        
        #region Physics

        protected override void Move()
        {
            
            if (!isForwardMoving)
            {
                forwardDirection.y -= Time.deltaTime * MoveSpeed;
                
                vehicleRigidbody.velocity =
                    Vector3.Lerp(vehicleRigidbody.velocity, forwardDirection, Time.deltaTime * MoveSpeed *2);
                
                if (Vector3.Distance(vehicleRigidbody.velocity, forwardDirection) < 0.2f)
                    isForwardMoving = true;
                
            }
            else
            {
                var forward = transform.forward;
                forward.y = -20 * Time.deltaTime;
                if (transform.position.y > 0.2f)
                {
                    forward.x = forward.z = 0;
                    forward.y = -1;
                }
                vehicleRigidbody.velocity = Vector3.Lerp(vehicleRigidbody.velocity, forward * MoveSpeed, Time.deltaTime * MoveSpeed *20);
            }
            
        }
        
        protected override void Rotate(Vector3 targetPosition)
        {
            if(target is null)
                return;

            targetPosition.y = transform.position.y;
            var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
        
        #endregion
    }
}