using UnityEngine;
using Custom.Scripts.L2;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private Camera characterCamera;
    [SerializeField] private L2ManagerScript l2ManagerScript;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Create ray from center of the screen
            var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
            RaycastHit hit;

            // Shot ray to find UI element to select
            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                // If object has certain tag then proceed
                if (hit.transform.tag == "HighlightedText")
                {
                    //get script through we which we show the UI
                    var emailInteractionScript = hit.collider.GetComponent<EmailInteractionScript>();
                    //check if script exists
                    if (emailInteractionScript)
                    {
                        emailInteractionScript.Select();
                    }
                }
                else if (hit.transform.tag == "CheckButton")
                {
                    var validateEmailScript = hit.collider.transform.parent.gameObject.GetComponent<ValidateEmailScript>();
                    validateEmailScript.GetResult();
                }
                else if (hit.transform.tag == "IncorrectButton")
                {
                    var validateEmailScript = hit.collider.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ValidateEmailScript>();
                    validateEmailScript.TryAgain();
                }
                else if (hit.transform.tag == "Fish")
                {
                    var collectFishScript = hit.collider.GetComponent<CollectFishScript>();
                    collectFishScript.Collect();
                }
                else if (hit.transform.tag == "OkButton")
                {
                    l2ManagerScript.StartLevel();
                }
            }   
        }
    }
}
