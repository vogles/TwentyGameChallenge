using System;
using Microsoft.Xna.Framework;

namespace TwentyGameChallenge.Core;

public class Transform : IComponent
{
    public Vector3 WorldPosition { get; set; } = Vector3.Zero;
    public Vector3 WorldScale { get; set; } = Vector3.One;
    public Quaternion WorldRotation { get; set; } = Quaternion.Identity;
    
    public GameObject GameObject { get; set; }
    
    public Matrix WorldMatrix =>
        Matrix.CreateScale(WorldScale) * 
        Matrix.CreateFromQuaternion(WorldRotation) *
        Matrix.CreateTranslation(WorldPosition);
    
    public Transform Translate(float x, float y, float z)
    {
        return Translate(new Vector3(x, y, z));
    }
    
    public Transform Translate(Vector3 translation)
    {
        WorldPosition += translation;
        return this;
    }
    
    public Transform Scale(float x, float y, float z)
    {
        return Scale(new Vector3(x, y, z));
    }
    
    public Transform Scale(Vector3 scale)
    {
        WorldScale = scale;
        return this;
    }
    
    public Transform Rotate(Vector3 eulerAngles)
    {
        var yaw = ToRadians(eulerAngles.Y);
        var pitch = ToRadians(eulerAngles.X);
        var roll = ToRadians(eulerAngles.Z);

        var rotation = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
        WorldRotation *= Quaternion.Inverse(WorldRotation) * rotation * WorldRotation;
     
        return this;
    }

    public void RotateAlongAxis(Vector3 axis, float angle)
    {
        var rotation = Quaternion.CreateFromAxisAngle(axis, angle);
        
        WorldRotation *= Quaternion.Inverse(WorldRotation) * rotation * WorldRotation;
    }

    public Vector3 RotationInEulerAngles()
    {
        var rotationMatrix = Matrix.CreateFromQuaternion(WorldRotation);

        var pitch = Math.Asin(-rotationMatrix.M32);
        var yaw = 0.0;
        var roll = 0.0;

        if (Math.Abs(rotationMatrix.M32) < 0.9999)
        {
            yaw = Math.Atan2(rotationMatrix.M31, rotationMatrix.M33);
            roll = Math.Atan2(rotationMatrix.M12, rotationMatrix.M22);
        }
        else
        {
            yaw = Math.Atan2(-rotationMatrix.M13, rotationMatrix.M11);
            roll = 0f;
        }

        return new Vector3(ToDegrees((float)pitch), ToDegrees((float)yaw), ToDegrees((float)roll));
    }

    private float ToDegrees(float radians)
    {
        return radians * (180f / MathF.PI);
    }

    private float ToRadians(float degrees)
    {
        return degrees * (MathF.PI / 180f);
    }
}