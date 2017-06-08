using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * シーン管理クラスのベースクラス
 * シーンスクリプトは、このスクリプトを継承して作成する
 */
public class BaseScene : ScriptableObject {
    /**
     * シーンに切り替わった際に呼び出される初期化関数
     */
    public virtual void Init()
    {
    }

    /**
     * このシーン中、Updateの最初に呼び出される処理
     */
    public virtual void Update()
    {
    }

    /**
     * シーンが切り変わる時に行う解放処理
     */
    public virtual void Destroy()
    {
    }
}
