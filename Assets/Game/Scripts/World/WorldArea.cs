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
            return new Rect(Vector2.zero, _size);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(!_isGizmoEnabled)
            {
                return;
            }

            Gizmos.DrawWireCube(AreaRect.position, AreaRect.size);
        }
#endif
    }
}
