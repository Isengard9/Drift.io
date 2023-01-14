using System;
using System.Collections.Generic;
using Game.Scripts.Ball;
using UnityEngine;

namespace Game.Scripts.Vehicle
{
    public class EnemyVehicle : Vehicle
    {
        #region Variables

        private Transform target;
        public Transform Target => target;

        [SerializeField] private float minDistanceFromTarget = 3;

        
        [SerializeField] private float rotateSpeed = 5;
        [SerializeField] private WreckingBallController wreckingBallController;
        #endregion

        protected override void OnInit()
        {
        }


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
                wreckingBallController.EnemyEffect(isRotatingTime);
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
            
            // vehicleRigidbody.velocity = Vector3.Lerp(vehicleRigidbody.velocity, forward * MoveSpeed,
            //     Time.deltaTime * 5);
        }
        
        

        protected override void Rotate()
        {
            if(target is null)
                return;
            
            var targetPos = target.position;
            targetPos.y = transform.position.y;
            var targetRotation = Quaternion.LookRotation(targetPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Ground"))
            {
                isForwardMoving = true;
            }
        }

        

        #endregion
    }
}