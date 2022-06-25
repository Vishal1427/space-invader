using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private void OnEnable()
    {
        GameManager.OnScoreUpdated += GameManager_OnScoreUpdated;
    }

    private void GameManager_OnScoreUpdated(int score)
    {
        Debug.Log("Score " + score);
        scoreText.text = "Score "+score;
    }

    private void OnDisable()
    {
        GameManager.OnScoreUpdated -= GameManager_OnScoreUpdated;
    }
}
