
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.tools.buttonGrid
{
    /// <summary>
    /// This class scrolls(repositions) the grid according to what button is selected
    /// </summary>
    public class ButtonGridScroller : MonoBehaviour
    {
        private GridLayoutGroup _gridLayoutGroup;
        private ButtonGrid _grid;
        private ScrollRect _scrollRect;

        void Awake()
        {
            _grid = GetComponent<ButtonGrid>();
            _gridLayoutGroup = _grid.GridLayout;
            _scrollRect = transform.parent.GetComponent<ScrollRect>();

        }

        void Update()
        {
            //check what the current selected game object is
            GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
            //get the iterable button
            IterableButton currentButton = currentSelectedGameObject.GetComponent<IterableButton>();

            if (currentButton != null)
            {
                if (CheckIfOutofBoundsTop(currentButton))
                {
                    
                }
                else if (CheckIfOutofBoundsBottom(currentButton))
                {
                    
                }
            }
            //check if current button is visible

        }
        /// <summary>
        /// Is the button visible on the grid? 
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool ButtonIsVisible(Button button)
        {

            return true;
        }
        /// <summary>
        /// checks if the button is outside the top bounds of the parent scroll rect
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool CheckIfOutofBoundsTop(IterableButton button)
        {
            //calculate the vertical bound of the button. Its height is set by the grid layout group
            float buttonHalfHeight = _gridLayoutGroup.cellSize.y/2;
            float verticalButtonPadding = _gridLayoutGroup.padding.top;
            float upperBounds = button.transform.position.y + buttonHalfHeight + verticalButtonPadding;

            return upperBounds > _scrollRect.transform.position.y; 
        }
        /// <summary>
        /// checks if the button is outside the bottom bounds of the parent scroll rect
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool CheckIfOutofBoundsBottom(IterableButton button)
        {
            //calculate the vertical bound of the button. Its height is set by the grid layout group
            float buttonHalfHeight = _gridLayoutGroup.cellSize.y / 2;
            float verticalButtonPadding = _gridLayoutGroup.padding.bottom;
            float lowerBounds = button.transform.position.y - buttonHalfHeight - verticalButtonPadding;

            return lowerBounds > _scrollRect.transform.position.y; 
        }
    }
}
