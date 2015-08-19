 

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.tools.buttonGrid
{
   public  class ButtonRow  
   {
       private List<IterableButton> _row;
       public List<IterableButton> Row
       {
           get
           {
               if (_row == null)
               {
                   _row = new List<IterableButton>(1);
               }
               return _row; 
           }
       }

       public IterableButton RemoveButton(int index)
       {
           Row[index].Deactivate();
           IterableButton returner = Row[index];
           Row[index] = null;
           return returner;
       }
       public int RowId { get; set; }
       public int Count { get { return Row.Count; } }

       public void Add(IterableButton newIterableButton)
       {
           Row.Add(newIterableButton);
       }

       public ButtonRow(int startCapacity)
       {
           _row = new List<IterableButton>(startCapacity);
       }
       public ButtonRow(Button button, int buttonRow, int buttonColumn, int rowId)
       {
           IterableButton newButtonStructure = new IterableButton()
           {
               Button = button,
               ColumnIndex = buttonColumn,
               RowIndex = buttonRow
           };
           Row.Add(newButtonStructure);
           RowId = rowId;
       }

       public ButtonRow(IterableButton buttonStructure, int rowId)
       {
           Row.Add(buttonStructure);
           RowId = rowId;
       }

       public void Remove(IterableButton b)
       {
           if (Count > 0)
           {
               
           }
       }
   }
}
