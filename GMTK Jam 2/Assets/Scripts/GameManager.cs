using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    GameObject Player;
    public GameObject lastBullet;

    public int currentLevel, currentEnemies, killedEnemies;
    public GameObject player;
    public Vector3[] positions;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        currentLevel = 1;
        killedEnemies = 0;
        currentEnemies = EnemyCount(currentLevel);

        Player = Instantiate(player, PlayerPositionReturn(currentLevel), Quaternion.identity);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().name == "SampleScene")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Time.timeSinceLevelLoad < 0.1f)
        {
#if !VERSION1
            if (!FindObjectOfType<PlayerController>())
#else
            if (!FindObjectOfType<Player>())
#endif
            {
                Player = Instantiate(player, PlayerPositionReturn(currentLevel), Quaternion.identity);
            }
            killedEnemies = 0;
            Camera.main.transform.parent.position = new Vector3(20 * (currentLevel - 1), 0, 0);
            Player.transform.position = PlayerPositionReturn(currentLevel);
            if (Player.GetComponent<Shoot>() != null)
                Player.GetComponent<Shoot>().canShoot = true;
        }

        if (killedEnemies >= currentEnemies)
        {
            killedEnemies = 0;
            currentLevel++;
            Destroy(lastBullet);
            currentEnemies = EnemyCount(currentLevel);
            Camera.main.transform.parent.position += new Vector3(20, 0, 0);
            if (currentLevel <= 5)
                Player.transform.position = PlayerPositionReturn(currentLevel);
            if (Player.GetComponent<Shoot>() != null)
                Player.GetComponent<Shoot>().canShoot = true;
        }

        if (currentLevel > 5)
        {
            SceneManager.LoadScene("MainMenu");
            Destroy(gameObject);
            instance = null;
        }
    }

    int EnemyCount(int level)
    {
        if (level == 1 || level == 2)
            return 2;
        else return 3;
    }

    Vector3 PlayerPositionReturn(int level)
    {
        return positions[level - 1];
    }
}
