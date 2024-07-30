using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float loadDelay = 1f;

  //  public GameObject GameSessionObject;
  //  public GameObject ScenePersistObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag== "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
   
      
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(loadDelay);//delay koymak zorunda m�y�z bir bak:
        int sonrakiSahne = SceneManager.GetActiveScene().buildIndex + 1;
        if (sonrakiSahne == SceneManager.sceneCountInBuildSettings)
        {
         //   Destroy(GameSessionObject.gameObject);
       //     Destroy(ScenePersistObject.gameObject);
            sonrakiSahne = 0;//eger son bolumu gecersek ilk bolume atacak bunu son bolumu gecersek ara sahne gelsin �eklinde yapabilirz
            // ya da son b�l�m� ge�ersek gamesession ve scenemanager'I silerek ana menuye atacak -> d�zenleem ben GOATIM 
        }
        FindObjectOfType<ScenePermission>().ResetScenePersist();
        SceneManager.LoadScene(sonrakiSahne);
    }
 
}
