using UnityEngine;
using UnityEngine.EventSystems;

namespace KiyuzuDev.ITGWDO.TooltipView
{
    public class TooltipTrigger : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        public string content;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipManager.Show(content);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Hide();
        }
    }
}
