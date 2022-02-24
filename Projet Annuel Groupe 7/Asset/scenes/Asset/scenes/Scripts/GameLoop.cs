using UnityEngine;

namespace Scenes.Scripts
{
    public class GameLoop : MonoBehaviour
    {
        public Player player1;
        public Player player2;

        private int currentCircuit = 0;

        public Circuit[] circuits;
    
        // Start is called before the first frame update
        void Start()
        {
            foreach (var cir in circuits)
            {
                cir.setGl(this);
            }
        }

        public void courseFinished()
        {
            currentCircuit++;
            nextCircuit();
        }

        void nextCircuit()
        {
            foreach (var cir in circuits)
            {
                cir.enabled = false;
            }
            circuits[currentCircuit].enabled = true;
        }
    }
}
