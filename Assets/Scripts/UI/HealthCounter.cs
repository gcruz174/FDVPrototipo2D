using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace UI
{
    public class HealthCounter : MonoBehaviour
    {
        [SerializeField] 
        private GameObject heartPrefab;

        private List<GameObject> _hearts = new();
        private PlayerHealth _playerHealth;

        private void Awake()
        {
            _playerHealth = FindFirstObjectByType<PlayerHealth>();
            for (var i = 0; i < _playerHealth.Health; i++)
            {
                var heartInstance = Instantiate(heartPrefab, transform);
                _hearts.Add(heartInstance);
            }
        }

        private void OnEnable()
        {
            _playerHealth.HealthChanged += OnHealthChanged;
        }

        private void OnDisable()
        {
            _playerHealth.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int newHealth)
        {
            for (var i = 0; i < _hearts.Count; i++)
            {
                _hearts[i].SetActive(i < newHealth);
            }
        }
    }
}
