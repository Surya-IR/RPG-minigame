using Fungus;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] bool isUnlocked;
    [SerializeField] bool isInteractable;

    [SerializeField] string chestID;

    [SerializeField] Flowchart chart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isInteractable = false;
        isUnlocked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isInteractable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInteractable = false;
    }

    public void InteractWithChest()
    {
            chart.ExecuteBlock("OpenChest");
    }

    public void FinishInteraction()
    {
        OverworldManager.Ins.EnableControl();
    }

    public void UnlockChest()
    {
        Debug.Log("Chest is Unlocked");
        isUnlocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InteractWithChest();
        }
        
    }
}
