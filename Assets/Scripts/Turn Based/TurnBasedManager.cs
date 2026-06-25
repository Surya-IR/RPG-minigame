using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnBasedManager : MonoBehaviour
{
    public enum Turn
    {
        none,
        player,
        enemy,
    }

    public static TurnBasedManager Ins;

    private List<PartyScript> playerParty = new List<PartyScript>();
    private List<EnemyScript> enemyParty = new List<EnemyScript>();

    public KeyValuePair<Turn, int> currentTurn;

    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartPlayerTurn();
    }

    //Lump party, sort by speed
    public void indexParty(PartyScript party)
    {
        playerParty.Add(party);
        playerParty.OrderBy(x=>x.speedData).ToList();
    }


    //Lump enemies, sort by speed
    public void indexEnemy(EnemyScript enemy)
    {
        int enemyCount = enemyParty.Count;
        enemy.characterName = enemyCount +"-" +enemy.characterName;

        enemyParty.Add(enemy);
        enemyParty.OrderBy(x=>x.speedData).ToList();

    }

    public void RemoveEnemy(string characterName)
    {
        enemyParty.RemoveAll(x => x.characterName == characterName);
        
    }

    #region PLAYER_CYCLE_TURN
    public void StartPlayerTurn()
    {
        currentTurn = new KeyValuePair<Turn, int>(Turn.player, 0);
        StartCoroutine(CommandManager.Ins.CommandStartPlayerTurn());
    }

    public void NextPlayerTurn()
    {
        int lastTurn = currentTurn.Value;
        int nextTurn = lastTurn + 1;


        if (nextTurn > playerParty.Count - 1)
        {
            endPlayerTurn();
        }
    }

    private void endPlayerTurn()
    {
        StartCoroutine(CommandManager.Ins.CommandEndPlayerTurn());
        StartEnemyTurn();
    }
    #endregion

    private void CheckWinCondition()
    {
        List<EnemyScript> activeEnemy = enemyParty.Where(enemy => enemy.isDead == false).ToList();
        if (activeEnemy.Count == 0)
        {
            Debug.Log("Player Wins");
        }
        else
        {
            NextPlayerTurn();
        }
    }


    #region PLAYER_ATTACK_PHASE
    public void ActivePartyAttackEnemy(string targetName)
    {
        if (currentTurn.Key == Turn.player && currentTurn.Value == 0)
        {
            EnemyScript target = enemyParty.Find(x => x.characterName == targetName);
            
                if (target.characterName == targetName && target.isDead == false)
                {
                    target.GetDamage(playerParty[currentTurn.Value].attackData);
                }
        }

        CheckWinCondition();
    }
    #endregion

    public void StartEnemyTurn()
    {
        currentTurn = new KeyValuePair<Turn, int>(Turn.enemy, 0);
        StartCoroutine(NextEnemyTurn());
    }

    private IEnumerator NextEnemyTurn()
    {
        yield return new WaitForSeconds(1);

            foreach (EnemyScript enemy in enemyParty)
            {
                ActiveEnemyAttackParty(enemy);
            }
        EndEnemyTurn();
    }

    private void EndEnemyTurn()
    {
        StartPlayerTurn();
    }

    private void ActiveEnemyAttackParty(EnemyScript enemy)
    {
        int targetIndex = Random.Range(0, playerParty.Count - 1);
        PartyScript selectTarget = playerParty[targetIndex];
        selectTarget.GetDamage(enemy.attackData);
    }

    void Update()
    {
    }
}
