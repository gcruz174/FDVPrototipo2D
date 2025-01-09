using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] 
    private float speed = 1;
    [SerializeField] 
    private GameObject player;
    
    private Renderer _renderer;
    private Vector2 localScale;
        
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        localScale = transform.localScale;
    }
        
    private void Update()
    {
        var materials = _renderer.materials;
        for (var i = 0; i < materials.Length; i++)
        {
            var offset = player.transform.position / localScale / (speed / i);
            offset.y = 0.05f;
            materials[i].mainTextureOffset = offset;
        }
    }
}