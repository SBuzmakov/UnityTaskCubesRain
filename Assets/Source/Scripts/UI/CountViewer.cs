using System.Globalization;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class CountViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countText;

        public void SetCountView(int count)
        {
            _countText.text = $"{count.ToString(CultureInfo.CurrentCulture)}";
        }
    }
}