using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed =10f;
    bool isSlow = false;
    Rigidbody2D rb;
    public void AddMovement(Vector2 position)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (position - (Vector2)transform.position).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.transform.parent.gameObject);
        }
        if(collision.gameObject.tag=="Player")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "DestroyWall")
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && !isSlow)
        {
            speed /= 2f;
            rb.velocity = rb.velocity.normalized * speed;
            isSlow = true;
        }
        else if(!(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && isSlow)
        {
            speed *= 2f;
            rb.velocity = rb.velocity.normalized * speed;
            isSlow = false;
        }
    }
}
