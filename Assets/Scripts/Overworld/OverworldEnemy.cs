using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldEnemy : MonoBehaviour
{
    [SerializeField] string encounterID;
    public bool isActive;

    [SerializeField] bool isGuardingChest;
    [SerializeField] TreasureChest chest;

    [SerializeField] bool inCutscene;

    [SerializeField] float cutsceneTravelDist;
    [SerializeField] float cutsceneSpeed;
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
        if (inCutscene)
        {
            MoveHorizontal();
        }
    }

    public IEnumerator MoveHorizontal()
    {
        Debug.Log("Now moving Horizontal");
        Vector3 pos = gameObject.transform.position;
        pos.x += cutsceneTravelDist * cutsceneSpeed * Time.deltaTime;
        yield return new WaitForSeconds(1);

        cutsceneTravelDist *= -1;
        gameObject.transform.position = pos;
    }

    public void EnterCutscene()
    {
        inCutscene = true;
    }

    public void ExitCutscene()
    {
        inCutscene= false;
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
