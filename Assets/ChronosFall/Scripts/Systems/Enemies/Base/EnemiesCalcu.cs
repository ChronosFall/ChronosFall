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
        
        // 変数
        private int _atk;
        private float _skillMult;
        private int _enemyLv;
        private int _playerLv;
        private float _lvFactor;
        private int _def;
        private float _dCoef;
        private float _elemMult;
        private float _breakMult;
        private float _critRoll;
        private float _critMult;
        private float _dmgMod;
        private float _pierce;
        private float _randomOffset;
        
        
        /// <summary>
        /// 敵へのダメージ計算システム
        /// </summary>
        /// <param name="damage">攻撃力 [int]</param>
        /// <param name="elementType">プレイヤーの属性 [ElementType]</param>
        /// <param name="eData">敵が持っている初期データ [EnemyData]</param>
        /// <returns>最終ダメージ [int]</returns>
        public int EnemiesTakeDamageCalcu(int damage, ElementType elementType,EnemyData eData)
        {
            // init
            // TODO : ここのゲームオブジェクト取得システムを変更
            PlayerBase playerData = GameObject.Find("temp#1").GetComponent<PlayerBase>();
            _basePlayerData = playerData.BasePdata; // フィールドから取得
            _playerData = Instantiate(_basePlayerData);
            
            // Atk 攻撃者の攻撃力（武器込み）
            _atk = damage;
            Debug.Log(_atk);
            
            // SkillMult スキル倍率
            _skillMult = _playerData.skillRate;
            Debug.Log(_skillMult);
            
            // LvFactor Lv1差につき3%変動
            _playerLv = _playerData.level;
            _enemyLv = eData.level;
            _lvFactor = (float)(1 + 0.3 * (_playerLv - _enemyLv));
            Debug.Log(_lvFactor);
            
            // Def 防御値（被ダメ側）
            _def = eData.def;
            Debug.Log(_def);
            
            // ElemMult 属性係数 (弱点1.5 / 無効0 / 耐性0.7)
            if (eData.enemyWeakpoint.Contains(elementType)) _elemMult = 1.5f;
            else if (eData.enemyResistancePoint.Contains(elementType)) _elemMult = 0.7f;
            Debug.Log(_elemMult);
            
            // TODO : BreakMultとCritRollについて更に詳しく考える
            // BreakMult ブレイク補正
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= _playerData.breakChance) _breakMult = _playerData.breakMult;
            Debug.Log(_breakMult);
            
            // CritRoll CritMultiplier (会心発生で乗算)
            _critRoll = 1;
            _critMult = 1;
            
            // DMGMod 被ダメ増減 (バフ/デバフ)
            _dmgMod = 1;
            
            // Pierce 防御貫通 (%)
            _pierce = _playerData.pierce;
            Debug.Log(_pierce);
            
            // RandomOffset ランダム振れ幅
            float randomOffsetValue = 5;
            randomOffsetValue *= 0.01f;
            _randomOffset = UnityEngine.Random.Range(-randomOffsetValue, randomOffsetValue);
            Debug.Log(_randomOffset);
            
            // FinalDamage = ((Atk * SkillMult) * LvFactor * (100 / (100 + (Def * (1 - Pierce%)) * dCoef)) * ElemMult * BreakMult * DMGMod * CritFactor) * (1 ± RandomOffset)）
            _finalDamage = ((_atk * _skillMult) * _lvFactor * (100 / (100 + (_def * (1 - _pierce)) * _dCoef)) * _elemMult * _breakMult * _dmgMod * _critMult) * (1 + _randomOffset); 
            
            if (_finalDamage <= 0) _finalDamage = 0;
            
            Debug.LogWarning($"FINAL DAMAGE : {_finalDamage}");
            // 四捨五入
            return (int)Math.Round(_finalDamage, 0);
        } 
    }
}