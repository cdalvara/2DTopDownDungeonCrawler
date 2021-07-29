using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playGame()
    {
        mainMenu.SetActive(false);
        SceneManager.LoadScene("EntranceMain");
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void back()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
