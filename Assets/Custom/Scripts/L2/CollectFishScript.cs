using UnityEngine;

namespace Custom.Scripts.L2 {
	
	//zbieranie ryb
	public class CollectFishScript : MonoBehaviour {
		private L2ManagerScript l2ManagerScript; //manager 2. urovne
		
		//inicializacia
		void Start () {
			l2ManagerScript = GameObject.Find("Player").GetComponent<L2ManagerScript>();
		}

		void Update () {
			transform.Rotate(0,90*Time.deltaTime,0); //pomale otacanie ryby
		}

		public void Collect() {
			l2ManagerScript.points++;	//zvysenie stavu
			Destroy(gameObject);		//znicenie objektu
		}
	}
}
