using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KiyuzuDev.ITGWDO.TooltipView
{
    public class Tooltip : MonoBehaviour
    {
        public TMP_Text contentField;
        
        private RectTransform rectTransform;
        private LayoutElement layoutElement;

        public int lineWrapLimit;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.pivot = new Vector2(0.05f, 0.05f);
            layoutElement = GetComponent<LayoutElement>();
        }

        private void Update()
        {
            if (Application.isEditor)
            {
                int contentLength = contentField.text.Length;
                layoutElement.enabled = contentLength > lineWrapLimit;
            }

            rectTransform.anchoredPosition = Vector2.zero;
        }

        public void SetText(string _content)
        {
            contentField.text = _content ?? "";
        }

        
        
    }
}
