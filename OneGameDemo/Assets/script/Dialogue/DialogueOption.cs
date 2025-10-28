using System;

[Serializable]
public class DialogueOption
{
    public string text;
    public Dialogue nextDialogue;
    public string methodToCall;
    public string parameter;

    public bool IsAvailable()
    {
        return true;
    }
}
