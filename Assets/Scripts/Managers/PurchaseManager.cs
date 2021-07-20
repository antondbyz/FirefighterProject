using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseManager : MonoBehaviour, IStoreListener
{
    public static PurchaseManager Instance;
    public const string RemoveAdsId = "rescuer.removeads";
    public event System.Action RemoveAdsPurchaseCompleted;

    private IStoreController controller;
    private IExtensionProvider extensions;

    private void Awake() 
    { 
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of PurchaseManager!");

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(RemoveAdsId, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }

    public void PurchaseProduct(string productId)
    {
        controller.InitiatePurchase(productId);
    }

    public bool IsProductPurchased(string productId)
    {
        if(controller != null)
        {
            for(int i = 0; i < controller.products.all.Length; i++)
            {
                if(controller.products.all[i].definition.id == productId)
                {
                    return controller.products.all[i].hasReceipt;
                }
            }
        }
        return false;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;   
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        switch(purchaseEvent.purchasedProduct.definition.id)
        {
            case RemoveAdsId: RemoveAdsPurchaseCompleted?.Invoke(); break;
        }
        return PurchaseProcessingResult.Complete;
    }
}