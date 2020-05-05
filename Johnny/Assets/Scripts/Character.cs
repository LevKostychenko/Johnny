using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Extencions;
using Assets.Scripts.Identifiers;
using UnityEngine;

public sealed class Character : MonoBehaviour
{
    private readonly float characterSpeed = 3.0f;
    private readonly float jumpForce = 6f;
    private readonly float aimMovingSpeed = 2f;
    private readonly float aimMovingRadius = 3f;
    private readonly float bulletSpeed = 20f;
    private Vector3 aimOffset;
    private float aimMovingAngle = 0f;
    private bool isJumped = false;
    private bool isAimAdded = false;
    private bool isLanding = false;
    private bool isShotMade = false;
    private bool isEnemyDied = false;
    private bool isCharacterDied = false;
    private GameObject aim;
    private BoxCollider2D bulletColider;
    private Vector3 bulletDistinationPoint;
    private GameResult gameResult;

    public bool isGameStart = false;

    public GameObject enemy;
    public GameObject bullet;
    public SpriteRenderer bulletSprite;
    public Sprite aimSprite;
    public Rigidbody2D characterRigidBody;
    public Animator characterAnimator;
    public SpriteRenderer characterSprite;
    public Rigidbody2D enemyRigidBody;
    public Animator enemyAnimator;
    public SpriteRenderer enemySprite;
    public BoxCollider2D enemyBoxCollider;

    private void Awake()
    {
        bulletColider = bullet.GetComponentInChildren<BoxCollider2D>();
        bulletSprite = bullet.GetComponentInChildren<SpriteRenderer>();

        characterRigidBody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        characterSprite = GetComponent<SpriteRenderer>();

        enemyRigidBody = enemy.GetComponent<Rigidbody2D>();
        enemyAnimator = enemy.GetComponent<Animator>();
        enemySprite = enemy.GetComponent<SpriteRenderer>();
        enemyBoxCollider = enemy.GetComponent<BoxCollider2D>();

        gameResult = GameObject.Find(Globals.BackgroundName).GetComponent<GameResult>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        IronSource.Agent.init(Globals.IronSourceAppKey);
        IronSource.Agent.validateIntegration();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGameStart)
        {
            CheckLanding();
            if (transform.position.x <= 9.0f)
            {
                Move();
            }
            else
            {
                Time.timeScale = .2f;
                if (!isJumped)
                {
                    MakeJump();
                }

                if (!isAimAdded)
                {
                    AddAim();
                }

                if (isAimAdded)
                {
                    MoveAim();
                }

                if (isAimAdded && isJumped && !isLanding && !isShotMade)
                {
                    if (Input.GetButtonDown(Globals.FireOne))
                    {
                        MakeShot(aim.transform.position,
                            new Vector3(transform.position.x + 1f, transform.position.y),
                            () => characterAnimator.SetInteger(Globals.AnimationState, CharacterStates.Shot.ToInt()));
                    }
                }

                if (isShotMade && isAimAdded && isJumped && !isLanding)
                {
                    MoveBullet();
                }

                if (transform.position.x >= 13.5f && !isEnemyDied && !isCharacterDied)
                {
                    KillCharacter();
                }

                if (isCharacterDied || isEnemyDied)
                {
                    EndGame();
                }
            }
        }
    }

    private void Move()
    {
        characterAnimator.SetInteger(Globals.AnimationState, CharacterStates.Run.ToInt());
        Vector3 tempvector = Vector3.right * 200.0f;
        transform.position = Vector3.MoveTowards(transform.position, tempvector, characterSpeed * Time.deltaTime);
    }

    private void MakeJump()
    {
        var jumpDirection = new Vector2(5f, 3f);
        characterRigidBody.AddForce(jumpDirection.normalized * jumpForce, ForceMode2D.Impulse);
        isJumped = true;
    }

    private void AddAim()
    {
        aim = new GameObject();
        SpriteRenderer aimSpriteRendere = aim.AddComponent<SpriteRenderer>();

        aimSpriteRendere.sprite = aimSprite;

        Vector3 aimStartPosition = enemy.transform.position;

        aim.transform.position = aimStartPosition;
        aimOffset = aimStartPosition;
        isAimAdded = true;
    }

    private void CheckLanding()
    {
        var lowerPoint = new Vector2(transform.position.x, transform.position.y - 1.2f);
        Collider2D[] colliders = Physics2D.OverlapPointAll(lowerPoint);
        isLanding = colliders.Length > 0;
    }

    private void MoveAim()
    {
        aimMovingAngle += Time.deltaTime;

        var x = (Mathf.Cos(aimMovingAngle * aimMovingSpeed) * aimMovingRadius) + aimOffset.x - 1f;
        var y = (Mathf.Sin(aimMovingAngle * aimMovingSpeed) * aimMovingRadius) + aimOffset.y - 1.5f;
        aim.transform.position = new Vector2(x, y);
    }

    private void MakeShot(Vector3 bulletDistination, Vector3 bulletStart, Action optionalAtion = null)
    {
        bulletDistinationPoint = bulletDistination;
        if (optionalAtion != null)
        {
            optionalAtion.Invoke();
        }

        bullet.SetActive(true);
        bullet.transform.position = bulletStart;

        isShotMade = true;
    }

    private void MoveBullet()
    {
        bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, bulletDistinationPoint, Time.deltaTime * bulletSpeed);

        var bottomLeft = new Vector2(enemySprite.transform.position.x - 0.5f, enemySprite.transform.position.y - 0.7f);
        var upperRight = new Vector2(enemySprite.transform.position.x + 0.7f, enemySprite.transform.position.y + 0.5f);
        
        Collider2D[] colliders = Physics2D.OverlapAreaAll(bottomLeft, upperRight);

        if (colliders.Length == 2 && !isEnemyDied)
        {
            KillEnemy();
        }

        if (bullet.transform.position.y >= 2.7f)
        {
            bullet.SetActive(false);
        }
    }

    private void KillEnemy()
    {
        aim.SetActive(false);
        bullet.SetActive(false);
        enemyAnimator.SetInteger(Globals.AnimationState, CharacterStates.Die.ToInt());
        isEnemyDied = true;
    }

    private void KillCharacter()
    {
        bullet.SetActive(false);
        aim.SetActive(false);
        enemyAnimator.SetInteger(Globals.AnimationState, CharacterStates.Shot.ToInt());
        characterAnimator.SetInteger(Globals.AnimationState, CharacterStates.Die.ToInt());
        isCharacterDied = true;
    }

    private void EndGame()
    {
        if (isCharacterDied && !isEnemyDied)
        {
            gameResult.isFinishedSuccessfuly = false;
        }
        else
        {
            gameResult.isFinishedSuccessfuly = true;
        }

        gameResult.isGameEnd = true;        
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
