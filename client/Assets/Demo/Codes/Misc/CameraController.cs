using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [HideInInspector]
    private Transform target;
    public float distance;
    public float height;
    private Vector3 lookDir;

    private float scopeLen;
    private float baseLen;
    private bool inScope;
    void Start()
    {

    }

    void LateUpdate()
    {
        if (target == null) return;
        transform.position = target.position - lookDir;
    }

    public void SetFollowTarget(Transform target)
    {
        this.target = target;
        var dir = -target.forward * distance + target.up * height;
        transform.position = target.position + dir;
        lookDir = target.position - transform.position;
        baseLen = lookDir.magnitude;
        scopeLen = 10;
    }
    #region ÍûÔ¶¾µ½»»¥
    public void DoScopeCamera(bool isScope)
    {
        if (isScope)
            StartCoroutine(LerpScope(baseLen+scopeLen));
        else
            StartCoroutine(LerpScope(baseLen));
    }

    private IEnumerator LerpScope(float targetLen)
    {
        var curLen = lookDir.magnitude;
        float temp = 0;
        while (Mathf.Abs(targetLen - lookDir.magnitude) > 0.1f)
        {
            lookDir = lookDir.normalized * Mathf.Lerp(curLen, targetLen, temp);
            yield return null;
            temp += 0.1f;
        }
    }
    #endregion

}
