using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldEnemy : MonoBehaviour
{
    [SerializeField] string encounterID;
    public bool isActive;

    [SerializeField] bool isGuardingChest;
    [SerializeField] TreasureChest chest;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        OverworldManager.Ins.SetBattleID(encounterID);
        if (other.gameObject.GetComponent<OverworldController>() != null)
        {
            SceneManager.LoadScene("TurnBasedScene");
        }
    }

    void Update()
    {
        
    }

    public void UnlockingChestAfterDeath()
    {
        if (isGuardingChest && chest != null)
        {
            Debug.Log("chest name: " + chest.gameObject.name);
            //chest.UnlockChest();
        }
    }

    public string EncounterID
    {
        get { return encounterID; }
    }
}
