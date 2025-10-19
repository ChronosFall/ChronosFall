using IConnectComponent;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMove : MonoBehaviour
{
    [Header("カメラ")]
        private float _sensitivity = 1.0f;
        private float _maxLookAngleX = 55f;
    [Header("カメラピボット")] 
        private GameObject _cameraPivot;

    private float _currentX = 0f; // 現在の上下回転角度
    private float _currentY = 0f; // 現在の左右回転角度

    private void Start()
    {
        // カーソルをロックする
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (_maxLookAngleX <= 0f) _maxLookAngleX = 80f; // デフォルト値を設定

        // マウスの移動量を取得
        float rotateX = 0f - Input.GetAxis("Mouse Y") * _sensitivity;
        float rotateY = Input.GetAxis("Mouse X") * _sensitivity;

        // 累積回転角度を更新
        _currentX += rotateX;
        _currentY += rotateY;

        // 回転の制限を適用
        _currentX = Mathf.Clamp(_currentX, -_maxLookAngleX, _maxLookAngleX);

        // オイラー角で直接設定することでZ軸の回転を防ぐ
        _cameraPivot.transform.rotation = Quaternion.Euler(_currentX, _currentY, 0f);

        // ESCキーでカーソルロックを解除
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}