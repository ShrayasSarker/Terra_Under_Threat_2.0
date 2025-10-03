using UnityEngine;

public class ThreatClickHandler : MonoBehaviour
{
    public string city;
    public int population;
    public string risk;
    public ThreatUIController uiController;

    void OnMouseDown()
    {
        if (uiController != null)
        {
            uiController.ShowInfo(city, population, risk);
        }
    }
}