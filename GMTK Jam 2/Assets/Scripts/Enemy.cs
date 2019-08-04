using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject object1, object2;
    GameObject currentObject;
    public Rigidbody2D rb;
    public float offsetValue = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        currentObject = object1;
        rb.transform.position = object1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            rb.velocity = (currentObject.transform.position - rb.transform.position).normalized * speed;
            if (rb.transform.position.x > currentObject.transform.position.x - offsetValue && rb.transform.position.x < currentObject.transform.position.x + offsetValue &&
                rb.transform.position.y > currentObject.transform.position.y - offsetValue && rb.transform.position.y < currentObject.transform.position.y + offsetValue)
            {
                ChangeDirection();
            }
        }
        else
            rb.velocity = Vector2.zero;
    }

    void ChangeDirection()
    {
        currentObject = currentObject == object1 ? object2 : object1;
        rb.velocity = (currentObject.transform.position - rb.transform.position).normalized * speed;
    }
}
