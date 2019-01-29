using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMath : MonoBehaviour
{
    public static float LeftAngle180(Vector3 start, Vector3 end)
    {
        float leftAngle = Vector3.Angle(start, end);

        // xy 평면에 평행한 2D 이기 때문에 z값만 비교한다
        if (Vector3.Cross(start, end).z < 0)
        {
            leftAngle *= -1f;
        }

        return leftAngle;
    }

    public static float LeftAngle360(Vector3 start, Vector3 end)
    {
        float leftAngle = Vector3.Angle(start, end);

        // xy 평면에 평행한 2D 이기 때문에 z값만 비교한다
        if (Vector3.Cross(start, end).z < 0)
        {
            leftAngle = 360f - leftAngle;
        }

        return leftAngle;
    }
}
