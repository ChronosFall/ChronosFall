using ChronosFall.Scripts.Configs;
using UnityEngine;

namespace ChronosFall.Scripts.Systems
{
    public class ChangeConfig : MonoBehaviour
    {
        public KeyCode walkForward = KeyCode.W;
        public KeyCode walkBack = KeyCode.S;
        public KeyCode walkRight = KeyCode.D;
        public KeyCode walkLeft = KeyCode.A;
        public KeyCode moveDash = KeyCode.LeftShift;
        public KeyCode interact = KeyCode.F;
        public KeyCode nextCharacter = KeyCode.Space;
        public KeyCode previousCharacter = KeyCode.LeftControl;
        public bool isReset = false;
        
        private void Update()
        {
            // TODO : そのうちUpdate()を削除してコンフィグを変えた際のみに実行するようにする
            SetConfig();
            
            if (isReset)
            {
                AllResetConfig();
            }
        }

        public void SetConfig()
        {
            CharacterInputKey.WalkForward = walkForward;
            CharacterInputKey.WalkBack = walkBack;
            CharacterInputKey.WalkRight = walkRight;
            CharacterInputKey.WalkLeft = walkLeft;
            CharacterInputKey.MoveDash = moveDash;
            CharacterInputKey.Interact = interact;
            CharacterInputKey.NextCharacter = nextCharacter;
            CharacterInputKey.PreviousCharacter = previousCharacter;
        }

        public void AllResetConfig()
        {
            walkForward = KeyCode.W;
            walkBack = KeyCode.S;
            walkRight = KeyCode.D;
            walkLeft = KeyCode.A;
            moveDash = KeyCode.LeftShift;
            interact = KeyCode.F;
            nextCharacter = KeyCode.Space;
            previousCharacter = KeyCode.LeftControl;
            isReset = false;
        }
    }
}