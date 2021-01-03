using UnityEngine;
using UnityEngine.SceneManagement;

namespace Custom.Scripts {
    //prepnutie na inu scenu pri kolizii
    public class OnTriggerSwitchScript : MonoBehaviour {

        public string destinationSceneName; //scena, na ktoru sa prepne
        private FadeScript fadeScript; 
        
        void Start() {
            fadeScript = gameObject.GetComponent<FadeScript>();
            fadeScript.FadeOut();
        }
        
        private void LoadSceneInvoke() {
            SceneManager.LoadScene(destinationSceneName); 
        }

        private void LoadScene() {
            fadeScript.FadeIn();
            Invoke(nameof(LoadSceneInvoke),2.1f);
        }
        
        //kolizia portalu s hracom
        private void OnTriggerEnter(Collider other) {
            
            if (other.CompareTag("Player")) {
                LoadScene();
            }
        }

    }
}
