using UnityEngine;

namespace Custom.Scripts.L2 {
	//efekt vodnej hladiny
	public class WaterEffectScript : MonoBehaviour {

		public Projector projector; 
		public float fps = 25.0f;      //frekvencia
		public Texture2D[] pictures;   //textury v podobe rastrovych obrazkov 
		
		private int id = 0;
		void Start(){
			ChangeTexture(); //nastavenie prvotnej textury
			
			//opakovane sa vola funkcia "ChangeTexture"
			InvokeRepeating ("ChangeTexture", 1 / fps, 1 / fps);
		}
 
		//zmena textury
		void ChangeTexture() {
			
			//prechadzanie pola od zaciatku
			if (id == pictures.Length)
				id = 0;
			
			//nastavenie textury
			projector.material.SetTexture("_ShadowTex", pictures[id]);
			id++;
		}
	}
}
