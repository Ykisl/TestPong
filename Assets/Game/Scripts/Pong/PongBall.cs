namespace TestPong.Pong.Pad
{
    using System.Linq;
    using UnityEngine;

    public class PongBall : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body;
        [Space]
        [SerializeField] private bool _isActiveOnStart = false;
        [Space]
        [SerializeField] private float _startSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _speedStep;

        private bool _isActive;
        private float _speed;
        private Vector2 _moveDirection;

        private void Start()
        {
            SetIsActive(true);
        }

        #region ActiveState

        public void SetIsActive(bool isActive)
        {
            _isActive = isActive;
            if (_isActive)
            {
                ActiveBall();
                return;
            }

            DeactiveBall();
        }

        private void ActiveBall()
        {
            _body.simulated = true;
            _speed = _startSpeed;
            _moveDirection = new Vector2(1, 1);

            UpdateBodyVelocity();
        }

        private void DeactiveBall()
        {
            _body.simulated = false;
            _speed = 0;
            _moveDirection = Vector2.zero;

            UpdateBodyVelocity();
        }

        #endregion

        private void UpdateBodyVelocity()
        {
            _body.velocity = _moveDirection * _speed;
        }

        private void SetMoveDirection(Vector2 moveDirection)
        {
            _moveDirection = moveDirection;
            UpdateBodyVelocity();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(!_isActive)
            {
                return;
            }

            var hitGameObject = collision.gameObject;
            if (hitGameObject == null)
            {
                return;
            }

            if (collision.contacts.Length > 0)
            {
                var contact = collision.contacts.First();

                var newMoveDirection = Vector2.Reflect(_moveDirection, contact.normal);
                SetMoveDirection(newMoveDirection);
            }
        }
    }
}