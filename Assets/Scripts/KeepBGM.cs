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
    // �Q�[����ʂɈڍs�����烁�j���[��ʂ�BGM�I�u�W�F�N�g��j�󂷂�
    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("���ӃL�������@���T�o���Q�[��"))
        {
            Destroy(gameObject);
        }
    }


    // �{�^������̂Ƃ��͂��̊֐��g��
    public void DestroySingleton()
    {
        Destroy(gameObject);
    }

}
