using UnityEngine;

public class GridMaskData : MonoBehaviour
{
    public Vector3 gridSize;
    public Vector3 worldSize;

    [SerializeField]
    private WorldMatrix _gridData;

    [SerializeField]
    private GridMaskDataTexture _gridTexture;

    private void Awake()
    {
        _gridData = new WorldMatrix(Mathf.CeilToInt(worldSize.x / gridSize.x), 
            Mathf.CeilToInt(worldSize.y / gridSize.y), 0);
        _gridTexture.Create(_gridData.width, _gridData.height, gridSize.x, gridSize.y);
    }

    private void Update()
    {
        _gridTexture.UpdateTexture(_gridData);

        if (Input.GetMouseButton(0))
        {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + worldSize * 0.5f;
            var x = Mathf.RoundToInt(position.x / gridSize.x);
            var y = Mathf.RoundToInt(position.y / gridSize.y);
            if (_gridData.IsInside(x, y))
                _gridData.StoreValue(1 << 0, x, y);
        }
        
        if (Input.GetMouseButton(1))
        {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + worldSize * 0.5f;
            var x = Mathf.RoundToInt(position.x / gridSize.x);
            var y = Mathf.RoundToInt(position.y / gridSize.y);
            if (_gridData.IsInside(x, y))
                _gridData.StoreValue(1 << 1, x, y);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + worldSize * 0.5f;
            var x = Mathf.RoundToInt(position.x / gridSize.x);
            var y = Mathf.RoundToInt(position.y / gridSize.y);
            if (_gridData.IsInside(x, y))
                _gridData.StoreValue(1 << 2, x, y);
        }

        
    }
}
