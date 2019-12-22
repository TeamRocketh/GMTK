﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject arrow;
    public float distance;

    public bool isMoving { get { return move; } }

    bool move;
    float startTime;
    Vector3 destination;
    Rigidbody2D rb;
    Shoot gunHandler;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gunHandler = GetComponent<Shoot>();
        arrow.SetActive(false);
        distance /= 100;
    }

    private void Update()
    {
        rb.velocity = Vector2.zero;
        float journeyLength = 1;

        switch (TouchInput.state)
        {
            case TouchState.DRAG:
                if (!arrow.activeInHierarchy)
                    arrow.SetActive(true);
                arrow.transform.rotation = Quaternion.Euler(0, 0, Vector3.SignedAngle(Vector3.right, -TouchInput.pullVector, Vector3.forward));
                break;
            case TouchState.UP:
                if (arrow.activeInHierarchy)
                    arrow.SetActive(false);
                Vector3 touchVector = new Vector3(TouchInput.pullVector.x, TouchInput.pullVector.y, transform.position.z);
                float amount = distance;
                if (TouchInput.distanceMoved / 100 < distance)
                    amount = TouchInput.distanceMoved / 100;
                destination = transform.position - touchVector * amount;
                startTime = Time.time;
                move = true;
                journeyLength = Vector3.Distance(transform.position, destination);
                gunHandler.ShootBullet();
                //rb.AddForce(-TouchInput.pullVector * distance * 1000, ForceMode2D.Force);
                //rb.velocity = -TouchInput.pullVector * speed * Time.deltaTime;
                TouchInput.state = TouchState.NONE;
                break;
        }

        if (move)
        {
            float distCovered = (Time.time - startTime) * Time.deltaTime * 10;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, destination, fractionOfJourney);
            if (Vector3.Distance(transform.position, destination) < 0.1f)
                move = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        move = false;
    }
}