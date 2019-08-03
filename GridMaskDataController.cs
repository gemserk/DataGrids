using UnityEngine;

namespace Gemserk.DataGrids
{
    public class GridMaskDataController : MonoBehaviour
    {
        [SerializeField]
        private GridMaskData _gridData;

//        private GridMaskDataTexture _gridTexture;

//        private void Awake()
//        {
//            var worldSize = _gridData.worldSize;
//            
//            _gridTexture = new GridMaskDataTexture(TextureFormat.RGBA32, _spriteRenderer, _colors, 
//                _gridData.width, _gridData.height, gridSize.x, gridSize.y);
//        }
        
        private void Update()
        {
            var worldSize = _gridData.worldSize;
            var gridSize = _gridData.gridSize;
            
            if (Input.GetMouseButton(0))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + worldSize * 0.5f;
                var x = Mathf.RoundToInt(position.x / gridSize.x);
                var y = Mathf.RoundToInt(position.y / gridSize.y);
                _gridData.StoreValue(1 << 0, x, y);
            }
        
            if (Input.GetMouseButton(1))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + worldSize * 0.5f;
                var x = Mathf.RoundToInt(position.x / gridSize.x);
                var y = Mathf.RoundToInt(position.y / gridSize.y);
                _gridData.StoreValue(1 << 1, x, y);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + worldSize * 0.5f;
                var x = Mathf.RoundToInt(position.x / gridSize.x);
                var y = Mathf.RoundToInt(position.y / gridSize.y);
                _gridData.StoreValue(1 << 2, x, y);
            }
        }

    }
}