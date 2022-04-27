using System.Collections;
using UnityEngine;

public enum PlayerMovementState
{
    Walking = 0,
    Sneaking = 1,
    Running = 2
}

public class PlayerMovementController : MonoBehaviour
{
    #region [var]

    #region [01. ボタン押下判定関連]
    [SerializeField]
    private PlayerMovementState playerMovementState;
    
    /// <summary>
    /// 移動スピード
    /// </summary>
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float sneakingSpeed = .5f;
    [SerializeField]
    private float walkingSpeed = 1.5f;
    [SerializeField]
    private float runningSpeed = 3f;

    /// <summary>
    /// Pointer座標
    /// </summary>
    [SerializeField]
    private Transform pointer;
    #endregion
    
    #endregion

    #region [func]

    #region [00. コンストラクタ] 
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public void ActivePlayerMovement()
    {
        this.moveSpeed = walkingSpeed;
        this.playerMovementState = PlayerMovementState.Walking;
        
        // Pointer座標初期化
        this.pointer.position = this.transform.position;
        
        // 移動ボタン押下判定コルーチンの開始準備
        this.CatchPlayerMovementInputAsync();
        //
        this.CatchPlayerChangingSpeedInputAsync();
    }
    #endregion

    #region [01. 移動ボタン入力判定]
    /// <summary>
    /// 移動ボタン入力コルーチンの開始
    /// </summary>
    private void CatchPlayerMovementInputAsync()
    {
        // コルーチンスタート
        GlobalCoroutine.Play(this.CatchPlayerMovementInput(), "CatchPlayerMovementInput", null);
    }

    /// <summary>
    /// 移動入力判定コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator CatchPlayerMovementInput()
    {
        Debug.LogFormat($"Player Movement Input Activated", DColor.cyan);
        
        while (true)
        {
            // Player移動
            this.MoveToPoint(this.pointer.position);

            // 入力判定
            if (Vector3.Distance(transform.position, pointer.position) <= .05f)
            {
                // 横移動、縦移動
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    this.pointer.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    this.pointer.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
            
           //yield return null;
           yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// Player移動
    /// </summary>
    /// <param name="point"></param>
    private void MoveToPoint(Vector3 point)
    {
        // 移動
        this.transform.position =
            Vector3.MoveTowards(this.transform.position, point, moveSpeed * Time.deltaTime);
    }
    #endregion


    /// <summary>
    /// 移動速度変更ボタン入力コルーチンの開始
    /// </summary>
    private void CatchPlayerChangingSpeedInputAsync()
    {
        // コルーチンスタート
        GlobalCoroutine.Play(this.CatchPlayerChangingSpeedInput(), "CatchPlayerChangingSpeedInput", null);
    }
    
    /// <summary>
    /// 移動速度変更ボタン入力コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator CatchPlayerChangingSpeedInput()
    {
        Debug.LogFormat($"Player Changing Speed Input Activated", DColor.cyan);
        
        while (true)
        {
            // Running
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                this.moveSpeed = runningSpeed;
                this.playerMovementState = PlayerMovementState.Running;
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                this.moveSpeed = walkingSpeed;
                this.playerMovementState = PlayerMovementState.Walking;
            }
                
            // Sneaking
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                this.moveSpeed = sneakingSpeed;
                this.playerMovementState = PlayerMovementState.Sneaking;
            }
            if(Input.GetKeyUp(KeyCode.LeftControl))
            {
                this.moveSpeed = walkingSpeed;
                this.playerMovementState = PlayerMovementState.Walking;
            }

            yield return new WaitForFixedUpdate();
        }
    }
   
    #endregion
}
