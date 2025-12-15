using System.Collections.Generic;
using ChronosFall.Scripts.Configs;
using ChronosFall.Scripts.Interfaces;
using ChronosFall.Scripts.Systems;
using UnityEngine;

namespace ChronosFall.Scripts.Characters.Player
{
    [RequireComponent(typeof(ChangeConfig))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerController : MonoBehaviour, IDamageablePlayer
    {
        // ベースデータ
        [SerializeField] private CharacterManager characterManager;
        private CharacterRuntimeData _characterRuntimeData;
        
        // 移動系統
        private Rigidbody _rb;
        private Animator _animator;
        private Vector2 _inputAxis; // -1~1の入力値
        private float _currentSpeed;
        private bool _isDashing;
        private GameObject _cameraObject;
        [SerializeField] private float walkSpeed = 2f;
        [SerializeField] private float dashSpeed = 5.3f;
        [SerializeField] private bool tempStopAnimator = false;
        
        // 攻撃系統
        private const float AttackRange = 5f; // 攻撃距離
        private const float AttackAngel = 40f; // 攻撃範囲
        private const float MinStep = 5f; // 最小ステップ角度
        private List<GameObject> _attackedEnemies; // 攻撃した敵List
        
        // インタラクト系統
        private const float SearchRadius = 1f; // インタラクトの半径

        // 初期化
        private void Start()
        {
            characterManager = CharacterManager.Instance;
            _characterRuntimeData = characterManager.GetActiveCharacter();
    
            if (_characterRuntimeData == null)
            {
                Debug.LogError("キャラクターデータの取得に失敗！GameManagerが初期化されていない可能性があります");
            }
            // アニメーション等
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
            _cameraObject = UnityEngine.Camera.main.gameObject;
            _currentSpeed = walkSpeed;
            _attackedEnemies = new List<GameObject>();
            _attackedEnemies.Clear();
        }
        
        // メイン処理
        private void Update()
        {
            ProcessInput();
            PlayerAttack();
            PlayerInteract();
        }

        private void FixedUpdate()
        {
            UpdateMovement();
        }

        private void LateUpdate()
        {
            UpdateAnimation();
        }
        
        /// <summary>
        /// 入力処理
        /// </summary>
        private void ProcessInput()
        {
            // 入力をリセット
            _inputAxis = Vector2.zero;

            // WASD入力を取得
            if (Input.GetKey(CharacterInputKey.WalkForward)) _inputAxis.y += 1f;
            if (Input.GetKey(CharacterInputKey.WalkBack)) _inputAxis.y -= 1f;
            if (Input.GetKey(CharacterInputKey.WalkLeft)) _inputAxis.x -= 1f;
            if (Input.GetKey(CharacterInputKey.WalkRight)) _inputAxis.x += 1f;

            // 入力の正規化 (斜め移動が速くならないように)
            if (_inputAxis.magnitude > 1f)
            {
                _inputAxis.Normalize();
            }

            // ダッシュ入力
            _isDashing = Input.GetKey(CharacterInputKey.MoveDash);
            _currentSpeed = _isDashing ? dashSpeed : walkSpeed;

            if (Input.GetKeyDown(CharacterInputKey.NextCharacter)) characterManager.SwitchNextPlayerCharacter();
            if (Input.GetKeyDown(CharacterInputKey.PreviousCharacter)) characterManager.SwitchPreviousPlayerCharacter();
        }

        /// <summary>
        /// 移動処理
        /// </summary>
        private void UpdateMovement()
        {
            if (!_cameraObject || !_rb) return;

            // カメラの向きを取得
            float cameraRotationY = _cameraObject.transform.eulerAngles.y;

            // プレイヤーをカメラの向きに同期
            transform.rotation = Quaternion.Euler(0f, cameraRotationY, 0f);

            // カメラ基準の方向ベクトルを計算
            Vector3 cameraForward = GetCameraForward();
            Vector3 cameraRight = GetCameraRight();

            // 移動ベクトルを計算
            Vector3 moveDirection = (cameraForward * _inputAxis.y + cameraRight * _inputAxis.x).normalized;
            Vector3 moveVelocity = moveDirection * _currentSpeed;

            // Rigidbodyに適用 (Y軸の速度は保持)
            _rb.linearVelocity = new Vector3(moveVelocity.x, _rb.linearVelocity.y, moveVelocity.z);
        }

        /// <summary>
        /// アニメーション更新
        /// </summary>
        private void UpdateAnimation()
        {
            if (!_animator || !_animator.runtimeAnimatorController || tempStopAnimator) return;
            
            _animator.speed = 1.0f;
            _animator.SetFloat(PlayerMovementAnimator.MoveSpeed,_rb.linearVelocity.magnitude);
        }

        /// <summary>
        /// カメラの前方向を取得（Y軸を無視）
        /// </summary>
        private Vector3 GetCameraForward()
        {
            Vector3 forward = _cameraObject.transform.forward;
            forward.y = 0f;
            return forward.normalized;
        }

        /// <summary>
        /// カメラの右方向を取得（Y軸を無視）
        /// </summary>
        private Vector3 GetCameraRight()
        {
            Vector3 right = _cameraObject.transform.right;
            right.y = 0f;
            return right.normalized;
        }

        /// <summary>
        /// プレイヤーが攻撃
        /// </summary>
        private void PlayerAttack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Rayの本数を計算 : 最低2本、扇の角度 / 最小ステップ数 + 1 本のRayCastが呼ばれる
                int rayCount = Mathf.Max(2, Mathf.CeilToInt(AttackAngel / MinStep) + 1);
                // 各Raycastの角度間隔を計算 
                float each = AttackAngel / (rayCount - 1);
                
                for (int i = 0; i < rayCount; i++)
                {
                    // 各Rayの角度を計算
                    float angle = -AttackAngel / 2f + each * i;
                    // 計算した角度でRayの方向ベクトルを作成
                    Vector3 dir = Quaternion.Euler(0, angle, 0) * transform.forward;
                    
                    if (Physics.Raycast(transform.position + Vector3.up, dir, out RaycastHit hit, AttackRange))
                    {
                        
                        if (hit.collider.TryGetComponent(out IEnemyDamageable enemy))
                        {
                            // 2連続の攻撃判定が入らないように
                            if (_attackedEnemies.Contains(hit.collider.gameObject)) continue;

                            // ダメージを取得
                            var attackerData = _characterRuntimeData;
                            var defenderData = enemy.GetEnemyData();
                            Debug.Log(defenderData);
                
                            int damage = DamageCalculator.CalculatePlayerToEnemy(
                                attackerData, 
                                defenderData
                            );
                            
                            // プレイヤーの情報を渡す
                            enemy.TakeDamage(damage, _characterRuntimeData.Element);
                            
                            Debug.Log($"Enemy has been attacked by player! Damage: {damage}");
                            _attackedEnemies.Add(hit.collider.gameObject);
                        }
                    }
                }
                _attackedEnemies.Clear();
            }
        }
        /// <summary>
        /// プレイヤーのインタラクト
        /// </summary>
        private void PlayerInteract()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                //searchRadiusの範囲内のコライダーを取得
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, SearchRadius);

                //最も近いオブジェクトを探す
                Collider closestObject = null;
                //最も近い距離を初期化(ようわからん)
                float closestDistance = Mathf.Infinity;

                //forEachでコライダーを検索していく
                foreach (var hitCollider in hitColliders)
                {
                    //タグ:Interactableがついているかどうか
                    if (hitCollider.CompareTag("Interactable"))
                    {
                        //距離計算
                        float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            //最も近いコライダーを保存
                            closestObject = hitCollider;
                        }
                    }
                }
                if (!closestObject) return;
                // InterfaceScriptを実装しているコンポーネントを探す
                IInterface interactable = closestObject.GetComponent<IInterface>();
                if (interactable != null) interactable.Interact();
                else Debug.LogError($"{closestObject.gameObject.name} は IInteractable を実装していません！");
            }
        }
        
        // IPlayerDamageable 実装
        public void TakeDamage(int damage, List<ElementType> elementType)
        {
            _characterRuntimeData.CurrentHealth -= damage;

            characterManager.UpdateCharacterHealth(
                _characterRuntimeData.CharacterId, 
                _characterRuntimeData.CurrentHealth
            );

            Debug.Log($"Player took {damage} damage from {elementType}! HP: {_characterRuntimeData.CurrentHealth}");
            
            if (_characterRuntimeData.CurrentHealth <= 0)
            {
                characterManager.OnCharacterDeath(_characterRuntimeData.CharacterId);
                _characterRuntimeData = characterManager.GetActiveCharacter();
                
                if (_characterRuntimeData != null && characterManager.aliveCharacterIds.Count > 0)
                {
                    Debug.Log($"次のキャラに交代: {_characterRuntimeData.CharacterName}");
            
                    // TODO: キャラモデルの切り替え
                    // SwitchCharacterModel(_characterRuntimeData.CharacterId);
                }
                else
                {
                    Debug.LogError("全滅しました！");
                    // TODO: ゲームオーバー処理
                }
            }
        }

        public CharacterRuntimeData GetCharacterRuntimeData()
        {
            return _characterRuntimeData;
        }
    }
}