using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;


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

        /*** upravy od natalky ***/
        public SteamVR_LaserPointer laserPointer;   //nezabudnut pridat RightHand na GameObject s tymto skriptom

        void Awake()    //natalka
        {
            laserPointer.PointerIn += PointerInside;
            laserPointer.PointerOut += PointerOutside;
            laserPointer.PointerClick += PointerClick;
        }

        //public EmailInteractionScript Instance;

        public void PointerClick(object sender, PointerEventArgs e)
        {
            Debug.Log("Clicked on canvas");
            if (e.target.name == "OkButton")
            {
                Debug.Log("Game starting");
                StartLevel();
            }
            //else if (e.target.name == "Text (7)")
            else if (e.target.name.Contains("Text"))
            {
                Debug.Log("Clicked on " + e.target.name);
                //EmailInteractionScript.Instance.Select();
                //Instance.Select();//toto samozrejme nejde lebo tam nic nie je
                //ako spristupnit fuknicu select daneho objektu Text (7)?
                //cize chcem najst Text(7) a nasledne zo skriptu ktory na nom je
                //zavolat funkciu select

                //ked tam teraz ja priradim skript z objektu tak
                //sa mi tie funkcie budu volat len na ten dejen gameobject a nie ostatne
                GameObject clickedText = GameObject.Find(e.target.name);
                //tu by sa potom dala dat podmienka ze ak je to prazdne tak 
                //nech to preskoci a evidentne ziadnu funcku nevola... len ze ci to 
                //nebude padat ak sa top bude snazit spristupnit objekt ktory neexistuje
                EmailInteractionScript script = (EmailInteractionScript)clickedText.GetComponent(typeof(EmailInteractionScript));
                script.Select();
            }
        }

        public void PointerInside(object sender, PointerEventArgs e) {
        }

        public void PointerOutside(object sender, PointerEventArgs e) {
        }
        /*** koniec natalky ***/


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
