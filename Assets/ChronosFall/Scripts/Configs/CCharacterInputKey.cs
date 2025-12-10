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
    
    public static class CCharacterInputKey
    {
        // JSON保存
        public static void SaveKeyConfig()
        {
            if (new CharacterController() != null) { 
                JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(new CharacterInputKey()),Resources.Load("Databases/KeyConfig"));
            }
        }
        
        // JSON読み込み
        public static void LoadKeyConfig()
        {
            JsonUtility.ToJson(new CharacterInputKey(), Resources.Load("Databases/KeyConfig"));
        }
    }
}