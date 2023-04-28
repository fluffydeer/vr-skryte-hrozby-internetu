using UnityEngine;

namespace Custom.Scripts.EndScene
{
    public class EndSceneManagerScript : MonoBehaviour
    {
        private AudioManagerScript audioManagerScript; //script na manipulaciu so zvukom
        void Start()
        {
            //nastavenie hlasitosti
            audioManagerScript = gameObject.GetComponent<AudioManagerScript>();
            audioManagerScript.SetVolume();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
            {
                ExitGame();
            }
        }

        //ukoncenie hry
        public void ExitGame()
        {
#if UNITY_EDITOR
			    UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}

