using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region [var]

    #region [01. ボタン押下判定関連]
    /// <summary>
    /// 移動スピード
    /// </summary>
    [SerializeField]
    private float moveSpeed = 5f;
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
        // Pointer座標初期化
        this.pointer.position = this.transform.position;
        
        // 移動ボタン押下判定コルーチンの開始準備
        this.CatchPlayerMovementInputAsync();
    }
    #endregion

    #region [01. 移動ボタン入力判定]
    /// <summary>
    /// 移動ボタン入力コルーチンの開始準備
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
        Debug.LogFormat($"Player Movement Activated", DColor.cyan);
        
        while (true)
        {
            // Player移動
            this.MoveToPoint(this.pointer.position);

            // 入力判定
            if (Vector3.Distance(transform.position, pointer.position) <= .05f)
            {
                // 横移動
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    this.pointer.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
                // 縦移動
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    this.pointer.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
            
            yield return null;
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
   
    #endregion
}
