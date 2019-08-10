using UnityEngine;
using UnityEngine.UI;

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
        private Text _text;

        [SerializeField]
        private Transform _unit;
        
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
            
            var gridAreas = FindObjectsOfType<TestAreaObject>();
            
            for (var i = 0; i < _gridMaskData.gridData.width; i++)
            {
                for (var j = 0; j < _gridMaskData.gridData.height; j++)
                {
                    var position = _gridMaskData.GetWorldPosition(i, j);
                    foreach (var gridArea in gridAreas)
                    {
                        if (gridArea.mainCollider.OverlapPoint(position))
                        {
                            _gridMaskData.StoreFlagValue((int) gridArea.gameArea, position);
                        }
                    }
                }
            }
            
            if (Input.GetMouseButton(0))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _gridMaskData.StoreFlagValue((int) TestGameArea.Area1, position);
            }
        
            if (Input.GetMouseButton(1))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _gridMaskData.StoreFlagValue((int) TestGameArea.Area2, position);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _gridMaskData.StoreFlagValue((int) TestGameArea.Area3, position);
            }

            var gridValue = (TestGameArea) _gridMaskData.GetValue(_unit.position);

            _text.text = $"Current: {gridValue}";

        }

        private void LateUpdate()
        {
            _gridTexture.UpdateTexture(_gridMaskData.gridData);
        }
    }
}