using ChronosFall.Scripts.Core.Configs;
using UnityEngine;

namespace ChronosFall.Scripts.Characters.Player.PlayerControls
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float walkSpeed = 2f;
        [SerializeField] private float dashSpeed = 5.3f;

        private Rigidbody _rb;
        private float _currentSpeed;
        private GameObject _cameraObject;
        private Vector2 _moveAxis;
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _cameraObject = UnityEngine.Camera.main?.gameObject;
        }
    
        private void FixedUpdate()
        {
            UpdateMovement();
        }
    
        /// <summary>
        /// 移動処理
        /// </summary>
        private void UpdateMovement()
        {
            var inputAxis = Vector2.zero;
            
            if (Input.GetKey(CharacterInputKey.WalkForward)) inputAxis.y += 1f;
            if (Input.GetKey(CharacterInputKey.WalkBack)) inputAxis.y -= 1f;
            if (Input.GetKey(CharacterInputKey.WalkLeft)) inputAxis.x -= 1f;
            if (Input.GetKey(CharacterInputKey.WalkRight)) inputAxis.x += 1f;
            
            _moveAxis = inputAxis.magnitude > 1f ? inputAxis.normalized : inputAxis;
        
            _currentSpeed = Input.GetKey(CharacterInputKey.MoveDash) ? dashSpeed : walkSpeed;

            // カメラの向きを取得
            float cameraRotationY = _cameraObject.transform.eulerAngles.y;

            // プレイヤーをカメラの向きに同期
            transform.rotation = Quaternion.Euler(0f, cameraRotationY, 0f);

            // カメラ基準の方向ベクトルを計算
            Vector3 cameraForward = GetCameraForward();
            Vector3 cameraRight = GetCameraRight();

            // 移動ベクトルを計算
            Vector3 moveDirection = (cameraForward * _moveAxis.y + cameraRight * _moveAxis.x).normalized;
            Vector3 moveVelocity = moveDirection * _currentSpeed;

            // Rigidbodyに適用 (Y軸の速度は保持)
            _rb.linearVelocity = new Vector3(moveVelocity.x, _rb.linearVelocity.y, moveVelocity.z);
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
