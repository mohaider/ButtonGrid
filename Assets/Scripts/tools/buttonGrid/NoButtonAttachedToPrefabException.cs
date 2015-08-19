using System;
 

namespace Assets.Scripts.tools.buttonGrid
{
    public class NoButtonAttachedToPrefabException: Exception 
    {
        public NoButtonAttachedToPrefabException() : base("Button component not attached to prefab, ensure that you have one attached") { }
        public NoButtonAttachedToPrefabException(string msg) : base(msg) { }
        public NoButtonAttachedToPrefabException(string msg,Exception innerException) : base(msg,innerException) { }


    }
}
