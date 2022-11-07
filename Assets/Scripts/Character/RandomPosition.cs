using MyCamera;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable StringLiteralTypo

namespace Character
{
    public class RandomPosition : MonoBehaviour
    {
        [Tooltip("Chia 360 độ ra divedePart phần để spawn")]
        [SerializeField][Range(1, 360)] private int dividePart = 10;

        [Tooltip("Random vị trí dọc theo direct trong khoảng min (x) đến max (y)")]
        private static readonly Vector2 PaddingByDirect = new Vector2(2f, 3f);
        [Tooltip("Random vị trí xung quanh vị trí vật sau khi paddingByDirect")]
        private static readonly Vector2 RandomAround = new Vector2(0.5f, 1f);

        private static int _dividePart;

        private void Awake()
        {
            _dividePart = dividePart;
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static void ReSpawnObjectRandom(GameObject go)
        {
            go.transform.position = RandomSpawnPosition(UIGameManager._ins && UIGameManager._ins.joystick.Direction != Vector2.zero
                ? ClampPartIndex(DirectToPartIndex(UIGameManager._ins.joystick.Direction) + Random.Range(-4, 4))
                : Random.Range(0, _dividePart));
        }
    
        private static Vector3 RandomSpawnPosition(int partIndex)
        {
            Vector3 result = new Vector3();
            float alphaAngle = (float)partIndex / _dividePart * 2f * Mathf.PI;
            Vector2 direct = new Vector2(Mathf.Cos(alphaAngle), Mathf.Sin(alphaAngle));
            float aX = Mathf.Abs(direct.x) < 0.001f? 999f : (CameraController.RightTop.x - CameraController.Center.x) / direct.x;
            float aY = Mathf.Abs(direct.y) < 0.001f? 999f : (CameraController.RightTop.y - CameraController.Center.y) / direct.y;
            float a = Mathf.Min(Mathf.Abs(aX), Mathf.Abs(aY));
            result.x = CameraController.Center.x + direct.x * a;
            result.y = CameraController.Center.y + direct.y * a;
            result += (Vector3)direct.normalized * new MyRange(PaddingByDirect.x, PaddingByDirect.y).RandomInRange() 
                      + (Vector3)new MyRange(RandomAround.x, RandomAround.y).RandomVector2InRange();
            return result;
        }
    
        private static int DirectToPartIndex(Vector2 direct)
        {
            float alphaAngle = Vector2.SignedAngle(Vector2.right, direct);
            int res = ClampPartIndex(Mathf.RoundToInt(_dividePart * alphaAngle / 360f));
            return res;
        }
    
        private static int ClampPartIndex(int partIndex)
        {
            if (partIndex == 0) return 0;
            return (partIndex % _dividePart + _dividePart);
        }

        #region Editor

#if UNITY_EDITOR
        [Header("==== EDITOR ONLY ====")]
        [SerializeField] private GameObject testGO;
        [SerializeField] private bool testRespawn;
    
        private void OnValidate()
        {
            _dividePart = dividePart;
            if (testRespawn)
            {
                testRespawn = false;
                if (testGO)
                {
                    ReSpawnObjectRandom(testGO);
                }
            }
        }
    
#endif

        #endregion
    
    }
}
