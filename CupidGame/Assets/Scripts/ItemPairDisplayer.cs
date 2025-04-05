using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPairDisplayer : MonoBehaviour
{
    public Image imageLeft;
    public Image imageRight;

    [System.Serializable]
    public struct ColorSprite
    {
        public Heart.HeartColor color;
        public Sprite sprite;
    }

    public List<ColorSprite> colorSpriteMap;

    private Dictionary<Heart.HeartColor, Sprite> spriteDict;

    void Awake()
    {
        // Create quick lookup from enum to sprite
        spriteDict = new();
        foreach (var entry in colorSpriteMap)
        {
            spriteDict[entry.color] = entry.sprite;
        }
    }

    void OnEnable()
    {
        // Optional: Set empty images initially
        imageLeft.sprite = null;
        imageRight.sprite = null;
    }

    public void DisplayOrder(List<Heart.HeartColor> order)
    {
        if (order.Count >= 2)
        {
            imageLeft.sprite = spriteDict[order[0]];
            imageRight.sprite = spriteDict[order[1]];
        }
        else
        {
            Debug.LogWarning("Order does not contain enough items to display.");
        }
    }
}