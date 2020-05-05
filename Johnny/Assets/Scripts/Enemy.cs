using Assets.Scripts.Enums;
using Assets.Scripts.Extencions;
using Assets.Scripts.Identifiers;
using UnityEngine;

public sealed class Enemy : MonoBehaviour
{
    public Rigidbody2D enemyRigidBody;
    public Animator enemyAnimator;
    public SpriteRenderer enemySprite;

    private void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemySprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator.SetInteger(Globals.AnimationState, CharacterStates.Idle.ToInt());
    }

    void OnApplicationPause(bool isPaused)
    {
        Debug.Log("unity-script: OnApplicationPause = " + isPaused);
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
