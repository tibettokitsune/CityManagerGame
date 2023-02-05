using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.GamePlay.UI
{
    public class BuildingJoystickCell : MonoBehaviour
    {
        public ReactiveCommand OnChoose { get; } = new ReactiveCommand();
        
        [SerializeField] private Image icon;
        [SerializeField] private Image backgroundFillCircle;
        [SerializeField] private RectTransform interactiveRoot;
        [SerializeField] private TextMeshProUGUI lbl;
        [SerializeField] private float radius;


        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private ResourceUICell _resourceCellPrefab;

        private const float PriceLengthScale = 0.7f;
        public bool IsActive { get; private set; }
        public void Fill(float rotationAngle, float widthAngle, Sprite sprite, string name, Price[] prices)
        {
            icon.sprite = sprite;
            icon.SetNativeSize();
            lbl.text = name;
            backgroundFillCircle.transform.eulerAngles = new Vector3(0, 0, -rotationAngle);
            backgroundFillCircle.fillAmount = widthAngle / 360f;
            
            var radians = -(widthAngle / 2f + rotationAngle + 90) * Mathf.Deg2Rad;
            var x = Mathf.Cos(radians);
            var y = Mathf.Sin(radians);
            
            interactiveRoot.anchoredPosition = new Vector2(x, y) * radius;

            
            for (int i = 0; i < prices.Length; i++)
            {
                var cell = Instantiate(_resourceCellPrefab, interactiveRoot.transform.parent);
                
                cell.Fill(prices[i].resourceAsset);
                cell.UpdateText(prices[i].number);

                var rt = (RectTransform) cell.transform;

                rt.anchoredPosition = interactiveRoot.anchoredPosition + new Vector2(x, y) * radius +
                                      Vector2.down * i * (float)prices.Length / 2f * PriceLengthScale;
            }
        }

        public void Scale(float scale, RectTransform mousePointer, float impactRadius)
        {
            var evaluation = (interactiveRoot.position - mousePointer.position).magnitude / impactRadius;
            var interactiveScale = Mathf.Clamp( _curve.Evaluate(evaluation) * scale, 1f, scale);
            interactiveRoot.transform.localScale = Vector3.one * interactiveScale;

            IsActive = evaluation < 0.5f;
        }
    }
}