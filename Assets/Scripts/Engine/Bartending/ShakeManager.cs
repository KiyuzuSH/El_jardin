using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ShakeManager : MonoBehaviour
    {
        public static ShakeManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                Instance = this;
            }
        }
        
        private void OnDestroy()
        {
            Destroy(Instance);
        }

        public Image shakeCup;
        public Slider progress;

        [Range(1,150)]
        public int expectedTimes = 10;
        private float timePerPress;
        
        private bool LeftPressing;
        private bool RightPressing;

        public Image fill;
        public GameObject btnPourOut;
        
        private void Start()
        {
            timePerPress = 1.0f / expectedTimes;
            btnPourOut.GetComponent<Button>().onClick.AddListener(ShakeComplete);
            btnPourOut.SetActive(false);
            shakeCup.sprite = Resources.Load<Sprite>("Sprites/Items/Shaker/shaker_close");
            
        }

        private void Update()
        {
            if (progress.value > 0.99f)
            {
                btnPourOut.SetActive(true);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    if (!LeftPressing)
                    {
                        LeftPressing = true;
                        progress.value += timePerPress;
                    }

                if (Input.GetKeyUp(KeyCode.LeftArrow))
                    LeftPressing = false;

                if (Input.GetKeyDown(KeyCode.RightArrow))
                    if (!RightPressing)
                    {
                        RightPressing = true;
                        progress.value += timePerPress;
                    }
            
                if (Input.GetKeyUp(KeyCode.RightArrow))
                    RightPressing = false;
            }
        }

        private void ShakeComplete()
        {
            // TODO: 提交酒水数据
            shakeCup.sprite = Resources.Load<Sprite>("Sprites/Items/Shaker/shaker_open");
            shakeCup.SetNativeSize();
            var x = 2 * shakeCup.rectTransform.rect.size;
            shakeCup.rectTransform.sizeDelta = x;
            Invoke(nameof(TmpChangePic), 3);
        }

        private void TmpChangePic()
        {
            // TODO: A New Wine Pic
            // shakeCup.sprite = Resources.Load<Sprite>("Sprites/Items/Shaker/shaker_open");
            // TODO: Move Next
        }
    }
}