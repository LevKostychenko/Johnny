using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject character;

    public Rigidbody2D characterRigidBody;

    public Vector3 offset;

    private void Awake()
    {
        characterRigidBody = character.GetComponentInChildren<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        offset = transform.position - characterRigidBody.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = characterRigidBody.transform.position + offset;
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }
}
