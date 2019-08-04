using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
