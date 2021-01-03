using UnityEngine;
using UnityEngine.UI;


namespace Custom.Scripts.L1 { 
    
    //manager prvej urovne hry
    public class L1ManagerScript : MonoBehaviour {
        //zvukove efekty
        public AudioSource winSound;
        public AudioSource correctSound;
        public AudioSource incorrectSound;
        
        public GameObject endLevelTeleport; //teleport do 3. urovne
        public GameObject endLevelPanel; //info zobrazene na konci urovne

        public SimpleHealthBar healthBar; //progress bar
        public int status = 0; //pocet spravnych odpovedi
      
        public EnterGateScript enterGateScript;

        private bool alreadyWon = false; // ukoncenie urovne
        private AudioManagerScript audioManagerScript; //script na manipulaciu so zvukom

        private GameObject[] virusRoomObjects;
        //inicializacia
        void Start() {
            virusRoomObjects = GameObject.FindGameObjectsWithTag("VirusRoom");
            
            //nastavenie hlasitosti
            audioManagerScript = gameObject.GetComponent<AudioManagerScript>();
            audioManagerScript.SetVolume();
   
            endLevelTeleport.SetActive(false);
            endLevelPanel.SetActive(false);
            ToggleVirusRoom(true);
            healthBar.UpdateBar(0, 10);
           
        }
    
        
        void Update() {
            UpdateStatus(); //aktualizuje sa stav
        }
        
        //aktualizacia stavu
        private void UpdateStatus() {
            healthBar.UpdateBar(status, 10); //progress bar
            if (status == 10 && !alreadyWon) {
                winSound.Play(); //zvukovy efekt
                //panel s teleportom na 2. uroven
                endLevelTeleport.SetActive(true);
                ToggleVirusRoom(false);
                
                endLevelPanel.SetActive(true);  //ukazanie instrukcii
                
                healthBar.transform.parent.gameObject.SetActive(false); //zmiznutie progress baru
                alreadyWon = true;
                enterGateScript.gameFinished = true;
            }
        }

        private void ToggleVirusRoom(bool isVisible)
        {
            foreach (GameObject go in virusRoomObjects) {
                go.SetActive(isVisible);
            }
        }

    }
}
