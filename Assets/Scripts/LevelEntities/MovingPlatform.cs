using UnityEngine;

namespace LevelEntities
{
    public class MovingPlatform : MonoBehaviour
    {
        [Header("Movement Settings")]
        public Vector2 pointA;
        public Vector2 pointB;
        public float speed = 2f;

        private Vector2 _targetPosition;

        private void Start()
        {
            _targetPosition = pointB;
        }

        private void FixedUpdate()
        {
            Vector2 currentPosition = transform.position;
            var direction = (_targetPosition - currentPosition).normalized;
            var step = speed * Time.deltaTime;
        
            transform.Translate(direction * step);
            
            if (Vector2.Distance(currentPosition, _targetPosition) < step)
                _targetPosition = _targetPosition == pointA ? pointB : pointA;
        }
    }
}

