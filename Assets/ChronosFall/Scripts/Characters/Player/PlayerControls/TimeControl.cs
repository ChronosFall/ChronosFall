using ChronosFall.Scripts.Configs;
using RedGirafeGames.Agamotto.Scripts.Runtime;
using UnityEngine;

namespace ChronosFall.Scripts.Characters.Player.PlayerControls
{
    public class TimeControl : MonoBehaviour
    {
        // 時間操作
        private TimeStone _timeStone;
        private int _timeState;
        
        /// <summary>
        /// 時間操作
        /// </summary>
        private void Update()
        {
            if (Input.GetMouseButtonDown(CharacterInputKey.IsInvertTimeControl ? 1 : 0))
            {
                // TODO : メインシステムを考える
                switch (_timeState)
                {
                    case 0:
                        _timeStone.StopRecording();
                        _timeState = 1;
                        break;
                }
            }
        }
    }
}