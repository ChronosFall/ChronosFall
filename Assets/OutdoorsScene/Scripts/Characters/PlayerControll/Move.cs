using UnityEngine;
using GameAnimations;

public class Move : MonoBehaviour
{
    [Header("カメラ")] public GameObject cameraObject;
    [Header("プレイヤーの通常移動速度")] public float setMoveSpeed = 2f;
    [Header("プレイヤーのダッシュ移動速度")] public float setDashSpeed = 10f;
    [Header("プレイヤーの移動速度を保存")] public float moveCurrSpeed;
    [Header("プレイヤーの座標軸移動速度")] public Vector3 moveSpeedAxis;
    [Header("プレイヤーのRigidBody")] public Rigidbody playerRb;
    [Header("アニメーションに適用する移動速度")]public float totalSpeedAxis;

    [Header("アニメーターコンポーネント")] public Animator animator;

    [Header("DEBUG:ANIMATION_SPEED_AXIS")] public float animationSpeedAxisX;
    public float animationSpeedAxisY;
    public float cameraCurrRotateY;
    [Header("DEBUG:INPUT_KEY_ANIMATION")] public float inputHorizontal;
    public float inputVertical;

    void Start()
    {

        // アニメーターコンポーネント取得
        animator = GetComponent<Animator>();
        //プレイヤーのRigidbodyを取得
        playerRb = GetComponent<Rigidbody>();
        //最初に代入
        moveCurrSpeed = setMoveSpeed;
    }
    public void Update()
    {

        //カメラの向いてる向きを取得
        cameraCurrRotateY = cameraObject.transform.eulerAngles.y;
        //モデルにカメラの向いてる向きをY座標軸のみ同期
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, cameraCurrRotateY, transform.eulerAngles.z);

        //カメラに対して前と右を取得
        Vector3 cameraForward = Vector3.Scale(cameraObject.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(cameraObject.transform.right, new Vector3(1, 0, 1)).normalized;

        //moveVelocityを0で初期化する
        moveSpeedAxis = Vector3.zero;

        //移動入力
        if (Input.GetKey(KeyCode.W))
        {
            InputReset();
            moveSpeedAxis += moveCurrSpeed * cameraForward;
            inputVertical = 1f; // 前進入力
        }
        if (Input.GetKey(KeyCode.A))
        {
            InputReset();
            moveSpeedAxis -= moveCurrSpeed * cameraRight;
            inputHorizontal = -1f; // 左移動入力
        }
        if (Input.GetKey(KeyCode.S))
        {
            InputReset();
            moveSpeedAxis -= moveCurrSpeed * cameraForward;
            inputVertical = -1f; // 後退入力
        }
        if (Input.GetKey(KeyCode.D))
        {
            InputReset();
            moveSpeedAxis += moveCurrSpeed * cameraRight;
            inputHorizontal = 1f; // 右移動入力
        }
        //Left/Right Shiftキーでダッシュ
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveCurrSpeed = setDashSpeed;
            animator.speed = 2.0f; // アニメーション速度を倍速に変更
        }
        else
        {
            moveCurrSpeed = setDashSpeed;
            animator.speed = 1.0f; // アニメーション速度を通常に戻す
        }

        //移動メソッド
        ApplyForce();
    }
    /// <summary>
    /// 入力をリセットする
    /// </summary>
    void InputReset()
    {
        inputHorizontal = 0f;
        inputVertical = 0f;
    }
    /// <summary>
    /// 移動方向に力を加える（重力対応）
    /// </summary>
    void ApplyForce()
    {
        // 現在のY軸の速度を保存
        float currY = playerRb.linearVelocity.y;

        // X/Z軸の移動速度を適用
        playerRb.linearVelocity = new Vector3(moveSpeedAxis.x, currY, moveSpeedAxis.z);

        // アニメーション用のパラメータ計算
        if (animator && animator.runtimeAnimatorController)
        {
            float moveAmount = moveSpeedAxis.magnitude;

            // 動きがある場合（閾値を小さくして感度を上げる）
            if (moveAmount > 0.01f)
            {
                //アニメーション速度を計算
                animationSpeedAxisX = inputHorizontal;
                animationSpeedAxisY = inputVertical;

                animator.SetBool(PlayerMoveAnimator.IsWalking, true);
                animator.SetFloat(PlayerMoveAnimator.SpeedAxisX, animationSpeedAxisX);
                animator.SetFloat(PlayerMoveAnimator.SpeedAxisY, animationSpeedAxisY);
            }
        }
    }

}