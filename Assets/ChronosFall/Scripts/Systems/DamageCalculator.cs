using System;
using ChronosFall.Scripts.Enemies;
using UnityEngine;
using ChronosFall.Scripts.Interfaces;

namespace ChronosFall.Scripts.Systems
{
    public static class DamageCalculator
    {
        /// <summary>
        /// Player -> Enemy
        /// </summary>
        /// <param name="attacker">プレイヤー</param>
        /// <param name="defender">ダメージを受ける敵</param>
        /// <returns>最終ダメージ量</returns>
        public static int CalculatePlayerToEnemy(CharacterRuntimeData attacker, EnemyRuntimeData defender)
        {
            // atk 素攻撃力
            float atk = attacker.BaseAtk;
            
            // SkillMult スキル倍率
            float skillMult = attacker.SkillLevel;
            
            // LvFactor 1.2 ^ ( PLv - ELv )
            int playerLv = attacker.Level;
            int enemyLv = defender.Level;
            float lvFactor = (float)(Math.Pow(1.12,playerLv - enemyLv));
            
            // Def 防御値（被ダメ側）
            int def = defender.BaseDef;
            
            // ElemMult 属性係数 (弱点1.5 / 無効0 / 耐性0.7)
            float elemMult = 1f;
                if (defender.Weakness.Contains(attacker.Element)) elemMult = 1.5f;
                else if (defender.Resistance.Contains(attacker.Element)) elemMult = 0.7f;
            
            // BreakMult ブレイク補正
            float breakMult = 1f;
            if (defender.isBroken) breakMult = 1.3f;
            
            // CritRoll CritMultiplier (会心発生で乗算)
            float critMult = 1f;
            if (UnityEngine.Random.Range(0.0f,1.0f) <= attacker.CritRate) critMult = attacker.CritMult;

            // DMGMod 被ダメ増減 (バフ/デバフ)
            float dmgMod = 1;
            
            // dCoef
            float dCoef = 1f;
            // TODO : なんだっけこれ
            
            // RandomOffset ランダム振れ幅
            float randomOffsetValue = 5 * 0.01f;
            float randomOffset = UnityEngine.Random.Range(-randomOffsetValue, randomOffsetValue);

            Debug.Log($"atk {atk} skill {skillMult} LvFactor {lvFactor} def {def} dCoef {dCoef} ElemMult {elemMult} breakMult {breakMult} critMult {critMult} randomoffset {randomOffset} ");
            // FinalDamage =    Atk * SkillMult * LvFactor * (100 / (100 + Def * dCoef)) * ElemMult * BreakMult * DMGMod * CritFactor * (1 ± RandomOffset))
            float finalDamage = atk * skillMult * lvFactor * (100 / (100 + def * dCoef)) * elemMult * breakMult * dmgMod * critMult * (1 + randomOffset); 
            
            if (finalDamage <= 0) finalDamage = 0;
            
            Debug.LogWarning("FINAL DAMAGE : {finalDamage}");
            // 四捨五入
            return (int)Math.Round(finalDamage, 0);
        }

        public static int CalculateEnemyToPlayer(EnemyRuntimeData attacker, CharacterRuntimeData defender)
        {
            float finalDamage = attacker.BaseAtk * 1.1f;
            return (int)finalDamage;
        }
    }
}