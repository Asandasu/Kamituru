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

    [System.Serializable]
    public class Dialogue
    {
        [TextArea(3, 5)]
        public string sentence;

        [Header("左の立ち絵")]
        public Sprite leftSprite;

        [Header("右の立ち絵")]
        public Sprite rightSprite;
    }

    [SerializeField] private Dialogue[] dialogues;
    private int currentSentence = 0;
    public bool isConversation { get; private set; }

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
        if (isConversation && input.attack)
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
        isConversation = true;
        currentSentence = 0;
        windowPoint.color = new Color(0, 0, 0, 1);
        windowPoint.sprite = window;
        ShowDialogue();
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
        }
        else
        {
            rightCharacter.sprite = null;
            rightCharacter.enabled = false;
        }
    }
    private void EndConversation()
    {
        isConversation = false; dialogueText.text = "";
        windowPoint.sprite = null;
        windowPoint.color = new Color(0, 0, 0, 0);
        leftCharacter.sprite = null;
        rightCharacter.sprite = null;
        leftCharacter.enabled = false;
        rightCharacter.enabled = false;
    }
}