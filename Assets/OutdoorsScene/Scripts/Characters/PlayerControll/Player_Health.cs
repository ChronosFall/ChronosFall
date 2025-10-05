using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{

    [Header("初期HP")]public int HP = 100;
    [Header("HPバーのスライダー")]public Slider hPbarSlider;
    [Header("HP数の表示")]public TextMeshProUGUI hPText;

    void Start()
    {
        //HPバーを取得
        hPbarSlider = GameObject.Find("HPbar").GetComponent<Slider>();
        //初期設定HPを最大に変更
        hPbarSlider.maxValue = HP;
    }
    void Update()
    {
        //UIのHPバーにHPを反映
        hPbarSlider.value = HP;
        //テキストにHPを反映
        hPText.text = "HP " + HP + " / " + hPbarSlider.maxValue;
        //もしHPが0以下になったら
        if (HP <= 0)
        {
            /*
             * todo:
             * 
             * 試験的にラグドールを実装予定
             * https://github.com/medakoro0321/ChronosFall/issues/14 [#14]
             */
            Destroy(gameObject);
            //リセット
            HP = 0;
        }
    }
}