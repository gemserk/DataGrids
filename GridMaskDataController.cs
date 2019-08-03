using UnityEngine;

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

        [SerializeField]
        private bool _clearInUpdate;
        
        private void Awake()
        {
            _gridMaskData = new GridMaskData(worldSize, gridSize);
            _gridTexture = new GridMaskDataTexture(TextureFormat.RGBA32, _spriteRenderer, _colors, 
                _gridMaskData.gridData.width, _gridMaskData.gridData.height, gridSize.x, gridSize.y);

 
        }
        
        private void Update()
        {   
            if (_clearInUpdate)
                _gridMaskData.gridData.Clear();
            
            var colliders = FindObjectsOfType<Collider2D>();
            
            for (var i = 0; i < _gridMaskData.gridData.width; i++)
            {
                for (var j = 0; j < _gridMaskData.gridData.height; j++)
                {
                    var position = _gridMaskData.GetWorldPosition(i, j);
                    foreach (var collider in colliders)
                    {
                        if (collider.OverlapPoint(position))
                        {
                            _gridMaskData.StoreValue(collider.gameObject.layer, position);
                        }
                    }
                }
            }
            
            if (Input.GetMouseButton(0))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _gridMaskData.StoreValue(1 << 0, position);
            }
        
            if (Input.GetMouseButton(1))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _gridMaskData.StoreValue(1 << 1, position);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _gridMaskData.StoreValue(1 << 2, position);
            }
        }

        private void LateUpdate()
        {
            _gridTexture.UpdateTexture(_gridMaskData.gridData);
        }
    }
}