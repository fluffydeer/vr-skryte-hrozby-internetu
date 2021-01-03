using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.L2 {
    //manager druhej urovne hry
    public class L2ManagerScript : MonoBehaviour {
        public int points = 0; //pocet bodov (zozbieranych ryb)
        public GameObject phishingMails; //phishing emaily
        public GameObject exampleMail; //prikladovy mail
        public GameObject winPanel; //panel zobrazeny na konci hry
        public Text pointsStatusText; //bodovy stav = text na vrchnej casti obrazovky
       
        private AudioManagerScript audioManagerScript; //script na ovladanie hlasitosti
        
        private Animator phishingAnimator;  //animacia pre phishing maily
        private Animator exampleAnimator; //animacia pre prikladovy mail
        private Animator winAnimator; //animacia pre vyhernu tabulu a TeleportPoint
      
        //inicializacia
        void Start() {
            //nastavenie hlasitosti
            audioManagerScript = gameObject.GetComponent<AudioManagerScript>();
            audioManagerScript.SetVolume();
            
            phishingMails.transform.localScale = new Vector3(0, 0, 0);
            winPanel.transform.localScale = new Vector3(0, 0, 0);

            exampleMail.SetActive(true);
            
            phishingAnimator = phishingMails.GetComponent<Animator>();
            exampleAnimator = exampleMail.GetComponent<Animator>();
            winAnimator = winPanel.GetComponent<Animator>();

            
            phishingAnimator.SetBool("moveDown",false);
    
            exampleAnimator.SetBool("moveUp",false);
            winAnimator.SetBool("moveDown",false);
        }
     
      
        void Update() {
            pointsStatusText.text = points+"/10"; //stav na hornej casti obrazovky
            if (points == 10) { //pozbierane vsetky ryby - level je dokonceny
                pointsStatusText.color = Color.green;
                FinishLevel(); //ukoncenie levelu
            }
        }

        //zaciatok levelu
        public void StartLevel() {
            //zobrazenie mailov
            phishingMails.transform.localScale = new Vector3(1, 1, 1);
     
            //animacia - spustenie mailov nadol, prikladovy mail nahor
            phishingAnimator.SetBool("moveDown",true);
            exampleAnimator.SetBool("moveUp",true);
            Invoke("HideExample",5.0f);
        }

        //koniec levelu
        public void FinishLevel() {
            //objavenie tabule a teleportu do 3. urovne
            winPanel.transform.localScale = new Vector3(1, 1, 1);
            
            //animacia - maily nahor, teleport nadol
            phishingAnimator.SetBool("moveDown",false);
            winAnimator.SetBool("moveDown",true);
            Invoke("HidePhishingMails",5.0f); //maily zmiznu
        }

        //zmiznutie prikladoveho mailu
        private void HideExample() {
            exampleMail.transform.localScale = new Vector3(0, 0, 0);
        }
        
        //zmiznutie mailov
        private void HidePhishingMails() {
            phishingMails.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
