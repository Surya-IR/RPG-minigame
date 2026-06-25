using UnityEngine;

public class PartyScript : CharacterScript
{
    public string characterName;
    public Stats.CharacterRole role;
    [SerializeField] float health;
    [SerializeField] float mana;
    [SerializeField] float attackDamage;
    [SerializeField] float speed;
    [SerializeField] float evasion;
    [SerializeField] float block;
    [SerializeField] float blockChance;

    public bool isDead;
    [SerializeField] Stats characterStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (characterStats != null)
        {
            role = characterStats.role;
            characterName = characterStats.name;
            health = characterStats.health;
            mana = characterStats.mana;
            attackDamage = characterStats.damage;
            speed = characterStats.speed;
        }
    }

    void Start()
    {
        
        if (gameObject.GetComponent<PartyScript>() != null)
        {

            TurnBasedManager.Ins.indexParty(gameObject.GetComponent<PartyScript>());

        }
    }

    public override void GetDamage(float damage)
    {
        HealthData = health - damage;
        if (health <= 0)
        {
            isDead = true;
        }
    }

    public override void GetHeal(float heal)
    {
        HealthData = health + heal;
    }

    // Update is called once per frame
    void Update()
    {
        
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
