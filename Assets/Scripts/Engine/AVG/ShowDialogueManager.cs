using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ShowDialogueManager : MonoBehaviour
    {
        public static ShowDialogueManager Instance { get; private set; }

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
            SetJumpState(true);
            gameObject.SetActive(true);
            buttonStop.onClick.AddListener(OnStopJumping);
            textName = NameGO.gameObject.GetComponentInChildren<TMP_Text>();
            textDialogue = DialogueGO.gameObject.GetComponentInChildren<TMP_Text>();
        }

        private void OnDestroy()
        {
            buttonStop.onClick.RemoveAllListeners();
            Destroy(Instance);
        }

        private bool _textJumpFinished;
        public bool TextJumpFinished
        {
            get => _textJumpFinished;
            set => _textJumpFinished = value;
        }

        [Range(0, 1)] public float timeTextJump = 0.025f;
        public Button buttonStop;

        public CanvasGroup NameGO;
        public TMP_Text textName;
        public CanvasGroup DialogueGO;
        public TMP_Text textDialogue;

        private string textPassedIn;

        private void SetJumpState(bool _state)
        {
            TextJumpFinished = _state;
            DialogueManager.Instance.SetJumpState(_state);
        }

        private IEnumerator TextJump(string _text = "")
        {
            textDialogue.text = "";
            SetJumpState(false);
            foreach (var c in _text)
            {
                if (TextJumpFinished == false)
                {
                    textDialogue.text += c;
                    if (timeTextJump < 0f)
                    {
                        timeTextJump = 0.1f;
                    }

                    yield return new WaitForSeconds(timeTextJump);
                }
            }

            SetJumpState(true);
        }

        private void OnStopJumping()
        {
            if (!TextJumpFinished)
            {
                StopCoroutine(TextJump());
                textDialogue.text = textPassedIn;
                SetJumpState(true);
            }
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
