using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int lives = 3;
    private int points = 15;
    public static GameManager gameManagerInstance;

    private void OnEnable()
    {
        Invader.OnInvaderKilled += Invader_OnInvaderKilled;
    }

    private void Invader_OnInvaderKilled(GameObject killedInvader)
    {
        this._setPoints(killedInvader);
    }

    private void Awake()
    {
        if (gameManagerInstance != null)
        {
            Destroy(gameManagerInstance);
        }
        gameManagerInstance = this;
    }

    private void _setPoints(GameObject invader)
    {
        GameObject[] invaders = Invaders.invadersInst.getInvadersObject();
        for (int i = 0; i < invaders.Length; i++)
        {
            if (invaders[i].tag.Equals(invader.tag))
            {
                score += ((i + 1) * points);
                if (OnScoreUpdated != null)
                {
                    OnScoreUpdated(score);
                }
                break;
            }
        }
    }

    public delegate void ScoreUpdater(int score);
    public static event ScoreUpdater OnScoreUpdated;
}
