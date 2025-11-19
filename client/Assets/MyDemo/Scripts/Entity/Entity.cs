using UnityEngine;

public class Entity : MonoBehaviour
{
    public SetDir SetDir { get; private set; }
    public EntityVisual  EntityVisual { get; private set; }
   
    [SerializeField] protected float maxHP;
    protected float currentHealth;
    protected virtual void Awake()
    {
        SetDir = GetComponent<SetDir>();
        EntityVisual = GetComponent<EntityVisual>();
        EntityVisual.Init();
    }
    public virtual void Damage(float damage)
    {
        currentHealth -= damage;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
