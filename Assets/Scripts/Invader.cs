using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    //private void 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (OnInvaderKilled != null)
        {
            OnInvaderKilled();
        }
        GameManager.gameManagerInstance.setPoints(gameObject);
        Destroy(gameObject);
    }
    public delegate void InvaderKilled();
    public static event InvaderKilled OnInvaderKilled;
}
