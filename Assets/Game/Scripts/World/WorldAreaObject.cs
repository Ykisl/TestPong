namespace TestPong.World
{
    using UnityEngine;
    using Zenject;
    using Zenject.ReflectionBaking.Mono.Cecil.Cil;

    public class WorldAreaObject : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private WorldArea _worldArea;
        [Space]
        [SerializeField] private Vector2 _size;
        [SerializeField] private bool _isClampPositionVertical;
        [SerializeField] private bool _isClampPositionHorizontal;
        [Space]
        [SerializeField] private bool _isGizmoEnabled;

        public Rect ObjectRect
        {
            get => GetObjectRect();
        }

        public Rect GetObjectRect(Vector2? position = null)
        {
            position ??= _transform.position;

            var rect = new Rect(position.Value, _size);
            rect.center = position.Value;
            return rect;
        }

        public Vector2 ClampInAreaPosition(Vector2 absolutePosition)
        {
            return ClampInAreaPosition(absolutePosition, out var isPositionOutOfArea);
        }

        public Vector2 ClampInAreaPosition(Vector2 absolutePosition, out bool isPositionOutOfArea)
        {
            var inAreaPosition = absolutePosition;
            isPositionOutOfArea = false;

            var worldAreaRect = _worldArea.AreaRect;
            var objectRect = GetObjectRect(absolutePosition);

            if (_isClampPositionVertical)
            {
                if(objectRect.yMax > worldAreaRect.yMax)
                {
                    inAreaPosition.y = worldAreaRect.yMax - _size.y/2;
                    isPositionOutOfArea = true;
                }

                if (objectRect.yMin < worldAreaRect.yMin)
                {
                    inAreaPosition.y = worldAreaRect.yMin + _size.y / 2;
                    isPositionOutOfArea = true;
                }
            }

            if (_isClampPositionHorizontal)
            {
                if (objectRect.xMax > worldAreaRect.xMax)
                {
                    inAreaPosition.x = worldAreaRect.xMax - _size.x / 2;
                    isPositionOutOfArea = true;
                }

                if (objectRect.xMin < worldAreaRect.xMin)
                {
                    inAreaPosition.x = worldAreaRect.xMin + _size.x / 2;
                    isPositionOutOfArea = true;
                }
            }

            return inAreaPosition;
        }

        public Vector2 GetInAreaPosition()
        {
            return ClampInAreaPosition(_transform.position);
        }

        public void SetInAreaPosition(Vector2 position)
        {
            TrySetInAreaPosition(position);
        }

        public bool TrySetInAreaPosition(Vector2 position)
        {
            var inAreaPosition = ClampInAreaPosition(position, out var isPositionOutOfArea);
            _transform.position = inAreaPosition;

            return !isPositionOutOfArea;
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            if (!_isGizmoEnabled)
            {
                return;
            }

            Gizmos.DrawWireCube(ObjectRect.center, _size);
        }
#endif
    }
}
