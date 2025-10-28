using System;

[Serializable]
public class Dialogue
{
    public string dialogueName;
    public string[] sentences;
    public DialogueOption[] options;
    public bool isRepeatable = true;
    public string questId;

    public Dialogue(string[] sentences)
    {
        this.sentences = sentences;
    }
}
