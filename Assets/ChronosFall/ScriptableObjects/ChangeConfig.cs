using ChronosFall.Scripts.Configs;
using UnityEngine;

namespace ChronosFall.ScriptableObjects
{
    public class ChangeConfig : MonoBehaviour
    {
        public static CharacterInputKey Walk = new CharacterInputKey();
        
        public KeyCode changeWalkFront = KeyCode.W;
        public KeyCode changeWalkBack = KeyCode.S;
        public KeyCode changeWalkRight = KeyCode.D;
        public KeyCode changeWalkLeft = KeyCode.A;
        public KeyCode changeMoveDash = KeyCode.LeftShift;
        public KeyCode changeInteract = KeyCode.F;

        private void Update()
        {
            CharacterInputKey.WalkFront = changeWalkFront;
            CharacterInputKey.WalkBack = changeWalkBack;
            CharacterInputKey.WalkRight = changeWalkRight;
            CharacterInputKey.WalkLeft = changeWalkLeft;
            CharacterInputKey.MoveDash = changeMoveDash;
            CharacterInputKey.Interact = changeInteract;
        }
    }
}