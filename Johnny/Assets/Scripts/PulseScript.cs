using System.Collections;
using Assets.Scripts.Identifiers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class PulseScript : MonoBehaviour, IPointerClickHandler
{
    private readonly float approachSpeed = 0.01f;
    private readonly float growthBound = 1.2f;
    private readonly float shrinkBound = 0.9f;
    private readonly float textMoveSpeed = 5f;

    private float currentRatio = 1;
    private Text startText;
    private bool isTextMove = false;
    private bool keepGoing = true;
    private GameObject character;
    private RectTransform gameNameRect;
    private RectTransform startRect;

    private void Awake()
    {
        startText = gameObject.GetComponent<Text>();
        StartCoroutine(Pulse());
        character = GameObject.Find(Globals.CharacterName);
        gameNameRect = GameObject.Find(Globals.GameName).GetComponent<RectTransform>();
        startRect = GameObject.Find(Globals.StartGame).GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isTextMove)
        {
            gameNameRect.offsetMin += new Vector2(gameNameRect.offsetMin.x, textMoveSpeed);
            gameNameRect.offsetMax += new Vector2(gameNameRect.offsetMax.x, textMoveSpeed);
            startRect.offsetMin += new Vector2(startRect.offsetMin.x, -textMoveSpeed);
            startRect.offsetMax += new Vector2(startRect.offsetMax.x, -textMoveSpeed);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {       
        character.GetComponent<Character>().isGameStart = true;
        isTextMove = true;
    }

    IEnumerator Pulse()
    {
        while (keepGoing)
        {
            while (currentRatio != growthBound)
            {
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);

                startText.transform.localScale = Vector3.one * currentRatio;

                yield return new WaitForEndOfFrame();
            }

            while (currentRatio != shrinkBound)
            {
                currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed);

                startText.transform.localScale = Vector3.one * currentRatio;

                yield return new WaitForEndOfFrame();
            }
        }
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
