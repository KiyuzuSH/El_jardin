using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MindShowManager : MonoBehaviour
    {
        public static MindShowManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                Instance = this;
            }
        }

        private void Start()
        {
            // gameObject.SetActive(false);
            AtRight = true;
            txtlines = GetComponentInChildren<TMP_Text>();
            textBox = GetComponent<ScrollRect>();
            scrollbar = GetComponent<ScrollRect>().verticalScrollbar;
            txtBoxHeight = txtBoxTransform.GetComponent<RectTransform>().rect.height;
            minimalize.onClick.AddListener(OnMinClick);
        }

        private void FixedUpdate()
        {
            
        }

        private void OnDestroy()
        { 
            minimalize.onClick.RemoveListener(OnMinClick);
            Destroy(Instance);
        }

        private string textPassedIn;
        
        private TMP_Text txtlines;
        
        public void ShowLines(string _text)
        {
            textPassedIn = _text.Replace("\\n", "\n");
            if(AtRight) MoveLeft();
            StartCoroutine(TextJump(textPassedIn));
        }
        
        private IEnumerator TextJump(string _text = "")
        {
            txtlines.text = "";
            AVGConsts.MindTextNotJumping = false;
            foreach (var c in _text)
            {
                if (AVGConsts.MindTextNotJumping == false)
                {
                    txtlines.text += c;
                    yield return new WaitForSeconds(AVGConsts.MindTextJumpTime);
                }
            }
            AVGConsts.MindTextNotJumping = true;
        }
        
        public void StopJumping()
        {
            if (AVGConsts.MindTextNotJumping) return;
            StopCoroutine(TextJump());
            AVGConsts.MindTextNotJumping = true;
            txtlines.text = textPassedIn;
        }

        public Button minimalize;
        public CanvasGroup btnMin;
        
        public Transform txtBoxTransform;
        private float txtBoxHeight;

        private void OnMinClick()
        {
            if(!AtRight) MoveRight();
        }
        
        private bool AtRight { get; set; }
        
        //TODO: Animation
        private void MoveRight()
        {
            gameObject.transform.localPosition += new Vector3(300, 0, 0);
            AtRight = true;
        }
        
        // Color.Lerp(txtBox.GetComponent<Image>().color, Color.clear, Time.deltaTime);

        private void MoveLeft()
        {
            gameObject.transform.localPosition += new Vector3(-300, 0, 0);
            AtRight = false;
        }
        
        //TODO: Resize-able Scroll Rect
        private ScrollRect textBox;
        private Scrollbar scrollbar;
        

    }
}
