using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DialogueViewManager : MonoBehaviour
    {
        public static DialogueViewManager Instance { get; private set; }

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
            TextJumpFinished = true;
            gameObject.SetActive(true);
            textName = NameGO.gameObject.GetComponentInChildren<TMP_Text>();
            textDialogue = DialogueGO.gameObject.GetComponentInChildren<TMP_Text>();
            if (timeTextJump < 0f)
                timeTextJump = 0.1f;
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }

        private bool _textJumpFinished;
        public bool TextJumpFinished
        {
            get => _textJumpFinished;
            set => _textJumpFinished = value;
        }

        [Range(0, 1)] public float timeTextJump = 0.075f;
        public Button buttonStop;

        public CanvasGroup NameGO;
        public TMP_Text textName;
        public CanvasGroup DialogueGO;
        public TMP_Text textDialogue;

        private string textPassedIn;

        private IEnumerator TextJump(string _text = "")
        {
            textDialogue.text = "";
            TextJumpFinished = false;
            foreach (var c in _text)
            {
                if (TextJumpFinished == false)
                {
                    textDialogue.text += c;
                    yield return new WaitForSeconds(timeTextJump);
                }
            }
            TextJumpFinished = true;
        }

        public void StopJumping()
        {
            if (TextJumpFinished) return;
            StopCoroutine(TextJump());
            TextJumpFinished = true;
            textDialogue.text = textPassedIn;
        }

        public void UpdateText(string _name, string _text)
        {
            textPassedIn = _text.Replace("\\n", "\n");
            StartCoroutine(TextJump(textPassedIn));
            if (_name == "")
                NameGO.alpha = 0;
            else
            {
                NameGO.alpha = 1;
                textName.text = _name;
            }
        }
    }
}
