using KiyuzuDev.ITGWDO.Core;
using UnityEngine;
using UnityEngine.UI;

namespace KiyuzuDev.ITGWDO.Bartending
{
    public class ShakeManager : MonoBehaviour
    {
        #region Singleton

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

        #endregion

        public Image shakeCup;
        public Slider progress;

        [Range(1,150)]
        public int expectedTimes = 10;
        private float durationTimePerPress;
        
        private bool LeftPressing;
        private bool RightPressing;

        public Image fill;
        public GameObject btnPourOut;
        public GameObject box;
        
        private void Start()
        {
            durationTimePerPress = 1.0f / expectedTimes;
            btnPourOut.GetComponent<Button>().onClick.AddListener(ShakeComplete);
            btnPourOut.SetActive(false);
            box.SetActive(false);
            shakeCup.sprite = Resources.Load<Sprite>("Sprites/Items/Shaker/shaker_close");
            LeftPressing = false;
            RightPressing = false;
        }

        private void Update()
        {
            if (progress.value > 0.99f)
            {
                btnPourOut.SetActive(true);
                fill.color = Color.green;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                if (!LeftPressing)
                {
                    LeftPressing = true;
                    shakeCup.rectTransform.rotation = Quaternion.Euler(0,0,15);
                    progress.value += durationTimePerPress;
                }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                shakeCup.rectTransform.rotation = Quaternion.Euler(0,0,0);
                LeftPressing = false;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
                if (!RightPressing)
                {
                    RightPressing = true;
                    shakeCup.rectTransform.rotation = Quaternion.Euler(0,0,-15);
                    progress.value += durationTimePerPress;
                }
        
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                shakeCup.rectTransform.rotation = Quaternion.Euler(0,0,0);
                RightPressing = false;
            }
        }

        private void ShakeComplete()
        {
            btnPourOut.GetComponent<Button>().interactable = false;
            // TODO: 提交酒水数据
            shakeCup.sprite = Resources.Load<Sprite>("Sprites/Items/Shaker/shaker_open");
            shakeCup.SetNativeSize();
            var x = 2 * shakeCup.rectTransform.rect.size;
            shakeCup.rectTransform.sizeDelta = x;
            Invoke(nameof(TmpChangePic), 1);
        }

        private void TmpChangePic()
        {
            btnPourOut.SetActive(false);
            shakeCup.sprite = Resources.Load<Sprite>("Sprites/Wine/Risei");
            box.SetActive(true);
            shakeCup.SetNativeSize();
            Invoke(nameof(MoveNext),2);
        }

        private void MoveNext()
        {
            // TODO: wine DATA
            LegacySceneLoader.Instance.LoadScene(1);
        }
    }
}