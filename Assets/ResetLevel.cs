using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ResetLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // gets the curent screen
        Scene sceneLoaded = SceneManager.GetActiveScene();
        // reloads level
        SceneManager.LoadScene(sceneLoaded.buildIndex);
    }
}
