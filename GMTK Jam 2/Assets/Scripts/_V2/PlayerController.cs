﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject arrow;
    public float distance;

    float angle;

    Rigidbody2D rb;

    public bool isMoving
    {
        get
        {
            return rb.velocity.magnitude > 0.1f;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        arrow.SetActive(false);
    }

    private void Update()
    {
        switch (TouchInput.state)
        {
            case TouchState.DRAG:
                if (!arrow.activeInHierarchy)
                    arrow.SetActive(true);
                angle = Vector3.SignedAngle(Vector3.right, -TouchInput.pullVector, Vector3.forward);
                arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
                break;
            case TouchState.UP:
                if (arrow.activeInHierarchy)
                    arrow.SetActive(false);
                rb.AddForce(-TouchInput.pullVector * distance * 1000, ForceMode2D.Force);
                //rb.velocity = -TouchInput.pullVector * speed * Time.deltaTime;
                TouchInput.state = TouchState.NONE;
                break;
        }
    }
}