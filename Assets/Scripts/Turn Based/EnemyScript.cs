using DG.Tweening;
using Ilumisoft.HealthSystem;
using TMPro;
using UnityEngine;

public class EnemyScript : CharacterScript
{
    public string characterName;
    public Stats.CharacterRole role;
    [SerializeField] float maxHealth;
    [SerializeField] float health;
    [SerializeField] float maxMana;
    [SerializeField] float mana;
    [SerializeField] float attackDamage;
    [SerializeField] float speed;
    [SerializeField] float evasion;
    [SerializeField] float block;
    [SerializeField] float blockChance;

    public bool isDead;

    [SerializeField] Health healthBar;

    [SerializeField] Animator anim;

    [SerializeField] Stats characterStats;

    [SerializeField] Canvas damageNumber;

    [SerializeField] TMP_Text damageText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (characterStats != null)
        {
            maxHealth = characterStats.health;
            maxMana= characterStats.mana;

            role = characterStats.role;
            characterName = characterStats.name;
            health = characterStats.health;
            mana = characterStats.mana;
            attackDamage = characterStats.damage;
        }
    }

    void Start()
    {
        healthBar.MaxHealth = maxHealth;
        healthBar.SetHealth(HealthData);

        role = (Stats.CharacterRole)1;
        if (gameObject.GetComponent<EnemyScript>() != null)
        {
           TurnBasedManager.Ins.indexEnemy(gameObject.GetComponent<EnemyScript>());
        }
    }

    public override void GetDamage(float damage)
    {
        HealthData = health - damage;
        healthBar.SetHealth(HealthData);
        if (health <= 0)
        {
            isDead = true;
            anim.SetBool("isIdle", false);
            anim.Play("Mutant Dying");
            TurnBasedManager.Ins.RemoveEnemy(characterName);
        }
    }

    public void AnimateAttack()
    {
        anim.SetBool("isIdle", false);
        anim.Play("Mutant Swiping");
        anim.SetBool("isIdle", true);
    }

    public void AnimateIdle()
    {
        Debug.Log("Return/Start Idle");
        anim.SetBool("isIdle", true);
        anim.Play("Mutant Idle");
    }

    public override void GetHeal(float heal)
    {
        //throw new System.NotImplementedException();
    }

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
