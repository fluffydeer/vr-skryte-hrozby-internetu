using System;
using UnityEngine;
//using Valve.VR.Extras;

namespace Custom.Scripts.L2 {

	//interakcie hraca s emailom
	public class EmailInteractionScript : MonoBehaviour {
		
		private Transform highlightedText;	//oznaceny text
		public bool selected = false;		//hrac oznacil text
		public bool isCorrect = false;		//text treba oznacit => true, netreba oznacit => false
		
		//farebne pozadia
		public GameObject backgroundCorrect; 
		public GameObject backgroundIncorrect;
		public GameObject background;

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


        //oznacenie textu - zobrazi sa pozadie
        public void Select() {
			if (selected) {
				background.SetActive(false);
				selected = false;
			}
			else {
				background.SetActive(true);
				selected = true;
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
