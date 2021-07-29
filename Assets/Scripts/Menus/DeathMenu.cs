using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public PlayerScore playerScore;

    public GameObject gameOverMenuUI;
    public GameObject enemyValue;
    public GameObject enemyText;
    public GameObject levelValue;
    public GameObject levelText;
    public GameObject bossValue;
    public GameObject bossText;

    private bool isEndLevel = false;

    private int totalEnemies = 0;
    private int totalLevels = 0;
    private int totalBoss = 0;

    public void EndGame()
    {
        AudioManager.instance.StopPlaying("Main Menu");
        AudioManager.instance.StopPlaying("Theme");
        AudioManager.instance.Play("Death");
        gameOverMenuUI.SetActive(true);
        isEndLevel = true;
        Time.timeScale = 0.0f;
        
        PauseMenu.GameIsPaused = true;
        
        totalEnemies = (int)(playerScore.GetKilled());
        // totalLevels = (int)(playerScore.GetCleared());
        // totalBoss = (int)(playerScore.GetCleared());
    }

    void Update()
    {
        if (isEndLevel == false) { gameOverMenuUI.SetActive(false); return; }

        enemyValue.SetActive(true);
        enemyText.SetActive(true);
        // levelValue.SetActive(true);
        // levelText.SetActive(true);
        // bossValue.SetActive(true);
        // bossText.SetActive(true);

        string nscore = totalEnemies.ToString();
        Text temp = enemyValue.GetComponent<Text>();
        temp.text = nscore;

        // nscore = totalLevels.ToString();
        // temp = levelValue.GetComponent<Text>();
        // temp.text = nscore;

        // nscore = totalBoss.ToString();
        // temp = bossValue.GetComponent<Text>();
        // temp.text = nscore;


    }

    public void RestartLevel()
    {
        AudioManager.instance.StopPlaying("Death");
        AudioManager.instance.Play("Theme");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        AudioManager.instance.StopPlaying("Death");
        AudioManager.instance.Play("Main Menu");
        SceneManager.LoadScene("MainMenu");
    }
}
