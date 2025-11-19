using UnityEngine;
/// <summary>
/// UI面板
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public abstract class UIPanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    protected virtual string UIPanelName => GetType().Name;

    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _canvasGroup=GetComponent<CanvasGroup>();
    }
    public virtual void Show(bool ani)
    {
        
    }

    public virtual void Hide(bool ani)
    {
        
    }
}
