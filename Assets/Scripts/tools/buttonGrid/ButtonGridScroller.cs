
using System.Collections;
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
        [SerializeField]
        private ScrollRect _scrollRect;
        private RectTransform _parentRectTransform;
        private RectTransform _currentRectTransform; //the parent to all the buttons
        public float ScrollSpeed =1f;
        private bool _renderedFirstFrame=false;

        //these two values is to work around a severe unity limitation, where the GridLayoutGroup positions a GUI element at 0,0 then repositions a gui element
        //calling CheckIfOutofBoundsBottom, CheckIfButtonOutofBoundsTop would result in inconsistent results 
        private IterableButton _previousButton; 

        void Awake()
        {
            _grid = transform.parent.GetComponent<ButtonGrid>();
            _gridLayoutGroup = _grid.GridLayout;
            _scrollRect = transform.parent.GetComponent<ScrollRect>();
            _parentRectTransform = _scrollRect.GetComponent<RectTransform>();
            _currentRectTransform = GetComponent<RectTransform>();
        }
 

        void LateUpdate()
        {
            //check what the current selected game object is
            GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
            //get the iterable button
            if(currentSelectedGameObject != null)
            {
                IterableButton currentButton = currentSelectedGameObject.GetComponent<IterableButton>();
                //check if this button has changed since the last frame
                if (currentButton != null && currentButton == _previousButton && !_isRepositioningParent)
                {

                    if (ButtonGridTools.CheckIfButtonOutofBoundsTop(_gridLayoutGroup, _scrollRect, _parentRectTransform, currentButton))
                    {
                        _isRepositioningParent = true;
                        StartCoroutine(RepositionParent(currentButton.RectTransform.position)); 
                    }
                    else if (ButtonGridTools.CheckIfButtonOutBoundsBottom(_gridLayoutGroup, _scrollRect, _parentRectTransform, currentButton))
                    {
                        _isRepositioningParent = true;
                        StartCoroutine(RepositionParent(currentButton.RectTransform.position));
                        }

                }
                _previousButton = currentButton;
            }
            if (currentSelectedGameObject == null)
            {
                _previousButton = null;
            }
           
         
            //check if current button is visible
        }

        private bool _isRepositioningParent; //is the parent being repositioned?
        /// <summary>
        /// called when a button is out of bound
        /// </summary>
        private void RepositionParentRectTransform(Vector2 newPos)
        {
            
        }
        /// <summary>
        /// Repositions the parent rect transform according to the position of the button that is passed in
        /// </summary>
        /// <param name="buttonPos"></param>
        /// <returns></returns>
        private IEnumerator RepositionParent(Vector2 buttonPos)
        {
            float timeStartLerping = Time.time;
            Vector2 currentPos = _currentRectTransform.position ; 
            float buttonTopPos = buttonPos.y + (_gridLayoutGroup.cellSize.y/2);
            float parentRectHalfHeight = _parentRectTransform.rect.height/2f;
            float parentTopPos = parentRectHalfHeight + _parentRectTransform.position.y;
            float deltaParentAndButtonPos = parentTopPos - buttonTopPos;
            float newPos = currentPos.y + deltaParentAndButtonPos - _gridLayoutGroup.spacing.y;  
            float timeTakenDuringLerp = 1/(ScrollSpeed+0.01f); //how fast do we lerp towards the target pos?
            float distanceToMove = newPos - _currentRectTransform.position.y+0.01f;
            Debug.Log("distance to move " + distanceToMove);
            float startYPos = currentPos.y;
            while (true)
            { 
                float timeSinceStartLerp = Time.time - timeStartLerping ;//prevent div by zero 
                Debug.Log(timeTakenDuringLerp);
                float percentageCompleted = timeSinceStartLerp / timeTakenDuringLerp; 
                float newYPos = Mathf.Lerp(startYPos, newPos, percentageCompleted);
                currentPos.y = newYPos;
                _currentRectTransform.position = currentPos;
                if (percentageCompleted >= 1.0f)
                {
                    _isRepositioningParent = false;
                    yield break;
                }
                yield return null;
            }
        }
      
    }
   
    public static class ButtonGridTools
    {
        /// <summary>
        /// Checks if the button is out of bounds from the button, from the scrollRectsParent
        /// </summary>
        /// <param name="layoutGroup"></param>
        /// <param name="scrollRect"></param>
        /// <param name="scrollRectsParent"></param>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool CheckIfButtonOutBoundsBottom(GridLayoutGroup layoutGroup, ScrollRect scrollRect,
            RectTransform scrollRectsParent, IterableButton button)
        {
            float buttonHalfHeight = layoutGroup.cellSize.y / 2;
            float verticalButtonPadding = layoutGroup.padding.bottom;
            float lowerButtonBound = button.RectTransform.position.y - buttonHalfHeight - verticalButtonPadding - layoutGroup.spacing.y;
            float lowerBounds = scrollRect.transform.position.y - (scrollRectsParent.rect.height / 2);
            return lowerButtonBound < lowerBounds; 
        }

        public static bool CheckIfButtonOutofBoundsTop(GridLayoutGroup layoutGroup, ScrollRect scrollRect,
            RectTransform scrollRectsParent, IterableButton button)
        {
            //calculate the vertical bound of the button. Its height is set by the grid layout group
            float buttonHalfHeight = layoutGroup.cellSize.y / 2;
            float verticalButtonPadding = layoutGroup.padding.top;
            float upperButtonBounds = button.transform.position.y + buttonHalfHeight + verticalButtonPadding;
            float upperBounds = scrollRect.transform.position.y + (scrollRectsParent.rect.height / 2);
            return upperButtonBounds > upperBounds;
        }
    }
}
