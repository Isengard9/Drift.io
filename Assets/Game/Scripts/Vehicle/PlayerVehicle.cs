using System;
using UnityEngine;

namespace Game.Scripts.Vehicle
{
    public class PlayerVehicle : Vehicle
    {
        
        #region Variables

        [SerializeField] private VariableJoystick joystick;

        [SerializeField] private float rotationSpeed = 5;

        #endregion

        #region Init

        protected override void OnInit()
        {
        }

        #endregion

        #region Monobehaviour

        private void FixedUpdate()
        {
            if (!GameManager.IsGameStarted || GameManager.IsGameEnded)
            {
                vehicleRigidbody.velocity -= Vector3.up * Time.fixedDeltaTime * MoveSpeed;
                return;
            }

            if (Input.GetMouseButton(0))
            {
                Rotate(Vector3.zero);
            }

            Move();
        }

        #endregion

        #region Physics

        protected override void Move()
        {
            if (!isForwardMoving)
            {
                forwardDirection.y -= Time.fixedDeltaTime * MoveSpeed*2;
                vehicleRigidbody.velocity =
                    Vector3.Lerp(vehicleRigidbody.velocity, forwardDirection, Time.fixedDeltaTime * MoveSpeed * 2);

                if (Vector3.Distance(vehicleRigidbody.velocity, forwardDirection) < 0.1f)
                    isForwardMoving = true;
            }
            else
            {
                MoveSpeed += Input.GetMouseButton(0) ? Time.fixedDeltaTime : -Time.fixedDeltaTime;
                MoveSpeed = Mathf.Clamp(MoveSpeed, MinMaxMoveSpeed.x, MinMaxMoveSpeed.y);
                var forward = transform.forward;
                forward.y = -20 * Time.fixedDeltaTime;

                if (transform.position.y > 0.2f)
                {
                    forward.x = forward.z = 0;
                    forward.y = -1;
                }

                vehicleRigidbody.velocity = Vector3.Lerp(vehicleRigidbody.velocity, forward * MoveSpeed,
                    Time.fixedDeltaTime * MoveSpeed * 20);
            }
        }

        protected override void Rotate(Vector3 targetPosition)
        {
            var horizontal = joystick.Horizontal;
            var vertical = joystick.Vertical;


            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0, Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg, 0),
                Time.fixedDeltaTime * rotationSpeed);
        }

        #endregion
    }
}