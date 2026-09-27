using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkEventBox : MonoBehaviour
{
    [SerializeField] private Conversation conversation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            conversation.StartConversation();
            Destroy(this);
        }
    }
}
