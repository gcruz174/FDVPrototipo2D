using System;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField] 
    private GameObject cinemachineCamera;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        cinemachineCamera.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        cinemachineCamera.SetActive(false);
    }
}
