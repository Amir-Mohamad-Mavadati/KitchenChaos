using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    [SerializeField] private FryingRecipeSO[] FryingRecipeSOArray;
    [SerializeField] private BurnedRecipeSO[] BurnedRecipeSOArray;
    private float fryingTimer = 0;
    private float fryiedTimer = 0;
    private FryingRecipeSO FryingRecipeSO;
    private BurnedRecipeSO BurnedRecipeSO;

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private State state;

    private void Start()
    {
        state = State.Idle;
        OnStateChanged?.Invoke(this , new OnStateChangedEventArgs
        {
            state = state
        });
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:

                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = fryingTimer / FryingRecipeSO.FryingTimerMax
                    });

                    if (fryingTimer > FryingRecipeSO.FryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(FryingRecipeSO.Output, this);
                        fryingTimer = 0;
                        state = State.Fried;
                        OnStateChanged?.Invoke(this , new OnStateChangedEventArgs
                        {
                            state = state
                        });
                        
                    }

                    break;
                case State.Fried:

                    BurnedRecipeSO = GetBurnedRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                     OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = fryiedTimer / BurnedRecipeSO.BurnedTimerMax
                    });

                    fryiedTimer += Time.deltaTime;

                    if (fryiedTimer > BurnedRecipeSO.BurnedTimerMax)
                    {
                        fryiedTimer = 0;
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(BurnedRecipeSO.Output, this);
                        state = State.Burned;
                        OnStateChanged?.Invoke(this , new OnStateChangedEventArgs
                        {
                            state = state
                        });

                         OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            ProgressNormalized = 0
                        });
                    }

                    break;
                case State.Burned:
                    break;
                
            }
        }
    }

    public override void Interact(Player Player)
   {
       // Interaction logic for the stove counter
      
        if (!HasKitchenObject())
        {
            if (Player.HasKitchenObject() && HasRecipeWithInput(Player.GetKitchenObject().GetKitchenObjectSO()))
            {
                Player.GetKitchenObject().SetKitchenObjectParent(this);
                FryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                state = State.Frying;
                OnStateChanged?.Invoke(this , new OnStateChangedEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    ProgressNormalized = fryingTimer / FryingRecipeSO.FryingTimerMax
                });
                
            }
            else
            {
                // Player has nothing
            }
        }
        else
        {
            if (Player.HasKitchenObject())
            {
                // Both have kitchen object
                 if (Player.GetKitchenObject().TryGetPlate(out PlateKichenObject plateKichenObject))
                {
                    // Player is holdig plate
                    if(plateKichenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;

                        OnStateChanged?.Invoke(this , new OnStateChangedEventArgs
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            ProgressNormalized = 0
                        });
                    }
                }
            }
            else
            {
                // Player pickup the kitchen object from counter
                fryingTimer = 0;
                fryiedTimer = 0;
                GetKitchenObject().SetKitchenObjectParent(Player);

                state = State.Idle;

                OnStateChanged?.Invoke(this , new OnStateChangedEventArgs
                {
                    state = state
                });

                 OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    ProgressNormalized = 0
                });
            }
        }
   }

   private KitchenObjectsSO GetOutputForKitchenObject(KitchenObjectsSO InputKitchenObjectSO)
    {
        FryingRecipeSO FryingRecipeSO = GetFryingRecipeSOWithInput(InputKitchenObjectSO);
        if (FryingRecipeSO != null)
        {
            return FryingRecipeSO.Output;
        }
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectsSO InputKitchenObjectSO)
    {
        FryingRecipeSO FryingRecipeSO = GetFryingRecipeSOWithInput(InputKitchenObjectSO);
        return FryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectsSO InputKitchenObjectSO)
    {
        foreach (FryingRecipeSO FryingRecipeSO in FryingRecipeSOArray)
        {
            if (FryingRecipeSO.Input == InputKitchenObjectSO)
            {
                return FryingRecipeSO;
            }
        }
        return null;
    }

    private BurnedRecipeSO GetBurnedRecipeSOWithInput(KitchenObjectsSO InputKitchenObjectSO)
    {
        foreach (BurnedRecipeSO BurnedRecipeSO in BurnedRecipeSOArray)
        {
            if (BurnedRecipeSO.Input == InputKitchenObjectSO)
            {
                return BurnedRecipeSO;
            }
        }
        return null;
    }
}
