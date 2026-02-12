namespace JakesCommands.Core;

public static class RotationHelper
{
    public static double ToRadians(RotationOption rotationOption)
    {
        return ((int)rotationOption) * (Math.PI / 180d);
    }

    public static RotationOption ParseDegrees(int degrees)
    {
        return degrees switch
        {
            90 => RotationOption.Rotate90,
            180 => RotationOption.Rotate180,
            _ => throw new ArgumentOutOfRangeException(nameof(degrees), degrees, "Only 90 or 180 degree rotations are supported.")
        };
    }
}
