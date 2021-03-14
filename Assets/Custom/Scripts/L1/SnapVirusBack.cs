using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapVirusBack : MonoBehaviour
{
    public GameObject virus;
    //musi to byt napevno, lebo ak pouzivatel do toho drgne tak to odleti inde
    private Vector3 initialPosition;
    public double x;

    public void SaveInitialPosition()
    {
        initialPosition = virus.transform.position;
        Debug.Log("halooo " + initialPosition);
    }

    public void SnapVirusBackToInitialPosition()
    {
        virus.transform.position = initialPosition;
        Debug.Log("halooo2 " + initialPosition);

    }

}
