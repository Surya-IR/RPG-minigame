using DG.Tweening;
using Fungus;
using Ilumisoft.HealthSystem;
using System.Collections;
using UnityEngine;

public class PartyScript : CharacterScript
{
    public string characterName;
    public Stats.CharacterRole role;

    public float MaxHealth;
    public float MaxMana;

    [SerializeField] float health;
    [SerializeField] float mana;
    [SerializeField] float attackDamage;
    [SerializeField] float speed;
    [SerializeField] float evasion;
    [SerializeField] float block;
    [SerializeField] float blockChance;

    public bool isDead;

    [SerializeField] Health healthBar;
    [SerializeField] Stats characterStats;

    [SerializeField] float fadeSpeed;

    [SerializeField] Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        AssignStats();
    }

    void Start()
    {
        isDead = false;
        healthBar.MaxHealth = MaxHealth;
        healthBar.SetHealth(HealthData);

        if (gameObject.GetComponent<PartyScript>() != null)
        {
            TurnBasedManager.Ins.indexParty(gameObject.GetComponent<PartyScript>());
        }
    }

    public override void GetDamage(float damage)
    {
        if (health <= 0)
        {
            isDead = true;
            anim.Play("NordstromDeath");
        }
        else
        {
            HealthData = health - damage;
            healthBar.SetHealth(HealthData);
        }
    }

    public override void GetHeal(float heal)
    {
        HealthData = health + heal;
        if (HealthData > MaxHealth)
        {
            health= MaxHealth;
        }
    }

    public void AnimateAttack()
    {
        anim.SetBool("isIdle", false);
        anim.Play("StableSlash");
    }

    public void AnimateIdle()
    {
        anim.SetBool("isIdle", false);
        anim.Play("TurnBasedIdle");
    }

    public void AssignStats()
    {
        Debug.Log("Assigning Stats");
        if (characterStats != null)
        {
            MaxHealth = characterStats.health;
            MaxMana = characterStats.mana;

            role = characterStats.role;
            characterName = characterStats.name;
            health = characterStats.health;
            mana = characterStats.mana;
            attackDamage = characterStats.damage;
            speed = characterStats.speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Animator GetAnim
    {
        get { return anim; }
        set { anim = value; }
    }

    public float HealthData
    {
        get { return health; }
        set { health = value; }
    }

    public float attackData
    {
        get { return attackDamage; }
        set { attackDamage = value; }
    }

    public float speedData
    {
        get { return speed; }
        set { speed = value; }
    }
}
