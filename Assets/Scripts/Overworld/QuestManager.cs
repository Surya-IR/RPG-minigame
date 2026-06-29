using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] List<OverworldEnemy> enemies = new List<OverworldEnemy>();
    [SerializeField] TreasureChest chest;

    bool isQuestComplete;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<OverworldEnemy> activeEnemies = enemies.Where(x=> x.gameObject.activeSelf).ToList();
        if (activeEnemies.Count <= 0)
        {
            isQuestComplete = true;
        }
        else
        {
            isQuestComplete = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isQuestComplete)
        {
            Debug.Log("All Enemies Defeated");
            chest.gameObject.SetActive(true);
        }
    }
}
