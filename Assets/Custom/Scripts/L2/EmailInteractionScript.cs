using System;
using UnityEngine;
//using Valve.VR.Extras;

namespace Custom.Scripts.L2 {
	
	//interakcie hraca s emailom
	public class EmailInteractionScript : MonoBehaviour {
		
		private Transform highlightedText; //oznaceny text
		public bool selected = false; //hrac oznacil text
		public bool isCorrect = false; //text treba oznacit => true, netreba oznacit => false
		
		//farebne pozadia
		public GameObject backgroundCorrect; 
		public GameObject backgroundIncorrect;
		public GameObject background;

		//aby L2MananagerScript mohol spristupnit select
		//public static EmailInteractionScript Instance;
		
		//inicializacia
		void Start() {
			highlightedText = gameObject.GetComponent<Transform>();
			
			if (backgroundCorrect != null)
				backgroundCorrect.SetActive(false);
			if (backgroundIncorrect != null)
				backgroundIncorrect.SetActive(false);
			if (background != null)
				background.SetActive(false);
		}

		/*** upravy od natalky ***/
		/*public SteamVR_LaserPointer laserPointer;   //nezabudnut pridat RightHand na GameObject s tymto skriptom

		void Awake()    //natalka
		{
			laserPointer.PointerIn += PointerInside;
			laserPointer.PointerOut += PointerOutside;
			laserPointer.PointerClick += PointerClick;
		}

		public void PointerClick(object sender, PointerEventArgs e)
		{
			//Debug.Log("Clicked on " + e.target.name);
			//musim zobrazit spravny text - ale ako zobrazit ten spravny?
			//spracuje sa a ako prvy a potom cez staticku premennu

			//vzdy by to malo pracovat s prave jednym textom ak tomu spravne 
			//rozumiem a preto tu netreba mat mega switch na vsetky texty
			//by sa oplatilo tu potom dat design pattern na switche
			//takze ta funkcia sa spusti pre kazdy text ktory je na tom platne 
			//spusta sa aj pre vsetky ostatne platna alebo je to separatne 
			//pre kazde platno? nie je to separatne cize ak kliknem na 
			//text tak sa to bude aplikovat na vsetky texty, cize tam treba dat switch
			//a unikatne id pre kazdy text alebo zaroven kontrolovat aj meno
			//parenta -> email2... a zahrnut to do podmienky
			/*if (e.target.name == "Text (7)")
			{
				Debug.Log("Clicked on " + e.target.name);
				Select();

			}else if (e.target.name == "Text")
			{
				Debug.Log("Clicked on " + e.target.name);
				Select();

			}else if (e.target.name == "Text (1)"){
				Debug.Log("Clicked on " + e.target.name);
				Select();
			}
			else if (e.target.name == "Text (2)")
			{
				Debug.Log("Clicked on " + e.target.name);
				Select();
			}
			else if (e.target.name == "Text (3)")
			{
				Debug.Log("Clicked on " + e.target.name);
				Select();
			}
			else if (e.target.name == "Text (5)")
			{
				Debug.Log("Clicked on " + e.target.name);
				Select();
			}*/


		/*}

		public void PointerInside(object sender, PointerEventArgs e)
		{
		}

		public void PointerOutside(object sender, PointerEventArgs e)
		{
		}
		/*** koniec natalky ***/


		//oznacenie textu - zobrazi sa pozadie
		public void Select() {
			Debug.Log("sme v selecte. Background: " + background);
			if (selected) {
				background.SetActive(false);
				selected = false;
				Debug.Log("pozadie ziadne");
			}
			else {
				background.SetActive(true);
				selected = true;
				Debug.Log("pozadie zvyraznene");

			}
		}
		
		//odstranenie vsetkych oznaceni emailu
		public void Clear() {
			selected = false;
			background.SetActive(false);
			backgroundCorrect.SetActive(false);
			backgroundIncorrect.SetActive(false);
			
		}
		
		//oznacenie nazeleno pri spravnej odpovedi
		public void HighlightCorrect() {
			background.SetActive(false);
			backgroundCorrect.SetActive(true);
			
		}
		
		//oznacenie nacerveno pri nespravnej odpovedi
		public void HighlightIncorrect() {
			background.SetActive(false);
			backgroundIncorrect.SetActive(true);
		}
		
	}
}
