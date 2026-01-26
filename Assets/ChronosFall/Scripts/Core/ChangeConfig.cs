using ChronosFall.Scripts.Core.Configs;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChronosFall.Scripts.Core
{
    public class ChangeConfig : MonoBehaviour
    {
        public KeyCode walkForward;
        public KeyCode walkBack;
        public KeyCode walkRight;
        public KeyCode walkLeft;
        public KeyCode moveDash ;
        public KeyCode interact;
        public KeyCode nextCharacter;
        public KeyCode previousCharacter;
        public KeyCode gamePauseMenu;
        public int playerAttack;
        public bool isInvertTimeControl;
        public static bool isReset;
        public bool isChanged;

        public ChangeConfig()
        {
            Init();
        }

        private void Update()
        {
            // TODO : コンフィグを変えた際のみに実行するようにする
            
            if (isReset)
            {
                Init();
            }

            if (isChanged)
            {
                SetConfig();
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
            walkForward = KeyCode.W;
            walkBack = KeyCode.S;
            walkRight = KeyCode.D;
            walkLeft = KeyCode.A;
            moveDash = KeyCode.LeftShift;
            interact = KeyCode.F;
            nextCharacter = KeyCode.Space;
            previousCharacter = KeyCode.LeftControl;
            gamePauseMenu = KeyCode.Escape;
            playerAttack = 0;
            isInvertTimeControl = false;
            isReset = false;
            isChanged = true;
        }
        
        /// <summary>
        /// コンフィグ変更
        /// </summary>
        private void SetConfig()
        {
            CharacterInputKey.WalkForward = walkForward;
            CharacterInputKey.WalkBack = walkBack;
            CharacterInputKey.WalkRight = walkRight;
            CharacterInputKey.WalkLeft = walkLeft;
            CharacterInputKey.MoveDash = moveDash;
            CharacterInputKey.Interact = interact;
            CharacterInputKey.NextCharacter = nextCharacter;
            CharacterInputKey.PreviousCharacter = previousCharacter;
            //CharacterInputKey.PlayerAttack = playerAttack;
            CharacterInputKey.IsInvertTimeControl = isInvertTimeControl;
            
            SystemKey.GamePauseMenu = gamePauseMenu;
            isChanged = false;
        }
    }
}