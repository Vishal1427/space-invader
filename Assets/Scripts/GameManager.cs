using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    private int points = 15;
    public static GameManager gameManagerInstance;
    public List<GameObject> enemyList = new List<GameObject>();
    public int lives = 3;
    public GameState gameState = GameState.IN_GAME;

    private void OnEnable()
    {
        Invader.OnInvaderKilled += Invader_OnInvaderKilled;
        Cannon.onLiveReduced += Cannon_onLiveReduced;
    }

    private void Cannon_onLiveReduced(int cannonLives)
    {
        lives = cannonLives;
        if (lives <= 0)
        {
            setGameState(GameState.STOP);   //Stop game on lives of player = 0
        }
    }

    public void setGameState(GameState gameState)
    {
        if (onGameStateHandler != null)
        {
            onGameStateHandler(gameState);
        }
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

    //Points updating for player
    private void _setPoints(GameObject invader)
    {
        GameObject[] invaders = Invaders.invadersInst.getInvadersObject();
        enemyList.Remove(invader);
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
    private void OnDisable()
    {
        Invader.OnInvaderKilled -= Invader_OnInvaderKilled;
    }

    public delegate void ScoreUpdater(int score);
    public static event ScoreUpdater OnScoreUpdated;
    public delegate void GameStateHandler(GameState gameState);
    public static event GameStateHandler onGameStateHandler;
}

public enum GameState{
    MENU,
    IN_GAME,
    PAUSE,
    STOP
}