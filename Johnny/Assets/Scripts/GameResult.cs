using Assets.Scripts.Identifiers;
using UnityEngine;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    private Image image;
    private bool isColorChanged = false;
    private bool isGameOverTextAdded = false;
    public bool isDoubleXPTextAdded = false;
    private GameObject character;
    private GameObject background;
    private GameObject gameOverText;
    private GameObject doubleXP;


    public bool isGameEnd = false;
    public bool isFinishedSuccessfuly = true;

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    private void Awake()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);

        background = GameObject.Find(Globals.BackgroundName);
        gameOverText = GameObject.Find(Globals.GameOverText);
        doubleXP = GameObject.Find(Globals.DoubleXP);
        image = GetComponentInChildren<Image>();
        character = GameObject.Find(Globals.CharacterName);
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGameEnd)
        {
            if (!isColorChanged)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 0.5f * Time.deltaTime);
            }
            else
            {
                if (!isGameOverTextAdded)
                {
                    AddGameOverText();
                }

                if (!isDoubleXPTextAdded && isFinishedSuccessfuly)
                {
                    AddDoubleXPText();
                }
            }

            if (image.color.a <= .53f)
            {
                isColorChanged = true;
            }
        }                
    }

    private void AddGameOverText()
    {
        Text text = gameOverText.GetComponent<Text>();
        text.text = isFinishedSuccessfuly ? "Level completed successfully" : "Level failed";
        text.transform.position = new Vector3(16.4f, -1.5f, -0.5f);
        isGameOverTextAdded = true;
    }

    private void AddDoubleXPText()
    {
        Text text = doubleXP.GetComponent<Text>();
        text.transform.position = new Vector3(16.4f, 0.9f, -0.5f);
        text.gameObject.AddComponent<DoubleXpScript>();
        isDoubleXPTextAdded = true;
    }
}
