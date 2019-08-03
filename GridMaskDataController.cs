using UnityEngine;
using UnityEngine.Serialization;

namespace Gemserk.DataGrids
{
    public class GridMaskDataController : MonoBehaviour
    {
        [SerializeField]
        private Vector3 gridSize;
        
        [SerializeField]
        private Vector3 worldSize;
        
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private Color[] _colors;

        private GridMaskData _gridMaskData;

        private GridMaskDataTexture _gridTexture;
        
        private void Awake()
        {
            _gridMaskData = new GridMaskData(worldSize, gridSize);
            _gridTexture = new GridMaskDataTexture(TextureFormat.RGBA32, _spriteRenderer, _colors, 
                _gridMaskData.gridData.width, _gridMaskData.gridData.height, gridSize.x, gridSize.y);
        }
        
        private void Update()
        {   
            if (Input.GetMouseButton(0))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + worldSize * 0.5f;
                var x = Mathf.RoundToInt(position.x / gridSize.x);
                var y = Mathf.RoundToInt(position.y / gridSize.y);
                _gridMaskData.StoreValue(1 << 0, x, y);
            }
        
            if (Input.GetMouseButton(1))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + worldSize * 0.5f;
                var x = Mathf.RoundToInt(position.x / gridSize.x);
                var y = Mathf.RoundToInt(position.y / gridSize.y);
                _gridMaskData.StoreValue(1 << 1, x, y);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + worldSize * 0.5f;
                var x = Mathf.RoundToInt(position.x / gridSize.x);
                var y = Mathf.RoundToInt(position.y / gridSize.y);
                _gridMaskData.StoreValue(1 << 2, x, y);
            }
        }

        private void LateUpdate()
        {
            _gridTexture.UpdateTexture(_gridMaskData.gridData);
        }
    }
}