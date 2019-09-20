﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ExitState());
    }

    IEnumerator ExitState()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex+1);

    }
}
