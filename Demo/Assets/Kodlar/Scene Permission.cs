using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePermission : MonoBehaviour
{
    //public GameObject gameSess;
    void Awake()
    {
        int numScenePersist = FindObjectsOfType<ScenePermission>().Length;
        if (numScenePersist > 1 || SceneManager.GetActiveScene().buildIndex==0)
        {
            Destroy(gameObject);
            Debug.Log("Scene Persist icin active scene=0");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetScenePersist()
    {
        Destroy(gameObject);
        // Destroy(gameSess.gameObject); Bölümlerde dümdüz ilerlerkenki sorunu çözdü ama ayný bölümü tekrar yüklediðimizde olan sorunlarý cozmedi
    }
}
