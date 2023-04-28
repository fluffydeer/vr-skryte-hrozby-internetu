using UnityEngine;
using System.Collections;
using Custom.Scripts.L1;

public class InteractableForViruses : MonoBehaviour
{
    public GameObject generalPanelText;
    public GameObject panelVirusText;
    private string playerString = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (other.name == playerString)
        {
            panelVirusText.SetActive(true);
            generalPanelText.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        panelVirusText.SetActive(false);
        generalPanelText.SetActive(true);
    }
}


/*
namespace Valve.VR.InteractionSystem.Sample {
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]	//automaticky prida dany objekt na GO
	public class InteractableForViruses : MonoBehaviour {
		public GameObject generalPanelText;
		public GameObject panelVirusText;

		private Vector3 oldPosition;
		private Vector3 oldRotation;
		private Vector3 oldScale;

		private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);
		private Interactable interactable;

		//-------------------------------------------------
		void Awake() {
			interactable = this.GetComponent<Interactable>();
		}


		//-------------------------------------------------
		// Called when a Hand starts hovering over this object
		//-------------------------------------------------
		private void OnHandHoverBegin(Hand hand) {
			panelVirusText.SetActive(true);
			generalPanelText.SetActive(false);
		}


		//-------------------------------------------------
		// Called when a Hand stops hovering over this object
		//-------------------------------------------------
		private void OnHandHoverEnd(Hand hand) {
			panelVirusText.SetActive(false);
			generalPanelText.SetActive(true);
		}


		//-------------------------------------------------
		// Called every Update() while a Hand is hovering over this object
		//-------------------------------------------------
		private void HandHoverUpdate(Hand hand) {
			GrabTypes startingGrabType = hand.GetGrabStarting();
			bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

			if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None) {
				// Save our position/rotation so that we can restore it when we detach
				oldPosition = transform.position;
				oldRotation = transform.localEulerAngles;
				oldScale = transform.localScale;

				// Call this to continue receiving HandHoverUpdate messages,
				// and prevent the hand from hovering over anything else
				hand.HoverLock(interactable);

				// Attach this object to the hand
				hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
			} else if (isGrabEnding) {
				// Detach this object from the hand
				hand.DetachObject(gameObject);

				// Call this to undo HoverLock
				hand.HoverUnlock(interactable);

				// Restore position/rotation
				transform.position = oldPosition;
				transform.localEulerAngles = oldRotation;
				transform.localScale = oldScale;
			}
		}


		//-------------------------------------------------
		// Called when this GameObject becomes attached to the hand
		//-------------------------------------------------
		private void OnAttachedToHand(Hand hand) {
		}


		//-------------------------------------------------
		// Called when this GameObject is detached from the hand
		//-------------------------------------------------
		private void OnDetachedFromHand(Hand hand) {
		}


		//-------------------------------------------------
		// Called every Update() while this GameObject is attached to the hand
		//-------------------------------------------------
		private void HandAttachedUpdate(Hand hand) {
		}


		private void Update() {
		}


		//-------------------------------------------------
		// Called when this attached GameObject becomes the primary attached object
		//-------------------------------------------------
		private void OnHandFocusAcquired(Hand hand) {
		}


		//-------------------------------------------------
		// Called when another attached GameObject becomes the primary attached object
		//-------------------------------------------------
		private void OnHandFocusLost(Hand hand) {
		}
	}
}*/


