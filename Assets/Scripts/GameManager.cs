using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject obj;
    public GameObject player;
    public PlayerManager playerManager;
    public PlayerController playerController;

    public GameObject enemy;
    public EnemyManager enemyManager;
    public EnemyController enemyController;


    public GameObject[] seinozi;

    [SerializeField] Vector3 playerPosition;
    [SerializeField] GameObject startObj;

    public static GameManager instance;

    public bool isWin;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        obj = (GameObject)Resources.Load("Player" + PlayerPrefs.GetInt("Data"));
        CreatePlayer();
        FindPlayer();
        FindEnemy();
    }
    private void Update()
    {
        
        if(playerManager.number >= 5)
        {
            MaxNumber(playerManager.number);
        }
        else
        {
            MaxNumber(enemyManager.hitCount);
        }

        if (TimeManager.startCount >= 0)
        {
            startObj.SetActive(true);
        }
        else
        {
            startObj.SetActive(false);
        }
    }

    private void CreatePlayer()
    {
        GameObject prefbObj = Instantiate(obj, playerPosition, Quaternion.identity);

        prefbObj.transform.position = new Vector3(-3, 3, 3);
        prefbObj.transform.Rotate(new Vector3(0, 180, 0));
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("player");
        playerManager = player.GetComponent<PlayerManager>();
        playerController = player.GetComponent<PlayerController>();
    }
    public void FindEnemy()
    {
        enemy = GameObject.FindGameObjectWithTag("enemy");
        enemyManager = enemy.GetComponent<EnemyManager>();
        enemyController = enemy.GetComponent<EnemyController>();
    }
    
    void MaxNumber(float number)
    {
        enemyManager.hitCount = number;
    }
    public void IsBattle(bool win)
    {
        //１なら勝利２なら敗北とリザルト画面に表示させる
        if(win)
        {
            PlayerPrefs.SetInt("Result", 1);
        }else
        {
            PlayerPrefs.SetInt("Result", 2);
        }
        SceneManager.LoadScene("Result");
    }
    
}
