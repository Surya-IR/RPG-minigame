using Fungus;
using MoonSharp.VsCodeDebugger.SDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] bool isUnlocked;
    [SerializeField] bool isInteractable;

    [SerializeField] string chestID;

    [SerializeField] Flowchart chart;

    [SerializeField] GameObject highlightText;

    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource bgmWin;

    private OverworldController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isInteractable = false;
        isUnlocked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isInteractable = true;
        highlightText.SetActive(true);
        if (other.gameObject.GetComponent<OverworldController>() != null)
        {
            player = other.gameObject.GetComponent<OverworldController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        highlightText.SetActive(false);
        isInteractable = false;
    }

    public void InteractWithChest()
    {
        bgm.Stop();
        bgmWin.Play();
        highlightText.SetActive(false);
        chart.ExecuteBlock("OpenChest");
    }

    public void FinishInteraction()
    {
        OverworldManager.Ins.EnableControl();
        SceneManager.LoadScene("MainMenuScene");
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
