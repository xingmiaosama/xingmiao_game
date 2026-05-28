using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public Animator interactAnim;
    public DialogueSO dialogueSO;

    private void OnEnable()
    {
        rb.velocity = Vector2.zero;
        anim.Play("Idle");
        interactAnim.Play("Open");
        rb.isKinematic = true;
    }

    private void OnDisable()
    {
        interactAnim.Play("Close");
        rb.isKinematic = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DialogueManager.Instance.isDialogueActive)
            {
                DialogueManager.Instance.AdvanceDialogue();
            }
            else
            {
                DialogueManager.Instance.StartDialogue(dialogueSO);
            }
        }
    }
}
