using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldManager : MonoBehaviour
{
    public static OverworldManager Ins;

    private Vector3 playerLastPos = Vector3.zero;
    private Quaternion playerLastRot = Quaternion.identity;

    [SerializeField] OverworldController player;

    private bool checkWorldPos;

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }

        DontDestroyOnLoad(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        checkWorldPos = true;
        SceneManager.sceneLoaded += EnterDungeonPositionPlayer;
    }

    public void EnterNextArea()
    {
        var coordinates = GameObject.Find("medievalDoor");
        if (coordinates != null)
        {

        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void EnterBattle()
    {
        checkWorldPos= false;
        player.gameObject.SetActive(false);
    }

    public void PostBattlePositionPlayer()
    {
        Debug.Log("Repositioning Player");

        player.gameObject.transform.position= playerLastPos;
        player.gameObject.transform.rotation= playerLastRot;
    }

    public void EnterDungeonPositionPlayer(Scene scene, LoadSceneMode mode)
    {
        GameObject[] objs = scene.GetRootGameObjects();

        Door entryDoor = new Door();
        foreach (GameObject obj in objs)
        {
            if (obj.GetComponent<Door>() != null)
            {
                entryDoor = obj.GetComponent<Door>();
            }
        }
        player.transform.position = entryDoor.SpawnLocation.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (checkWorldPos)
        {
            playerLastPos = player.gameObject.transform.position;
            playerLastRot = player.gameObject.transform.rotation;
        }
    }
}
