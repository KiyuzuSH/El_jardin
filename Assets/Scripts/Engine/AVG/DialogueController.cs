using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Game
{
    public class DialogueController : MonoBehaviour
    {
        public DialogueData dialogueEmpty;
        public DialogueData dialogueFinish;

        private Stack<SentenceLine> dialogueEmptyStack = new Stack<SentenceLine>();
        private Stack<SentenceLine> dialogueFinishStack = new Stack<SentenceLine>();

        public bool isTalking;

        private void Awake()
        {
            FillDialogueStack();
        }

        public void FillDialogueStack()
        {
            for (int i = dialogueEmpty.dataList.Count - 1; i >= 0; i--)
            {
                dialogueEmptyStack.Push(dialogueEmpty.dataList[i]);
            }
            for (int i = dialogueFinish.dataList.Count - 1; i >= 0; i--)
            {
                dialogueFinishStack.Push(dialogueFinish.dataList[i]);
            }
        }

        public void ShowDialogueEmpty()
        {
            if (!isTalking)
                StartCoroutine(DialogueRoutine(dialogueEmptyStack));
        }

        public void ShowDialogueFinish()
        {
            if (!isTalking)
                StartCoroutine(DialogueRoutine(dialogueFinishStack));
        }

        private IEnumerator DialogueRoutine(Stack<SentenceLine> data)
        {
            isTalking = true;
            Debug.Log("Dialogue Routine ON");
            // if (data.TryPop(out string result))
            {
                Debug.Log("Start Dialogue-ing");
                // EventHandler.CallShowDialogueEvent(result);
                yield return null;
                // EventHandler.CallGameStateChangedEvent(GameState.Pause);// Pause The Game
            }
            // else
            {
                // EventHandler.CallShowDialogueEvent(string.Empty);
                FillDialogueStack();
                // EventHandler.CallGameStateChangedEvent(GameState.GamePlay);// Pause The Game
            }

            isTalking = false;
        }
    }
}