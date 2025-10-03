using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThreatUIController : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI cityText;
    public TextMeshProUGUI populationText;
    public TextMeshProUGUI riskText;
    public Button closeButton;

    void Start()
    {
        infoPanel.SetActive(false);
        closeButton.onClick.AddListener(() => infoPanel.SetActive(false));
    }

    public void ShowInfo(string city, int population, string risk)
    {
        cityText.text = $"City: {city}";
        populationText.text = $"Population: {population:N0}";
        riskText.text = $"Risk Level: {risk}";
        infoPanel.SetActive(true);
    }
}