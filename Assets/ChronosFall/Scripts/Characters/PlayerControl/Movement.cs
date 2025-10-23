using ChronosFall.Scripts.Interfaces;
using UnityEngine;


namespace ChronosFall.Scripts.Characters.PlayerControl
{
    public class Movement : MonoBehaviour
    {
        [Header("DEBUG")] [SerializeField] private bool tempStopAnimator;
        
        [Header("参照")] private GameObject _cameraObject;

        [Header("移動設定")] [SerializeField] private float walkSpeed = 2f;
        [SerializeField] private float dashSpeed = 10f;

        // コンポーネント
        private Rigidbody _rb;
        private Animator _animator;

        // 移動状態
        private Vector2 _inputAxis; // -1~1の入力値
        private float _currentSpeed;
        private bool _isDashing;

        private void Start()
        {
            InitializeComponents();
            _currentSpeed = walkSpeed;
        }

        private void Update()
        {
            ProcessInput();
            UpdateMovement();
            UpdateAnimation();
        }

        /// <summary>
        /// コンポーネントの初期化
        /// </summary>
        private void InitializeComponents()
        {
            _animator = Components.GetComponent<Animator>(gameObject);
            _rb = Components.GetComponent<Rigidbody>(gameObject);
			_cameraObject = UnityEngine.Camera.main?.gameObject; // カメラを検索
        }

        /// <summary>
        /// 入力処理
        /// </summary>
        private void ProcessInput()
        {
            // 入力をリセット
            _inputAxis = Vector2.zero;

            // WASD入力を取得
            if (Input.GetKey(KeyCode.W)) _inputAxis.y += 1f;
            if (Input.GetKey(KeyCode.S)) _inputAxis.y -= 1f;
            if (Input.GetKey(KeyCode.A)) _inputAxis.x -= 1f;
            if (Input.GetKey(KeyCode.D)) _inputAxis.x += 1f;
            // TODO : キーコンフィグ追加

            // 入力の正規化（斜め移動が速くならないように）
            if (_inputAxis.magnitude > 1f)
            {
                _inputAxis.Normalize();
            }

            // ダッシュ入力
            _isDashing = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            _currentSpeed = _isDashing ? dashSpeed : walkSpeed;
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

            // Rigidbodyに適用（Y軸の速度は保持）
            _rb.linearVelocity = new Vector3(moveVelocity.x, _rb.linearVelocity.y, moveVelocity.z);
        }

        /// <summary>
        /// アニメーション更新
        /// </summary>
        private void UpdateAnimation()
        {
            if (!_animator || !_animator.runtimeAnimatorController || tempStopAnimator) return;

            bool isMoving = _inputAxis.magnitude > 0.01f;

            if (isMoving)
            {
                // 移動アニメーション
                _animator.SetBool(PlayerMovementAnimator.IsWalking, true);
                _animator.SetFloat(PlayerMovementAnimator.SpeedAxisX, _inputAxis.x);
                _animator.SetFloat(PlayerMovementAnimator.SpeedAxisY, _inputAxis.y);

                // ダッシュ時はアニメーション速度を変更
                _animator.speed = _isDashing ? 2.0f : 1.0f;
            }
            else
            {
                // 停止アニメーション
                _animator.SetBool(PlayerMovementAnimator.IsWalking, false);
                _animator.SetFloat(PlayerMovementAnimator.SpeedAxisX, 0f);
                _animator.SetFloat(PlayerMovementAnimator.SpeedAxisY, 0f);
                _animator.speed = 1.0f;
            }
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
    }
}