using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public static DialogueTrigger instance;

    public Dialogues[] dialogues;
    BoxCollider2D boxCollider;

    public GameObject DialogueShower;
    Animator DialogueAnimator;


    private void Start()
    {
        instance = this;

        boxCollider = GetComponent<BoxCollider2D>();
        DialogueAnimator = DialogueShower.GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.CompareTag("PinkPlayer"))||(collision.CompareTag("BluePlayer")))
        {
            TriggerDialogue();
        }
    }
    public void TriggerDialogue()
    {
        DialogueManager.instance.dialoguePlaying = true;
        DialogueAnimator.SetBool("End", false);
        DialogueAnimator.SetBool("Start",true);
        
        FindObjectOfType<DialogueManager>().StartDialogue(dialogues);
        Destroy(gameObject);

    }


}
