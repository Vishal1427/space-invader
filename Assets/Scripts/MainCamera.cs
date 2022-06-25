using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera cameraInstance;
    private Vector3 leftEdge;
    private Vector3 rightEdge;

    private void Awake()
    {
        if(cameraInstance != null)
        {
            Destroy(cameraInstance);
        }
        cameraInstance = this;
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }
    public Vector3 getCamerLeftEdge()
    {
        return leftEdge;
    }

    public Vector3 getCamerRightEdge()
    {
        return rightEdge;
    }
}
