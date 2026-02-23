using Microsoft.Xna.Framework;

namespace TwentyGameChallenge.Core;

public static class VectorExtension
{
    #region Vector2
    public static Vector3 ToVector3(this Vector2 vec)
    {
        return new Vector3(vec.X, vec.Y, 0);
    }

    public static Vector4 ToVector4(this Vector2 vec)
    {
        return new Vector4(vec.X, vec.Y, 0, 0);
    }
    #endregion
    
    #region Vector3
    public static Vector2 ToVector2(this Vector3 vec)
    {
        return new Vector2(vec.X, vec.Y);
    }

    public static Vector4 ToVector3(this Vector3 vec)
    {
        return new Vector4(vec.X, vec.Y, vec.Z, 0);
    }
    #endregion

    #region Vector4

    public static Vector2 ToVector2(this Vector4 vec)
    {
        return new Vector2(vec.X, vec.Y);
    }
    
    public static Vector3 ToVector3(this Vector4 vec)
    {
        return new Vector3(vec.X, vec.Y, vec.Z);
    }
    #endregion
}