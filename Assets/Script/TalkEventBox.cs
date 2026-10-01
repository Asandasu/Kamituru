using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkEventBox : MonoBehaviour
{
    [SerializeField] private Conversation conversation;
    [SerializeField] private PlayerAnimation animation;
    [Header("Triggerでオブジェに触れると会話")]
    public string talkMode;
    
    bool isAfterBattle = false;

    private void Update()
    {
        if(talkMode == "AfterBattle" && isAfterBattle)
        {
            animation.WaitMode();
            conversation.StartConversation();
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && talkMode == "Trigger")
        {
            conversation.StartConversation();
            Destroy(this);
        }
    }

    public void AfterBattleEvent()
    {
        isAfterBattle = true;
    }
}
