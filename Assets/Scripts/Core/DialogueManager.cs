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
            LoadLineById(110001);
            ProcessLine();
        }

        public void LoadLineById(int id)
        {
            PresentLine = ScriptManager.Instance.LoadSpecificLine(id);
        }
        
        public void ProcessLine()
        {
            PresentLineID = PresentLine.lineId;
            // Deal Event Fore
            // Deal Event Main
            switch (PresentLine.DialogueLineType)
            {
                case EnumDialogueLineType.TitleLine:
                    AVGView.Instance.UpdateAnnouncementTitle(PresentLine.content);
                    break;
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
                    Debug.Log(PresentLine.personName + "\n" + PresentLine.content);
                    // Process event After
                    LoadLineById(PresentLine.toLine);
                    return;
                case EnumDialogueLineType.GameLine:
                    // TODO: Turn to Game Scene
                    break;
            }
        }

        public void MoveNextLine()
        {
            // Deal eventAfter
            LoadLineById(PresentLine.toLine);
        }

        private void ProcessEventFore() {
			var args = PresentLine.eventFore.args;
			switch (PresentLine.eventFore.eventType)
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
                    AVGBackgroundView.Instance.FullCGOff();
                    AVGBackgroundView.Instance.PartCGOff();
                    break;
                case EnumDialogueEventType.BlackOn: {
                        int duration = args.Length >= 1 ? int.Parse(args[0]) : 2;
                        GameManager.Instance.FadeBlackScreenOpacity(1, duration);
                        break;
                    }
                case EnumDialogueEventType.BlackOff: {
                        int duration = args.Length >= 1 ? int.Parse(args[0]) : 2;
                        GameManager.Instance.FadeBlackScreenOpacity(0, duration);
					    break;
					}
				case EnumDialogueEventType.HumanLoad:

                    break;
                case EnumDialogueEventType.HumanChangePos:
                    // TODO: not used, write if used
                    break;
                case EnumDialogueEventType.HumanUnload:

                    break;
            }
            
        }
    }
}
