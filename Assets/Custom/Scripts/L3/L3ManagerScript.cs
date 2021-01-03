using System;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.L3 {
	//manager tretej urovne hry
	public class L3ManagerScript : MonoBehaviour {

		public Text infoText; //text na tabuli
		public SimpleHealthBar healthBar; //progress bar - percentualny stav sily hesla
		public float barValue = 0.0f;	//aktualny stav na progress bare
		
		//casti hradu
		public GameObject plane;
		public GameObject walls;
		public GameObject gate;
		public GameObject flags;
		public GameObject towers;
		public GameObject portal;
		
		//zvukove efekty
		public AudioSource stormSound;
		public AudioSource winSound;

		private bool big = false; 		//v hesle je velke pismeno
        private bool number = false;	//cislo
        private bool special = false;	//specialny znak
        private bool alreadyFinished = false;	//ukonceny level
        private ParticleSystem[] particleSystems; //particle systemy v scene
        private String password = ""; //finalne heslo
        

        public AudioManagerScript audioManagerScript; //script na ovladanie zvuku

        //inicializacia
		void Start () {
			//nastavenie hlasitosti
			audioManagerScript.SetVolume();
			
			//text na tabuli
			infoText.text = "Poskladajte silné heslo.\n\n" +
			                "- aspoň 8 znakov\n" +
			                "- aspoň jedno veľké pismeno\n" +
			                "- aspoň jedno číslo\n" +
			                "- aspoň jeden špeciálny znak";
			
			
			particleSystems = FindObjectsOfType<ParticleSystem>();
		
		}
		
		void Update () {
			//aktualizacia stavu na progress bare
			barValue = 0.0f;
			if (big)
				barValue += 25.0f;
			if (number)
				barValue += 25.0f;
			if (special)
				barValue += 25.0f;
			if (password.Length>=8)
				barValue += 25.0f;
			healthBar.UpdateBar( barValue, 100.0f );

			ShowCastle(barValue); //zobrazenie hradu podla % na progress bare

		}

		void Win() {
			//vyhra iba ak splna vsetky podmienky
			if (special && number && big &&
			    (password.Length >= 8) && !alreadyFinished)  {

				//text v pripade vyhry
				infoText.fontSize = 50;
				infoText.text = "Výborne!\n" +
				                "Vaše heslo je:\n"+password;

				//spustenie ohnostrojov a vypnutie pustnej burky
				foreach (ParticleSystem ps in particleSystems) {
					if (ps.CompareTag("PlayParticles")) {
						ps.Play();
					}
					else if (ps.CompareTag("StopParticles")) {
						ps.Stop();
					}
				}
				stormSound.Stop(); //vypnutie zvuku vetra
				winSound.Play(); //spustenie vyherneho zvukoveho efektu
				alreadyFinished = true;
			}
			else { //niektora z podmienok nie je splnena - nie je koniec hry
				//text zobrazeny pocas hry
				infoText.fontSize = 30;
				infoText.text = "Poskladajte silné heslo.\n\n" +
				                "- aspoň 8 znakov\n" +
				                "- aspoň jedno veľké pismeno\n" +
				                "- aspoň jedno číslo\n" +
				                "- aspoň jeden špeciálny znak";

			}
		}

		private void OnCollisionEnter(Collision other) {
			//other.gameObject - dotykajuci sa objekt
			switch (other.gameObject.tag) { 

				case "SpecialSymbol":
					//prida sa nove pismeno
					password += other.gameObject.name[0]; 
					//pismeno bolo polozene stol => true
					special = true; 
					break;

				case "Number":
					password += other.gameObject.name[0];
					number = true;
					break;

				case "BigLetter":
					password += other.gameObject.name[0];
					big = true;
					break;

				case "SmallLetter":
					password += other.gameObject.name[0];
					break;
			}
			Win(); //vyhodnoti, ci je koniec hry
		}

		//zobrazenie casti hradu podla percenta na progress bare
		private void ShowCastle(float bar) {
			if (bar >= 25.0f) //25% ... podlaha
				plane.SetActive(true);
			else
				plane.SetActive(false);

			if (bar >= 50.0f) //50% ... steny
				walls.SetActive(true);
			else
				walls.SetActive(false);

			if (bar >= 75.0f) //75% ... veze
				towers.SetActive(true);
			else
				towers.SetActive(false);

			if (bar >= 100.0f) { //100% ... cely hrad
				gate.SetActive(true);
				flags.SetActive(true);
				portal.SetActive(true);
			}
			else {
				gate.SetActive(false);
				flags.SetActive(false);
				portal.SetActive(false);
			}
		}
	
		//dotyk stola s pismenom
		private void OnCollisionExit(Collision other) {
			//other.gameObject - dotykajuci sa objekt
			switch (other.gameObject.tag) {
				case "SpecialSymbol":
					/* z hesla sa vymaze posledne pismeno zodpovedajuce
					odstranenemu pismenu */
					password = password.Remove(
						password.LastIndexOf(other.gameObject.name[0]),1);
					//pismeno opustilo stol => false
					special = false;
					break;
				case "Number":
					password = password.Remove(
						password.LastIndexOf(other.gameObject.name[0]),1);
					number = false;
					break;
				case "BigLetter":
					password = password.Remove(
						password.LastIndexOf(other.gameObject.name[0]),1);
					big = false;
					break;
				case "SmallLetter":
					password = password.Remove(
						password.LastIndexOf(other.gameObject.name[0]),1);
					break;
			}
			Win(); //vyhodnoti, ci je koniec hry
		}
	}
}
