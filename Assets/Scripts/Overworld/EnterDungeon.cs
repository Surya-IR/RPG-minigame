using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDungeon : MonoBehaviour
{
    public bool isInteractable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        isInteractable = true;
    }

    public void EnteringDungeon()
    {
        if (isInteractable)
        {
            SceneManager.LoadScene("DungeonScene");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
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
