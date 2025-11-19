using TMPro;
using UnityEngine.UI;

namespace MyDemo
{
    public struct ExpData
    {
        public int Exp;
        public int MaxExp;
    }

    public struct HpData
    {
        public int Hp;
        public int MaxHp;
    }

    /// <summary>
    /// 用户状态面板
    /// </summary>
    public class PlayerStatPanel : UIPanel
    {
        private TextMeshProUGUI _playerLevelText;
        private TextMeshProUGUI _expText;
        private Image _expSlider;
        private Image _hpSlider;
      

        protected override void Awake()
        {
            base.Awake();
            _playerLevelText = transform.Find("Level/LevelText").GetComponent<TextMeshProUGUI>();
            _expText= transform.Find("Exp/ExpText").GetComponent<TextMeshProUGUI>();
            _expSlider = transform.Find("Exp/ExpSlider").GetComponent<Image>();
            _hpSlider = transform.Find("Hp/HpSlider").GetComponent<Image>();
        }

        private void OnEnable()
        {
           EventManager.Register<int>(GameEventKey.PlayerLevelVisual,SetCharacterLevelVisual);
           EventManager.Register<ExpData>(GameEventKey.PlayerExpVisual,SetExpVisual);
           EventManager.Register<HpData>(GameEventKey.PlayerHpVisual,SetHpVisual);
        }

        private void OnDisable()
        {
            EventManager.Unregister<int>(GameEventKey.PlayerLevelVisual,SetCharacterLevelVisual);
            EventManager.Unregister<ExpData>(GameEventKey.PlayerExpVisual,SetExpVisual);
            EventManager.Unregister<HpData>(GameEventKey.PlayerHpVisual,SetHpVisual);
        }

        private void SetCharacterLevelVisual(int level)
        {
            _playerLevelText.text = "Lv" + level;
        }

        /// <summary>
        /// 设置经验值UI的显示
        /// </summary>
        private void SetExpVisual(ExpData expData)
        {
            _expSlider.fillAmount = (float)expData.Exp / expData.MaxExp;
            _expText.text = (float)expData.Exp + "/" + expData.MaxExp;
        }

        private void SetHpVisual(HpData hpData)
        {
            _expSlider.fillAmount = (float)hpData.Hp / hpData.MaxHp;
            _expText.text = (float)hpData.Hp + "/" + hpData.MaxHp;
        }
    }
}