﻿using System.Collections;
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
        private bool gameStarted = false;

        private GameObject previouslyClickedCheckAnswersButton;
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

        public void PointerClick(object sender, PointerEventArgs e) {
            Debug.Log("Clicked on canvas");
            Debug.Log("Clicked on " + e.target.name);

            if (e.target.name == "OkButton") {
                Debug.Log("Game starting");
                gameStarted = true;
                StartLevel();
            } else if (e.target.name == "CheckAnswersButton") {
                //todo, kde je ten skript? treba zohnat tu danu naplnenu instanciu
                //mame to na Canvase - ako ten skript zohnat ked Canvas ma stale to iste
                //meno vsade? mozno viem nejakym sposobom zohnat unikatne parent id?
                //podla canvas mena ich nemozem vuýhladavat lebo nie su unikatne
                //1. canvas potiahnem do public variable pre kazdy mail - to by bolo 11.x
                //2. dostanem sa ku skriptu rodica a nasledne zavolam funkciu
                //transform.parent.gameObject.GetComponent<"Validate Email Script">().GetResult();
                //ValidateEmailScript.GetResult();

                //hladame parenta buttonu
                //GameObject Canvas = transform.parent.gameObject.GetComponent<"Validate Email Script">().GetResult();
                Debug.Log("KONTROLUJEM");
                GameObject clickedButton = getClickedGameObject(e.target);
                previouslyClickedCheckAnswersButton = clickedButton;
                GameObject canvas = clickedButton.transform.parent.gameObject;  //parent zakliknuteho buttonu
                ValidateEmailScript script = (ValidateEmailScript)canvas.GetComponent(typeof(ValidateEmailScript));
                //todo script neobsahuje referenciu
                if (script.GetResult()) {
                    clickedButton.SetActive(false);
                }
                Debug.Log("meno parenta: " + canvas.name);
            } else if (e.target.name == "CorrectAnswerButton") {
                GameObject clickedButton = getClickedGameObject(e.target);
                GameObject popUpIncorrect = clickedButton.transform.parent.gameObject;  //parent zakliknuteho buttonu
                GameObject canvas = popUpIncorrect.transform.parent.gameObject;  //parent zakliknuteho buttonu
                ValidateEmailScript script = (ValidateEmailScript)canvas.GetComponent(typeof(ValidateEmailScript)); previouslyClickedCheckAnswersButton.SetActive(false);
                //todo script neobsahuje referenciu
                script.GetFish();


            } else if (e.target.name == "IncorrectAnswerButton") {
                GameObject clickedButton = getClickedGameObject(e.target);
                //toto nebude canvas ale go popupincorrect
                GameObject popUpIncorrect = clickedButton.transform.parent.gameObject;  //parent zakliknuteho buttonu
                GameObject canvas = popUpIncorrect.transform.parent.gameObject;  //parent zakliknuteho buttonu
                ValidateEmailScript script = (ValidateEmailScript)canvas.GetComponent(typeof(ValidateEmailScript));
                //todo script neobsahuje referenciu
                script.TryAgain();

            }
            //else if (e.target.name == "Text (7)")
            else if (e.target.name.Contains("Text"))    //tu by som to mohla kontrolovat podla
            {//tagov a potom by nebolo treba prepisovat tie idcka spat
                //otazka je zi ci tu mam spravny Text a ze ci fakt budu rozdielne!
                //Debug.Log("Clicked on " + e.target.name);
                Debug.Log("Instance ID " + e.target.GetInstanceID().ToString());
                //EmailInteractionScript.Instance.Select();
                //Instance.Select();//toto samozrejme nejde lebo tam nic nie je
                //ako spristupnit fuknicu select daneho objektu Text (7)?
                //cize chcem najst Text(7) a nasledne zo skriptu ktory na nom je
                //zavolat funkciu select


                //je ten e.target referencia alebo len kopia?
                //string previousName = e.target.name;
                //e.target.name = e.target.GetInstanceID().ToString();
                //ked tam teraz ja priradim skript z objektu tak
                //sa mi tie funkcie budu volat len na ten dejen gameobject a nie ostatne
                //GameObject clickedText = GameObject.Find(e.target.name);

                GameObject clickedText = getClickedGameObject(e.target);
                //tu by sa potom dala dat podmienka ze ak je to prazdne tak 
                //nech to preskoci a evidentne ziadnu funcku nevola... len ze ci to 
                //nebude padat ak sa top bude snazit spristupnit objekt ktory neexistuje
                //toto nebude canvas ale go popupincorrect
                GameObject canvas = clickedText.transform.parent.gameObject;  //parent zakliknuteho buttonu
                ValidateEmailScript script = (ValidateEmailScript)canvas.GetComponent(typeof(ValidateEmailScript));
                if (!gameStarted || !script.getAlreadyChecked()) {
                    EmailInteractionScript emailScript = (EmailInteractionScript)clickedText.GetComponent(typeof(EmailInteractionScript));
                    emailScript.Select();
                }
                //ak to id nevratim spat tak ma nepusti podmienka lebo chyba Text v nazve
                //e.target.name = previousName;   //toto sa bude dat dat vyssie
            }
        }

        //hladanie GameObjektov podla ich instance ID, kedze tam mame asi 10 Buttonov
        //s tym istym id a to iste aj pre texty na canvasoch
        public GameObject getClickedGameObject(Transform clickedObject) {
            string previousName = clickedObject.name;
            //poitrebujem objketu nastavit id cize ja tam potrebujem poslat objekt
            clickedObject.name = clickedObject.GetInstanceID().ToString();
            GameObject ourPreciousGameObject = GameObject.Find(clickedObject.name);
            clickedObject.name = previousName;
            return ourPreciousGameObject;
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
