using Assets.Scripts;
using Assets.Scripts.GameLogic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class TestAddForce : MonoBehaviour
{
    [SerializeField] private Vector2 startPosition, endPosition;
    [SerializeField] private Transform position;
    [SerializeField] private BallMover ballMover;
    [SerializeField] private Animator animator;
    [SerializeField] private int countLife;

    public UnityEvent<int> OnChangeLife;


    private void Awake()
    {
        InputActions inputs = new();
        inputs.UI.Touch.started += e => startPosition = inputs.UI.Position.ReadValue<Vector2>();
        inputs.UI.Position.performed += e =>
        {            
            endPosition = e.ReadValue<Vector2>();
        };
        inputs.UI.Touch.canceled += e =>
        {
            AddForce();
        };
        inputs.Enable();
    }

    private void Start()
    {
        ResetPosition();
        countLife = 3;
        OnChangeLife?.Invoke(countLife);
    }


    [Button]
    private void AddForce()
    {
        if(ballMover.Velocity.magnitude > 0.1f || countLife == 0)
        {
            return;
        }
        animator.Play("Hit");
        ballMover.Hit(endPosition - startPosition);
    }

    [Button]
    public void ResetPosition()
    {
        ballMover.Move(position.position);
    }

    private void Update()
    {
        if (ballMover.Velocity.magnitude > 0.1f || ballMover.gameObject.transform.position == position.position || countLife == 0)
        {
            return;
        }
        ResetPosition();
        OnChangeLife?.Invoke(--countLife);
    }
}
