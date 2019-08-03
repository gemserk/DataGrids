using UnityEngine;

namespace Gemserk.DataGrids
{
    public class GridMaskData
    {
        private Vector3 gridSize;
        private Vector3 worldSize;

        public GridData gridData;

        public void StoreValue(int value, int x, int y)
        {
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
