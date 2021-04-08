using UnityEngine;
using System.Collections;
using Custom.Scripts.L1;

namespace Valve.VR.InteractionSystem.Sample {
	//-------------------------------------------------------------------------
	[RequireComponent(typeof(Interactable))]	//automaticky prida dany objekt na GO
	public class InteractableForViruses : MonoBehaviour {
		public GameObject generalPanelText;
		public GameObject panelVirusText;

		private TextMesh generalText;
		private TextMesh hoveringText;
		
		private Vector3 oldPosition;
		private Vector3 oldRotation;
		private Vector3 oldScale;


		private float attachTime;

		private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);

		private Interactable interactable;
		private Vector3 position;
		private Vector3 rotation;


		//-------------------------------------------------
		void Awake() {
			/*position = transform.position;
			rotation = transform.localEulerAngles;
			oldScale = transform.localScale;


			/*var textMeshs = GetComponentsInChildren<TextMesh>();
			generalText = textMeshs[0];
			hoveringText = textMeshs[1];

			generalText.text = "No Hand Hovering";
			hoveringText.text = "Hovering: False";*/

			interactable = this.GetComponent<Interactable>();
			//panelText = GameObject.Find("VirusRoomPanelText").GetComponent<TextMesh>();/* GetComponent<L2ManagerScript>();*/

		}


		//-------------------------------------------------
		// Called when a Hand starts hovering over this object
		//-------------------------------------------------
		private void OnHandHoverBegin(Hand hand) {
			//generalText.text = "Hovering hand: " + hand.name;
			Debug.Log("Hover begin");
			panelVirusText.SetActive(true);
			generalPanelText.SetActive(false);
		}


		//-------------------------------------------------
		// Called when a Hand stops hovering over this object
		//-------------------------------------------------
		private void OnHandHoverEnd(Hand hand) {
			//generalText.text = "No Hand Hovering";
			Debug.Log("Hover end");
			// panelText.text = "end";
			panelVirusText.SetActive(false);
			generalPanelText.SetActive(true);
			/*transform.position = position;
			transform.localEulerAngles = rotation;
			transform.localScale = oldScale;*/
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
			//generalText.text = string.Format("Attached: {0}", hand.name);
			//attachTime = Time.time;
			Debug.Log("Hover holding");

		}



		//-------------------------------------------------
		// Called when this GameObject is detached from the hand
		//-------------------------------------------------
		private void OnDetachedFromHand(Hand hand) {
			//generalText.text = string.Format("Detached: {0}", hand.name);
		}


		//-------------------------------------------------
		// Called every Update() while this GameObject is attached to the hand
		//-------------------------------------------------
		private void HandAttachedUpdate(Hand hand) {
			//generalText.text = string.Format("Attached: {0} :: Time: {1:F2}", hand.name, (Time.time - attachTime));
		}

		private bool lastHovering = false;
		private void Update() {
			/*if (interactable.isHovering != lastHovering) //save on the .tostrings a bit
			{
				hoveringText.text = string.Format("Hovering: {0}", interactable.isHovering);
				lastHovering = interactable.isHovering;
			}*/
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
}
