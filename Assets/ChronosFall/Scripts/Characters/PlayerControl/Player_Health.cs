using ChronosFall.Scripts.Interfaces;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ChronosFall.Scripts.Characters.PlayerControl
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("PlayerHealth系")] private int _hp = 100;
        private Slider _hPbarSlider; // HPバースライダー
        private TextMeshProUGUI _hPText; // HPGUI
        private Animator _animator;

        private void Start()
        {
            _hPbarSlider = Components.GetComponent<Slider>("HPbar");
            _hPText = Components.GetComponent<TextMeshProUGUI>("HPText");
            _animator = Components.GetComponent<Animator>(gameObject);
            // HPバーのMax数値を設定
            _hPbarSlider.maxValue = _hp;
        }

        private void Update()
        {
            // UIのHPバーにHPを反映
            _hPbarSlider.value = _hp;
            // [ HP <_hp> / <maxValue> ]
            _hPText.text = "HP " + _hp + " / " + _hPbarSlider.maxValue;
            if (_hp > 0) return;
            Dead();
        }
        /// <summary>
        /// 死亡処理
        /// </summary>
        private void Dead()
        {
            _hp = 0;
            _animator.SetTrigger(PlayerOtherAnimator.IsDead);
        }
    }
}