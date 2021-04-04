using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;


namespace Custom.Scripts.L2 {
    //manager druhej urovne hry
    public class L2ManagerScript : MonoBehaviour {
        public int points = 0;                          //pocet bodov (zozbieranych ryb)
        public GameObject phishingMails;                //phishing emaily
        public GameObject exampleMail;                  //prikladovy mail
        public GameObject winPanel;                     //panel zobrazeny na konci hry
        public Text pointsStatusText;                   //bodovy stav = text na vrchnej casti obrazovky

        private AudioManagerScript audioManagerScript;  //script na ovladanie hlasitosti
        private bool gameStarted = false;

        private GameObject previouslyClickedCheckAnswersButton;
        private Animator phishingAnimator;              //animacia pre phishing maily
        private Animator exampleAnimator;               //animacia pre prikladovy mail
        private Animator winAnimator;                   //animacia pre vyhernu tabulu a TeleportPoint

        /*** upravy od natalky ***/
        public SteamVR_LaserPointer laserPointer;   //nezabudnut pridat RightHand na GameObject s tymto skriptom

        void Awake() {
            laserPointer.PointerIn += PointerInside;
            laserPointer.PointerOut += PointerOutside;
            laserPointer.PointerClick += PointerClick;
        }

        public void PointerClick(object sender, PointerEventArgs e) {
            //Debug.Log("Clicked on canvas");
            //Debug.Log("Clicked on " + e.target.name);

            if (e.target.name == "OkButton") {
                handleOkButton();
            } else if (e.target.name == "CheckAnswersButton") {
                handleCheckAnswersButton(e.target);

                /*GameObject clickedButton = getClickedGameObject(e.target);
                previouslyClickedCheckAnswersButton = clickedButton;
                GameObject canvas = clickedButton.transform.parent.gameObject;         //parent zakliknuteho buttonu
                ValidateEmailScript script = (ValidateEmailScript)canvas.GetComponent(typeof(ValidateEmailScript));
                if (script.GetResult()) {
                    clickedButton.SetActive(false);
                }*/
            } else if (e.target.name == "CorrectAnswerButton") {
                handleCorrectAnswerButton(e.target);
                /*GameObject clickedButton = getClickedGameObject(e.target);
                GameObject popUpIncorrect = clickedButton.transform.parent.gameObject;  //parent zakliknuteho buttonu
                GameObject canvas = popUpIncorrect.transform.parent.gameObject;
                ValidateEmailScript script = (ValidateEmailScript)canvas.GetComponent(typeof(ValidateEmailScript));
                previouslyClickedCheckAnswersButton.SetActive(false);
                script.GetFish();*/
            } else if (e.target.name == "IncorrectAnswerButton") {
                handleIncorrectAnswerButton(e.target);
                /*GameObject clickedButton = getClickedGameObject(e.target);
                GameObject popUpIncorrect = clickedButton.transform.parent.gameObject;  //parent zakliknuteho buttonu
                GameObject canvas = popUpIncorrect.transform.parent.gameObject;
                ValidateEmailScript script = (ValidateEmailScript)canvas.GetComponent(typeof(ValidateEmailScript));
                script.TryAgain();*/
            } else if (e.target.name.Contains("Text")) {
                handleText(e.target);

                /*GameObject clickedText = getClickedGameObject(e.target);
                GameObject canvas = clickedText.transform.parent.gameObject;           
                ValidateEmailScript script = (ValidateEmailScript)canvas.GetComponent(typeof(ValidateEmailScript));
                if (!gameStarted || !script.getAlreadyChecked()) {
                    EmailInteractionScript emailScript = (EmailInteractionScript)clickedText.GetComponent(typeof(EmailInteractionScript));
                    emailScript.Select();
                }*/
            }
        }

        public void handleOkButton() {
            gameStarted = true;
            StartLevel();
        }

        public void handleCheckAnswersButton(Transform clickedObject) {
            GameObject clickedButton = getClickedGameObject(clickedObject);
            previouslyClickedCheckAnswersButton = clickedButton;
            ValidateEmailScript script = getValidateEmailScriptFirstLevel(clickedObject);
            if (script.GetResult()) {
                clickedButton.SetActive(false);
            }
        }

        public void handleCorrectAnswerButton(Transform clickedObject) {
            previouslyClickedCheckAnswersButton.SetActive(false);
            ValidateEmailScript script = getValidateEmailScriptSecondLevel(clickedObject);
            script.GetFish();
        }


        public void handleIncorrectAnswerButton(Transform clickedObject) {
            ValidateEmailScript script = getValidateEmailScriptSecondLevel(clickedObject);
            script.TryAgain();
        }

        public void handleText(Transform clickedObject) {
            GameObject clickedText = getClickedGameObject(clickedObject);
            ValidateEmailScript script = getValidateEmailScriptFirstLevel(clickedObject);
            if (!gameStarted || !script.getAlreadyChecked()) {
                EmailInteractionScript emailScript = (EmailInteractionScript)clickedText.GetComponent(typeof(EmailInteractionScript));
                emailScript.Select();
            }
        }

        public ValidateEmailScript getValidateEmailScriptFirstLevel(Transform clickedObject) {
            GameObject clickedText = getClickedGameObject(clickedObject);
            GameObject canvas = clickedText.transform.parent.gameObject;                     //parent zakliknuteho buttonu
            return (ValidateEmailScript)canvas.GetComponent(typeof(ValidateEmailScript));
        }

        public ValidateEmailScript getValidateEmailScriptSecondLevel(Transform clickedObject) {
            GameObject clickedButton = getClickedGameObject(clickedObject);
            GameObject popUp = clickedButton.transform.parent.gameObject;       //parent zakliknuteho buttonu
            GameObject canvas = popUp.transform.parent.gameObject;
            return (ValidateEmailScript)canvas.GetComponent(typeof(ValidateEmailScript));
        }

        //hladanie GameObjektov podla ich instance ID, kedze tam mame asi 10 Buttonov
        //s tym istym id a to iste aj pre texty na canvasoch
        public GameObject getClickedGameObject(Transform clickedObject) {
            string previousName = clickedObject.name;
            //poitrebujem objektu nastavit id cize ja tam potrebujem poslat objekt
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
