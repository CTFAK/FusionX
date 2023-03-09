namespace FusionX;

public static class MathHelper
{
    private const float degToRad = 0.017453292519943295f;
    public static float SinDeg(double deg)
    {
        deg = (0 + (deg % 360) + 360) % 360;
        return (float)Math.Sin(deg * degToRad);
    }
}