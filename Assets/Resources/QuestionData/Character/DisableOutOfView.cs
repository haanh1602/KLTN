using System.Collections;
using MyCamera;
using UnityEngine;

// ReSharper disable StringLiteralTypo

namespace Character
{
    public class DisableOutOfView : MonoBehaviour
    {
        [Tooltip("[KHÔNG DÙNG CHO GAMEOBJECT CHỨA SCRIPT] GameObject hiển thị hình ảnh")]
        [SerializeField] private GameObject rendererGO;
        
        [Space(15)]
        [Tooltip("Tăng / giảm vùng biên của vùng nhìn thấy")]
        [SerializeField] private Vector2 padding = Vector2.one;
        [Tooltip("Khoảng thời gian giữa 2 lần chạy check visible")]
        [SerializeField][Range(0.02f, 0.5f)] private float gapTime = 0.25f;

        // ReSharper disable once MemberCanBePrivate.Global
        public float DisableTime { get; set; }

        private void OnEnable()
        {
            StopAllCoroutines();
            StartCoroutine(IESetVisible());
        }

        private bool CanVisible(float extraX = 0f, float extraY = 0f)
        {
            Vector2 distanceFromCenter = (Vector2)transform.position - CameraController.Center;
            return Mathf.Abs(distanceFromCenter.x) < CameraController.Width / 2f + extraX &&
                   Mathf.Abs(distanceFromCenter.y) < CameraController.Height / 2f + extraY;
        }

        private IEnumerator IESetVisible()
        {
            var canVisible = CanVisible(padding.x, padding.y);
            rendererGO.gameObject.SetActive(canVisible);
            yield return new WaitForSeconds(gapTime);
            if (canVisible) DisableTime = 0;
            else DisableTime += gapTime;
            if (gameObject.activeSelf) StartCoroutine(IESetVisible());
        }
    }
}
