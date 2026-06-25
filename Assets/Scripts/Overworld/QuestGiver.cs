using Fungus;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public bool isInteractable;
    public bool inDialogue = false;
    [SerializeField] Flowchart flowchart;
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
        inDialogue=true;
        flowchart.ExecuteBlock("NPCQuest");

    }

    private void OnTriggerExit(Collider other)
    {
        isInteractable = false;
    }

    // Update is called once per frame
    void Update()
    {
       //InteractWithPlayer();
    }
}
