using UnityEngine;

namespace Assets.Scripts.Tools
{
    public static class ExtendedClass
    {
        public static Vector2 Project(this Vector2 vector, Vector2 onNormal)
        {
            float num = Vector2.Dot(onNormal, onNormal);
            if (num < Mathf.Epsilon)
            {
                return Vector2.zero;
            }

            float num2 = Vector2.Dot(vector, onNormal);
            return new Vector2(onNormal.x * num2 / num, onNormal.y * num2 / num);
        }
    }
}
