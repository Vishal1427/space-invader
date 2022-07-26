using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    private void Start()
    {
        livesText.text = "Lives " + GameManager.gameManagerInstance.lives;
        scoreText.text = "Score " + GameManager.gameManagerInstance.score;
    }

    private void OnEnable()
    {
        GameManager.OnScoreUpdated += GameManager_OnScoreUpdated;
        Cannon.onLiveReduced += Cannon_onLiveReduced;
    }

    private void Cannon_onLiveReduced(int lives)
    {
        livesText.text = "Lives " + lives;
    }

    private void GameManager_OnScoreUpdated(int score)
    {
        scoreText.text = "Score "+score;
    }

    private void OnDisable()
    {
        GameManager.OnScoreUpdated -= GameManager_OnScoreUpdated;
        Cannon.onLiveReduced -= Cannon_onLiveReduced;
    }
}
