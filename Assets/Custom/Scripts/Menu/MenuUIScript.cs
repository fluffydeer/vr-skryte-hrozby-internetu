using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Valve.VR.Extras;

namespace Custom.Scripts.Menu {

	//interakcie s pouzivatelskym rozhranim v menu
	public class MenuUIScript : MonoBehaviour {
		public SteamVR_LaserPointer laserPointer;                   //laser shooting from right hand
		public GameObject aboutPanel, tutorialPanel, infoPanel;     //UI panely
		public Slider musicSlider, effectsSlider;                   //ovladace hlasitosti
		private AudioManagerScript audioManagerScript;              //script na ovladanie zvuku
		private int contentSwitch = 0;

		//inicializacia
		void Start() {
			audioManagerScript = gameObject.GetComponent<AudioManagerScript>();
			musicSlider.value = PlayerPrefs.GetFloat("music", audioManagerScript.defaultVolume);
			effectsSlider.value = PlayerPrefs.GetFloat("effects", audioManagerScript.defaultVolume);
			audioManagerScript.SetVolume();
		}

		//ukoncenie hry
		public void ExitGame() {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

		//prepinanie panelov - pomocna funkcia
		public void TogglePanel(GameObject visible, GameObject hidden) {
			visible.SetActive(true);
			hidden.SetActive(false);
		}

		//zobrazenie informacii o hre
		public void ShowInfoPanel() {
			TogglePanel(infoPanel, aboutPanel);
		}

		//zmiznutie informacii o hre
		public void HideInfoPanel() {
			TogglePanel(aboutPanel, infoPanel);
		}

		//zobrazenie tutorialu
		public void ShowTutorialPanel() {
			TogglePanel(tutorialPanel, aboutPanel);
		}

		//zmiznutie tutorialu
		public void HideTutorialPanel() {
			TogglePanel(aboutPanel, tutorialPanel);
		}

		public void IncreaseMusicVolume() {
			musicSlider.value += 0.1f;
			UpdateMusicVolume();
		}

		public void DecreaseMusicVolume() {
			musicSlider.value -= 0.1f;
			UpdateMusicVolume();
		}

		public void IncreaseEffectsVolume()
		{
			effectsSlider.value += 0.1f;
			UpdateEffectsVolume();
		}

		public void DecreaseEffectsVolume()
		{
			effectsSlider.value -= 0.1f;
			UpdateEffectsVolume();
		}

		//nastavenie hlasitosti hudby
		public void UpdateMusicVolume() {
			PlayerPrefs.SetFloat("music", musicSlider.value);
			audioManagerScript.SetVolume();
		}

		//nastavenie hlasitosti zvukovych efektov
		public void UpdateEffectsVolume() {
			PlayerPrefs.SetFloat("effects", effectsSlider.value);
			audioManagerScript.SetVolume();
		}

		//nastavenie hlasitosti na predvolenu hodnotu
		public void ResetVolume() {
			//nastavenie hodnot v PlayerPrefs
			PlayerPrefs.SetFloat("music", audioManagerScript.defaultVolume);
			PlayerPrefs.SetFloat("effects", audioManagerScript.defaultVolume);
			//nastavenie polohy slidera
			musicSlider.value = audioManagerScript.defaultVolume;
			effectsSlider.value = audioManagerScript.defaultVolume;

			audioManagerScript.SetVolume(); //aktualizacia hlasitosti
		}

		//scroll contents in InfoCanvas
		private void Scroll(int switchTo)
        {
			if ((contentSwitch+switchTo) > -1 && (contentSwitch+switchTo) < 7)	//od Content1 po Content7
			{
				infoPanel.transform.GetChild(contentSwitch).gameObject.SetActive(false);
				contentSwitch+=switchTo;
				infoPanel.transform.GetChild(contentSwitch).gameObject.SetActive(true);
			}
		}

		private void ScrollUp(){
			Scroll(-1);
		}

		private void ScrollDown(){
			Scroll(1);
		}

		void Awake(){
			laserPointer.PointerIn += PointerInside;
			laserPointer.PointerOut += PointerOutside;
			laserPointer.PointerClick += PointerClick;
		}

		public void PointerClick(object sender, PointerEventArgs e){
            switch (e.target.name)
            {
				case "ExitButton":
					ExitGame();
					break;
				case "ShowTutorialButton":
					ShowTutorialPanel();
					break;
				case "HideTutorialButton":
					HideTutorialPanel();
					break;
				case "ShowInfoButton":
					ShowInfoPanel();
					break;
				case "HideInfoButton":
					HideInfoPanel();
					break;


				case "ScrollViewUp":            //LaserPointer reaguje len na click neda sa dragovat
					Debug.Log("ScrollViewUp");
					ScrollUp();
					break;
				case "ScrollViewDown":            //LaserPointer reaguje len na click neda sa dragovat
					Debug.Log("ScrollViewDown");
					ScrollDown();
					break;
				case "DemoButton":
					Debug.Log("Demo Button was clicked");


					break;
				case "ResetButton":
					ResetVolume();
					break;
				case "MusicVolumeUp":		//LaserPointer reaguje len na click neda sa dragovat
					IncreaseMusicVolume();
					break;
				case "MusicVolumeDown":
					DecreaseMusicVolume();
					break;
				case "EffectsVolumeUp":
					IncreaseEffectsVolume();
					break;
				case "EffectsVolumeDown":
					DecreaseEffectsVolume();
					break;
				default:
					break;
			}

		}

		public void PointerInside(object sender, PointerEventArgs e){}

		public void PointerOutside(object sender, PointerEventArgs e){}
	}
}
