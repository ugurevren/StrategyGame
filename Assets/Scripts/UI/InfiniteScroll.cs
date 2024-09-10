using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private List<GameObject> itemPrefabs;
    [SerializeField] private float itemHeight = 100f; 
    [SerializeField] private int bufferSize = 10; 
    private List<GameObject> items = new List<GameObject>();
    private int totalDataCount = 100;
    [SerializeField] private float _spacing = 10f; 
    [SerializeField] private float _xOffset = 0f; 

    private float scrollStartPos;
    private float itemHeightWithSpacing;
    private float contentHeight;

    private void Start()
    {
        itemHeightWithSpacing = itemHeight + _spacing;
        contentHeight = totalDataCount * itemHeightWithSpacing;
        InitializeItems();
        scrollStartPos = contentPanel.anchoredPosition.y;
    }

    private void Update()
    {
        HandleScrolling();
    }

    private void InitializeItems()
    {
        for (int i = 0; i < bufferSize; i++)
        {
            var newItem = Instantiate(GetPrefabForIndex(i), contentPanel);
            var itemRect = newItem.GetComponent<RectTransform>();
            var yPos = -i * itemHeightWithSpacing;
            itemRect.anchoredPosition = new Vector2(_xOffset, yPos);
            items.Add(newItem);
        }

        // Set the content size based on the number of items
        contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x, contentHeight);
    }

    private void HandleScrolling()
    {
        var scrollPosY = contentPanel.anchoredPosition.y;

        // Detect if we've scrolled beyond the buffer on the top or bottom
        if (scrollPosY - scrollStartPos > itemHeightWithSpacing)
        {
            scrollStartPos += itemHeightWithSpacing;
            RecycleItemFromTopToBottom();
        }
        else if (scrollPosY - scrollStartPos < -itemHeightWithSpacing)
        {
            scrollStartPos -= itemHeightWithSpacing;
            RecycleItemFromBottomToTop();
        }

        // Check if we need to add more items at the top or bottom
        if (scrollPosY - contentPanel.anchoredPosition.y > 0)
        {
            AddItemsAtTop();
        }
        else if (contentPanel.anchoredPosition.y - scrollPosY > 0)
        {
            AddItemsAtBottom();
        }
    }

    private void RecycleItemFromTopToBottom()
    {
        // Recycle top item to the bottom
        var topItem = items[0];
        items.RemoveAt(0);

        var topItemRect = topItem.GetComponent<RectTransform>();
        var newYPos = items[items.Count - 1].GetComponent<RectTransform>().anchoredPosition.y - itemHeightWithSpacing;
        topItemRect.anchoredPosition = new Vector2(_xOffset, newYPos);
        items.Add(topItem);
    }

    private void RecycleItemFromBottomToTop()
    {
        // Recycle bottom item to the top
        var bottomItem = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);

        var bottomItemRect = bottomItem.GetComponent<RectTransform>();
        var newYPos = items[0].GetComponent<RectTransform>().anchoredPosition.y + itemHeightWithSpacing;
        bottomItemRect.anchoredPosition = new Vector2(_xOffset, newYPos);
        items.Insert(0, bottomItem);
    }

    private void AddItemsAtTop()
    {
        // Add items to the top if there is space
        while (items[0].GetComponent<RectTransform>().anchoredPosition.y + contentPanel.rect.height < 0)
        {
            var newItem = Instantiate(GetPrefabForIndex(items.Count), contentPanel);
            var newItemRect = newItem.GetComponent<RectTransform>();
            var newYPos = items[0].GetComponent<RectTransform>().anchoredPosition.y + itemHeightWithSpacing;
            newItemRect.anchoredPosition = new Vector2(_xOffset, newYPos);
            items.Insert(0, newItem);
        }
    }

    private void AddItemsAtBottom()
    {
        // Add items to the bottom if there is space
        while (items[items.Count - 1].GetComponent<RectTransform>().anchoredPosition.y < -contentPanel.rect.height)
        {
            var newItem = Instantiate(GetPrefabForIndex(items.Count), contentPanel);
            var newItemRect = newItem.GetComponent<RectTransform>();
            var newYPos = items[items.Count - 1].GetComponent<RectTransform>().anchoredPosition.y - itemHeightWithSpacing;
            newItemRect.anchoredPosition = new Vector2(_xOffset, newYPos);
            items.Add(newItem);
        }
    }
    
    private GameObject GetPrefabForIndex(int index)
    {
        var prefabIndex = index % itemPrefabs.Count;
        return itemPrefabs[prefabIndex];
    }

}
