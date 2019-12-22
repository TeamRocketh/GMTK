using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    [HideInInspector] public bool canShoot = true;

    float offset = 0.5f;

    public void ShootBullet()
    {
        if (canShoot)
        {
            canShoot = false;
            FindObjectOfType<Sound>().PlayShoot();
            Vector2 mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 spawnPos = transform.position + ((Vector3)mouseClickPos - transform.position).normalized * offset;
            spawnPos = transform.position - (Vector3)TouchInput.pullVector * offset;
            GameObject temp = Instantiate(bullet, spawnPos, Quaternion.identity);
            //temp.GetComponent<Bullet>().AddMovement(mouseClickPos);
            GameManager.instance.lastBullet = temp;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        Destroy(GameManager.instance.lastBullet);
        canShoot = true;
    }
}
