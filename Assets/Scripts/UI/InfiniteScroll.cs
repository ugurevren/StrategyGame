using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class InfiniteScroll : MonoBehaviour
    {
        //I didn't understand the revision "Infinite Scrollview should work properly with generally accepted aspect ratios."
        //because I tested it with all possible desktop aspect ratios and couldn't find any errors.
        //If the aspect ratio changes in runtime, it doesn't work,
        //but if the aspect ratio changes under normal conditions (before going into play mode) it works.
        
        [SerializeField] private RectTransform contentPanel;
        [SerializeField] private List<GameObject> itemPrefabs;
        [SerializeField] private float itemHeight = 100f; 
        [SerializeField] private int bufferSize = 10; 
        private List<GameObject> _items = new List<GameObject>();
        private int _totalDataCount = 100;
        [SerializeField] private float _spacing = 10f; 
        [SerializeField] private float _xOffset = 0f; 

        private float _scrollStartPos;
        private float _itemHeightWithSpacing;
        private float _contentHeight;

        private void Start()
        {
            _itemHeightWithSpacing = itemHeight + _spacing;
            _contentHeight = _totalDataCount * _itemHeightWithSpacing;
            InitializeItems();
            _scrollStartPos = contentPanel.anchoredPosition.y;
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
                var yPos = -i * _itemHeightWithSpacing;
                itemRect.anchoredPosition = new Vector2(_xOffset, yPos);
                _items.Add(newItem);
            }

            // Set the content size based on the number of items
            contentPanel.sizeDelta = new Vector2(contentPanel.sizeDelta.x, _contentHeight);
        }

        private void HandleScrolling()
        {
            var scrollPosY = contentPanel.anchoredPosition.y;

            // Detect if we've scrolled beyond the buffer on the top or bottom
            if (scrollPosY - _scrollStartPos > _itemHeightWithSpacing)
            {
                _scrollStartPos += _itemHeightWithSpacing;
                RecycleItemFromTopToBottom();
            }
            else if (scrollPosY - _scrollStartPos < -_itemHeightWithSpacing)
            {
                _scrollStartPos -= _itemHeightWithSpacing;
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
            var topItem = _items[0];
            _items.RemoveAt(0);

            var topItemRect = topItem.GetComponent<RectTransform>();
            var newYPos = _items[_items.Count - 1].GetComponent<RectTransform>().anchoredPosition.y - _itemHeightWithSpacing;
            topItemRect.anchoredPosition = new Vector2(_xOffset, newYPos);
            _items.Add(topItem);
        }

        private void RecycleItemFromBottomToTop()
        {
            // Recycle bottom item to the top
            var bottomItem = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);

            var bottomItemRect = bottomItem.GetComponent<RectTransform>();
            var newYPos = _items[0].GetComponent<RectTransform>().anchoredPosition.y + _itemHeightWithSpacing;
            bottomItemRect.anchoredPosition = new Vector2(_xOffset, newYPos);
            _items.Insert(0, bottomItem);
        }

        private void AddItemsAtTop()
        {
            // Add items to the top if there is space
            while (_items[0].GetComponent<RectTransform>().anchoredPosition.y + contentPanel.rect.height < 0)
            {
                var newItem = Instantiate(GetPrefabForIndex(_items.Count), contentPanel);
                var newItemRect = newItem.GetComponent<RectTransform>();
                var newYPos = _items[0].GetComponent<RectTransform>().anchoredPosition.y + _itemHeightWithSpacing;
                newItemRect.anchoredPosition = new Vector2(_xOffset, newYPos);
                _items.Insert(0, newItem);
            }
        }

        private void AddItemsAtBottom()
        {
            // Add items to the bottom if there is space
            while (_items[_items.Count - 1].GetComponent<RectTransform>().anchoredPosition.y < -contentPanel.rect.height)
            {
                var newItem = Instantiate(GetPrefabForIndex(_items.Count), contentPanel);
                var newItemRect = newItem.GetComponent<RectTransform>();
                var newYPos = _items[_items.Count - 1].GetComponent<RectTransform>().anchoredPosition.y - _itemHeightWithSpacing;
                newItemRect.anchoredPosition = new Vector2(_xOffset, newYPos);
                _items.Add(newItem);
            }
        }
    
        private GameObject GetPrefabForIndex(int index)
        {
            var prefabIndex = index % itemPrefabs.Count;
            return itemPrefabs[prefabIndex];
        }

    }
}
