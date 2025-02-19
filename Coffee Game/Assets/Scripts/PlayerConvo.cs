using UnityEngine;

public class PlayerConvo : MonoBehaviour
{
    [SerializeField] float talkDistance = 2;
    bool inConversation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        Debug.Log("Interact");
        if (inConversation)
        {
            Debug.Log("Skipping Line");
            GameManager.Instance.SkipLine();
        }
        else
        {
            Debug.Log("Looking for NPC");
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, talkDistance, Vector2.up, 0, LayerMask.GetMask("NPC"));
            if (hit)
            {
                Debug.Log("Hit Something!!" + hit.collider.gameObject.name);
                if (hit.collider.gameObject.TryGetComponent(out NPC npc))
                {
                    Debug.Log("speaking with npc");
                    {
                        if (hit.collider.name == "NPC")
                        {
                            Debug.Log("player speaks with john");
                            if (gameObject.GetComponent<PlayerController>().hasCoffee)

                            {
                                Debug.Log("Player speaks and has the coffee");
                                GameManager.Instance.StartDialogue(npc.dialogueAsset.dialogue, npc.coffeePosition, npc.npcName, 3); //if has coffee
                                gameObject.GetComponent<PlayerController>().removeCoffee();
                                GameManager.Instance.resetArena();
                            }
                            else
                            {
                                Debug.Log("player speaks but has no coffee");
                                GameManager.Instance.StartDialogue(npc.dialogueAsset.dialogue, npc.StartPosition, npc.npcName, 2); //if no coffee
                            }
                        }
                        else if (hit.collider.name == "NPC 2")
                        {
                            Debug.Log("player speaks with jack");
                            GameManager.Instance.StartDialogue(npc.dialogueAsset.dialogue, npc.StartPosition, npc.npcName, 3);
                        }
                    }
                }
            }
        }
    }

    void JoinConversation()
    {
        inConversation = true;
    }

    void LeaveConversation()
    {
        inConversation = false;
    }

    private void OnEnable()
    {
        GameManager.OnDialogueStarted += JoinConversation;
        GameManager.OnDialogueEnded += LeaveConversation;
    }

    private void OnDisable()
    {
        GameManager.OnDialogueStarted -= JoinConversation;
        GameManager.OnDialogueEnded -= LeaveConversation;
    }
}
