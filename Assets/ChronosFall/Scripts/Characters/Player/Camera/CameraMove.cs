using ChronosFall.Scripts.Configs;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChronosFall.Scripts.Characters.Player.Camera
{
    public class CameraMove : MonoBehaviour
    {
        // カメラから見て上下 : X軸回転（ピッチ） | 左右 : Y軸回転（ヨー）
        public float sensitivityX = 1.0f;
        public float sensitivityY = 1.0f; 
        [SerializeField] private GameObject cameraPivot;
        [SerializeField] public GameObject player;
        
        private const float MaxLookAngleX = 55f;
        private bool _isLockCursor; // クリックのロック
        private float _currentX; // 現在の上下回転角度
        private float _currentY; // 現在の左右回転角度
        private float _rotateX;
        private float _rotateY;

        private void Start()
        {
            // カーソルをロックする
            Cursor.lockState = CursorLockMode.Locked;
            _isLockCursor = true;
            // カメラの中心を固定
            cameraPivot =  GameObject.FindGameObjectWithTag("MainCameraPivot");
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            // マウス移動を逆転
            if (CCamera.CameraRotateInvert)
            {
                _rotateX = Input.GetAxis("Mouse X") * sensitivityX;
                _rotateY = Input.GetAxis("Mouse Y") * sensitivityY;
            }
            else
            {
                // マウスの移動量を取得
                _rotateX = 0f - Input.GetAxis("Mouse Y") * sensitivityX;
                _rotateY = Input.GetAxis("Mouse X") * sensitivityY;
            }

            // 累積回転角度を更新
            _currentX += _rotateX;
            _currentY += _rotateY;

            // 回転の制限を適用
            _currentX = Mathf.Clamp(_currentX, -MaxLookAngleX, MaxLookAngleX);

            // オイラー角で直接設定することでZ軸の回転を防ぐ
            cameraPivot.transform.rotation = Quaternion.Euler(_currentX, _currentY, 0f);
            cameraPivot.transform.position = player.transform.position;

            // ESCキーでカーソルロックを解除
            if (Input.GetKeyDown(KeyCode.Escape) && !_isLockCursor)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}