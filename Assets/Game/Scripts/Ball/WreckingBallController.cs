using System;
using UnityEngine;

namespace Game.Scripts.Ball
{
    public class WreckingBallController : MonoBehaviour
    {
        [SerializeField] private Vehicle.Vehicle myVehicle;

        [Header("Joint")] [SerializeField] public HingeJoint joint;

        [SerializeField] private bool isSpinning = false;
        [SerializeField] private Vector3 defaultConnectedAnchor = new Vector3(0, 1, -1);
        [SerializeField] private Vector3 spinAnchor = new Vector3(0, 0, 2);
        [SerializeField] private Vector3 spinConnectedAnchor = new Vector3(0, 1, 3);
        [SerializeField] private float spinTime = 5;
        [SerializeField] private Rigidbody wreckingRigidbody;
        [Header("Renderer")] [SerializeField] private LineRenderer lr;

        private void Update()
        {
            if (!isSpinning)
            {
                joint.anchor = Vector3.Lerp(
                    joint.anchor,
                    Vector3.forward * Convert(myVehicle.MoveSpeed, 5, 7, 2, 5),
                    Time.deltaTime * 2
                );
                joint.connectedAnchor = Vector3.Lerp(
                    joint.connectedAnchor,
                    defaultConnectedAnchor,
                    Time.deltaTime * 4);
            }
            else
            {
                joint.anchor = Vector3.Lerp(
                    joint.anchor,
                    spinAnchor,
                    Time.deltaTime * 5
                );
                joint.connectedAnchor = Vector3.Lerp(
                    joint.connectedAnchor,
                    spinConnectedAnchor,
                    Time.deltaTime * 5);
            }

            lr.SetPosition(0, myVehicle.transform.position + Vector3.up);
            lr.SetPosition(1, transform.position);
        }

        public void EnemyEffect(bool useEffect)
        {
            joint.useMotor = useEffect;

            var m = joint.motor;
            
            m.targetVelocity = useEffect ? 2 : 5000;
            m.force = useEffect ? 2 : 5000;
            
            joint.motor = m;

        }
        public void SpinStart()
        {
            if (isSpinning)
                return;

            isSpinning = true;
            joint.useLimits = false;
            joint.useMotor = true;
            lr.enabled = false;
            Invoke(nameof(SpinStop), spinTime);
        }

        public void SpinStop()
        {
            isSpinning = false;
            joint.useLimits = true;
            joint.useMotor = false;
            lr.enabled = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var vehicle = collision.transform.GetComponent<Vehicle.Vehicle>();

            if (vehicle is not null)
            {
               ForceVehicle(vehicle);
            }
        }

        private void ForceVehicle(Vehicle.Vehicle vehicle)
        {
            
            var forceDirection = (vehicle.transform.position - transform.position).normalized * 10;
            forceDirection.y = 10;
            vehicle.forwardDirection = forceDirection;
            vehicle.isForwardMoving = false;


            // vehicle.vehicleRigidbody.AddForce(
            //     (forceDirection.normalized * wreckingRigidbody.velocity.magnitude * 10 * vehicle.vehicleRigidbody.mass) + Vector3.up * 100 * vehicle.vehicleRigidbody.mass,
            //     ForceMode.Impulse);
        }

        public float Convert(float value, float min1, float max1, float min2, float max2)
        {
            return (value - min1) * (max2 - min2) / (max1 - min1) + min2;
        }
    }
}