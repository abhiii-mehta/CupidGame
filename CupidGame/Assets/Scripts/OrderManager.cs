using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    public int orderSize = 2;
    public int startingArrows = 10;

    private List<HeartVariant> currentOrder = new();
    private List<HeartVariant> collectedHearts = new();
    private int currentArrows;
    private int score = 0;
    private bool gameEnded = false;

    public struct HeartVariant
    {
        public Heart.HeartColor color;
        public Heart.MaskType mask;

        public HeartVariant(Heart.HeartColor c, Heart.MaskType m)
        {
            color = c;
            mask = m;
        }

        public override string ToString()
        {
            return color + " + " + mask;
        }

        public bool Equals(HeartVariant other)
        {
            return this.color == other.color && this.mask == other.mask;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        score = 0;
        currentArrows = startingArrows;
        GameUIManager.Instance.UpdateScore(score);
        GameUIManager.Instance.UpdateArrows(currentArrows);
        GenerateNewOrder();
    }

    public void GenerateNewOrder()
    {
        collectedHearts.Clear();
        currentOrder.Clear();
        this.gameEnded = false;

        int safety = 0;

        while (currentOrder.Count < orderSize && safety < 100)
        {
            Heart.HeartColor color = (Heart.HeartColor)Random.Range(0, 3);
            Heart.MaskType mask = (Heart.MaskType)Random.Range(0, 3);
            HeartVariant variant = new HeartVariant(color, mask);

            bool alreadyInOrder = currentOrder.Exists(h => h.Equals(variant));
            if (!alreadyInOrder)
                currentOrder.Add(variant);

            safety++;
        }

        string orderText = "New Order: ";
        for (int i = 0; i < currentOrder.Count; i++)
        {
            orderText += currentOrder[i].ToString();
            if (i < currentOrder.Count - 1) orderText += " | ";
        }

        Debug.Log(orderText);
        FindFirstObjectByType<ItemPairDisplayer>()?.DisplayOrder(currentOrder);
    }

    public void CollectHeart(Heart.HeartColor color, Heart.MaskType mask)
    {
        if (this.gameEnded) return;

        HeartVariant shot = new HeartVariant(color, mask);
        bool match = currentOrder.Exists(o => o.Equals(shot));

        if (!match)
        {
            if (!this.gameEnded)
            {
                Debug.Log("Wrong heart! You shot: " + shot);
                var player = PlayerProfileManager.Instance?.GetCurrentPlayer();
                if (player != null && score > player.highScore)
                {
                    player.highScore = score;
                    PlayerProfileManager.Instance.SavePlayer(player);
                    Debug.Log($"New high score saved: {score} for {player.playerName}");
                }


                this.gameEnded = true;
                FindFirstObjectByType<InGameMenuManager>()?.ShowGameOver();
            }
            return;
        }



        if (collectedHearts.Exists(h => h.Equals(shot)))
        {
            Debug.Log("Already collected: " + shot);
            return;
        }

        collectedHearts.Add(shot);
        Debug.Log("Collected: " + shot);

        if (collectedHearts.Count == currentOrder.Count)
        {
            score++;
            GameUIManager.Instance.UpdateScore(score);
            Debug.Log("Order complete! Score: " + score);
            GenerateNewOrder(); // Keep playing
        }
    }

    public void UseArrow()
    {
        if (this.gameEnded) return;

        currentArrows--;
        GameUIManager.Instance.UpdateArrows(currentArrows);

        if (currentArrows <= 0)
        {
            Debug.Log("Out of arrows!");
            var player = PlayerProfileManager.Instance.GetCurrentPlayer();
            if (player != null && score > player.highScore)
            {
                player.highScore = score;
                PlayerProfileManager.Instance.SavePlayer(player);
                Debug.Log($"New high score saved: {score} for {player.playerName}");
            }

            this.gameEnded = true;
            FindFirstObjectByType<InGameMenuManager>()?.ShowGameOver();
        }


    }

}

