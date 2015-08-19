

using UnityEngine; 

namespace Assets.Scripts.tools.buttonGrid
{
    /// <summary>
    /// This class refocuses the button in the event the event system no longer has a Iterable button selected
    /// </summary>
    public class ButtonRefocuser: MonoBehaviour
    {
        private ButtonGrid _grid;

        void Awake()
        {
            _grid = GetComponent<ButtonGrid>();
            
        }

        void Update()
        {
            //Continuously poll the event system for 
        }
    }
}
