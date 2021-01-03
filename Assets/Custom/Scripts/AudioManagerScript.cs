using UnityEngine;
using UnityEngine.Audio;

namespace Custom.Scripts {
    
    //ovladanie hlasitosti
    public class AudioManagerScript : MonoBehaviour {
    
        public float defaultVolume = 0.5f; //predvolena vyska hlasitosti
        public AudioMixer mixer; 

        //prevod z linearnej stupnice na decibely
        private float LinearToDecibel(float linear) {
            //podla vzorca: db = 20*log10(V2/V1)
            return (linear != 0) ? 20.0f * Mathf.Log10(linear) : -144.0f;
        }

        //nastavenie hlasitosti
        public void SetVolume() {
            float musicVolume = PlayerPrefs.GetFloat("music",defaultVolume);
            float effectsVolume = PlayerPrefs.GetFloat("effects",defaultVolume);
            mixer.SetFloat("musicVolume", LinearToDecibel(musicVolume));
            mixer.SetFloat("effectsVolume", LinearToDecibel(effectsVolume));
        }
    }
}
