using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    PlayerStats player;

    // PlayerStats���p���I�ɍX�V
    void Update()
    {
        player = FindObjectOfType<PlayerStats>();
    }

    // Boss���j�ŃQ�[���I��
    private void OnDestroy()
    {
        Debug.LogWarning("Boss�̓V������");
        player.Kill();
    }
}
