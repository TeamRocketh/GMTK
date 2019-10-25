using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound ins;

    public AudioClip enemy, shoot, wall;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (ins == null)
            ins = this;
        else Destroy(gameObject);
    }

    public void PlayEnemy()
    {
        GetComponent<AudioSource>().PlayOneShot(enemy);
    }
    public void PlayShoot()
    {
        GetComponent<AudioSource>().PlayOneShot(shoot);
    }
    public void PlayWall()
    {
        GetComponent<AudioSource>().PlayOneShot(wall);
    }
}
