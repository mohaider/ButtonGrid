using System; 
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.tools.buttonGrid
{
    /// <summary>
    /// Any button that wants to be a part of the grid needs this as their component. This component calls UseCallback on the event of a button clicked with the set actions. 
    /// Actions need to be of type void and are updates(or assigned) with the UpdateCallback(Action ) method
    /// </summary>
    public class IterableButton : MonoBehaviour
    {
      
        private enum CallbackType
        {
            None, NoParam 
        }
        #region callbacks

        private Action _callback;
        /* private Action<object> _callbackWithOneParam;
         [Obsolete]
         private object _callbackOneData;
         [Obsolete]
         private Action<object, object> _callbackWithTwoParam;
         [Obsolete]
         private object[] _callbackTwoData = new object[2];
         [Obsolete]
         private Action<object, object, object> _callbackWithThreeParams;
         [Obsolete]
         private object[] _callbackThreeData = new object[3];
         [Obsolete]
         private Action<object, object, object, object> _callbackWithFourParams;
         [Obsolete]
         private object[] _callbackFourData = new object[4];*/
        #endregion

        [SerializeField]
        private int _rowIndex;
        [SerializeField]
        private int _columnIndex;

        private CallbackType _callbackTypeUsed = CallbackType.None;


        public Button Button { get; set; }


        /// <summary>
        /// Initializes the Button with its rowId, column Id and its button
        /// </summary>
        /// <param name="rowId"></param>
        /// <param name="columnId"></param>
        /// <param name="b"></param>
        public void Init(int rowId, int columnId, Button b)
        {
           /*
            RowIndex = rowId;
            ColumnIndex = columnId;*/
            Button = b;
            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(UseCallback);
            SetRowAndColumnIndex(rowId, columnId);
        }

        public void UpdateCallback(Action callback)
        {
            _callbackTypeUsed = CallbackType.NoParam;
            _callback = callback;
        }

        public Navigation Navigation
        {
            get { return Button.navigation; }
            set { Button.navigation = value; }
        }
  
        /// <summary>
        /// the index of the row of this button represented on the grid
        /// </summary>
        public int RowIndex
        {
            get { return _rowIndex; }
            set { _rowIndex = value; }
        }
        /// <summary>
        /// the index of the column of this button represented on the grid
        /// </summary>
        public int ColumnIndex
        {
            get { return _columnIndex; }
            set { _columnIndex = value; }
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((IterableButton)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (Button != null ? Button.GetHashCode() : 0);
            }
        }


        protected bool Equals(IterableButton obj)
        {
            return base.Equals(obj) && Equals(Button, obj.Button);
        }


        public static bool operator ==(IterableButton b1, IterableButton b2)
        {
            if (ReferenceEquals(b1, null))
            {
                if (ReferenceEquals(b2, null))
                {
                    return true;
                }
                return false;
            }
            else
            {
                return b1.Equals(b2);
            }
        }
        public static bool operator !=(IterableButton b1, IterableButton b2)
        {
            return !(b1 == b2);
        }
        /// <summary>
        /// Set a new row and column index
        /// </summary>
        /// <param name="newRowId"></param>
        /// <param name="newColumnId"></param>
        internal void SetRowAndColumnIndex(int newRowId, int newColumnId)
        {
            RowIndex = newRowId;
            ColumnIndex = newColumnId;
            Button.name = ("Button " + newRowId + ", " + newColumnId);
        }
        /// <summary>
        /// Instantiating and destroying objects is too much work. Simply deactivate this object
        /// </summary>
        internal void Deactivate()
        {
            gameObject.SetActive(false);
        }
        /// <summary>
        /// sets the but button to active
        /// </summary>
        internal void Activate()
        {
            gameObject.SetActive(true);
        }
        /// <summary>
        /// Method assigned on awake to the OnClick event of Button, calls the callback assigned
        /// </summary>
        public void UseCallback()
        {
            switch (_callbackTypeUsed)
            {
                case CallbackType.None:
                    Debug.Log("No callback Assigned. Not doing anything");
                    break;
                case CallbackType.NoParam:
                    _callback();
                    break; 
            }
        }
    }
}
