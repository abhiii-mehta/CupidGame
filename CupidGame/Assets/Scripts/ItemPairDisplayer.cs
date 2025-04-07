using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPairDisplayer : MonoBehaviour
{
    public Image imageLeft;
    public Image imageRight;

    [System.Serializable]
    public struct VariantSprite
    {
        public Heart.HeartColor color;
        public Heart.MaskType mask;
        public Sprite sprite;
    }

    public List<VariantSprite> variantSpriteMap;
    private Dictionary<string, Sprite> spriteDict;

    void Awake()
    {
        // Map each color + mask combo to a sprite
        spriteDict = new();
        foreach (var entry in variantSpriteMap)
        {
            string key = GetKey(entry.color, entry.mask);
            spriteDict[key] = entry.sprite;
        }
    }

    void OnEnable()
    {
        imageLeft.sprite = null;
        imageRight.sprite = null;
    }

    string GetKey(Heart.HeartColor color, Heart.MaskType mask)
    {
        return $"{color}_{mask}";
    }

    public void DisplayOrder(List<OrderManager.HeartVariant> order)
    {
        if (order.Count >= 2)
        {
            imageLeft.sprite = spriteDict[GetKey(order[0].color, order[0].mask)];
            imageRight.sprite = spriteDict[GetKey(order[1].color, order[1].mask)];
        }
        else
        {
            Debug.LogWarning("Order does not contain enough items to display.");
        }
    }
}
