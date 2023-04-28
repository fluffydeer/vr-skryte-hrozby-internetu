using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TeleportToAnotherLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // gets the curent screen
        Scene sceneLoaded = SceneManager.GetActiveScene();
        // loads next level
        SceneManager.LoadScene(sceneLoaded.buildIndex + 1);
    }
}
