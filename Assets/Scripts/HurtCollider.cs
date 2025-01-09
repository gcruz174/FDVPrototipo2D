using Player;
using UnityEngine;

public class HurtCollider : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
    }
}
