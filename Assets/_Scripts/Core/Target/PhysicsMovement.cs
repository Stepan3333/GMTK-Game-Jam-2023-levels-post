using UnityEngine;

namespace Core.Targets
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsMovement : MonoBehaviour, IMovement
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _decceleration;
        private float _velocityPower = 0.5f;

        private const float EPSILON = 0.01f;

        public float MaxMoveSpeed { get => _maxSpeed; set => _maxSpeed = value; }

        public void Move(Vector3 direction)
        {
            Vector3 targetVelocity = new Vector3(direction.x * MaxMoveSpeed, 0f, direction.z * MaxMoveSpeed);
            Vector3 velocityDifference = targetVelocity - _rigidbody.velocity;
            float accelerationRate = (Mathf.Abs(targetVelocity.magnitude) > EPSILON) ? _acceleration : _decceleration;
            float movementX = Mathf.Pow(Mathf.Abs(velocityDifference.x) * accelerationRate, _velocityPower) * Mathf.Sign(velocityDifference.x);
            float movementZ = Mathf.Pow(Mathf.Abs(velocityDifference.z) * accelerationRate, _velocityPower) * Mathf.Sign(velocityDifference.z);

            _rigidbody.AddForce(new Vector3(movementX, _rigidbody.velocity.y, movementZ));
        }
    }
}