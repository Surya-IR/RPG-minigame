using UnityEngine;

public abstract class CharacterScript : MonoBehaviour
{

    public enum CharacterAction
    {
        Attack,
        Defend,
        Skill,
        Escape
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
       
        Debug.Log("This process from CharacterScript also runs in Script: " + gameObject.name);
    }
    void Start()
    {
       
    }

    void Update()
    {

    }

    public abstract void GetDamage(float damage);

    public abstract void GetHeal(float heal);
   

    
    // Update is called once per frame
   
}
