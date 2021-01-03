using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts {
    public class FadeScript : MonoBehaviour {
        public GameObject fadeImageGameObject; 
        private Image fadeImage;
       
        // Start is called before the first frame update
        void Start() {
            fadeImage = fadeImageGameObject.GetComponent<Image>();
            FadeOut();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void FadeIn() {
            fadeImageGameObject.SetActive(true);
            fadeImage.canvasRenderer.SetAlpha(0.0f);
            fadeImage.CrossFadeAlpha(1.0f,1.2f,false);
            
        }

        private void HideImage() {
            fadeImageGameObject.SetActive(false);
        }

        public void FadeOut() {
            fadeImage.canvasRenderer.SetAlpha(1.0f);
            fadeImage.CrossFadeAlpha(0.0f,1f,false);
            Invoke(nameof(HideImage),1.05f);
        }
    }
}
