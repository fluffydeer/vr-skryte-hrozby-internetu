using System;
using UnityEngine;

namespace Custom.Scripts.L1 {
	
	//kontrola spravnej odpovede
	public class CheckAnswerScript : MonoBehaviour {
	
		public String correctTag; //spravny tag - ocakavany nazov malveru
		public GameObject redCanvas; //cervene pozadie textu
		public GameObject greenCanvas; //zelene pozadie textu
		public GameObject boxTop; //veko krabice
		public Light redLight; //cervene svetlo
		public L1ManagerScript l1ManagerScript; //manager prvej urovne

		private bool pushedButton = true; //stlacene tlacidlo
		private bool alreadyPlayed = false; //spustena hra

		private Animator closeBoxAnimator; //animacia
		private Collider collidedObject; //objekt, ktory bol vlozeny do krabice

		//inicializacia
		private void Start() {
			closeBoxAnimator = boxTop.GetComponent<Animator>();
			closeBoxAnimator.enabled = true;
			closeBoxAnimator.SetBool("correctAnswer", false);

			redLight.GetComponent<Light>().color = Color.red;
		}

		private void Update() {
			//CheckAnswer(); //kontrola odpovede
		}

        private void OnTriggerEnter(Collider other)
        {
            collidedObject = other;
            CheckAnswer(); //kontrola odpovede
        }

        //animacia zatvorenia krabice
        private void CloseBox() {
			closeBoxAnimator.SetBool("correctAnswer", true);
		}
		
		//ziskanie objektu, ktory prechadza triggerom
		private void OnTriggerStay(Collider other) {
			collidedObject = other;
		}


		//zmiznutie objektu
		private void HideObject() {
			collidedObject.gameObject.SetActive(false);
		}

		//stlacenie tlacidla
		public void SetPushedButton() {
			//buttons removed for our desktop version
			/*if (pushedButton == false) {
				pushedButton = true;
			}*/
		}

		//kontrola spravnej odpovede
		private void CheckAnswer() {
            //pushedButton dame prec
			if (pushedButton && !alreadyPlayed) { //stlacene tlacidlo
				
				if (collidedObject == null) { //v krabici nie je nic
                    Debug.Log("collided object is null");
                    pushedButton = false; //reset
				}
				else { //v krabici je objekt
                    Debug.Log("checking objects");
                    if (correctTag.Contains(collidedObject.tag)) { //spravny objekt
                        Debug.Log("correct object");
                        l1ManagerScript.correctSound.Play(); // zvukovy efekt
						l1ManagerScript.status++; //zvysi sa pocet bodov
						//zmena farby canvas
						redCanvas.SetActive(false);
						greenCanvas.SetActive(true);
						//zmena farby svetla
						redLight.GetComponent<Light>().color = Color.green;
						
						Invoke("CloseBox", 1.0f); //animacia zatvorenia krabice
						alreadyPlayed = true;
					}
					else { //nespravny objekt
                        Debug.Log("incorrect object");
                        l1ManagerScript.incorrectSound.Play(); // zvukovy efekt
						collidedObject.GetComponent<SpawnScript>().Spawn(); // zobrazenie na povodnom mieste
					}
					Invoke("HideObject", 2.0f); //zmiznutie objektu
					Destroy(collidedObject, 5); //znicenie objektu
					collidedObject = null;
					//pushedButton = false;
				}
			}
		}
	}
}


