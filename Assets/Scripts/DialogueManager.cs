using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }

    private Queue<Dialogues> sentences;
    public float waitTime;

    // Players

    public GameObject BluePic;
    public GameObject BlueBG;

    public bool dialoguePlaying = false;

    public GameObject Controller;

    //Animators
    public Animator DialogueAnimator;


    //public Text DialogueText;

    public TextMeshProUGUI DialogueText;


    [SerializeField] GameObject DestroyerObject;
    [SerializeField] GameObject bluePlayer;


    private void Start()
    {
        instance = this;
        sentences = new Queue<Dialogues>();

    }


    public void StartDialogue(Dialogues[] dialogue)
    {
        sentences.Clear();

        foreach (Dialogues sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        Dialogues sent = sentences.Dequeue();
        string sentence = sent.sentences;
        bool player = sent.BluePlayer;
        if (player)
        {
            BlueBG.SetActive(true);
            BluePic.SetActive(true);
        }
        else
        {
            BlueBG.SetActive(false);
            BluePic.SetActive(false);
        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));



    }


    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(waitTime);
        }
    }

    void EndDialogue()
    {
        DialogueAnimator.SetBool("Start", false);
        DialogueAnimator.SetBool("End", true);
        dialoguePlaying = false;
        LastDialogueCheck();


    }

    void LastDialogueCheck()
    {
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            if (!dialoguePlaying && bluePlayer.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("destroy")))
            {
                Destroy(DestroyerObject);
                StartCoroutine(waiting());
            }
        }
    }

    IEnumerator waiting()
    {

        yield return new WaitForSeconds(10f);
        StartCoroutine(LevelLoader.instance.LoadNextLevel(7));
    }


}