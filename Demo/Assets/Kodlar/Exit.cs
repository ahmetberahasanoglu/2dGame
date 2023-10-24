using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float loadDelay = 1f;
 

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag== "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
   
            
       
        

    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(loadDelay);
        int sonrakiSahne = SceneManager.GetActiveScene().buildIndex + 1;
        if (sonrakiSahne == SceneManager.sceneCountInBuildSettings)
        {
            sonrakiSahne = 0;
        }
        FindObjectOfType<ScenePermission>().ResetScenePersist();
        SceneManager.LoadScene(sonrakiSahne);
    }
 
}
