using System;
using UnityEngine;

public class GridMaskDataTexture : MonoBehaviour
{
    [Flags]
    public enum TestDataEnum
    {
        Data0 = 1 << 0,
        Data1 = 1 << 1   
    }
    
    [SerializeField]
    protected TextureFormat _textureFormat;

    [SerializeField]
    protected SpriteRenderer _spriteRenderer;

    [NonSerialized]
    private Texture2D _texture;

    [NonSerialized]
    private Color[] _colors;
    
    [SerializeField] 
    protected Color _startColor = new Color(0, 0, 0, 1.0f);
    
    private const float _defaultInterpolationColorSpeed = 6.0f;
    
    [SerializeField]
    protected float _interpolateColorSpeed = _defaultInterpolationColorSpeed;

    public void Create(int width, int height, float gridWidth, float gridHeight)
    {
        _texture =  new Texture2D(width, height, _textureFormat, false, false);
        _texture.filterMode = FilterMode.Point;
        _texture.wrapMode = TextureWrapMode.Clamp;
		
        _spriteRenderer.sprite = Sprite.Create(_texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 1);
        _spriteRenderer.transform.localScale = new Vector3(gridWidth, gridHeight, 1.0f);
        
        _colors = new Color[width * height];
        
        for (var i = 0; i < width * height; i++)
        {
            _colors[i] = _startColor;
        }
        
        _texture.SetPixels(_colors);
        _texture.Apply();
    }

    private WorldMatrix _worldMatrix;
    
    private bool _dirty = true;

    public void UpdateTexture(WorldMatrix worldMatrix)
    {
        _worldMatrix = worldMatrix;
        _dirty = true;
    }
        
    private void LateUpdate()
    {
        if (!_dirty || _texture == null)
            return;
        
        _dirty = false;
        
        var interpolationEnabled = _interpolateColorSpeed > Mathf.Epsilon;
        var alpha = Time.deltaTime * _interpolateColorSpeed;
        
        var width = _worldMatrix.width;
        var height = _worldMatrix.height;

        for (var i = 0; i < width * height; i++)
        {
            // get coordinates...
            var x = i % width;
            var y = Mathf.FloorToInt(i / width);
                
            var newColor = _startColor;

            var isData0 = _worldMatrix.IsValue((int) TestDataEnum.Data0, i);
            var isData1 = _worldMatrix.IsValue((int) TestDataEnum.Data1, i);

            if (isData0)
            {
                newColor.r = 1;
            }

            if (isData1)
            {
                newColor.g = 1;
            }
            
            if (interpolationEnabled)
            {
                newColor.r = Mathf.LerpUnclamped(_colors[i].r, newColor.r, alpha);
            } 
            
            _colors[i] = newColor;
        }
        
        _texture.SetPixels(_colors);
        _texture.Apply();
    }

}