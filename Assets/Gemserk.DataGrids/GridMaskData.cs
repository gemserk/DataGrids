using UnityEngine;

namespace Gemserk.DataGrids
{
    public class GridMaskData
    {
        private readonly Vector3 gridSize;
        private readonly Vector3 worldSize;
        
        // world to grid conversion

        public GridData gridData;

        public void StoreValue(int value, Vector2 position)
        {
            var x = Mathf.RoundToInt((position.x + worldSize.x * 0.5f) / gridSize.x);
            var y = Mathf.RoundToInt((position.y + worldSize.y * 0.5f) / gridSize.y);

            if (!gridData.IsInside(x, y)) 
                return;
            gridData.StoreValue(value, x, y);
        }

        public GridMaskData(Vector2 worldSize, Vector2 gridSize)
        {
            this.gridSize = gridSize;
            this.worldSize = worldSize;
            
            gridData = new GridData(Mathf.CeilToInt(worldSize.x / gridSize.x), 
                Mathf.CeilToInt(worldSize.y / gridSize.y), 0);
        }
        
    }
}
