using JakesCommands.Core;

namespace JakesCommands.Core.Tests;

public class RotationHelperTests
{
    [Fact]
    public void ToRadians_ReturnsPiOver2_For90Degrees()
    {
        double radians = RotationHelper.ToRadians(RotationOption.Rotate90);
        Assert.Equal(Math.PI / 2d, radians, 8);
    }

    [Fact]
    public void ToRadians_ReturnsPi_For180Degrees()
    {
        double radians = RotationHelper.ToRadians(RotationOption.Rotate180);
        Assert.Equal(Math.PI, radians, 8);
    }

    [Theory]
    [InlineData(90, RotationOption.Rotate90)]
    [InlineData(180, RotationOption.Rotate180)]
    public void ParseDegrees_AcceptsSupportedValues(int degrees, RotationOption expected)
    {
        RotationOption parsed = RotationHelper.ParseDegrees(degrees);
        Assert.Equal(expected, parsed);
    }

    [Fact]
    public void ParseDegrees_ThrowsForUnsupportedValue()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => RotationHelper.ParseDegrees(270));
    }
}
