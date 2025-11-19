using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDemo
{
    /// <summary>
    /// 人物等级系统
    /// </summary>
    public class PlayerLevelSys : MonoBehaviour
    {
        /// <summary>
        /// 当前经验值
        /// </summary>
        public int CurrentExp { get; private set; }

        /// <summary>
        /// 升级所需经验值
        /// </summary>
        public int ExpToNextLevel { get; private set; }

        public void Init()
        {
            ExpToNextLevel = HelperMgr.Instance().GetHelper<PlayerUpLevelHelper>().GetPlayerUpLevel(1).expRequired;
            InitCharacterLevel();
            var data = PlayerData.GetGameData();
            PlayerGameData data2 = data.GetData();
            CurrentExp = data2.currentExp;
            characterLevel = data2.currentPlayerLv;
            EventManager.Execute(GameEventKey.PlayerLevelVisual,characterLevel);
            ExpData expData;
            expData.Exp = CurrentExp;
            expData.MaxExp = ExpToNextLevel;
            EventManager.Execute(GameEventKey.PlayerExpVisual,expData);
        }

        private void OnEnable()
        {
            EventManager.Register<int>(GameEventKey.PlayerExpAdd, AddExp);
            EventManager.Register(GameEventKey.GameExit, SaveData);
        }

        private void SaveData()
        {
            var data = PlayerData.GetGameData();
            data.SetPlayerLv(characterLevel);
            data.SetExp(CurrentExp);
        }

        private void OnDisable()
        {
            EventManager.Unregister<int>(GameEventKey.PlayerExpAdd, AddExp);
            EventManager.Unregister(GameEventKey.GameExit, SaveData);
        }

        /// <summary>
        /// 人物当前等级
        /// </summary>
        /// <returns></returns>
        public int characterLevel;

        /// <summary>
        /// 人物升级函数
        /// </summary>
        private void LevelUp()
        {
            // 扣除升级所需经验
            CurrentExp -= ExpToNextLevel;
            //提升等级
            characterLevel++;
          //  EventManager.Execute(GameEventKey.PlayerLevelUp, characterLevel);
            EventManager.Execute(GameEventKey.PlayerLevelVisual,characterLevel);
        }

        /// <summary>
        /// 人物经验值增加
        /// </summary>
        /// <param name="addExp"></param>
        private void AddExp(int addExp)
        {
            if (addExp <=0) return;
            CurrentExp += addExp;
            //判断当前经验值是否可以升级
            while (CurrentExp >= ExpToNextLevel)
            {
                LevelUp();
            }
            ExpData expData = new ExpData
            {
                Exp = CurrentExp,
                MaxExp = ExpToNextLevel
            };
            
            EventManager.Execute(GameEventKey.PlayerExpVisual,expData);
        }

        /// <summary>
        /// 初始化人物等级
        /// </summary>
        /// <param name="level"></param>
        private void InitCharacterLevel(int level = 1)
        {
            characterLevel = level;
           // EventManager.Execute(GameEventKey.PlayerInitLevel, characterLevel);
        }
    }
}