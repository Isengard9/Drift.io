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

        private void Update()
        {
            if (!GameManager.IsGameStarted || GameManager.IsGameEnded) return;

            if (Input.GetMouseButton(0))
            {
                Rotate();
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
                    Vector3.Lerp(vehicleRigidbody.velocity, forwardDirection, Time.deltaTime * MoveSpeed * 2);

                if (Vector3.Distance(vehicleRigidbody.velocity, forwardDirection) < 0.2f)
                    isForwardMoving = true;
            }
            else
            {
                MoveSpeed += Input.GetMouseButton(0) ? Time.deltaTime : -Time.deltaTime;
                MoveSpeed = Mathf.Clamp(MoveSpeed, MinMaxMoveSpeed.x, MinMaxMoveSpeed.y);
                var forward = transform.forward;
                forward.y = -20 * Time.deltaTime;

                if (transform.position.y > 0.2f)
                {
                    forward.x = forward.z = 0;
                    forward.y = -1;
                }

                vehicleRigidbody.velocity = Vector3.Lerp(vehicleRigidbody.velocity, forward * MoveSpeed,
                    Time.deltaTime * MoveSpeed * 20);
            }
        }

        protected override void Rotate()
        {
            var horizontal = joystick.Horizontal;
            var vertical = joystick.Vertical;


            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0, Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg, 0),
                Time.deltaTime * rotationSpeed);
        }

        #endregion
    }
}