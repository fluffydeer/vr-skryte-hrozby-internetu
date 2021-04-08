using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class VirusTrigger : MonoBehaviour{
    public GameObject generalPanelText;
    public GameObject panelVirusText;
    private Interactable hehe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        //mozem ked tak kontrolovat ci to ma prefix finger
        Debug.Log("entered by " + other.name);

        panelVirusText.SetActive(true);
        generalPanelText.SetActive(false);

        //hehe.CreateHighlightRenderers();
        //hehe.UpdateHighlightRenderers();

        if (other.tag == "Hand") {
            Debug.Log("button collider entered by hand");
        }
        if (other.tag == "Player") {
            Debug.Log("button collider entered by player");

        }
    }

    void OnTriggerExit(Collider other) {
        //mozem ked tak kontrolovat ci to ma prefix finger
        Debug.Log("exited by " + other.name);

        panelVirusText.SetActive(false);
        generalPanelText.SetActive(true);
    }
}
