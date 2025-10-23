using UnityEngine;
using UnityEngine.Serialization;

namespace ChronosFall.Scripts.Characters.PlayerControl.Camera
{
    public class CameraMove : MonoBehaviour
    {
        // カメラから見て上下 : X軸回転（ピッチ） | 左右 : Y軸回転（ヨー）
        [Header("カメラ")] 
        public float sensitivityX = 1.0f; // 感度X
        public float sensitivityY = 1.0f; // 感度Y
        public float smoothTime = 0.05f;
        private float _maxLookAngleY = 55f; // カメラY座標軸の限度
        
        [Header("カメラピボット")]
        private GameObject _cameraPivot;
        private bool _isLockCursor; // クリックのロック

        private float _currentX = 0f; // 現在の上下回転角度
        private float _currentY = 0f; // 現在の左右回転角度
        // 補完用
        private float _smoothX, _smoothY;
        private float _velocityX, _velocityY;
        

        private void Start()
        {
            // カーソルをロックする
            Cursor.lockState = CursorLockMode.Locked;
            _isLockCursor = true;
            _cameraPivot =  GameObject.Find("PlayerCameraPivot");
        }

        private void Update()
        {
            if (_maxLookAngleY <= 0f) _maxLookAngleY = 80f; // デフォルト値を設定

            // マウスの移動量を取得
            float targetX = _currentX - Input.GetAxis("Mouse Y") * sensitivityX;
            float targetY = _currentY + Input.GetAxis("Mouse X") * sensitivityY;
            
            // Clampを使い上限を設定
            targetX = Mathf.Clamp(targetX, -_maxLookAngleY, _maxLookAngleY);
            
            // SmoothDampでゆっくり補完
            _currentX = Mathf.SmoothDamp(_currentX, targetX, ref _velocityX, smoothTime);
            _currentY = Mathf.SmoothDamp(_currentY, targetY, ref _velocityY, smoothTime);

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