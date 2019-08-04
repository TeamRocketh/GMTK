using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    float timer;
    void Awake() { timer = Time.time; }
    void Update() { if (Time.time - timer > 1) Destroy(gameObject); }
}