using UnityEngine;

public class Entity : MonoBehaviour
{
    public SetDir SetDir { get; private set; }
    protected virtual void Awake()
    {
        SetDir = GetComponent<SetDir>();
    }
}
