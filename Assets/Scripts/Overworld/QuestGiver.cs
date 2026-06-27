using Fungus;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    private bool isInteractable;
    private bool inDialogue = false;
    [SerializeField] Flowchart flowchart;

    [SerializeField] EnterDungeon dungeonDoor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        isInteractable= true;
    }

    public void QuestDialogue()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isInteractable)
            {
                inDialogue = true;
                flowchart.ExecuteBlock("NPCQuest");
                dungeonDoor.ActivateQuest();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInteractable = false;
    }

    // Update is called once per frame
    void Update()
    {
        QuestDialogue();
    }
}
