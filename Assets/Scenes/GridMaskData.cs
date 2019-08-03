using UnityEngine;

public class GridMaskData : MonoBehaviour
{
    public Vector2 gridSize;
    public Vector2 worldSize;

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
    }
}
