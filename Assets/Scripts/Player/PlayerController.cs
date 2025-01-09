using System;
using Effects;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpForce = 25.0f;
    
    [Header("Effects")]
    [SerializeField] private Transform feet;
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private ParticleSystem walkDust;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private BoxCollider2D _boxCollider2D;
    
    // State
    private bool _isGrounded;
    private bool _jumpPressed;
    private bool _lastFlip;
    private float _speedY;
    
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Moving = Animator.StringToHash("Moving");
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var moving = Mathf.Abs(horizontal) > 0.1f;
        _animator.SetBool(Moving, moving);
        UpdateCharacterDirection(horizontal);
        if (Input.GetKeyDown(KeyCode.Space)) _jumpPressed = true;
    }
    
    private void UpdateCharacterDirection(float horizontal)
    {
        if (Mathf.Abs(horizontal) > 0.1f)
            _lastFlip = horizontal > 0;
        _spriteRenderer.flipX = _lastFlip;
    }
    
    private void FixedUpdate()
    {
        var x = Input.GetAxisRaw("Horizontal");
        _speedY += -gravity * Time.fixedDeltaTime;
        var velocity = new Vector2(x * speed, _speedY);
        _rb.linearVelocity = velocity;
        
        CheckGrounded();
        SetWalkDustActive(_isGrounded);
        CheckJump();
        _jumpPressed = false;
    }
    
    private void CheckGrounded()
    {
        var bounds = _boxCollider2D.bounds;
        
        // Check left side
        var spriteLeft = transform.position + Vector3.left * bounds.extents.x;
        var hitLeft = Physics2D.Raycast(spriteLeft, Vector2.down, .6f);
        var leftGrounded = hitLeft.collider != null && 
                           (hitLeft.collider.CompareTag("Ground") || hitLeft.collider.CompareTag("Platform"));
        Debug.DrawLine(spriteLeft, spriteLeft + Vector3.down * .6f);
        
        // Check right side
        var spriteRight = transform.position + Vector3.right * bounds.extents.x;
        var hitRight = Physics2D.Raycast(spriteRight, Vector2.down, .6f);
        var rightGrounded = hitRight.collider != null && 
                            (hitRight.collider.CompareTag("Ground") || hitRight.collider.CompareTag("Platform"));
        Debug.DrawLine(spriteRight, spriteRight + Vector3.down * .6f);

        var newIsGrounded = leftGrounded || rightGrounded;
        
        if (!_isGrounded && newIsGrounded && _speedY < 0) 
            EffectsManager.Instance.SpawnEffect(EffectsManager.Effect.Land, feet.position);
        
        _isGrounded = newIsGrounded;
        _animator.SetBool(Grounded, _isGrounded);
        // limit falling down on ground but not jumping
        if (_isGrounded) _speedY = _speedY > 0 ? _speedY : 0;
    }
    
    private void SetWalkDustActive(bool active)
    {
        var emission = walkDust.emission;
        emission.enabled = active;
    }

    private void CheckJump()
    {
        if (!_jumpPressed || !_isGrounded) return;
        _speedY = jumpForce;
        EffectsManager.Instance.SpawnEffect(EffectsManager.Effect.Jump, feet.position);
        _isGrounded = false;
    }

    public void SpringJump(float force)
    {
        _speedY = force;
        _isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform")) 
            transform.SetParent(other.transform);
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
            transform.SetParent(null);
    }

    public void PlayWalkSound()
    {
        walkSound.Play();
    }
}
