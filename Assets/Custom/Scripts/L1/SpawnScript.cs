using UnityEngine;

//umiestnenie malveru na miesto, odkial ho hrac vzal - pri nespravnej odpovedi na otazku
namespace Custom.Scripts.L1 {
    public class SpawnScript : MonoBehaviour {
        public Transform spawnPoint; //bod, kde sa objekt objavi
        public GameObject objectToSpawn;  //objekt
    
        public void Spawn() {
            //vytvori sa nova instancia objektu, umiestni sa na poziciu objektu "spawnPoint"
            Instantiate(objectToSpawn,spawnPoint.position,spawnPoint.rotation);
        }
    }
}
