using UnityEngine;
using System.IO;

namespace ChronosFall.Scripts.Configs
{
    
    [System.Serializable]
    public class CharacterInputKey
    {
        public static KeyCode WalkForward {get; set;}
        public static KeyCode WalkBack {get; set;}
        public static KeyCode WalkRight {get; set;}
        public static KeyCode WalkLeft {get; set;}
        public static KeyCode MoveDash {get; set;}
        public static KeyCode Interact {get; set;}
        public static KeyCode NextCharacter {get; set;}
        public static KeyCode PreviousCharacter {get; set;}
        
    }
    
    public static class CCharacterInputKey
    {
        public static CharacterInputKey Walk = new();
        
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