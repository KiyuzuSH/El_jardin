using System;
using KiyuzuDev.ITGWDO.StoryData;
using KiyuzuDev.ITGWDO.View;
using UnityEngine;
using UnityEngine.UI;

namespace KiyuzuDev.ITGWDO.Core
{
    public class DialogueManager : MonoBehaviour
    {
        #region Singleton

        public static DialogueManager Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
        }
        
        #endregion

        [SerializeField] private Button continueButton;
        
        public void ProcessLine()
        {
            if (ScriptManager.PresentLineID > 43 && !AVGView.Instance.ismindAva())
                AVGView.Instance.SetAvailable();
            ProcessEvent(EventPlace.Fore);
            ProcessEvent(EventPlace.Main);
            switch (ScriptManager.PresentLine.DialogueLineType)
            {
                case EnumDialogueLineType.TitleLine:
                    AVGView.Instance.UpdateText("","");
                    AVGView.Instance.UpdateAnnouncementTitle(ScriptManager.PresentLine.content);
                    return;
                case EnumDialogueLineType.NarrationLine:
                    AVGView.Instance.UpdateText(ScriptManager.PresentLine.personName,ScriptManager.PresentLine.content);
                    break;
                case EnumDialogueLineType.MindLine:
                    AVGView.Instance.UpdateMind(ScriptManager.PresentLine.content);
                    break;
                case EnumDialogueLineType.ChooseLine:
                    continueButton.interactable = false;
                    AVGView.Instance.GenerateChoices();
                    return;
                case EnumDialogueLineType.MindChooseLine:
                    continueButton.interactable = false;
                    AVGView.Instance.GenerateMindChoices();
                    return;
                case EnumDialogueLineType.ControlLine:
                    AVGView.Instance.UpdateText("","");
                    Debug.Log("This is a control Line. ");
                    return;
                case EnumDialogueLineType.GameLine:
                    GlobalDataManager.Instance.NextLineID = ScriptManager.PresentLine.toLine;
                    LegacySceneLoader.Instance.LoadScene(2);
                    break;
            }
        }

        public void MoveNextLine()
        {
            if (ScriptManager.PresentLineID == 1) ProcessLine();
            ProcessEvent(EventPlace.After);
            ScriptManager.Instance.SetLineById(ScriptManager.PresentLine.toLine);
        }

        private void ProcessEvent(EventPlace evtPlc)
        {
            string[] args = { };
            EnumDialogueEventType type = EnumDialogueEventType.None;
            switch (evtPlc)
            {
                case EventPlace.Fore:
                    args = ScriptManager.PresentLine.eventFore.args;
                    type = ScriptManager.PresentLine.eventFore.eventType;
                    break;
                case EventPlace.Main:
                    args = ScriptManager.PresentLine.eventMain.args;
                    type = ScriptManager.PresentLine.eventMain.eventType;
                    break;
                case EventPlace.After:
                    args = ScriptManager.PresentLine.eventAfter.args;
                    type = ScriptManager.PresentLine.eventAfter.eventType;
                    break;
            }
			switch (type)
            {
                case EnumDialogueEventType.None:
                    return;
                case EnumDialogueEventType.Wait:
                    // TODO: not used, write if used
                    break;
                case EnumDialogueEventType.Style:
                    Enum.TryParse(args[0],out WorldStyle targetStyle);
                    GlobalDataManager.Instance.SetWorldStyle(targetStyle);
                    break;
                case EnumDialogueEventType.CGLoad:
                    switch (args[0].ToLower())
                    {
                        case "full":
                            AVGView.Instance.FullCGOn(
                                Resources.Load<Sprite>(args[1])
                                );
                            break;
                        case "part":
                            AVGView.Instance.PartCGOn(
                                Resources.Load<Sprite>(args[1])
                            );
                            break;
                    }
                    break;
                case EnumDialogueEventType.CGUnload:
                    switch (args[0].ToLower())
                    {
                        case "full":
                            AVGView.Instance.FullCGOff();
                            break;
                        case "part":
                            AVGView.Instance.PartCGOff();
                            break;
                    }
                    break;
                case EnumDialogueEventType.BlackOn:
                {
                    float duration = args.Length >= 1 ? float.Parse(args[0]) : 2;
                    GameManager.Instance.FadeBlackScreenOpacity(1, duration);
                    break;
                }
                case EnumDialogueEventType.BlackOff: 
                {
                    float duration = args.Length >= 1 ? float.Parse(args[0]) : 2;
                    GameManager.Instance.FadeBlackScreenOpacity(0, duration);
                    break;
                }
				case EnumDialogueEventType.HumanLoad:
                    AVGBackgroundView.Instance.HumanLoad(args[0],args[1]);
                    break;
                case EnumDialogueEventType.HumanPosChange:
                    // TODO: not used, write if used
                    break;
                case EnumDialogueEventType.HumanChange:
                    AVGBackgroundView.Instance.HumanChange(args[0],args[1]);
                    break;
                case EnumDialogueEventType.HumanUnload:
                    AVGBackgroundView.Instance.HumanUnload(args[0]);
                    break;
                case EnumDialogueEventType.HumanAllClear:
                    AVGBackgroundView.Instance.HumanAllClear();
                    break;
            }
        }
        
        public void SetLineOfDialogue(int id)
        {
            ScriptManager.Instance.SetLineById(id);
            ProcessLine();
        }
    }
}
