using UnityEngine;

namespace VG
{
    public static class Camera_Extensions
    {

        public static Vector2 GetWorldSize(this Camera camera)
        {
            Vector2 zeroPoint = camera.ViewportToWorldPoint(Vector3.zero);
            Vector2 onePoint = camera.ViewportToWorldPoint(Vector3.one);

            Vector2 size = new Vector2();
            size.x = Mathf.Abs(zeroPoint.x - onePoint.x);
            size.y = Mathf.Abs(zeroPoint.y - onePoint.y);

            return size;
        }

    }
}
