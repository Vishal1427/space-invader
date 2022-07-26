using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderMissile : Bullet
{
    private float missileSpeed = 200f;
    Rigidbody2D newBulletRigid;

    private void Start()
    {
        newBulletRigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        newBulletRigid.velocity = -transform.up * missileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        _missileCollide();
    }

    private void OnBecameInvisible()
    {
        _missileCollide();
    }

    private void _missileCollide() 
    {
        if(OnMissileCollide != null)
        {
            OnMissileCollide();
        }
        gameObject.SetActive(false);
    }

    public static event BulletCollide OnMissileCollide;
}
