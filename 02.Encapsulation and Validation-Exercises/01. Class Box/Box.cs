using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        set
        {this.length = value;}
    }

    public decimal Width
    {
        get { return this.width; }
        set
        { this.width = value; }
    }

    public decimal Height
    {
        get { return this.height; }
        set
        {this.height = value;}
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