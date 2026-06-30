using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldManager : MonoBehaviour
{
    public static OverworldManager Ins;

    private Vector3 playerLastPos = Vector3.zero;
    private Quaternion playerLastRot = Quaternion.identity;

    [SerializeField] OverworldController player;
    [SerializeField] Camera cam;

    private bool checkWorldPos;

    private string currentActiveBattle;

    private bool inBattle;

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
        else if (Ins != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
        DontDestroyOnLoad(player);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        checkWorldPos = true;
        SceneManager.sceneLoaded += EnterDungeonPositionPlayer;

        SceneManager.sceneLoaded += EnterBattle;
        SceneManager.sceneLoaded += ExitBattle;
    }

    void PositionCamera()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        else if(!inBattle)
        {
            Vector3 playerPos = player.gameObject.transform.position;
            cam.transform.position = playerPos + new Vector3(0, 5, -7f);
            cam.transform.LookAt(playerPos);
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void EnterBattle(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TurnBasedScene")
        {
            checkWorldPos = false;
            player.gameObject.SetActive(false);
            inBattle = true;
        }
    }

    public void ExitBattle(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "TurnBasedScene" && inBattle)
        {
            GameObject[] root = scene.GetRootGameObjects();
            foreach (GameObject r in root)
            {
                if (r.GetComponent<OverworldEnemy>() != null)
                {
                    if (r.GetComponent<OverworldEnemy>().EncounterID == currentActiveBattle)
                    {
                        r.gameObject.SetActive(false);
                    }
                }
            }
            checkWorldPos = true;
            inBattle = false;

            player.gameObject.SetActive(true);
            PostBattlePositionPlayer();
        }
    }

    public void ExitCutscene()
    {
        player.EnableControl();
        PositionCamera();
    }

    public void EnableControl()
    {
        player.EnableControl();
    }

    public void SetBattleID(string id)
    {
        currentActiveBattle = id;
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
                player.transform.position = entryDoor.SpawnLocation.position;
            }
        }
    }

    public OverworldController Player
    {
        get { return player; }
    }
    // Update is called once per frame
    void Update()
    {
        if (checkWorldPos)
        {
            playerLastPos = player.gameObject.transform.position;
            playerLastRot = player.gameObject.transform.rotation;
        }

        PositionCamera();
    }
}
