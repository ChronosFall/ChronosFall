using UnityEngine;
using ChronosFall.Scripts.Interfaces;

namespace ChronosFall.Scripts.Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        private Rigidbody _rb;
        [SerializeField] private float _detectionRadius; // 検知範囲
        [SerializeField] private LayerMask _detectionLayer; // 検知対象のレイヤー
        private readonly Collider[] _result = new Collider[2];
        private Transform _target; // プレイヤーターゲット
            
        private void Start()
        {
            _rb = Components.GetComponent<Rigidbody>(gameObject);
            _target = Components.GetComponent<Transform>("temp#1"); // TODO: 後ほど変更
        }
        private void Update()
        {
            if (_target) return;
            
            CheckPlayerAround();
        }

        /// <summary>
        /// 周囲にプレイヤーがいるかどうか
        /// </summary>
        private void CheckPlayerAround()
        {
            var hit = Physics.OverlapSphereNonAlloc(
                transform.position, // 中心
                _detectionRadius, // 範囲
                _result, // 結果を格納する配列
                _detectionLayer // 検出対象のレイヤー
                );
            if (hit > 0)
            {
                
            }
        }

        // なんかわからんけど動かないけどよしっ!
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        }
    }
}
