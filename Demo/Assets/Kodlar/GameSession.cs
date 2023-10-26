using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
   
    Health karakterCan;
    public int skor = 0;
    [SerializeField] float olumZaman = 0.1f;
    [SerializeField] TextMeshProUGUI canText;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        GameObject myObject = GameObject.Find("karakterim");
        karakterCan = myObject.GetComponent<Health>();
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
  
    void Start()
    {
       canText.text = karakterCan.Can.ToString();
       scoreText.text = skor.ToString();
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
        /*if (karakterCan.Can > 0)
        {
            playerCanlari -= amount;
        }
        else
        {
            playerCanlari = 0;
        }
       */
        /*var mevcutSahne = SceneManager.GetActiveScene().buildIndex; ölünce bastan baslatma kodu
        SceneManager.LoadScene(mevcutSahne);*/



       canText.text = karakterCan.Can.ToString();
    }
}
