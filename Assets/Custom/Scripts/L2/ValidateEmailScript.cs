using System.Collections.Generic;
using UnityEngine;

namespace Custom.Scripts.L2 {

	//kontrola spravnej odpovede v maili
	public class ValidateEmailScript : MonoBehaviour {

		public Transform spawnPoint; //bod inicializacie ryby
		public GameObject fish; //objekt ryby
		public GameObject popupCorrect; //okno - spravna odpoved
		public GameObject popupIncorrect; //okno - nespravna odpoved
		private bool alreadyChecked = false; //hrac skontroloval mail

		//script na interakcie s mailom
		private List<EmailInteractionScript> emailInteractionScripts = new List<EmailInteractionScript>();

		public bool getAlreadyChecked() {
			return alreadyChecked;
        }

		//inicializacia
		void Start () {
			popupCorrect.SetActive(false);
			popupIncorrect.SetActive(false);
			SetEmailInteractionScripts();
		}

		//ziskanie oznacenych casti textu
		private void SetEmailInteractionScripts() {

			//hadam, ze ten gameobject je Canvas
			foreach (Transform child in gameObject.GetComponent<Transform>()) {
				if (child.gameObject.CompareTag("HighlightedText")) {
					emailInteractionScripts.Add(child.GetComponent<EmailInteractionScript>());
				}
			}
		}
		
		//kontrola odpovede
		private bool ValidateAnswer() {
			bool result = true;
			
			foreach (EmailInteractionScript highlightedText in emailInteractionScripts) {
				// hrac spravne oznacil casti textu
				if (highlightedText.selected && highlightedText.isCorrect ||
				    !highlightedText.selected && !highlightedText.isCorrect) {
					highlightedText.HighlightCorrect(); //spravna odpoved - nazeleno
				}
				else { //oznacil nespravne
					highlightedText.HighlightIncorrect(); //nespravna odpoved - nacerveno
					result = false;
				}
			}
			return result;
		}

		//reset aktualneho mailu
		public void TryAgain() {
			popupIncorrect.SetActive(false);
			foreach (EmailInteractionScript highlightedText in emailInteractionScripts) {
				highlightedText.Clear();
			}
			Debug.Log("tryagain in validatescript");
		}

		//objavi sa ryba
		public void GetFish() {
			Debug.Log("rybka chytena - klikla som na button alebo som rybu chytila? ");
			popupCorrect.SetActive(false);
		}
		
		
		//zavolana pri kliknuti na tlacidlo "Skontroluj"
		public bool GetResult() {
			if (!alreadyChecked) {
				if (ValidateAnswer()) { //kontrola odpovede
					
					//aby sa neinicializovalo viacero ryb 
					foreach (GameObject f in GameObject.FindGameObjectsWithTag("Fish")) {
						Destroy(f);
					}
					//popupCorrect.SetActive(true); //info - spravna odpoved
					Instantiate(fish, spawnPoint.position, spawnPoint.rotation); //inicializacia ryby
					alreadyChecked = true;
					return true;
				}
				else {
					popupIncorrect.SetActive(true); //info - nespravna odpoved
					return false;
				}
			}return false;
		}
	}
}
