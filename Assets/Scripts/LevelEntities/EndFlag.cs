using System;
using UnityEngine;

namespace LevelEntities
{
    public class EndFlag : MonoBehaviour
    {
        public delegate void WinEvent();
        public event WinEvent Win;

        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
            _spriteRenderer.enabled = false;
            _collider.enabled = false;
        }

        private void OnEnable()
        {
            Coin.OnCollected += OnCoinCollected;
        }

        private void OnDisable()
        {
            Coin.OnCollected -= OnCoinCollected;
        }

        private void OnCoinCollected(int amount)
        {
            if (Coin.CoinCount != 0) return;
            _spriteRenderer.enabled = true;
            _collider.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            Win?.Invoke();
        }
    }
}
