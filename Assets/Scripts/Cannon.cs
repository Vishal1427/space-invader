using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    //[SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Rigidbody2D bullet;
    private float speed = 1000f;
    private float cannonMovementSpeed = 100f;
    private bool bulletActive = false;

    private void OnEnable()
    {
        Bullet.OnBulletCollide += Bullet_OnBulletCollide;
    }

    private void Bullet_OnBulletCollide()
    {
        bulletActive = false;
    }

    private void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            _shoot();
        }
        if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameObject.transform.Translate(-transform.right * cannonMovementSpeed * Time.deltaTime); 
        }
        if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(transform.right * cannonMovementSpeed * Time.deltaTime);
        }
    }

    private void _shoot()
    {
        if (!bulletActive)
        {
            bulletActive = true;
            var newBullet = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
            newBullet.velocity = transform.up * speed * Time.deltaTime;
        }
    }
}
