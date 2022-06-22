using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        Destroy(gameObject);

    }

    public delegate void BulletCollide();
    public static event BulletCollide OnBulletCollide;
}
