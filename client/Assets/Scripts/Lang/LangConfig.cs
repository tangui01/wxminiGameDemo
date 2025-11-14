//using ArabicSupport;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//case SystemLanguage.Arabic:
//    break;
//case SystemLanguage.French:
//    break;
//case SystemLanguage.German:
//    break;
//case SystemLanguage.Italian:
//    break;
//case SystemLanguage.Russian:
//    break;
//case SystemLanguage.Spanish:
//    break;

public class LangConfig
{
    public struct LangConfigData
    {
        public string key;
        public Dictionary<string, string> data;
    }

    Dictionary<string, Dictionary<string, string>> _config = new Dictionary<string, Dictionary<string, string>>();

    public static LangConfig _instance;

    public static LangConfig Instance()
    {
        if (_instance == null)
        {
            _instance = new LangConfig();

            _instance.Init();
        }

        return _instance;
    }

    public void Init()
    {
        var temp = new Dictionary<string, string>();
        temp.Add("German", "KLICKEN SIE ZUM ZURÜCK");
        temp.Add("Arabic", "انقر للخلف");
        temp.Add("French", "CLIQUEZ POUR RETOUR");
        temp.Add("Italian", "CLICCA SUL INDIETRO");
        temp.Add("Russian", "НАЖМИТЕ, ЧТОБЫ НАЗАД");
        temp.Add("Spanish", "ISLA DEL TOMATE");
        _config.Add("CLICK TO BACK", temp);
    }

    public string GetLand(string key, SystemLanguage Lan)
    {
        //Lan = SystemLanguage.Arabic;

        if (_config.ContainsKey(key))
        {
            var item = _config[key];

            var stingLan = Lan.ToString();
            if (item.ContainsKey(stingLan))
            {
                var str = item[stingLan];

                //if(stingLan == SystemLanguage.Arabic.ToString())
                //{
                //    return ArabicFixer.Fix(str, false, false);
                //}
                return str;
            }
        }

        return key;
    }
}
