using System.IO;
using UnityEngine;

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
    
    // TODO : QuickSave等で対応
    /*
    public static class CCharacterInputKey
    {
        private static CharacterInputKey walk = new();
        
        // JSON保存
        public static void SaveKeyConfig()
        {
            string json = JsonUtility.ToJson(walk, true);
            File.WriteAllText("/keyconfig.json", json);
        }
        
        // JSON読み込み
        public static void LoadKeyConfig()
        {
            string path = "/keyconfig.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                walk = JsonUtility.FromJson<CharacterInputKey>(json);
            }
        }
    }
    */
}