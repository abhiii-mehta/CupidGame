using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    public int orderSize = 2;
    public int totalHeartTypes = 3; // You can increase this as you add more hearts

    public List<Heart.HeartColor> currentOrder = new();
    private List<Heart.HeartColor> collectedHearts = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Debug.Log("OrderManager started");
        GenerateNewOrder();
    }

    public void GenerateNewOrder()
    {
        collectedHearts.Clear();
        currentOrder.Clear();

        int safety = 0;

        while (currentOrder.Count < orderSize && safety < 100)
        {
            Heart.HeartColor randomColor = (Heart.HeartColor)Random.Range(0, totalHeartTypes);
            Debug.Log("Trying to add: " + randomColor);

            if (!currentOrder.Contains(randomColor))
                currentOrder.Add(randomColor);

            safety++;
        }

        Debug.Log("New Order: " + string.Join(" + ", currentOrder));
    }

    public void CollectHeart(Heart.HeartColor color)
    {
        if (!currentOrder.Contains(color))
        {
            Debug.Log("Wrong heart! You shot: " + color + ". Expected: " + string.Join(" + ", currentOrder));
            FindFirstObjectByType<InGameMenuManager>().ShowGameOver();
            return;
        }

        if (collectedHearts.Contains(color))
        {
            Debug.Log("Already collected: " + color);
            return;
        }

        collectedHearts.Add(color);
        Debug.Log("Collected: " + color);

        if (collectedHearts.Count == currentOrder.Count)
        {
            Debug.Log("Order complete! You win!");
            FindFirstObjectByType<InGameMenuManager>().ShowVictory();
        }
    }
}
