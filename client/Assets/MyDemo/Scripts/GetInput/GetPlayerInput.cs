using UnityEngine;
using UnityEngine.EventSystems;
namespace MyDemo
{
    /// <summary>
    /// 获取玩家的输入
    /// </summary>
    public class GetPlayerInput : MonoBehaviour
    {
        private void Update()
        {
#if UNITY_EDITOR||UNITY_STANDALONE_WIN
            GetEditorInput();
#elif UNITY_ANDROID
        GetAndroidInput();
#endif
        }

        /// <summary>
        /// 获取编辑器下的键盘模拟输入(用鼠标点击模拟屏幕点击)
        /// </summary>
        private void GetEditorInput()
        {
            // 检查是否点击在UI上
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // 点击在UI上，不处理游戏对象点击
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                EventManager.Execute(GameEventKey.WeaponAttack);
            }
        }

        /// <summary>
        /// 获取安卓平台的输入
        /// </summary>
        private void GetAndroidInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                EventManager.Execute(GameEventKey.WeaponAttack);
            }
        }
    }
}