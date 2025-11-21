using System;
using ChronosFall.Scripts.Systems.Enemies.Data;
using ChronosFall.Scripts.Systems.Player.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace ChronosFall.Scripts.Systems.Enemies.Base
{
    public static class EnemiesCalcu
    {
        /// <summary>
        /// 敵へのダメージ計算システム
        /// </summary>
        /// <param name="atk">攻撃者の攻撃力 (武器込み) [int]</param>
        /// <param name="elementType">プレイヤーの属性 [ElementType]</param>
        /// <param name="eData">敵が持っているデータ [EnemyData]</param>
        /// <param name="pData">プレイヤーが持ってるデータ [PlayerData]</param>
        /// <returns>最終ダメージ [int]</returns>
        public static int EnemiesTakeDamageCalcu(int atk, ElementType elementType, EnemyData eData, PlayerData pData)
        {
            if (!pData)
            {
                Debug.LogAssertion("WARNING : not set to an instance of PlayerData!");
                return -1;
            }
            // SkillMult スキル倍率
            float skillMult = pData.skillRate;
            
            // LvFactor Lv1差につき3%変動
            int playerLv = pData.level;
            int enemyLv = eData.level;
            float lvFactor = (float)(1 + 0.03 * (playerLv - enemyLv));
            
            // Def 防御値（被ダメ側）
            int def = eData.def;
            
            // ElemMult 属性係数 (弱点1.5 / 無効0 / 耐性0.7)
            float elemMult = 1f;
            if (eData.enemyWeakpoint.Contains(elementType)) elemMult = 1.5f;
            else if (eData.enemyResistancePoint.Contains(elementType)) elemMult = 0.7f;
            
            // BreakMult ブレイク補正
            float breakMult = 1f;
            if (eData.isBroken) breakMult = 1.3f;
            
            // CritRoll CritMultiplier (会心発生で乗算)
            float critMult = 1f;
            if (UnityEngine.Random.Range(0.0f,1.0f) <= pData.critChance) critMult = pData.critMult;

            // DMGMod 被ダメ増減 (バフ/デバフ)
            float dmgMod = 1;
            
            // Pierce 防御貫通 (%)
            float pierce = pData.pierce;
            
            // dCoef
            float dCoef = 1f;
            
            // RandomOffset ランダム振れ幅
            float randomOffsetValue = 5 * 0.01f;
            float randomOffset = UnityEngine.Random.Range(-randomOffsetValue, randomOffsetValue);

            Debug.Log($"atk {atk} skill ${skillMult} LvFactor ${lvFactor} def ${def} pierce ${pierce} dCoef ${dCoef} ElemMult ${elemMult} breakMult ${breakMult} critMult ${critMult} randomoffset ${randomOffset} ");
            // FinalDamage =    ((Atk * SkillMult) * LvFactor * (100 / (100 + (Def * (1 - Pierce)) * dCoef)) * ElemMult * BreakMult * DMGMod * CritFactor) * (1 ± RandomOffset))
            float finalDamage = ((atk * skillMult) * lvFactor * (100 / (100 + (def * (1 - pierce)) * dCoef)) * elemMult * breakMult * dmgMod * critMult) * (1 + randomOffset); 
            
            if (finalDamage <= 0) finalDamage = 0;
            
            Debug.LogWarning($"FINAL DAMAGE : {finalDamage}");
            // 四捨五入
            return (int)Math.Round(finalDamage, 0);
        } 
    }
}