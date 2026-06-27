using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDungeon : MonoBehaviour
{
    private bool isInteractable;
    private bool isQuestActive;
    [SerializeField] Flowchart Dialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isInteractable = false;
        isQuestActive= false;
    }

    public void OnTriggerEnter(Collider other)
    {
        isInteractable = true;
    }

    public void EnteringDungeon()
    {
        if (isInteractable && isQuestActive)
        {
            SceneManager.LoadScene("DungeonScene");
        }
        else
        {
            Dialogue.ExecuteBlock("QuestInactive");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    public void ActivateQuest()
    {
        isQuestActive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnteringDungeon();
        }
    }
}
