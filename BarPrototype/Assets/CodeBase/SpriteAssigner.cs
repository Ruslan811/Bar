using System;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAssigner : MonoBehaviour
{
    [SerializeField] private Image targetImage; // Целевой объект, к которому будет применён спрайт
    [SerializeField] private Sprite[] sprites; // Массив спрайтов для выбора
    [SerializeField] private int index;
    

    private void OnEnable()
    {
        AssignRandomSprite();
    }

    private void AssignRandomSprite()
    {
        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogWarning("Sprite array is empty or null. Cannot assign a sprite.");
            return;
        }

        if (targetImage == null)
        {
            Debug.LogWarning("Target image is not assigned. Cannot assign a sprite.");
            return;
        }

        //int randomIndex = Random.Range(0, sprites.Length);
        targetImage.sprite = sprites[index];
    }
}
