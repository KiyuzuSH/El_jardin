using System;
using KiyuzuDev.ITGWDO.StoryData;
using KiyuzuDev.ITGWDO.View;
using UnityEngine;

namespace KiyuzuDev.ITGWDO.Core
{
    public class DialogueManager : MonoBehaviour
    {
        #region Singleton

        public static DialogueManager Instance { get; private set; }
        
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
        
        public static DialogueLine PresentLine { get; private set; }
        public static int PresentLineID { get; set; }

        private void Start()
        {
            LoadLineById(1001);
            ProcessLine();
        }

        public void LoadLineById(int id)
        {
            PresentLine = ScriptManager.Instance.LoadSpecificLine(id);
        }
        
        public void ProcessLine()
        {
            PresentLineID = PresentLine.lineId;
            if (PresentLineID == 1001) AVGBackgroundView.Instance.SetBlack();
            if (PresentLineID == 1003) AVGBackgroundView.Instance.SetVisible();
            if (PresentLineID > 1043 && !AVGView.Instance.ismindAva())
                AVGView.Instance.SetAvailable();
            ProcessEvent(EventPlace.Fore);
            ProcessEvent(EventPlace.Main);
            switch (PresentLine.DialogueLineType)
            {
                case EnumDialogueLineType.TitleLine:
                    AVGView.Instance.UpdateText("","");
                    AVGView.Instance.UpdateAnnouncementTitle(PresentLine.content);
                    return;
                case EnumDialogueLineType.NarrationLine:
                    AVGView.Instance.UpdateText(PresentLine.personName,PresentLine.content);
                    break;
                case EnumDialogueLineType.MindLine:
                    AVGView.Instance.UpdateMind(PresentLine.content);
                    break;
                case EnumDialogueLineType.ChooseLine:
                    AVGView.Instance.GenerateChoices();
                    return;
                case EnumDialogueLineType.MindChooseLine:
                    AVGView.Instance.GenerateMindChoices();
                    return;
                case EnumDialogueLineType.ControlLine:
                    AVGView.Instance.UpdateText("","");
                    Debug.Log("This is a control Line. ");
                    return;
                case EnumDialogueLineType.GameLine:
                    // TODO: Turn to Game Scene
                    break;
            }
        }

        public void MoveNextLine()
        {
            ProcessEvent(EventPlace.After);
            LoadLineById(PresentLine.toLine);
        }

        private void ProcessEvent(EventPlace evtPlc)
        {
            string[] args = { };
            EnumDialogueEventType type = EnumDialogueEventType.None;
            switch (evtPlc)
            {
                case EventPlace.Fore:
                    args = PresentLine.eventFore.args;
                    type = PresentLine.eventFore.eventType;
                    break;
                case EventPlace.Main:
                    args = PresentLine.eventMain.args;
                    type = PresentLine.eventMain.eventType;
                    break;
                case EventPlace.After:
                    args = PresentLine.eventAfter.args;
                    type = PresentLine.eventAfter.eventType;
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
                            AVGBackgroundView.Instance.FullCGOn(
                                Resources.Load<Sprite>(args[1])
                                );
                            break;
                        case "part":
                            AVGBackgroundView.Instance.PartCGOn(
                                Resources.Load<Sprite>(args[1])
                            );
                            break;
                    }
                    break;
                case EnumDialogueEventType.CGUnLoad:
                    switch (args[0].ToLower())
                    {
                        case "full":
                            AVGBackgroundView.Instance.FullCGOff();
                            break;
                        case "part":
                            AVGBackgroundView.Instance.PartCGOff();
                            break;
                        case "all":
                            AVGBackgroundView.Instance.FullCGOff();
                            AVGBackgroundView.Instance.PartCGOff();
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
    }
}
