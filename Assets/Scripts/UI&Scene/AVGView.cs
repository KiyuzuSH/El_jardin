using UnityEngine;
using UnityEngine.UIElements;

namespace KiyuzuDev.ITGWDO.UI
{
    public class AVGView : MonoBehaviour
    {
        private VisualElement rootVE;

        private void Awake()
        {
            rootVE = GetComponent<UIDocument>().rootVisualElement;
            
        }
        
    }
}
