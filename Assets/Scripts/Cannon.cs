using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private float cannonMovementSpeed = 2f;
    private bool bulletActive = false, moveLeft = false, moveRight = false;
    Vector3 leftEdge, rightEdge;
    int lives;
    private GameState gameStateVal;
    private List<GameObject> bulletPool = new List<GameObject>();

    private void Start()
    {
        gameStateVal = GameManager.gameManagerInstance.gameState;
        lives = GameManager.gameManagerInstance.lives;
    }

    private void OnEnable()
    {
        Bullet.OnBulletCollide += Bullet_OnBulletCollide;
        GameManager.onGameStateHandler += GameManager_onGameStateHandler;

        //Initializing left and right edge of the camera
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void GameManager_onGameStateHandler(GameState gameState)
    {
        gameStateVal = gameState;
    }

    private void Bullet_OnBulletCollide()
    {
        bulletActive = false;
    }

    private void Update()
    {
        Vector3 movement = transform.right * cannonMovementSpeed * Time.deltaTime;  // Player movement speed

        //For pause option 
        if (Input.GetKeyDown("escape"))
        {
            if(gameStateVal == GameState.IN_GAME)
            {
                gameStateVal = GameState.PAUSE;
            }else if (gameStateVal == GameState.PAUSE)
            {
                gameStateVal = GameState.IN_GAME;
            }
            GameManager.gameManagerInstance.setGameState(gameStateVal);
        }
        //Player(cannon) movement and shooting
        if(gameStateVal  == GameState.IN_GAME)
        {
            if (Input.GetKeyDown("space"))
            {
                _shoot();
            }
            else if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveLeft = true;
            }
            else if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveRight = true;
            }
            else if (Input.GetKeyUp("a") || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                moveLeft = false;
            }
            else if (Input.GetKeyUp("d") || Input.GetKeyUp(KeyCode.RightArrow))
            {
                moveRight = false;
            }

            //Movememnt of player
            if (moveRight)
            {
                _moveRight(rightEdge, movement);
            }
            else if (moveLeft)
            {
                _moveLeft(leftEdge, movement);
            }
        }
    }

    private void _shoot()
    {
        if (!bulletActive)
        {
            bulletActive = true;
            _getbullet();
        }
    }

    //Instatiate bullet using pooling technic
    private GameObject _getbullet()
    {
        for(int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeSelf)
            {
                bulletPool[i].SetActive(true);
                bulletPool[i].transform.position = gameObject.transform.position;
                bulletPool[i].transform.rotation = gameObject.transform.rotation;
                return bulletPool[i];
            }
        }
        GameObject newBullet = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
        bulletPool.Add(newBullet);
        return newBullet;
    }

    //Player movement mechanism - Left
    private void _moveLeft(Vector3 leftEdge, Vector3 movement)
    {
        Vector3 cannonPosition = gameObject.transform.position - (gameObject.transform.localScale + movement);    //Finds Position of cannon after movement
        if (cannonPosition.x > MainCamera.cameraInstance.getCamerLeftEdge().x)
        {
            gameObject.transform.Translate(-movement);
        }
    }

    //Player movement mechanism - Right
    private void _moveRight(Vector3 rightEdge, Vector3 movement)
    {
        Vector3 cannonPosition = gameObject.transform.position + gameObject.transform.localScale + movement;    //Finds Position of cannon after movement
        if (cannonPosition.x < MainCamera.cameraInstance.getCamerRightEdge().x)
        {
            gameObject.transform.Translate(movement);

        }
    }

    private void OnDisable()
    {
        Bullet.OnBulletCollide -= Bullet_OnBulletCollide;
        GameManager.onGameStateHandler -= GameManager_onGameStateHandler;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("Missile"))
        {
            lives--;
            if (onLiveReduced != null)
            {
                onLiveReduced(lives);
            }
        }
    }

    public delegate void LiveReduced(int lives);
    public static event LiveReduced onLiveReduced;
}
