using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.UI
{
    public class ResourceUICell : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI numberLbl;
        public void Fill(ResourceAsset resourceAsset)
        {
            icon.sprite = resourceAsset.icon;
            icon.SetNativeSize();
        }

        public void UpdateText(int number)
        {
            numberLbl.text = number.ToString();
        }
    }
}