using System;
using Effects;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int amount = 1;
    
    public delegate void OnCollect(int amount);

    public static int CoinCount = 0;
    public static event OnCollect OnCollected;

    private void Awake()
    {
        CoinCount++;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        EffectsManager.Instance.SpawnEffect(EffectsManager.Effect.CoinCollect, transform.position);
        Destroy(gameObject);
        CoinCount--;
        OnCollected?.Invoke(amount);
    }
}
