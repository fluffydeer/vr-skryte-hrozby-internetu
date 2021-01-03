using UnityEngine;

namespace Custom.Scripts.EndScene {
    public class EndSceneManagerScript : MonoBehaviour {
        private AudioManagerScript audioManagerScript; //script na manipulaciu so zvukom
        void Start() {
            //nastavenie hlasitosti
            audioManagerScript = gameObject.GetComponent<AudioManagerScript>();
            audioManagerScript.SetVolume();
        }

    }
}
