using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    //[SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Rigidbody2D bullet;
    private float bulletSpeed = 2000f;
    private float cannonMovementSpeed = 2f;
    private bool bulletActive = false, moveLeft = false, moveRight = false;
    Vector3 leftEdge, rightEdge;

    private void OnEnable()
    {
        Bullet.OnBulletCollide += Bullet_OnBulletCollide;
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void Bullet_OnBulletCollide()
    {
        bulletActive = false;
    }

    private void Update()
    {
        Vector3 movement = transform.right * cannonMovementSpeed * Time.deltaTime;
        if (Input.GetKeyDown("space"))
        {
            _shoot();
        }else if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLeft = true;
        }else if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRight = true;
        }else if (Input.GetKeyUp("a") || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveLeft = false;
        }else if (Input.GetKeyUp("d") || Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveRight = false;
        }
        if (moveRight)
        {
            _moveRight(rightEdge, movement);
        }else if (moveLeft)
        {
            _moveLeft(leftEdge, movement);
        }
    }

    private void _shoot()
    {
        if (!bulletActive)
        {
            bulletActive = true;
            var newBullet = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
            newBullet.velocity = transform.up * bulletSpeed * Time.deltaTime;
        }
    }

    private void _moveLeft(Vector3 leftEdge, Vector3 movement)
    {
        Vector3 cannonPosition = gameObject.transform.position - (gameObject.transform.localScale + movement);    //Finds Position of cannon after movement
        if (cannonPosition.x > MainCamera.cameraInstance.getCamerLeftEdge().x)
        {
            gameObject.transform.Translate(-movement);
        }
    }

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
    }
}
