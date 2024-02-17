using UnityEngine;

namespace Game
{
    public class Utils : MonoBehaviour
    {
        private static Transform _canvas;
        public static Transform Canvas
        {
            get
            {
                if (_canvas == null)
                    _canvas = GameObject.Find("Canvas").transform;
                return _canvas;
            }
        }
    }
}
