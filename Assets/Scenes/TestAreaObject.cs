using System;
using UnityEngine;

namespace Gemserk.DataGrids
{
    [Flags]
    public enum TestGameArea
    {
        Area1 = 1 << 0,
        Area2 = 1 << 1,
        Area3 = 1 << 2
    }
    
    public class TestAreaObject : MonoBehaviour
    {
        public Collider2D mainCollider;

        public TestGameArea gameArea;
        
    }
}