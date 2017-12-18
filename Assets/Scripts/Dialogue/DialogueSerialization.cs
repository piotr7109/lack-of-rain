using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueSerialization {
    private static string dirPath = "Assets/data/dialogues/";

    public static void CreateDialogue() {
        Dialogue dia = new Dialogue();
        DialogueNode[] diags = {
            new DialogueNode("Hello"),
            new DialogueNode("That's rude"),
            new DialogueNode("My name is [npc_name]"),
            new DialogueNode("Nice to meet you [player_name]")
        };

        Array.ForEach(diags, (dialogue => dia.AddNode(dialogue)));

        dia.AddOption("Exit", diags[1], null);
        dia.AddOption("Exit", diags[3], null);

        dia.AddOption("You smell", diags[0], diags[1]);
        dia.AddOption("Hello", diags[0], diags[2]);

        dia.AddOption("My name is [player_name]", diags[2], diags[3]);
        dia.AddOption("Call me [player_name]", diags[2], diags[3]);
        
        StreamWriter writer = new StreamWriter(dirPath + "dialogue2.json");
        string json = JsonUtility.ToJson(dia, true);

        writer.Write(json);
        writer.Close();
    }

    public static Dialogue LoadDialogue(string fileName) {
        string json = File.ReadAllText(dirPath + fileName + ".json");

        return JsonUtility.FromJson<Dialogue>(json);
    }

}
