using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public bool canShoot = true;
    public float offset = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            canShoot = false;
            FindObjectOfType<Sound>().PlayShoot();
            Vector2 mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 spawnPos = transform.position + ((Vector3)mouseClickPos - transform.position).normalized*offset;
            GameObject temp = Instantiate(bullet, spawnPos, Quaternion.identity);
            temp.GetComponent<Bullet>().AddMovement(mouseClickPos);
            GameManager.instance.lastBullet = temp;
        }
    }
}
