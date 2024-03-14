namespace TestPong.Pong.Pad
{
    using TestPong.World;
    using UnityEngine;

    public sealed class PongPad : MonoBehaviour
    {
        [SerializeField] private WorldAreaObject _areaObject;
        [Space]
        [SerializeField] private float _speed;

        private void Update()
        {
            var position = _areaObject.GetInAreaPosition();
            var delta = Time.deltaTime;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                position.y += _speed * delta;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                position.y -= _speed * delta;
            }

            _areaObject.SetInAreaPosition(position);
        }
    }

}