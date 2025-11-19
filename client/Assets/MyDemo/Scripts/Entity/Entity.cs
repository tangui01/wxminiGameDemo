using UnityEngine;

public class Entity : MonoBehaviour
{
    public SetDir SetDir { get; private set; }
    public EntityVisual  EntityVisual { get; private set; }
    protected Rigidbody2D Rb;
   
    [SerializeField] protected float maxHP;
    protected float currentHealth;
    [SerializeField] protected float attackValue;
    protected virtual void Awake()
    {
        SetDir = GetComponent<SetDir>();
        EntityVisual = GetComponent<EntityVisual>();
        EntityVisual.Init();
        Rb= GetComponent<Rigidbody2D>();
    }
    public void SetVelocity(Vector2 velocity)
    {
        Rb.velocity = velocity;
    }



    public virtual void Damage(float damage)
    {
        currentHealth -= damage;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetAttackValue()
    {
        return attackValue;
    }
}
