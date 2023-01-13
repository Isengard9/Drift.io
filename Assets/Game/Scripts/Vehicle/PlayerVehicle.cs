using UnityEngine;

namespace Game.Scripts.Vehicle
{
    public class PlayerVehicle : Vehicle
    {
        #region Variables

        [SerializeField] private VariableJoystick joystick;
        
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed = 5;

        #endregion

        #region On Init

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
            vehicleRigidbody.velocity = transform.forward * moveSpeed;
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