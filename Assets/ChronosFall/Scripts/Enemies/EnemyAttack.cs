using System.Collections;
using UnityEngine;

namespace ChronosFall.Scripts.Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        private Transform _target; // プレイヤーの位置
        private bool _isAttacking; // 攻撃中かどうかのフラグ 
        private int _damage; // ダメージ
        
        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        private void Update()
        {
            // 周辺3m以内にプレイヤーがいたら攻撃
            if (Vector3.Distance(_target.position, transform.position) < 3f && !_isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
        
        /// <summary>
        /// 攻撃
        /// </summary>
        private IEnumerator Attack()
        {
            _isAttacking = true;
            // 1~5秒待つ（秒単位に変更）
            var waitTime = Random.Range(1, 5);
            yield return new WaitForSeconds(waitTime);
            // 途中でプレイヤーが消えた場合は中断
            if (!_target) yield break;
            
            // プレイヤーとの距離を計測
            var attackDistance = Vector3.Distance(transform.position, _target.transform.position);

            // ダメージ [ 50 - 距離 * 10 ] 
            // TODO : さすがにカスシステムなので後で修正
            if (!(50 - attackDistance * 10 <= 0))
            {
                _damage = (int)(50 - attackDistance * 10);
            }
            // TODO : ダメージを与えるシステム
            
            Debug.Log("敵が " + _damage + " ダメージを与えた！");
            // 攻撃終了フラグを解除
            _isAttacking = false;
        }
    }
}