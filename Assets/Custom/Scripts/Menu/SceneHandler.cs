using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using Custom.Scripts.Menu;  //for including MenuUIScript

public class SceneHandler : MonoBehaviour
{
    
    public SteamVR_LaserPointer laserPointer;
    
    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        Debug.Log("clicked");
        if (e.target.name == "ExitButton")
        {
            Debug.Log("Button was clicked");
            //MenuUIScript.ExitGame();

        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "ExitButton")
        {
            Debug.Log("Button was entered");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        Debug.Log("Exited");
        if (e.target.name == "ExitButton")
        {
            Debug.Log("Button was exited");
        }
    }
}