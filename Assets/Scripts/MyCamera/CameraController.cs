using UnityEngine;

// ReSharper disable PossibleNullReferenceException

namespace MyCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;

        public float smoothRange = 7.5f;

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        private void LateUpdate()
        {
            var position = transform.position;
            var targetPosition = target.position;
            position = Vector3.Lerp(position, new Vector3(targetPosition.x, targetPosition.y, position.z), smoothRange * Time.deltaTime);
            transform.position = position;
        }

        public static float Height => UnityEngine.Camera.main.orthographicSize * 2.0f;
        public static float Width => Height * Screen.width / Screen.height;
        public static Vector2 Center => Camera.main.transform.position;
        public static Vector2 LeftBottom => Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        public static Vector2 LeftTop => Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        public static Vector2 RightTop => Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        public static Vector2 RightBottom => Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane));

        public static bool IsVisible(Vector2 position, float paddingInside = 0f)
        {
            return (Mathf.Abs(position.x - Center.x) < Width / 2f - paddingInside) && (Mathf.Abs(position.y - Center.y) < Height / 2f - paddingInside);
        }
    
#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            if (gameObject.layer != LayerMask.GetMask($"enemy"))
            {
                Gizmos.color = new Color(0, 1, 0, 0.1f);
                Gizmos.DrawCube(transform.position, new Vector3(Width, Height, 0.1f));
            }
        }
#endif
    }
}
