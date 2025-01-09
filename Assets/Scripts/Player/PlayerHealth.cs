using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource hurtSound;
        [SerializeField] 
        private float invincibilityTime = 0.5f;
        
        public int Health { get; private set; } = 3;
        
        public delegate void HealthChangeEvent(int newHealth);
        public event HealthChangeEvent HealthChanged;

        public delegate void DeathEvent();
        public event DeathEvent Death;

        private Animator _animator;
        private float _timer = 0f;
        private static readonly int Hurt = Animator.StringToHash("Hurt");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (transform.position.y < -8)
                TakeDamage(Health);

            if (_timer > 0f)
                _timer -= Time.deltaTime;
        }

        public void TakeDamage(int damage)
        {
            if (_timer > 0f) return;

            _timer = invincibilityTime;
            hurtSound.Play();
            Health -= damage;
            HealthChanged?.Invoke(Health);
            _animator.SetTrigger(Hurt);
            if (Health <= 0)
                Death?.Invoke();
        }
    }
}
