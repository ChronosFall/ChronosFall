using UnityEngine;

namespace ChronosFall.Scripts.Core.Configs
{
    [System.Serializable]
    // プレイヤー操作キー
    public class CharacterInputKey
    {
        public static KeyCode WalkForward { get; set; }
        public static KeyCode WalkBack { get; set; }
        public static KeyCode WalkRight { get; set; }
        public static KeyCode WalkLeft { get; set; }
        public static KeyCode MoveDash { get; set; }
        public static KeyCode Interact { get; set; }
        public static KeyCode NextCharacter { get; set; }
        public static KeyCode PreviousCharacter { get; set; }
        public static KeyCode PlayerAttack { get; set; }
        public static bool IsInvertTimeControl { get; set; }
    }

    [System.Serializable]
    public class SystemKey
    {
        public static KeyCode GamePauseMenu { get; set; }
    }
}