using RedGirafeGames.Agamotto.Scripts.Runtime;
using UnityEngine;
using System.Collections;
using ChronosFall.Scripts.Core.Configs;

namespace ChronosFall.Scripts.Characters.Player.PlayerControls
{
    /// <summary>
    /// プレイヤーの時間操作能力を管理
    /// v6.0仕様: 侵食ゲージ・クールダウン対応
    /// </summary>
    public class TimeControl : MonoBehaviour
    {
        [SerializeField] private TimeControlMode timeControlMode = TimeControlMode.TimeStop;
        [SerializeField] private KeyCode activationKey = KeyCode.Q;
        [SerializeField] private float slowTimeScale = 0.5f; // 遅延倍率
        [SerializeField] private float slowDuration = 5f; // 持続時間
        [SerializeField] private float cooldown = 10f; // クールダウン

        [Header("侵食設定")] [SerializeField] private float erosionIncreasePerUse = 20f; // 使用時の侵食増加量
        [SerializeField] private float maxErosion = 100f; // 最大侵食値
        [SerializeField] private float erosionDecayRate = 5f; // 自然減少速度（毎秒）

        [Header("参照")] [SerializeField] private TimeStone timeStone;

        // 内部状態
        private bool isTimeStopped = false;
        private bool isTimeSlowActive = false;
        private float cooldownTimer = 0f;
        private float currentErosion = 0f;
        private bool isActive = false;

        private void Start()
        {
            // TimeStone初期化（存在する場合）
            if (timeStone != null)
            {
                timeStone.StartRecording();
            }
        }

        private void Update()
        {
            // クールダウン管理
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.unscaledDeltaTime; // Time.timeScaleの影響を受けない
            }

            // 侵食ゲージ自然減少
            if (currentErosion > 0 && !isActive)
            {
                currentErosion -= erosionDecayRate * Time.deltaTime;
                currentErosion = Mathf.Max(currentErosion, 0);
            }

            // キー入力チェック
            bool activationInput = CharacterInputKey.IsInvertTimeControl
                ? Input.GetMouseButtonDown(1)
                : Input.GetKeyDown(activationKey);

            if (activationInput && cooldownTimer <= 0 && !isActive)
            {
                ActivateTimeControl();
            }
        }

        /// <summary>
        /// 時間操作を発動
        /// </summary>
        private void ActivateTimeControl()
        {
            // 侵食値がMAXなら発動不可
            if (currentErosion >= maxErosion)
            {
                Debug.LogWarning("[TimeControl] 侵食値がMAXのため発動できません");
                return;
            }

            // モードに応じて処理
            switch (timeControlMode)
            {
                case TimeControlMode.TimeStop:
                    ActivateTimeStop();
                    break;
                case TimeControlMode.TimeSlow:
                    ActivateTimeSlow();
                    break;
                default:
                    Debug.LogError($"[TimeControl] 未対応のモード: {timeControlMode}");
                    break;
            }

            // 侵食ゲージ増加
            currentErosion += erosionIncreasePerUse;
            currentErosion = Mathf.Min(currentErosion, maxErosion);

            // クールダウン開始
            cooldownTimer = cooldown;

            Debug.Log($"[TimeControl] 発動: {timeControlMode} | 侵食: {currentErosion:F1}%");
        }

        /// <summary>
        /// 時間停止モード（Agamotto使用）
        /// </summary>
        private void ActivateTimeStop()
        {
            if (!isTimeStopped)
            {
                // 時間停止
                timeStone.StopRecording();
                timeStone.FreezeTimeAgents(true);
                isTimeStopped = true;
                Debug.Log("[TimeControl] 時間停止");
            }
            else
            {
                // 時間再開
                timeStone.StartRecording();
                timeStone.FreezeTimeAgents(false);
                isTimeStopped = false;
                Debug.Log("[TimeControl] 時間再開");
            }
        }

        /// <summary>
        /// タイムスローモード（Unity Time.timeScale使用）
        /// </summary>
        private void ActivateTimeSlow()
        {
            if (isTimeSlowActive) return;

            isTimeSlowActive = true;
            Time.timeScale = slowTimeScale;
            Debug.Log($"[TimeControl] タイムスロー開始（{slowTimeScale}x速度）");

            StartCoroutine(EndTimeSlow());
        }

        /// <summary>
        /// タイムスロー終了
        /// </summary>
        private IEnumerator EndTimeSlow()
        {
            yield return new WaitForSecondsRealtime(slowDuration);

            Time.timeScale = 1.0f;
            isTimeSlowActive = false;
            Debug.Log("[TimeControl] タイムスロー終了");
        }

#if UNITY_EDITOR
        // エディタ上でのデバッグ表示
        private void OnGUI()
        {
            GUILayout.Label($"侵食: {currentErosion:F1}% / {maxErosion}");
            GUILayout.Label($"クールダウン: {cooldownTimer:F1}s");
            GUILayout.Label($"モード: {timeControlMode}");
            GUILayout.Label($"状態: {(isActive ? "発動中" : "待機中")}");
        }
#endif
    }

    /// <summary>
    /// 時間操作のモード
    /// </summary>
    public enum TimeControlMode
    {
        TimeStop, // 時間停止（Agamotto使用）
        TimeSlow // タイムスロー（Time.timeScale使用）
    }
}