using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
//using UnityEngine.UI.Extensions;

public class UbhBackground : UbhMonoBehaviour
{
    private const string TEX_OFFSET_PROPERTY = "_MainTex";
    [SerializeField, FormerlySerializedAs("_Speed")]
    private float m_speed = 0.1f;

    private Vector2 m_offset = UbhUtil.VECTOR2_ZERO;

    private Image img;
    public bool isImageUI = false;
    public bool isLineRenderer = false;
    public bool isMoveAxisX = false;
    //private UILineRenderer lineRenderer;
    private void Start()
    {
        UbhGameManager manager = FindObjectOfType<UbhGameManager>();
        if (manager != null && manager.m_scaleToFit)
        {
            Vector2 max = Camera.main.ViewportToWorldPoint(UbhUtil.VECTOR2_ONE);
            Vector2 scale = max * 2f;
            transform.localScale = scale;
        }
        if (isImageUI) img = gameObject.GetComponent<Image>();
       // if (isLineRenderer) lineRenderer = GetComponent<UILineRenderer>();

        if (isIntroBG)
            Intro();
    }
    private void Update()
    {
       // if (GameContext.isPause) return;
        float y = 0;
        if (isRunIntro && isIntroBG)
        {
            m_speed = Mathf.SmoothDamp(m_speed, baseScrollSpeed, ref value, Time.deltaTime * 600 * baseScrollSpeed);
            currentScrollSpeed = (baseScrollSpeed * 2 - m_speed);
            y = Mathf.Repeat(Time.time * currentScrollSpeed, 1f);
        } else
        {
            y = Mathf.Repeat(Time.time * m_speed, 1f);
        }
        //float y = Mathf.Repeat(Time.time * currentScrollSpeed, 1f);
        if (!isMoveAxisX)
        {
            m_offset.x = 0f;
            m_offset.y = y;
        }
        else
        {
            m_offset.x = y;
            m_offset.y = 0f;
        }

        if (isImageUI)
            img.materialForRendering.SetTextureOffset(TEX_OFFSET_PROPERTY, m_offset); 
        else
            renderer.sharedMaterial.SetTextureOffset(TEX_OFFSET_PROPERTY, m_offset);
    }

    //------------Tuan
    private float value = 1f;
    private float currentScrollSpeed;
    private float baseScrollSpeed;
    public bool isIntroBG;
    private bool isRunIntro;
    public ParticleSystem fx_Intro;
    public void Intro()
    {
        if (isRunIntro)
            return;
        baseScrollSpeed = m_speed;
        m_speed = m_speed * 15;
        if (fx_Intro != null)
            fx_Intro.Play();
        isRunIntro = true;
    }
}
