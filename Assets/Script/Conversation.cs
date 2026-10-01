using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Conversation : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [Header("会話ウィンドウ")]
    [SerializeField] private Sprite window;
    [SerializeField] private Image windowPoint;
    [Header("立ち絵")]
    [SerializeField] private Image leftCharacter;
    [SerializeField] private Image rightCharacter;

    [SerializeField] private BossMoveActiver moveActiver;

    [SerializeField] private PlayerMoveActiver activer;

    [System.Serializable]
    public class Dialogue
    {
        [TextArea(3, 5)]
        public string sentence;

        [Header("左の立ち絵")]
        public Sprite leftSprite;
        public bool isBlackLeft = false;

        [Header("右の立ち絵")]
        public Sprite rightSprite;
        public bool isBlackRight = false;
    }

    [SerializeField] private Dialogue[] dialogues;
    private int currentSentence = 0;
    public bool isConversation { get; private set; }
    public bool isTalk { get; private set; }

    void Start()
    {
        windowPoint.sprite = null;
        windowPoint.color = new Color(0, 0, 0, 0);
        dialogueText.text = "";
        leftCharacter.sprite = null;
        rightCharacter.sprite = null;
        leftCharacter.enabled = false;
        rightCharacter.enabled = false;
    }
    void Update()
    {
        if (isTalk && input.attack)
        {
            NextSentence();
        }
    }
    public void StartConversation()
    {
        if (dialogues == null || dialogues.Length == 0)
        {
            return;
        }
        StartCoroutine(StopGame());
    }
    private void NextSentence()
    {
        currentSentence++;
        if (currentSentence < dialogues.Length)
        {
            ShowDialogue();
        }
        else
        {
            EndConversation();
        }
    }
    private void ShowDialogue()
    {
        Dialogue dialogue = dialogues[currentSentence];
        // セリフ
        dialogueText.text = dialogue.sentence;
        // 左の立ち絵
        if (dialogue.leftSprite != null)
        {
            leftCharacter.sprite = dialogue.leftSprite;
            leftCharacter.enabled = true;
            if(dialogue.isBlackLeft)
            {
                leftCharacter.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                leftCharacter.color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            leftCharacter.sprite = null;
            leftCharacter.enabled = false;
        }

        // 右の立ち絵
        if (dialogue.rightSprite != null)
        {
            rightCharacter.sprite = dialogue.rightSprite;
            rightCharacter.enabled = true;
            if (dialogue.isBlackRight)
            {
                rightCharacter.color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                rightCharacter.color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            rightCharacter.sprite = null;
            rightCharacter.enabled = false;
        }
    }
    private void EndConversation()
    {
        dialogueText.text = "";
        windowPoint.sprite = null;
        windowPoint.color = new Color(0, 0, 0, 0);
        leftCharacter.sprite = null;
        rightCharacter.sprite = null;
        leftCharacter.enabled = false;
        rightCharacter.enabled = false;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        isTalk = false;
        isConversation = false;
        if (activer)
        {
            activer.PlayerMoveActive();
        }
        if (moveActiver)
        {
            moveActiver.BossMoveActive();
        }
    }

    private IEnumerator StopGame()
    {
        isConversation = true;
        if (activer)
        {
            activer.PlayerMoveStopper();
        }
        yield return new WaitForSeconds(1);
        isTalk = true;
        currentSentence = 0;
        windowPoint.color = new Color(0, 0, 0, 1);
        windowPoint.sprite = window;
        ShowDialogue();
    }
}