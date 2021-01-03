using UnityEngine;
using UnityEngine.SceneManagement;

namespace Custom.Scripts {
    //prepnutie na inu scenu pri kliknuti na tlacidlo
    public class OnButtonSwitchScript : MonoBehaviour {
        private FadeScript fadeScript;
        public string destinationSceneName; //scena, na ktoru sa prepne
        void Start() {
            fadeScript = gameObject.GetComponent<FadeScript>();
        }
        
        private void LoadSceneInvoke() {
            SceneManager.LoadScene(destinationSceneName); 
        }
      
        
        public void LoadScene() {
            fadeScript.FadeIn();
            Invoke(nameof(LoadSceneInvoke),1.5f);
        }
        
        
    }
}
