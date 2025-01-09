using LevelEntities;
using Player;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] 
        private GameObject gameOverScreen;
        [SerializeField] 
        private GameObject winScreen;

        private PlayerHealth _playerHealth;
        private EndFlag _endFlag;

        private void Awake()
        {
            _playerHealth = FindFirstObjectByType<PlayerHealth>();
            _endFlag = FindFirstObjectByType<EndFlag>();
        }

        private void OnEnable()
        {
            _playerHealth.Death += OnDeath;
            _endFlag.Win += OnWin;
        }

        private void OnDisable()
        {
            _playerHealth.Death -= OnDeath;
            _endFlag.Win -= OnWin;
        }

        private void OnDeath()
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }
        
        private void OnWin()
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);
        }
    }
}
