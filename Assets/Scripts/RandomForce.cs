using UnityEngine;

public class RandomForce : MonoBehaviour
{
    [SerializeField] 
    private float amount = 0.2f;
    
    private void Start()
    {
        var rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(Random.insideUnitCircle.normalized * amount);
    }
}
