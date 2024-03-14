namespace TestPong.World
{
    using UnityEngine;

    public sealed class WorldArea : MonoBehaviour
    {
        [SerializeField] private Vector2 _size;
        [Space]
        [SerializeField] private bool _isGizmoEnabled;

        public Rect AreaRect
        {
            get => GetAreaRect();
        }

        public Rect GetAreaRect()
        {
            var rect = new Rect(Vector2.zero, _size);
            rect.center = Vector2.zero;

            return rect;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(!_isGizmoEnabled)
            {
                return;
            }

            Gizmos.DrawWireCube(AreaRect.center, AreaRect.size);
        }
#endif
    }
}
