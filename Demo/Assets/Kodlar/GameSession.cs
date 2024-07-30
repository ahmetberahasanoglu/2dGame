using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
   
    Health karakterCan;
    public int skor = 0;
    [SerializeField] float olumZaman = 0.01f;
    [SerializeField] TextMeshProUGUI scoreText;
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    void Awake()
    {
        GameObject myObject = GameObject.Find("karakterim");
        karakterCan = myObject.GetComponent<Health>();
        int numGameSession = FindObjectsOfType<GameSession>().Length;

        if (numGameSession > 1 || SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
            Debug.Log("Gamesession icin active scene=0");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
       

    }
  
    void Start()
    {
       health = karakterCan.Can;
       scoreText.text = skor.ToString();
    }
    private void Update()
    {
        UpdateHearts();
    }
    private void UpdateHearts()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }


            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void ProcessPlayerDeath()
    {
        Invoke("ResetGameSession", olumZaman);
            //ResetGameSession();
    }
    public void AddToScore(int eklenecekSkor)
    {
         skor += eklenecekSkor;
         scoreText.text = skor.ToString();
    }
   void ResetGameSession()
    {
        //olunce bu ekrana gelecek
        var a = SceneManager.GetActiveScene().buildIndex;
        FindObjectOfType<ScenePermission>().ResetScenePersist();
        SceneManager.LoadScene(a);
        Destroy(gameObject);

    }
    public void TakeLife()
    {
        health = karakterCan.Can;
        UpdateHearts();
    }
}
