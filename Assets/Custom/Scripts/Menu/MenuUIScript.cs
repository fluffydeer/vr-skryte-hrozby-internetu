using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.Menu {

	//interakcie s pouzivatelskym rozhranim v menu
	public class MenuUIScript : MonoBehaviour {
		public GameObject aboutPanel, tutorialPanel, infoPanel;     //UI panely
		public Slider musicSlider, effectsSlider, sensitivitySlider;                   //ovladace hlasitosti
        private float sensitivityDefault = 0.6f;
		private AudioManagerScript audioManagerScript;              //script na ovladanie zvuku
		private int contentSwitch = 0;
        public Camera characterCamera;                              // Reference to the character camera.
        public PlayerMovement player;

        //inicializacia
        void Start() {
			audioManagerScript = gameObject.GetComponent<AudioManagerScript>();
			musicSlider.value = PlayerPrefs.GetFloat("music", audioManagerScript.defaultVolume);
			effectsSlider.value = PlayerPrefs.GetFloat("effects", audioManagerScript.defaultVolume);
			audioManagerScript.SetVolume();
        }

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
                    if (hit.transform.name == "ExitButton")
                    {
                        ExitGame();
                    }
                    else if (hit.transform.name == "ShowInfoButton")
                    {
                        ShowInfoPanel();
                    }
                    else if (hit.transform.name == "HideInfoButton")
                    {
                        HideInfoPanel();
                    }
                    else if (hit.transform.name == "ScrollViewUp")
                    {
                        ScrollUp();
                    }
                    else if (hit.transform.name == "ScrollViewDown")
                    {
                        ScrollDown();
                    }
                    else if (hit.transform.name == "ResetButton")
                    {
                        ResetVolume();
                    }
                    else if (hit.transform.name == "MusicVolumeUp")
                    {
                        IncreaseMusicVolume();
                    }
                    else if (hit.transform.name == "MusicVolumeDown")
                    {
                        DecreaseMusicVolume();
                    }
                    else if (hit.transform.name == "EffectsVolumeUp")
                    {
                        IncreaseEffectsVolume();
                    }
                    else if (hit.transform.name == "EffectsVolumeDown")
                    {
                        DecreaseEffectsVolume();
                    }
                    else if (hit.transform.name == "MouseSensitivityUp")
                    {
                        IncreaseMouseSensitivity();
                    }
                    else if (hit.transform.name == "MouseSensitivityDown")
                    {
                        DecreaseMouseSensitivity();
                    }
                }
            }
        }

		//ukoncenie hry
		public void ExitGame() {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

        public void IncreaseMouseSensitivity()
        {
            sensitivitySlider.value += 0.1f;
        }

        public void DecreaseMouseSensitivity()
        {
            sensitivitySlider.value -= 0.1f;
        }

        public void UpdateMouseSensitivity()
        {
            player.mouseSensitivity = sensitivitySlider.value;
            PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.value);
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
            PlayerPrefs.SetFloat("sensitivity", sensitivityDefault);
			//nastavenie polohy slidera
			musicSlider.value = audioManagerScript.defaultVolume;
			effectsSlider.value = audioManagerScript.defaultVolume;
            sensitivitySlider.value = sensitivityDefault;

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
	}
}
