using System;
using TMPro;
using UnityEngine;

namespace Helpers
{
    public class ChangeText: MonoBehaviour
    {
        [SerializeField] private string _text1;
        [SerializeField] private string _text2;
        private TextMeshProUGUI _text;
        private string _currentText;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _currentText = _text1;
            _text.text = _currentText;
        }

        public void Change()
        {
            _currentText = _currentText == _text1 ? _text2 : _text1;
            _text.text = _currentText;
        }
        
    }
    
}
