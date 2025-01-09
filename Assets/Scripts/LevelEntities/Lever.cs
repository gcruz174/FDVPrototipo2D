using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    [SerializeField] private UnityEvent onLeverPress;
    [SerializeField] private Sprite pressedSprite;

    private bool _pressed;
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_pressed || !other.CompareTag("Player")) return;
        _audioSource.Play();
        onLeverPress.Invoke();
        _spriteRenderer.sprite = pressedSprite;
        _pressed = true;
    }
}
