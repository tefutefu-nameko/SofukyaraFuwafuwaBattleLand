using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepBGM : MonoBehaviour
{
    public static KeepBGM instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("EXTRA " + this + " DELETED");
            Destroy(gameObject);
        }
    }

    // ゲームオーバーやタイトルメニューなど、特定のシーンでBGMオブジェクトを破棄する
    void Update()
    {
        // 以下の "ここにシーン名を入力" の部分を、BGMを止めたい実際のシーン名に書き換えてください
        if (SceneManager.GetActiveScene().name.Equals("そふキャラヴァンサバ風ゲーム"))
        {
            Destroy(gameObject);
        }
    }

    // ボタン操作などで外部からこのオブジェクトを破棄する際に使う
    public void DestroySingleton()
    {
        Destroy(gameObject);
    }
}