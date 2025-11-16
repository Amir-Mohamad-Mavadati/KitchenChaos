using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter BaseCounter;
    [SerializeField] private GameObject[] VisualGameObjectArray;

    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter == BaseCounter)
        {
            ShowVisual();
        }
        else
        {
            HideVisual();
        }
    }

    private void ShowVisual()
    {
        foreach (GameObject VisualGameObject in VisualGameObjectArray)
        {
            VisualGameObject.SetActive(true);
        }
        
    }
    
    private void HideVisual()
    {
        foreach (GameObject VisualGameObject in VisualGameObjectArray)
        {
            VisualGameObject.SetActive(false);
        }
    }
    
}
