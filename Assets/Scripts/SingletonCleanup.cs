using UnityEngine;

public class SingletonCleanup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (CommandManager.Ins != null)
            Destroy(CommandManager.Ins.gameObject);
        if (CutsceneManager.Ins != null)
            Destroy(CutsceneManager.Ins.gameObject);
        if (OverworldManager.Ins != null)
        {
            Destroy(OverworldManager.Ins.Player.gameObject);
            Destroy(OverworldManager.Ins.gameObject);
        }
        if (TurnBasedManager.Ins != null)
            Destroy(TurnBasedManager.Ins.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
