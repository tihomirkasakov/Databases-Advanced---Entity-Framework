using System;

class Box
{
    private decimal length;
    private decimal width;
    private decimal height;

    public Box(decimal length, decimal width, decimal height)
    {
        this.Length = length;
        this.Width = width;
        this.Height = height;
    }

    public decimal Length
    {
        get { return this.length; }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Length cannot be zero or negative.");
            }
            this.length = value;
        }
    }

    public decimal Width
    {
        get { return this.width; }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Width cannot be zero or negative.");
            }

            this.width = value;
        }
    }

    public decimal Height
    {
        get { return this.height; }
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Height cannot be zero or negative.");
            }
            this.height = value;
        }
    }

    public decimal SurfaceArea()
    {
        decimal surfaceArea = 2 * this.length * this.width + 2 * this.length * this.height + 2 * this.width * this.height;
        return surfaceArea;
    }

    public decimal LateralSurfaceArea()
    {
        decimal lateralSurfaceArea = 2 * this.length * this.height + 2 * this.width * this.height;
        return lateralSurfaceArea;
    }

    public decimal Volume()
    {
        decimal volume = this.length * this.width * this.height;
        return volume;
    }
}