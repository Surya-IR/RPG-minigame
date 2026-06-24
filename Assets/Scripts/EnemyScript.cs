using UnityEngine;

public class EnemyScript : CharacterScript
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
        }
    }

    void Start()
    {
        role = (Stats.CharacterRole)1;
        if (gameObject.GetComponent<EnemyScript>() != null)
        {
           TurnBasedManager.Ins.indexEnemy(gameObject.GetComponent<EnemyScript>());
        }
    }

    public override void GetDamage(float damage)
    {
        HealthData = health - damage;
        if (health <= 0)
        {
            isDead = true;
            gameObject.SetActive(false);
            TurnBasedManager.Ins.RemoveEnemy(characterName);
        }
    }

    public override void GetHeal(float heal)
    {
        //throw new System.NotImplementedException();
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
