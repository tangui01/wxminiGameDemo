using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

public static class SaveSystem
{
    [Serializable]
    class SaveData
    {
        public string data = "";

        public override string ToString()
        {
            return data;
        }
    }

    private static readonly string key = "bassData";
    private static readonly string EncryptKey = "16457605";
    public static void SavePlayer(string fu_id, string data)
    {
#if UNITY_WEBGL
        SaveData sd = new SaveData();
        sd.data = StringToByteString(data);

        //WX.StorageSetStringSync(fu_id, sd.data);
        //StarkSDK.API.GetStarkFileSystemManager().WriteFileSync(fu_id, sd.data, "utf8");
//#elif UNITY_ANDROID
//        SaveData sd = new SaveData();
//        sd.data = StringToByteString(data);
//        //StarkSDK.API.Save(sd, fu_id);
#else
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + key + fu_id;
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, StringToByteString(data));
        stream.Close();
#endif

    }

    //读取数据
    public static string LoadPlayer(string fu_id)
    {


#if UNITY_WEBGL
        //var data = WX.StorageGetStringSync(fu_id, "");

        ////return null;

        //if ("" == data)
        //{
        //    return null;
        //}
        //else
        //{
        //    return ByteStringToString(data);
        //}
#elif UNITY_ANDROID
        string path = Application.persistentDataPath + "/" + key + fu_id;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            string data = formatter.Deserialize(stream) as string;

            stream.Close();
            return ByteStringToString(data);
        }
        else
        {
            // Debug.LogError("Save file not found in  "+path);
            return null;
        }
#else
        string path = Application.persistentDataPath + "/" + key + fu_id;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            string data = formatter.Deserialize(stream) as string;

            stream.Close();
            return ByteStringToString(data);
        }
        else
        {
            // Debug.LogError("Save file not found in  "+path);
            return null;
        }
#endif


    }

    //加密
    public static string StringToByteString(string str)
    {
        return EncryptDES(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(str)));
    }

    //解密
    public static string ByteStringToString(string str)
    {
        return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(DecryptDES(str)));
    }

    #region  字符串加密解密
    private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
    /// <summary>
    /// DES加密字符串
    /// </summary>
    /// <param name="encryptString">待加密的字符串</param>
    /// <param name="key">加密密钥,要求为8位</param>
    /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
    public static string EncryptDES(string encryptString)
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(EncryptKey.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
            return Convert.ToBase64String(mStream.ToArray());
        }
        catch
        {
            //Debug.LogError("StringEncrypt/EncryptDES()/ Encrypt error!");
            return encryptString;
        }
    }

    /// <summary>
    /// DES解密字符串
    /// </summary>
    /// <param name="decryptString">待解密的字符串</param>
    /// <param name="key">解密密钥,要求为8位,和加密密钥相同</param>
    /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
    public static string DecryptDES(string decryptString)
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(EncryptKey);
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
        catch
        {
            //Debug.LogError("StringEncrypt/DecryptDES()/ Decrypt error!");
            return decryptString;
        }
    }
    #endregion
}
