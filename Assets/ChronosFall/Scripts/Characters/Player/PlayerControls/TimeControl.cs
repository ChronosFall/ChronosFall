using ChronosFall.Scripts.Configs;
using RedGirafeGames.Agamotto.Scripts.Runtime;
using RedGirafeGames.Agamotto.Scripts.Runtime.Agents;
using UnityEngine;

namespace ChronosFall.Scripts.Characters.Player.PlayerControls
{
    public class TimeControl : MonoBehaviour
    {
        private bool _isTimeStopped;
        [SerializeField] private int _timeState;
        
        [SerializeField] private TimeStone _timeStone;

        private void Start()
        {
            if (_timeStone == null)
            {
                return;
            }
            
            _timeStone.StartRecording();
        }

        /// <summary>
        /// 時間操作
        /// </summary>
        private void Update()
        {
            if (Input.GetMouseButtonDown(CharacterInputKey.IsInvertTimeControl ? 1 : 0))
            {
                Debug.Log("TIME STOPPED");
                // TODO : メインシステムを考える
                switch (_timeState)
                {
                    case 0:
                        if (!_isTimeStopped) {
                            // 時間停止
                            _timeStone.StopRecording();
                            _timeStone.FreezeTimeAgents(true);
                            _isTimeStopped = true;
                        }
                        else
                        {
                            _timeStone.StartRecording();
                            _timeStone.FreezeTimeAgents(false);
                            _isTimeStopped = false;
                        }
                        break;
                    case 1:
                        // ゆっくりに
                        Time.timeScale = 0.5f;
                        break;
                    default:
                        Debug.LogError("エラー!: 想定しない_timeStateが渡されました: " + _timeState );
                        break;
                }
            }
        }
    }
}