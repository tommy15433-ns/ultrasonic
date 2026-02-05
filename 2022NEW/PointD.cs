using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

using System.Drawing;
//
// Summary:
//     Represents an ordered pair of floating-point x- and y-coordinates that defines
//     a point in a two-dimensional plane.
[ComVisible(true)]
public struct PointD
{
    //
    // Summary:
    //     Represents a new instance of the System.Drawing.PointF class with member data
    //     left uninitialized.
    public static readonly PointD Empty;

    private double x;

    private double y;

    //
    // Summary:
    //     Gets a value indicating whether this System.Drawing.PointF is empty.
    //
    // Returns:
    //     true if both System.Drawing.PointF.X and System.Drawing.PointF.Y are 0; otherwise,
    //     false.
    [Browsable(false)]
    public bool IsEmpty
    {
        get
        {
            if (x == 0)
            {
                return y == 0;
            }

            return false;
        }
    }

    //
    // Summary:
    //     Gets or sets the x-coordinate of this System.Drawing.PointF.
    //
    // Returns:
    //     The x-coordinate of this System.Drawing.PointF.
    public double X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }

    //
    // Summary:
    //     Gets or sets the y-coordinate of this System.Drawing.PointF.
    //
    // Returns:
    //     The y-coordinate of this System.Drawing.PointF.
    public double Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }

    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.PointF class with the specified
    //     coordinates.
    //
    // Parameters:
    //   x:
    //     The horizontal position of the point.
    //
    //   y:
    //     The vertical position of the point.
    public PointD(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    //
    // Summary:
    //     Translates a System.Drawing.PointF by a given System.Drawing.Size.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     A System.Drawing.Size that specifies the pair of numbers to add to the coordinates
    //     of pt.
    //
    // Returns:
    //     Returns the translated System.Drawing.PointF.
    public static PointD operator +(PointD pt, Size sz)
    {
        return Add(pt, sz);
    }

    //
    // Summary:
    //     Translates a System.Drawing.PointF by the negative of a given System.Drawing.Size.
    //
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     The System.Drawing.Size that specifies the numbers to subtract from the coordinates
    //     of pt.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    public static PointD operator -(PointD pt, Size sz)
    {
        return Subtract(pt, sz);
    }

    //
    // Summary:
    //     Translates the System.Drawing.PointF by the specified System.Drawing.SizeF.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     The System.Drawing.SizeF that specifies the numbers to add to the x- and y-coordinates
    //     of the System.Drawing.PointF.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    public static PointD operator +(PointD pt, SizeF sz)
    {
        return Add(pt, sz);
    }

    //
    // Summary:
    //     Translates a System.Drawing.PointF by the negative of a specified System.Drawing.SizeF.
    //
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     The System.Drawing.SizeF that specifies the numbers to subtract from the coordinates
    //     of pt.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    public static PointD operator -(PointD pt, SizeF sz)
    {
        return Subtract(pt, sz);
    }

    //
    // Summary:
    //     Compares two System.Drawing.PointF structures. The result specifies whether the
    //     values of the System.Drawing.PointF.X and System.Drawing.PointF.Y properties
    //     of the two System.Drawing.PointF structures are equal.
    //
    // Parameters:
    //   left:
    //     A System.Drawing.PointF to compare.
    //
    //   right:
    //     A System.Drawing.PointF to compare.
    //
    // Returns:
    //     true if the System.Drawing.PointF.X and System.Drawing.PointF.Y values of the
    //     left and right System.Drawing.PointF structures are equal; otherwise, false.
    public static bool operator ==(PointD left, PointD right)
    {
        if (left.X == right.X)
        {
            return left.Y == right.Y;
        }

        return false;
    }

    //
    // Summary:
    //     Determines whether the coordinates of the specified points are not equal.
    //
    // Parameters:
    //   left:
    //     A System.Drawing.PointF to compare.
    //
    //   right:
    //     A System.Drawing.PointF to compare.
    //
    // Returns:
    //     true to indicate the System.Drawing.PointF.X and System.Drawing.PointF.Y values
    //     of left and right are not equal; otherwise, false.
    public static bool operator !=(PointD left, PointD right)
    {
        return !(left == right);
    }

    //
    // Summary:
    //     Translates a given System.Drawing.PointF by the specified System.Drawing.Size.
    //
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     The System.Drawing.Size that specifies the numbers to add to the coordinates
    //     of pt.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    public static PointD Add(PointD pt, Size sz)
    {
        return new PointD(pt.X + (double)sz.Width, pt.Y + (double)sz.Height);
    }
    public static PointD Add(PointD pt, SizeF sz)
    {
        return new PointD(pt.X + (double)sz.Width, pt.Y + (double)sz.Height);
    }


    //
    // Summary:
    //     Translates a System.Drawing.PointF by the negative of a specified size.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     The System.Drawing.Size that specifies the numbers to subtract from the coordinates
    //     of pt.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    public static PointD Subtract(PointD pt, Size sz)
    {
        return new PointD(pt.X - (double)sz.Width, pt.Y - (double)sz.Height);
    }


    //
    // Summary:
    //     Translates a System.Drawing.PointF by the negative of a specified size.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     The System.Drawing.SizeF that specifies the numbers to subtract from the coordinates
    //     of pt.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    public static PointD Subtract(PointD pt, SizeF sz)
    {
        return new PointD(pt.X - sz.Width, pt.Y - sz.Height);
    }

    //
    // Summary:
    //     Specifies whether this System.Drawing.PointF contains the same coordinates as
    //     the specified System.Object.
    //
    // Parameters:
    //   obj:
    //     The System.Object to test.
    //
    // Returns:
    //     This method returns true if obj is a System.Drawing.PointF and has the same coordinates
    //     as this System.Drawing.Point.
    public override bool Equals(object obj)
    {
        if (!(obj is PointD pointD))
        {
            return false;
        }

        if (pointD.X == X && pointD.Y == Y)
        {
            return pointD.GetType().Equals(GetType());
        }

        return false;
    }

    //
    // Summary:
    //     Returns a hash code for this System.Drawing.PointF structure.
    //
    // Returns:
    //     An integer value that specifies a hash value for this System.Drawing.PointF structure.
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    //
    // Summary:
    //     Converts this System.Drawing.PointF to a human readable string.
    //
    // Returns:
    //     A string that represents this System.Drawing.PointF.
    public override string ToString()
    {
        return string.Format(CultureInfo.CurrentCulture, "{{X={0}, Y={1}}}", new object[2] { x, y });
    }
}

