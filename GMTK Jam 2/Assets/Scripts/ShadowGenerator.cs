using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowGenerator : MonoBehaviour
{
    public GameObject shadow;
    public float ShadowDelay = 0.2f;

    float timer;

    private void Awake()
    {
        timer = ShadowDelay;
    }

    private void Update()
    {
#if VERSION1
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
#else
        if (GameManager.instance.playerController != null && GameManager.instance.playerController.isMoving)
#endif
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
            {
                GameObject temp = Instantiate(shadow, transform.position, transform.rotation);
                timer = ShadowDelay;
                Destroy(temp, 0.5f);
            }
        }
    }
}
