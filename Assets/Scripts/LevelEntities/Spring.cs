using System;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    
    private static readonly int SpringTrigger = Animator.StringToHash("Spring");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerController>().SpringJump(25f);
        _animator.SetTrigger(SpringTrigger);
        _audioSource.Play();
    }
}
