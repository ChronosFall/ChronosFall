using UnityEngine;

namespace ChronosFall.Scripts.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        private Transform _target; // プレイヤーの位置
        private const float MoveSpeed = 3f; // 移動速度
        private Rigidbody _rb;
        private bool _isAttacking; // 攻撃中かどうかのフラグ   
        private Vector3 _currSpeedAxis; //敵の座標軸移動速度
        private int _damage; // ダメージ


        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (!_target) return;

            // プレイヤーの方向を計算（高さは無視）
            var direction = (_target.position - transform.position).normalized;
            direction.y = 0; // Y軸の変化をなくす（地面に沿って移動）

            // 移動
            _currSpeedAxis = direction * MoveSpeed;
            _rb.linearVelocity = new Vector3(_currSpeedAxis.x, _rb.linearVelocity.y, _currSpeedAxis.z);

            // プレイヤーの方向を向く
            transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));
        }
    }
}