using UnityEngine;
using System.IO;

namespace ChronosFall.Scripts.Configs
{
    [System.Serializable]
    public class CharacterInputKey
    {
        public KeyCode walkFront = KeyCode.W;
        public KeyCode walkBehind = KeyCode.S;
        public KeyCode walkRight = KeyCode.D;
        public KeyCode walkLeft = KeyCode.A;
        public KeyCode walkDash = KeyCode.LeftShift;
        public KeyCode interact = KeyCode.F;
    }
    
    public static class CCharacterInputKey
    {
        public static CharacterInputKey Walk = new CharacterInputKey();
        
        // JSON保存
        public static void SaveKeyConfig()
        {
            string json = JsonUtility.ToJson(Walk, true);
            File.WriteAllText(Application.persistentDataPath + "/keyconfig.json", json);
        }
        
        // JSON読み込み
        public static void LoadKeyConfig()
        {
            string path = Application.persistentDataPath + "/keyconfig.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Walk = JsonUtility.FromJson<CharacterInputKey>(json);
            }
        }
    }
}