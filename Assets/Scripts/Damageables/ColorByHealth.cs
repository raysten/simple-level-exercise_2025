using UnityEngine;

namespace Damageables
{
    public class ColorByHealth : MonoBehaviour
    {
        private static readonly int _color = Shader.PropertyToID("_BaseColor");
        
        [SerializeField]
        private Color _fullHealthColor = Color.green;
        
        [SerializeField]
        private Color _halfDeadColor = Color.yellow;
        
        [SerializeField]
        private Color _deadColor = Color.red;
        
        [SerializeField]
        private Renderer _renderer;
        
        private MaterialPropertyBlock _materialPropertyBlock;

        private void Reset()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Awake()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
            ChangeColor(1f);
        }

        public void ChangeColor(float healthFraction)
        {
            var newColor = CalculateNewColor(healthFraction);
            ChangeColor(newColor);
        }

        private Color CalculateNewColor(float healthFraction)
        {
            const float halfColorRange = 0.5f;
            Color newColor;
            
            if (healthFraction > halfColorRange)
            {
                newColor = CalculateColorBetweenFullAndHalf(healthFraction, halfColorRange);
            }
            else
            {
                newColor = CalculateColorBetweenFullAndDead(healthFraction, halfColorRange);
            }

            return newColor;
        }

        private Color CalculateColorBetweenFullAndHalf(float healthFraction, float halfColorRange)
        {
            var interpolationFactor = (healthFraction - halfColorRange) / halfColorRange;
            return Color.Lerp(_halfDeadColor, _fullHealthColor, interpolationFactor);
        }

        private Color CalculateColorBetweenFullAndDead(float healthFraction, float halfColorRange)
        {
            var interpolationFactor = healthFraction / halfColorRange;
            return Color.Lerp(_deadColor, _halfDeadColor, interpolationFactor);
        }

        private void ChangeColor(Color color)
        {
            _renderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetColor(_color, color);
            _renderer.SetPropertyBlock(_materialPropertyBlock);
        }
    }
}
