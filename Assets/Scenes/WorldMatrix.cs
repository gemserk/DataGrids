using System;

public struct WorldMatrix
{
    public int width;
    public int height;

    public int[] values;
        
    public WorldMatrix(int width, int height, int value)
    {
        this.width = width;
        this.height = height;

        var length = width * height;
			
        values = new int[length];
            
        for (var i = 0; i < width * height; i++)
        {
            values[i] = value;
        }
    }
    
    public bool IsInside(int i, int j)
    {
        return i >= 0 && i < width && j >= 0 && j < height;
    }

    public void StoreValue(int value, int i, int j)
    {
        values[i + j * width] |= value;
    }

    public bool IsValue(int value, int i, int j)
    {
        return (values[i + j * width] & value) > 0;
    }
		
    public bool IsValue(int value, int i)
    {
        return (values[i] & value) > 0;
    }

    public void ClearValues()
    {
        Array.Clear(values, 0, values.Length);
    }

    public void Clear()
    {
        Array.Clear(values, 0, values.Length);
    }

}