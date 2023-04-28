using UnityEngine;
using UnityEngine.SceneManagement;

//toto sa tusim nikde nepouziva
namespace Custom.Scripts {
    //prepnutie na inu scenu pri kolizii
    public class OnTriggerSwitchScript : MonoBehaviour {

        public string destinationSceneName; //scena, na ktoru sa prepne
        private string destinationSceneName2 = "PhishingScene"; //scena, na ktoru sa prepne
        private string destinationSceneName3 = "PasswordScene"; //scena, na ktoru sa prepne
        private string destinationSceneName4 = "EndScene"; //scena, na ktoru sa prepne
        private string destinationSceneName5 = "MenuScene"; //scena, na ktoru sa prepne

        private static int sceneCounter = 0;

        private FadeScript fadeScript; 
        
        void Start() {
            fadeScript = gameObject.GetComponent<FadeScript>();
            fadeScript.FadeOut();
        }
        
        private void LoadSceneInvoke() {
            //toto by sa dalo refaktovat do 1 riadku ci?
            if(sceneCounter == 0) {
                sceneCounter++;
                SceneManager.LoadScene(destinationSceneName);
            }else if(sceneCounter == 1) {
                sceneCounter++;
                SceneManager.LoadScene(destinationSceneName2);
            } else if (sceneCounter == 2) {
                sceneCounter++;
                SceneManager.LoadScene(destinationSceneName3);
            } else if (sceneCounter == 3) {
                sceneCounter++;
                SceneManager.LoadScene(destinationSceneName4);
            } else if (sceneCounter == 4) {
                sceneCounter=0;
                SceneManager.LoadScene(destinationSceneName5);
            }
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
