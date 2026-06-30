using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDungeon : MonoBehaviour
{
    private bool isInteractable;
    private bool isQuestActive;
    [SerializeField] Flowchart Dialogue;

    [SerializeField] GameObject highlightText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isInteractable = false;
        isQuestActive= false;
    }

    public void OnTriggerEnter(Collider other)
    {
        isInteractable = true;
        highlightText.SetActive(true);
    }

    public void EnteringDungeon()
    {
        if (isInteractable && isQuestActive)
        {
            highlightText.SetActive(false);
            SceneManager.LoadScene("DungeonScene");
        }
        else if(isInteractable)
        {
            Dialogue.ExecuteBlock("QuestInactive");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        isInteractable= false;
        highlightText.SetActive(false);
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
