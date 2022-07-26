using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    [SerializeField] private GameObject[] invaders = new GameObject[3];
    [SerializeField] private GameObject enemyLaser;
    public int numberofInvadersInARow = 15;
    private float spaceBtwnInvaders = 0.3f;
    private float verticalMovement = 0.5f;
    public static Invaders invadersInst;
    private float invaderSpaceX;
    private float invaderSpaceY;
    public bool moveLeft = false, moveRight = false;
    public float laserInterval = 5.0f;
    List<GameObject> laserPool = new List<GameObject>();
    private float laserIntervalRT = 0;
    private GameState currentGameState;

    private void OnEnable()
    {
        GameManager.onGameStateHandler += GameManager_onGameStateHandler;
    }

    private void GameManager_onGameStateHandler(GameState gameState)
    {
        currentGameState = gameState;
    }

    private void Awake()
    {
        if (invadersInst != null)
        {
            Destroy(invadersInst);
        }
        invadersInst = this;
    }

    private void Start()
    {
        currentGameState = GameManager.gameManagerInstance.gameState;

        //Invaders assembly
        for (int i = 0; i < invaders.Length; i++)
        {
            Vector3 invaderScale = invaders[i].transform.localScale;
            invaderSpaceX = invaderScale.x + spaceBtwnInvaders;
            invaderSpaceY = invaderScale.y + spaceBtwnInvaders;
            float xSpaceReq = invaderSpaceX * numberofInvadersInARow;
            float xSpacePosition = (xSpaceReq / 2) * -1;
            for (int j=0; j < numberofInvadersInARow; j++)
            {
                Vector3 spawnPosition = new Vector3((xSpacePosition) + (invaderSpaceX * j), 2 + invaderSpaceY * i, 0);
                GameObject enemy = Instantiate(invaders[i], spawnPosition, Quaternion.identity);
                enemy.name = invaders[i].name + "_" +j;
                GameManager.gameManagerInstance.enemyList.Add(enemy);
            }
        }
        moveLeft = true;
    }

    private void Update()
    {
        //Spawn invader laser using timer
        if(currentGameState == GameState.IN_GAME)
        {
            laserIntervalRT += Time.deltaTime;
            if (laserIntervalRT > laserInterval)
            {
                laserIntervalRT = 0;
                SpawnMissile();
            }
        }
    }

    public GameObject[] getInvadersObject()
    {
        return invaders;
    }

    //Movement of enemy - toggle left
    public void moveLeftFn()
    {
        moveLeft = true;
        moveRight = false;
        actionAfterTightFn();
    }

    //Movement of enemy - toggle right
    public void moveRightFn()
    {
        moveLeft = false;
        moveRight = true;
    }

    //Enemy laser span timing mechanism
    public void actionAfterTightFn()
    {
        if (laserInterval >= 2)
        {
            laserInterval -= 2f;
        }else if(laserInterval >= 0.4)
        {
            laserInterval -= 0.1f;
        }
    }

    //Enemy laser spawn
    private void SpawnMissile()
    {
        List<GameObject> enamyList = GameManager.gameManagerInstance.enemyList;
        int randomEnemy = Random.Range(0, enamyList.Count);
        Vector3 randomInvaderPosiion = enamyList[randomEnemy].transform.position;
        _getMissile(randomInvaderPosiion);

    }

    //Instatiate laser object
    private GameObject _getMissile(Vector3 temp)
    {
        for (int i = 0; i < laserPool.Count; i++)
        {
            if (!laserPool[i].activeSelf)
            {
                laserPool[i].SetActive(true);
                laserPool[i].transform.position = temp;
                laserPool[i].transform.rotation = Quaternion.identity;
                return laserPool[i];
            }
        }
        GameObject newLaser = Instantiate(enemyLaser, gameObject.transform.position, gameObject.transform.rotation);
        laserPool.Add(newLaser);
        return newLaser;
    }
}
