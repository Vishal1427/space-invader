using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    private float invadersMovementSpeed = 2f;
    private float mysteryShipMovementSpeed = 8f;
    GameState gameStateVal;

    private void Start()
    {
        gameStateVal = GameManager.gameManagerInstance.gameState;
    }

    private void OnEnable()
    {
        GameManager.onGameStateHandler += GameManager_onGameStateHandler;
    }

    private void GameManager_onGameStateHandler(GameState gameState)
    {
        gameStateVal = gameState;
    }
    private void OnDisable()
    {
        GameManager.onGameStateHandler -= GameManager_onGameStateHandler;
    }

    private void Update()
    {
        if(gameStateVal == GameState.IN_GAME)
        {
            Vector3 invadersMovement = transform.right * invadersMovementSpeed * Time.deltaTime;    //Invader movement speed

            if (Invaders.invadersInst.moveLeft)
            {
                _moveLeft(invadersMovement);
            }
            if (Invaders.invadersInst.moveRight)
            {
                _moveRight(invadersMovement);
            }
        }
    }

    //On collision callback
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (OnInvaderKilled != null)
        {
            OnInvaderKilled(gameObject);
        }
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    //Function to move invaders left
    private void _moveLeft(Vector3 invadersMovement)
    {
        gameObject.transform.Translate(-invadersMovement);
        Vector3 invadersNextPos = gameObject.transform.position - (gameObject.transform.localScale + invadersMovement);    //Finds Position of cannon after movement
        if (invadersNextPos.x < MainCamera.cameraInstance.getCamerLeftEdge().x)
        {
            Invaders.invadersInst.moveRightFn();
        }
    }

    //Function to move invaders right
    private void _moveRight(Vector3 invadersMovement)
    {
        gameObject.transform.Translate(invadersMovement);
        Vector3 invadersNextPos = gameObject.transform.position + (gameObject.transform.localScale + invadersMovement);    //Finds Position of cannon after movement
        if (invadersNextPos.x > MainCamera.cameraInstance.getCamerRightEdge().x)
        {
            Invaders.invadersInst.moveLeftFn();
        }
    }
    public delegate void InvaderKilled(GameObject killedInvader);
    public static event InvaderKilled OnInvaderKilled;
}
