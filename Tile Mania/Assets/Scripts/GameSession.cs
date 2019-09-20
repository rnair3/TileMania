using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int lives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesDisplay;
    [SerializeField] TextMeshProUGUI scoreDisplay;

    private void Awake()
    {
        int numberGameSession = FindObjectsOfType<GameSession>().Length;

        if (numberGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        livesDisplay.text = lives.ToString();
        scoreDisplay.text = score.ToString();
    }

    public void ProcessDeath()
    {
        if (lives > 1)
        {
            lives--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            livesDisplay.text = lives.ToString();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreDisplay.text = score.ToString();
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
