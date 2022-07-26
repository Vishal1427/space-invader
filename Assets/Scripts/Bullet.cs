using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D bullet;
    private float bulletSpeed = 400f;


    private void Start()
    {
        bullet = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        bullet.velocity = transform.up * bulletSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Invader") || collision.gameObject.name.Contains("MysteryShip"))
            _bulletCollide();
    }

    private void OnBecameInvisible()
    {
        _bulletCollide();
    }

    private void _bulletCollide()
    {
        if (OnBulletCollide != null)
        {
            OnBulletCollide();
        }
        gameObject.SetActive(false);

    }

    public delegate void BulletCollide();
    public static event BulletCollide OnBulletCollide;
}
