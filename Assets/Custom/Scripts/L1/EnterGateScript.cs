using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.L1 {
	
	//zachytavanie prechodu cez branu
	public class EnterGateScript : MonoBehaviour {

		public AudioSource audioSource; //zvuk
		public GameObject welcomePanel; //panel s informaciami
		public Button button; //tlacidlo na paneli
		public L1ManagerScript l1ManagerScript; //manager prvej urovne
		public bool gameFinished = false; //ukonceny level
	
		private CanvasGroup canvasGroup; //canvas z panelu s informaciami
		private ParticleSystem[] particleSystems; //particle systemy
		private bool alreadyStarted = false; //spusteny level
		
		//inicializacia
		void Start () {
			particleSystems = FindObjectsOfType<ParticleSystem>();
			canvasGroup = welcomePanel.GetComponent<CanvasGroup>();
			canvasGroup.alpha = 0;
		}

		//kolizia  s objektom
		//private void OnTriggerEnter(Collider other) {

		//if (other.CompareTag("Player")) { //prechod hraca branou
		public void StartGame()
		{
			if (!alreadyStarted)
			{
				ShowGameStartRelatedStuff(); //zaciatok hry
				audioSource.Play(); //zvuk
				alreadyStarted = true;
			}

			//zobrazi sa panel s teleportom
			if (gameFinished)
			{
				l1ManagerScript.endLevelPanel.SetActive(false); //zmiznutie instrukcii
			}
		}
			//}
		//}
		
		void ShowGameStartRelatedStuff() {
			PlayParticles(); //spustenie particle systemov
			Invoke("ShowPanel",8.0f); //objavi sa panel
		}
	
		//zobrazenie panela
		public void ShowPanel() { 
			StartCoroutine(FadeIn());
		}
		
		//zmiznutie panela
		public void HidePanel() {
			StartCoroutine(FadeOut());
		}


		//postupne zobrazovanie panela
		public IEnumerator FadeIn() {

			while (canvasGroup.alpha  < 1.0) {
				canvasGroup.alpha += Time.deltaTime/2; //zvysovanie viditelnosti
				yield return null;
			}
		}
		//postupne miznutie panela
		public IEnumerator FadeOut() {
			while (canvasGroup.alpha  > 0.0) {
				canvasGroup.alpha -= Time.deltaTime/2; //znizovanie viditelnosti
				if (canvasGroup.alpha <= 0.05)
					canvasGroup.alpha = 0;
				yield return null;
			}
		}
	
		//spustenie casticovych systemov
		void PlayParticles() {
			foreach (ParticleSystem ps in particleSystems) {
				if (ps.CompareTag("PlayParticles"))
					ps.Play();
				else {
					ps.Stop();
				}
			}
		
		}
	}
}
