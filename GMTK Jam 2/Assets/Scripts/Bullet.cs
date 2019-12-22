﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed =10f;
    public GameObject bulletHitPS;
    bool isSlow = false;
    Vector2 lastFrameVel;
    Rigidbody2D rb;

#if !VERSION1
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = TouchInput.pullVector * speed;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameManager.instance.playerController.GetComponent<Collider2D>());
    }
#endif

    public void AddMovement(Vector2 position)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (position - (Vector2)transform.position).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(collision.gameObject.tag == "DestroyWall")
        {
            FindObjectOfType<Sound>().PlayWall();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Bounce")
        {
            FindObjectOfType<Sound>().PlayWall();
            Instantiate(bulletHitPS, transform.position, Quaternion.Euler(0, 0, 90 + Vector3.SignedAngle(Vector3.right, lastFrameVel, Vector3.forward)));
            Camera.main.GetComponent<Animator>().SetBool("CamShake", true);
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    { yield return new WaitForSeconds(0.25f); Camera.main.GetComponent<Animator>().SetBool("CamShake", false); }

    private void LateUpdate()
    {
        lastFrameVel = rb.velocity;
    }

    private void Update()
    {
        if (GameManager.instance.playerController != null && GameManager.instance.playerController.isMoving && !isSlow)
        {
            speed /= 2f;
            rb.velocity = rb.velocity.normalized * speed;
            isSlow = true;
        }
        else if(GameManager.instance.playerController != null && !GameManager.instance.playerController.isMoving && isSlow)
        {
            speed *= 2f;
            rb.velocity = rb.velocity.normalized * speed;
            isSlow = false;
        }
    }
}
