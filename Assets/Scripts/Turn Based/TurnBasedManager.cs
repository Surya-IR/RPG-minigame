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

    public bool isDefeated;

    public bool isWin;

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
        isDefeated = false;
        isWin = false;
        StartPlayerTurn();
    }

    //Lump party, sort by speed
    public void indexParty(PartyScript party)
    {
        playerParty.Add(party);
        playerParty.OrderBy(x=>x.speedData).ToList();
        Debug.Log("Party Numbers: " + playerParty.Count);
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
        else
        {
            currentTurn = new KeyValuePair<Turn, int>(Turn.player, nextTurn);
        }
    }

    public void endPlayerTurn()
    {
        List<PartyScript> aliveParty = playerParty.Where(x => !x.isDead).ToList();
        StartCoroutine(CommandManager.Ins.CommandEndPlayerTurn());

        if (aliveParty.Count > 0)
        {
            StartEnemyTurn();
        }
    }
    #endregion

    private void CheckWinCondition()
    {
        List<EnemyScript> activeEnemy = enemyParty.Where(enemy => enemy.isDead == false).ToList();
        List<PartyScript> activeParty = playerParty.Where(party => party.isDead == false).ToList();

        if (activeParty.Count == 0)
        {
            isDefeated = true;
            CommandManager.Ins.GameOverScreen();
        }
        else if (activeEnemy.Count == 0)
        {
            isWin = true;
            CommandManager.Ins.WinScreen();
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
        NextPlayerTurn();
    }
    #endregion

    public void StartEnemyTurn()
    {
        currentTurn = new KeyValuePair<Turn, int>(Turn.enemy, 0);
        StartCoroutine(CycleEnemyTurn());
    }

    private IEnumerator CycleEnemyTurn()
    {
            foreach (EnemyScript enemy in enemyParty)
            {
                ActiveEnemyAttackParty(enemy);
                yield return new WaitForSeconds(0.2f);
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

        CheckWinCondition();
    }

    void Update()
    {
    }
}
