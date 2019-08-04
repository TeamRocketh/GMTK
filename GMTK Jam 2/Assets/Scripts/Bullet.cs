using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public GameObject bulletHitPS;

    public float speed =10f;
    Rigidbody2D rb;

    Vector2 lastFrameVel;

    public void AddMovement(Vector2 position)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (position - (Vector2)transform.position).normalized * speed;
    }

    private void LateUpdate()
    {
        lastFrameVel = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player"|| collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "DestroyWall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bounce")
        {
            Instantiate(bulletHitPS, transform.position, Quaternion.Euler(0, 0, 90 + Vector3.SignedAngle(Vector3.right, lastFrameVel, Vector3.forward)));
            Camera.main.GetComponent<Animator>().SetBool("CamShake", true);
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.25f);
        Camera.main.GetComponent<Animator>().SetBool("CamShake", false);
    }
}
