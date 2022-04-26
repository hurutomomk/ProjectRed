using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // コルーチン
    private Coroutine coroutine;
    
    /// <summary>
    /// 
    /// </summary>
    public void ActivePlayerMovement()
    {
        this.CatchPlayerMovementInputAsync();
    }

    /// <summary>
    /// 移動ボタン押下判定コルーチンの開始準備
    /// </summary>
    private void CatchPlayerMovementInputAsync()
    {
        // if (this.coroutine != null)
        // {
        //     StopCoroutine(this.coroutine);
        // }
        //
        // // コルーチンスタート
        // this.coroutine = StartCoroutine(this.CatchPlayerMovementInput());

        // コルーチンスタート
        GlobalCoroutine.Play(this.CatchPlayerMovementInput(), "CatchPlayerMovementInput", null);
    }

    /// <summary>
    /// 移動ボタン押下判定コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator CatchPlayerMovementInput()
    {
        Debug.LogFormat($"Player Movement Activated", DColor.cyan);
        
        while (true)
        {
            
            yield return null;
        }
    }
}
