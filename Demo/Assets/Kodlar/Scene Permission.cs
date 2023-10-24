using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePermission : MonoBehaviour
{
    void Awake()
    {
        int numScenePersist = FindObjectsOfType<ScenePermission>().Length;
        if (numScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}
