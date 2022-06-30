using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    private float invadersMovementSpeed = 2f;
    private float mysteryShipMovementSpeed = 8f;

    private void Update()
    {
        Vector3 invadersMovement = transform.right * invadersMovementSpeed * Time.deltaTime;
        if (Invaders.invadersInst.moveLeft)
        {
            _moveLeft(invadersMovement);
        }
        if (Invaders.invadersInst.moveRight)
        {
            _moveRight(invadersMovement);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (OnInvaderKilled != null)
        {
            OnInvaderKilled(gameObject);
        }
        Destroy(gameObject);
    }

    private void _moveLeft(Vector3 invadersMovement)
    {
        gameObject.transform.Translate(-invadersMovement);
        Vector3 invadersNextPos = gameObject.transform.position - (gameObject.transform.localScale + invadersMovement);    //Finds Position of cannon after movement
        if (invadersNextPos.x < MainCamera.cameraInstance.getCamerLeftEdge().x)
        {
            Invaders.invadersInst.moveLeft = false;
            Invaders.invadersInst.moveRight = true;
        }
    }

    private void _moveRight(Vector3 invadersMovement)
    {
        gameObject.transform.Translate(invadersMovement);
        Vector3 invadersNextPos = gameObject.transform.position + (gameObject.transform.localScale + invadersMovement);    //Finds Position of cannon after movement
        if (invadersNextPos.x > MainCamera.cameraInstance.getCamerRightEdge().x)
        {
             Invaders.invadersInst.moveLeft = true;
             Invaders.invadersInst.moveRight = false;
        }
    }
    public delegate void InvaderKilled(GameObject killedInvader);
    public static event InvaderKilled OnInvaderKilled;
}
