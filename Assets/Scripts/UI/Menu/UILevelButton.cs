using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Toggle = UnityEngine.UI.Toggle;

public class UILevelButton : MonoBehaviour, IPointerClickHandler
{ 
    private int level = 1;
    [SerializeField] private Image iconImage;
    [SerializeField] private Toggle toggle;
    [SerializeField] private TextMeshProUGUI tmpLevel;
    [SerializeField] private Button btnStart;
    [SerializeField] private DOTweenAnimation dotAnimation;

    [SerializeField] private DOTweenAnimation newDotAnimation;
    
    private RectTransform _rectTransform;
    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
    }
    
    public int Level => level;

    private void Awake()
    {
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(OnToggleCLick);
    }

    private void Start()
    {
        btnStart.onClick.AddListener(() =>
        {
            FindObjectOfType<UIMenuManager>().OnStartGame(level);
            gameObject.SetActive(false);
        });
    }

    public void OnToggleCLick(bool isOn)
    {
        dotAnimation.DORestartById(isOn ? "levelInfo_open" : "levelInfo_close");
    }

    public void Init(int level)
    {
        this.level = level;
        tmpLevel.text = "Bài " + level.ToString();
        toggle.interactable = level <= GameData.Instance.LevelUnlock;
        if (level == GameData.Instance.LevelUnlock) ShowNewText();
    }

    public void SetToggleGroup(ToggleGroup toggleGroup)
    {
        toggle.group = toggleGroup;
    }

    public void ShowNewText()
    {
        if (newDotAnimation) newDotAnimation.DORestartById("show_new");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlayOnClick();
        if (!toggle.IsInteractable())
        {
            NotifyUI.Instance.ShowToast("Màn chơi chưa mở khóa!");
        }
    }
}
