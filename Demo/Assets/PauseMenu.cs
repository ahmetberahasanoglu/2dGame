using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject GameSessionObject;
    public GameObject ScenePersistObject;
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
           
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                oyunuDurdur();
            }
        }

    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;//zamanýn akýþ hýzý normale döndü
        GameIsPaused = false;
    }
    public void Reset()
    {
        GameIsPaused = false;
        var a = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(a);
        Time.timeScale = 1f;
        Destroy(ScenePersistObject.gameObject);
        Destroy(GameSessionObject.gameObject);
    }
    void oyunuDurdur()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;//Burada daha farklý bir yaklaþýmla oyunu durdurdgumuzda zamaný da yavaþlatabilirdik
        GameIsPaused = true;
    }
    public void loadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;//menuye bastýktan sonra tekrar oynaya týklayýp durdurmaya çalýþtýðýmýzda durmaz cünkü gameispaused hala true olarak gozukuyor o yüzden bu kodu koydum
        SceneManager.LoadScene(0);
        Destroy(GameSessionObject.gameObject);
        Destroy(ScenePersistObject.gameObject);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyundan cýkýyoruz");
    }
}
