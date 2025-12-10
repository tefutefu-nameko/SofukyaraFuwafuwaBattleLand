using UnityEngine;

public interface IDamageTextService
{
    void GenerateFloatingText(string text, Transform target, float duration = 1f, float speed = 1f);
}

