using System;
using System.Collections;
using TMPro;
using UnityEngine;

public enum ChosenActionType
{
    Default,
    Custom
}

public class TutorialPlayer : MonoBehaviour
{ 
    [SerializeField] private TMP_Text _text;
    public TMP_Text PublicText => _text;
    [field: SerializeField] public TutorialID TutorialID { get; private set; }

    public TutorialAction Action;
    [field: SerializeField] public StringStorage MainTexts { get; private set; }

    private int _currentMainTextIndex = -1;

    private const float FADE_DURATION = 0.3f;

    public event Action<TutorialID> OnTutorialEnd;
    public event Action OnTextStarted;

    public void MoveToNextNarratorText()
    {
        _currentMainTextIndex++;
        UpdateNarratorFrameText(MainTexts.Strings[_currentMainTextIndex]);
        StartCoroutine(FadeInText());
    }

    private void OnCurrentActionFinished()
    {
        Action.Exit();
        Action.OnActionFinished -= OnCurrentActionFinished;
        OnTutorialEnd?.Invoke(TutorialID);
        Destroy(gameObject);
    }

    public void IncreaseMainTextIndex()
    {
        _currentMainTextIndex++;
    }

    public void SetTextPosition(Vector2 position)
    {
        _text.rectTransform.localPosition = position;
    }

    private void UpdateNarratorFrameText(string text)
    {
        _text.text = text;
    }

    public void TextFadeAway()
    {
        StartCoroutine(FadeOutText());
    }

    private IEnumerator Start()
    {
        yield return null;
        Action.Init(this);
        Action.StartAction();
        StartCoroutine(FadeInText());
        Action.OnActionFinished += OnCurrentActionFinished;
    }

    private IEnumerator FadeOutText()
    {
        float startAlpha = _text.alpha;
        for (float t = 0; t < FADE_DURATION; t += Time.deltaTime)
        {
            _text.alpha = Mathf.Lerp(startAlpha, 0, t / FADE_DURATION);
            yield return null;
        }

        _text.alpha = 0;
    }

    private IEnumerator FadeInText()
    {
        float startAlpha = _text.alpha;
        for (float t = 0; t < FADE_DURATION; t += Time.deltaTime)
        {
            _text.alpha = Mathf.Lerp(startAlpha, 1, t / FADE_DURATION);
            yield return null;
        }

        _text.alpha = 1;
        OnTextStarted?.Invoke();
    }
}
