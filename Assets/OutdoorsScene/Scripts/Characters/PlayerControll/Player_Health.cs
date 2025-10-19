using IConnectComponent;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("PlayerHealth系")]
    private int _hp = 100;
    private Slider _hPbarSlider; // HPバースライダー
    private TextMeshProUGUI _hPText; // HPGUI

    private void Start()
    {
        _hPbarSlider = Components.GetComponent<Slider>("HPbar");
        _hPText = Components.GetComponent<TextMeshProUGUI>("HPText");
        // HPバーのMax数値を設定
        _hPbarSlider.maxValue = _hp;
    }
    private void Update()
    {
        // UIのHPバーにHPを反映
        _hPbarSlider.value = _hp;
        // [ HP <_hp> / <maxValue> ]
        _hPText.text = "HP " + _hp + " / " + _hPbarSlider.maxValue;
        
        // HPが無くなったら
        if (_hp > 0) return;
        // TODO : ラグドールの導入
        Destroy(gameObject);
        _hp = 0;
    }
}