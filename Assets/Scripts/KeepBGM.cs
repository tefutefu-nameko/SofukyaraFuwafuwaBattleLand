using System.Collections;
using System.Collections.Generic;
//using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepBGM : MonoBehaviour
{
    public static KeepBGM instance;

    void Awake()
    {
        if(instance == null)
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


    // Update is called once per frame
    // ゲーム画面に移行したらメニュー画面のBGMオブジェクトを破壊する
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("そふキャラヴァンサバ風ゲーム"))
        {
            Destroy(gameObject);
        }
    }


    // ボタン操作のときはこの関数使う
    public void DestroySingleton()
    {
        Destroy(gameObject);
    }

}
