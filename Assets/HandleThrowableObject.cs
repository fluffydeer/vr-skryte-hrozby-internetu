using UnityEngine;

public class HandleThrowableObject : MonoBehaviour
{
    [SerializeField] private Camera characterCamera;
    [SerializeField] private Transform slotForPickedObject;
    public LayerMask layerMask;

    private ThrowableObject pickedObject;

    private void Update()
    {
        //on mouse click
        if (Input.GetButtonDown("Fire1"))
        {
            if (pickedObject)
            {
                ThrowObject(pickedObject);
            }
            else
            {
                CheckObject();
            }
        }
    }

    private void PickObject(ThrowableObject currentlyPicked)
    {
        pickedObject = currentlyPicked;

        //disable rigidbody and reset velocities
        currentlyPicked.Rigidbody.isKinematic = true;
        currentlyPicked.Rigidbody.velocity = Vector3.zero;
        currentlyPicked.Rigidbody.angularVelocity = Vector3.zero;

        currentlyPicked.transform.SetParent(slotForPickedObject);

        // Reset position and rotation of picked object
        currentlyPicked.transform.localPosition = Vector3.zero;
        currentlyPicked.transform.localEulerAngles = Vector3.zero;
    }

    private void CheckObject()
    {
        //create ray from center of the screen
        var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;
        //emit ray to find object to pick
        if (Physics.Raycast(ray, out hit, 3.0f, layerMask))
        {
            //check if object has ThrowableObject class
            var throwable = hit.transform.GetComponent<ThrowableObject>();
            if (throwable)
            {
                PickObject(throwable);
            }
        }
    }

    private void ThrowObject(ThrowableObject item)
    {
        pickedObject = null;
        item.Rigidbody.isKinematic = false;
        item.transform.SetParent(null);
        item.Rigidbody.AddForce(item.transform.forward * 10, ForceMode.VelocityChange);
    }
}