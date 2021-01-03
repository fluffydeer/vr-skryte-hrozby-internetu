using UnityEngine;

//prepinanie zobrazenia informacii pri uchopenni malveru
namespace Custom.Scripts.L1 {
    public class TriggerDescriptionScript : MonoBehaviour {
        public GameObject infoPanels; //panely s instrukciami

        //zobrazenie informacii
        public void ShowPanels() {
            infoPanels.SetActive(true);
        }

        //zmiznutie informacii
        public void HidePanels() {
            infoPanels.SetActive(false);
        }

    }
}
