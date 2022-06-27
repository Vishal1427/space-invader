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
        Debug.Log(Invaders.invadersInst.moveLeft);
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
            OnInvaderKilled();
        }
        GameManager.gameManagerInstance.setPoints(gameObject);
        Destroy(gameObject);
    }

    private void _moveLeft(Vector3 invadersMovement)
    {
        gameObject.transform.Translate(-invadersMovement);
        Vector3 invadersNextPos = gameObject.transform.position - (gameObject.transform.localScale + invadersMovement);    //Finds Position of cannon after movement
        //Debug.Log(invadersNextPos.x + " " + MainCamera.cameraInstance.getCamerLeftEdge().x);
        if (invadersNextPos.x < MainCamera.cameraInstance.getCamerLeftEdge().x)
        {
            Debug.Log("Came at Left edge");
            Invaders.invadersInst.moveLeft = false;
            Invaders.invadersInst.moveRight = true;
        }
    }

    private void _moveRight(Vector3 invadersMovement)
    {
        gameObject.transform.Translate(invadersMovement);
        Vector3 invadersNextPos = gameObject.transform.position + (gameObject.transform.localScale + invadersMovement);    //Finds Position of cannon after movement
        //.Log(invadersNextPos.x + " " + MainCamera.cameraInstance.getCamerRightEdge().x);
        if (invadersNextPos.x > MainCamera.cameraInstance.getCamerRightEdge().x)
        {
            Debug.Log("Came at right edge");
             Invaders.invadersInst.moveLeft = true;
             Invaders.invadersInst.moveRight = false;
        }
    }
    public delegate void InvaderKilled();
    public static event InvaderKilled OnInvaderKilled;
}
