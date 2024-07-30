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
        Time.timeScale = 1f;//zaman�n ak�� h�z� normale d�nd�
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
        Time.timeScale = 0f;//Burada daha farkl� bir yakla��mla oyunu durdurdgumuzda zaman� da yava�latabilirdik
        GameIsPaused = true;
    }
    public void loadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;//menuye bast�ktan sonra tekrar oynaya t�klay�p durdurmaya �al��t���m�zda durmaz c�nk� gameispaused hala true olarak gozukuyor o y�zden bu kodu koydum
        SceneManager.LoadScene(0);
        Destroy(GameSessionObject.gameObject);
        Destroy(ScenePersistObject.gameObject);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyundan c�k�yoruz");
    }
}
