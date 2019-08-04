using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    float timer;
    void Awake() { timer = Time.time; }
    void Update() { if (Time.time - timer > 0.5f) Destroy(gameObject); }
}