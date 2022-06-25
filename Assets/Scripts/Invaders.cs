using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    [SerializeField] private GameObject[] invaders = new GameObject[3];
    private int numberofInvadersInARow = 15;
    private float spaceBtwnInvaders = 0.4f;
    public static Invaders invadersInst;

    private void Awake()
    {
        if (invadersInst != null)
        {
            Destroy(invadersInst);
        }
        invadersInst = this;
    }

    private void Start()
    {
        for (int i = 0; i < invaders.Length; i++)
        {
            Vector3 invaderScale = invaders[i].transform.localScale;
            float invaderSpaceX = invaderScale.x + spaceBtwnInvaders;
            float invaderSpaceY = invaderScale.y + spaceBtwnInvaders;
            float xSpaceReq = invaderSpaceX * numberofInvadersInARow;
            float xSpacePosition = (xSpaceReq / 2) * -1;
            for (int j=0; j < numberofInvadersInARow; j++)
            {
                Vector3 spawnPosition = new Vector3((xSpacePosition) + (invaderSpaceX * j), 2 + invaderSpaceY * i, 0);
                Instantiate(invaders[i], spawnPosition, Quaternion.identity);
            }
        }
    }

    public GameObject[] getInvadersObject()
    {
        return invaders;
    }
}
