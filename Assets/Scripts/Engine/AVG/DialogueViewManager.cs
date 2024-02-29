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
            gameObject.SetActive(true);
            textName = NameGO.gameObject.GetComponentInChildren<TMP_Text>();
            textDialogue = DialogueGO.gameObject.GetComponentInChildren<TMP_Text>();
        }

        private void OnDestroy()
        {
            Destroy(Instance);
        }
        
        public Button buttonStop;

        public CanvasGroup NameGO;
        public TMP_Text textName;
        public CanvasGroup DialogueGO;
        public TMP_Text textDialogue;

        private string textPassedIn;

        private IEnumerator TextJump(string _text = "")
        {
            textDialogue.text = "";
            AVGConsts.DialogueTextNotJumping = false;
            foreach (var c in _text)
            {
                if (AVGConsts.DialogueTextNotJumping == false)
                {
                    textDialogue.text += c;
                    yield return new WaitForSeconds(AVGConsts.DialogueTextJumpTime);
                }
            }
            AVGConsts.DialogueTextNotJumping = true;
        }

        public void StopJumping()
        {
            if (AVGConsts.DialogueTextNotJumping) return;
            StopCoroutine(TextJump());
            AVGConsts.DialogueTextNotJumping = true;
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
