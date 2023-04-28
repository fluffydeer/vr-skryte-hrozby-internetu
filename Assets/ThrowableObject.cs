using UnityEngine;

//to make object pickable 
[RequireComponent(typeof(Rigidbody))]
public class ThrowableObject : MonoBehaviour
{
    private Rigidbody rigidbody;
    public Rigidbody Rigidbody => rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
}