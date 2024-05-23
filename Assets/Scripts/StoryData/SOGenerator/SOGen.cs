using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using KiyuzuDev.ITGWDO.StoryData;

[Serializable]
public class DialogueLineRaw
{
    public int lineId = -1;
    public string dialogueLineType;
    public string personName;
    public string content;
    public string eventNames;
    public string eventArgs;
    public string choiceAtMindBox;
}

public class SOGen : MonoBehaviour
{
    // Asset文件保存路径
    const string pathSO = "Assets/Resources/StorySO/";
    const string pathScripts = "Assets/Resources/StoryScripts/";
    
    private DialogueLineRaw[] dialogueRaws;

    private void Start()
    {
        CreateStoryAsset("PV0514");
    }

    private void CreateStoryAsset(string scriptFileName)
    {
        TextAsset tA = AssetDatabase.LoadAssetAtPath<TextAsset>(pathScripts + scriptFileName + ".csv");
        dialogueRaws = CSVSerializer.Deserialize<DialogueLineRaw>(tA.text);
        List<DialogueLine> lineList = new List<DialogueLine>();
        foreach (var lineRaw in dialogueRaws)
        {
            DialogueLine line = ScriptableObject.CreateInstance<DialogueLine>();
            line.lineId = lineRaw.lineId;
            Enum.TryParse(lineRaw.dialogueLineType + "Line",out line.DialogueLineType);
            line.personName = lineRaw.personName;
            line.content = lineRaw.content;
            line.events = LoadEventListFromAsset(lineRaw.eventNames, lineRaw.eventArgs);
            bool.TryParse(lineRaw.choiceAtMindBox,out line.choiceAtMindBox);
            AssetDatabase.CreateAsset(line, pathSO + scriptFileName + "/" + line.lineId + ".asset");
            lineList.Add(line);
            EditorUtility.SetDirty(line);
        }
        StorySheet story = ScriptableObject.CreateInstance<StorySheet>();
        story.storyId = 99;
        story.dialogueLines = lineList;
        AssetDatabase.CreateAsset(story, pathSO + scriptFileName + ".asset");
        EditorUtility.SetDirty(story);
        AssetDatabase.SaveAssets();
    }

    private List<DialogueEventModel> LoadEventListFromAsset(string eventNames, string eventArgs)
    {
        var res = new List<DialogueEventModel>();
        var dialogueEvent = new DialogueEventModel();
        Enum.TryParse(eventNames, out dialogueEvent.eventType);
        string[] arg = new string[1];
        arg[0] = eventArgs;
        dialogueEvent.args = arg;
        res.Add(dialogueEvent);
        return res;
    }
}
