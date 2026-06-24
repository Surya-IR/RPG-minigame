using UnityEngine;
using UnityEngine.Events;

public class FungusTriggerTest : MonoBehaviour
{
    public UnityEvent dialogueFinish;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueFinish = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
