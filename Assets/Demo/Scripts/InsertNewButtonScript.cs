using UnityEngine;
using Assets.Scripts.tools.buttonGrid;

public class InsertNewButtonScript : MonoBehaviour
{

    public ButtonGrid Grid;
    public int NumberOfButtons;

    void Start()
    {
         Grid.InsertMultipleButtons(NumberOfButtons);
    }
    public void InsertNewButton()
    {
        
         Grid.InsertNewButton();
    }
}
