using UnityEngine;


namespace Custom.Scripts.Portal {
    
    //automaticky pohyb hraca
    public class PortalManagerScript : MonoBehaviour {

        public float speed = 0.2F; //rychlost
        private AudioManagerScript audioManagerScript; //script na manipulaciu so zvukom
        
        //inicializacia
        private void Start () {
            //nastavenie hlasitosti
            audioManagerScript = gameObject.GetComponent<AudioManagerScript>();
            audioManagerScript.SetVolume();
        }
        
        void Update () {
            //pohyb hraca
            GameObject player = GameObject.Find("Player");
            if (player) {
                player.transform.position -= Vector3.forward * speed;
            }
        }
    }
}
