using Fungus;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    private bool isInteractable;
    private bool inDialogue = false;
    [SerializeField] Flowchart flowchart;

    [SerializeField] EnterDungeon dungeonDoor;

    [SerializeField] GameObject highlightText;

    private bool questAccepted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questAccepted= false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isInteractable= true;
        highlightText.SetActive(true);
    }

    public void QuestDialogue()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !questAccepted)
        {
            if (isInteractable)
            {
                inDialogue = true;
                flowchart.ExecuteBlock("NPCQuest");
               // dungeonDoor.ActivateQuest();
                highlightText.SetActive(false);
                questAccepted = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInteractable = false;
        highlightText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        QuestDialogue();
    }
}
