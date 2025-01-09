using System;
using Effects;
using UnityEngine;

namespace LevelEntities
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] 
        private float speed;
        
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private int direction = 1;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var velocity = _rigidbody2D.linearVelocity;
            velocity.x = speed * direction;
            _rigidbody2D.linearVelocity = velocity;
            _spriteRenderer.flipX = direction == 1;

            var hit = Physics2D.Raycast(transform.position + Vector3.down * 0.1f, 
                Vector2.right * direction, 0.6f);
            if (hit && hit.collider.gameObject.CompareTag("Ground"))
                direction *= -1;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            other.GetComponent<PlayerController>().SpringJump(5f);
            EffectsManager.Instance.SpawnEffect(EffectsManager.Effect.Explode, transform.position);
            Destroy(gameObject);
        }
    }
}
