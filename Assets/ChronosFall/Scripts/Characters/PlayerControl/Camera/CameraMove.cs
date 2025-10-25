using UnityEngine;

namespace ChronosFall.Scripts.Characters.PlayerControl.Camera
{
    public class CameraMove : MonoBehaviour
    {
        // カメラから見て上下 : X軸回転（ピッチ） | 左右 : Y軸回転（ヨー）
        [Header("カメラ")]
        public float sensitivityX = 1.0f;
        public float sensitivityY = 1.0f;
        private float _maxLookAngleX = 55f;
        [Header("カメラピボット")] [SerializeField] private GameObject _cameraPivot;
        private bool _isLockCursor; // クリックのロック

        private float _currentX; // 現在の上下回転角度
        private float _currentY; // 現在の左右回転角度

        private void Start()
        {
            // カーソルをロックする
            Cursor.lockState = CursorLockMode.Locked;
            _isLockCursor = true;
            // カメラの中心を固定
            _cameraPivot =  GameObject.Find("PlayerCameraPivot");
        }

        private void Update()
        {
            if (_maxLookAngleX <= 0f) _maxLookAngleX = 80f; // デフォルト値を設定

            // マウスの移動量を取得
            float rotateX = 0f - Input.GetAxis("Mouse Y") * sensitivityX;
            float rotateY = Input.GetAxis("Mouse X") * sensitivityY;

            // 累積回転角度を更新
            _currentX += rotateX;
            _currentY += rotateY;

            // 回転の制限を適用
            _currentX = Mathf.Clamp(_currentX, -_maxLookAngleX, _maxLookAngleX);

            // オイラー角で直接設定することでZ軸の回転を防ぐ
            _cameraPivot.transform.rotation = Quaternion.Euler(_currentX, _currentY, 0f);

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