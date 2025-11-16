using System;
using ChronosFall.Scripts.Systems.Enemies.Data;
using ChronosFall.Scripts.Systems.Player.Base;
using ChronosFall.Scripts.Systems.Player.Data;
using UnityEngine;

namespace ChronosFall.Scripts.Systems.Enemies.Base
{
    public class EnemiesCalcu : ScriptableObject
    {
        private float _finalDamage;
        private PlayerData _basePlayerData;
        private PlayerData _playerData;
        
        /// <summary>
        /// 敵へのダメージ計算システム
        /// </summary>
        /// <param name="damage">攻撃力 [int]</param>
        /// <param name="elementType">プレイヤーの属性 [ElementType]</param>
        /// <param name="eData">敵が持っている初期データ [EnemyData]</param>
        /// <returns>最終ダメージ [int]</returns>
        public int EnemiesTakeDamageCalcu(int damage, ElementType elementType,EnemyData eData)
        {
            // TODO 全体的に作りが雑なので修正
            PlayerBase playerData = GameObject.Find("temp#1").GetComponent<PlayerBase>();
            _basePlayerData = playerData.BasePdata; // フィールドから取得

            _playerData = Instantiate(_basePlayerData);

            /*
             * memo
             * 最終ダメージ = ( 攻撃力 * スキル倍率 * 弱点補正 * ( 1 - 耐性補正 )) * クリティカル補正 * 被ダメージ倍率[バフ/デバフ] * [多分:ブレイク補正]
             *
             * 弱点倍率:
             *  弱点 : x1.5
             *  無し : x1.0
             *  耐性 : x0.7
             *  無効 : x0.0
             *
             * クリティカル倍率 :
             *  クリティカル%成功
             *   x1.5
             *
             * 防御力補正:
             * 
             * 
             */
            
            // 攻撃力 x スキル倍率
            _finalDamage = damage * _playerData.skillRate; // 最終ダメージ

            // x 弱点倍率
            if (eData.enemyWeakpoint.Contains(elementType)) _finalDamage += 1.5f; // 弱点 x1.5
            else if (eData.enemyResistancePoint.Contains(elementType)) _finalDamage *= 0.7f; // 耐性 x0.7

            // x クリティカル(成功でx1.5)
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= _playerData.critChance) _finalDamage *= 1.5f;
            Debug.LogAssertion($"FINAL DAMAGE : {_finalDamage}");
            
            // 四捨五入
            return (int)Math.Round(_finalDamage, 0);
        } 
    }
}