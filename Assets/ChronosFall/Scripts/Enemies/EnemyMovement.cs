using UnityEngine;
using System.Collections;

namespace ChronosFall.Scripts.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform _target; // プレイヤーの位置
        private const float MoveSpeed = 3f; // 移動速度
        private Rigidbody _rb;
        // private float HP = 100f; // TODO : 後で実装
        private bool _isAttacking; // 攻撃中かどうかのフラグ   
        private Vector3 _currSpeedAxis ; //敵の座標軸移動速度
        private int _damage; // ダメージ
        
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            // HP = Random.Range(50, 200);
            _target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }

        private void Update()
        {
            if (_target) return;

            // プレイヤーの方向を計算（高さは無視）
            var direction = (_target.position - transform.position).normalized;
            direction.y = 0; // Y軸の変化をなくす（地面に沿って移動）

            // 移動
            _currSpeedAxis = direction * MoveSpeed;
            _rb.linearVelocity = new Vector3(_currSpeedAxis.x, _rb.linearVelocity.y, _currSpeedAxis.z);

            // プレイヤーの方向を向く
            transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));
            
            /*
             * TODO : HPロジックとラグドールの実装
             *
             * if (HP <= 0)
             * {
             *
             *     Destroy(gameObject);
             * }
             */

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
            if (_target) yield break;

            var mainCharacter = GameObject.FindGameObjectsWithTag("Player")[0]; // TODO : 軽量化
            var attackDistance = Vector3.Distance(transform.position, mainCharacter.transform.position);

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