using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;


public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public EventHandler OnPlayerPickup;
    public EventHandler OnPlayerDropItem;
    public EventHandler OnPlayerWallking;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }

    [SerializeField] private PlayerInput PlayerInput;
    public bool IsWalking;
    [SerializeField] private float speed = 6f;
    private Vector3 lastInteractDir;
    private BaseCounter SelectedCounter;
    private KitchenObject KitchenObject;
    [SerializeField] private Transform KitchenObjectHoldPoint;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        PlayerInput.OnInteractAction += PlayerInput_OnInteractAction;
        PlayerInput.OnInteractAlternateAction += PlayerInput_OnInteractAlternateAction;
    }

    private void PlayerInput_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if (SelectedCounter != null)
        {
            SelectedCounter.InteractAlternate(this);
        }
    }

    private void PlayerInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        }

        if (SelectedCounter != null)
        {
            SelectedCounter.Interact(this);
        }
    }
    
    private void Update()
    {
        HandleMovement();
        HandleIntraction();
    }

    public bool GetIsWalking()
    {
        return IsWalking;
    }

    private void HandleIntraction()
    {
        Vector2 InputVector = PlayerInput.GetInputVector();
        Vector3 MoveDir = new Vector3(InputVector.x, 0, InputVector.y);
        if (MoveDir != Vector3.zero)
        {
            lastInteractDir = MoveDir;
        }
        float InteractDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit Hit, InteractDistance))
        {
            if (Hit.transform.TryGetComponent(out BaseCounter BaseCounter))
            {
                if (BaseCounter != SelectedCounter)
                {
                    SetSelectedCounter(BaseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
           SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 InputVector = PlayerInput.GetInputVector();
        Vector3 MoveDir = new Vector3(InputVector.x, 0, InputVector.y);
        IsWalking = MoveDir != Vector3.zero;
        float MoveDistance = speed * Time.deltaTime;
        float PlayerHight = 2f;
        float PlayerRius = .7f;
        bool CanMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHight, PlayerRius, MoveDir, MoveDistance);
        if (!CanMove)
        {
            Vector3 MoveDirX = new Vector3(MoveDir.x, 0, 0).normalized;
            CanMove = MoveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHight, PlayerRius, MoveDirX, MoveDistance);
            if (CanMove)
            {
                MoveDir = MoveDirX;
            }
            else
            {
                Vector3 MoveDirZ = new Vector3(0, 0, MoveDir.z).normalized;
                CanMove = MoveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * PlayerHight, PlayerRius, MoveDirZ, MoveDistance);
                if (CanMove)
                {
                    MoveDir = MoveDirZ;
                }
            }
        }

        if (CanMove)
        {
            transform.position += MoveDir * MoveDistance;
        }
        float RotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, MoveDir, Time.deltaTime * RotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter SelectedCounter)
    {
        this.SelectedCounter = SelectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { SelectedCounter = SelectedCounter });
    }
    
    public Transform Getkitchenobjectfollowtransform()
    {
        return KitchenObjectHoldPoint;
    }

    public void ClearKitchenObject()
    {
        KitchenObject = null;
        OnPlayerDropItem?.Invoke(this, EventArgs.Empty);
    }

    public KitchenObject GetKitchenObject()
    {
        return KitchenObject;
    }

    public void SetKitchenObject(KitchenObject KitchenObject)
    {
        this.KitchenObject = KitchenObject;

        if (KitchenObject != null)
        {
            OnPlayerPickup?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool HasKitchenObject()
    {
        return KitchenObject != null;
    }
}
