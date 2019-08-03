using UnityEngine;

namespace Gemserk.DataGrids
{
    public class GridMaskData : MonoBehaviour
    {
        public Vector3 gridSize;
        public Vector3 worldSize;

        [SerializeField]
        private GridData _gridData;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Color[] _colors;

        private GridMaskDataTexture _gridTexture;

        public void StoreValue(int value, int x, int y)
        {
            if (!_gridData.IsInside(x, y)) 
                return;
            _gridData.StoreValue(value, x, y);
        }

        private void Awake()
        {
            _gridData = new GridData(Mathf.CeilToInt(worldSize.x / gridSize.x), 
                Mathf.CeilToInt(worldSize.y / gridSize.y), 0);
            
            _gridTexture = new GridMaskDataTexture(TextureFormat.RGBA32, _spriteRenderer, _colors, 
                _gridData.width, _gridData.height, gridSize.x, gridSize.y);
        }
        
        private void LateUpdate()
        {
            _gridTexture.UpdateTexture(_gridData);
        }
    }
}
