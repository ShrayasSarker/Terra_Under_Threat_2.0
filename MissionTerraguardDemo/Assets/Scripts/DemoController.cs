using UnityEngine;
using UnityEngine.UI;

public class DemoController : MonoBehaviour
{
    [Header("Scene References")]
    public Transform earth;
    public Button scanButton;
    public Text statusText;
    public GameObject threatPrefab;

    [Header("Earth Settings")]
    public float earthSpinSpeed = 10f;
    public float scanDuration = 3f;
    public float markerOffset = 0.05f;

    [Header("Threat Settings")]
    public bool useRandomThreats = false;
    public int randomThreatCount = 10;
    [Header("Post-Scan UI")]
    public Button infoButton;
    public GameObject infoPanel;


    public Vector2[] threatLatLon = new Vector2[]
    {
    new Vector2(25.76f, -80.19f),   // Miami, USA
    new Vector2(6.52f, 3.37f),      // Lagos, Nigeria
    new Vector2(13.75f, 100.50f),   // Bangkok, Thailand
    new Vector2(-6.20f, 106.85f),   // Jakarta, Indonesia
    new Vector2(19.07f, 72.87f),    // Mumbai, India
    new Vector2(14.60f, 120.98f),   // Manila, Philippines
    new Vector2(10.77f, 106.70f),   // Ho Chi Minh City, Vietnam
    new Vector2(23.81f, 90.41f),    // Dhaka, Bangladesh
    new Vector2(31.23f, 121.47f),   // Shanghai, China
    new Vector2(4.17f, 73.51f)      // Malé, Maldives
    


    };

    [Header("Marker Appearance")]
    public float markerBaseScale = 0.05f;

    [Header("Pulse Settings")]
    public float pulseAmplitude = 0.03f;
    public float pulseSpeed = 2f;

    float timer = 0f;
    bool scanning = false;

    void Start()
    {
        statusText.text = "Ready to Scan";
        scanButton.onClick.AddListener(StartScan);
        infoButton.onClick.AddListener(ShowInfoPanel);
        infoPanel.SetActive(false);
    }

    void Update()
    {
        earth.Rotate(Vector3.up * earthSpinSpeed * Time.deltaTime, Space.Self);

        if (!scanning) return;

        timer += Time.deltaTime;
        statusText.text = $"Scanning... {Mathf.CeilToInt(scanDuration - timer)}s";
        if (timer >= scanDuration) FinishScan();
    }

    void StartScan()
    {
        if (scanning) return;
        scanning = true;
        timer = 0f;
        statusText.text = "Scanning...";
    }

    void FinishScan()
    {
        scanning = false;
        ClearThreats();

        float radius = GetEarthLocalRadius() + markerOffset;

        if (useRandomThreats)
        {
            for (int i = 0; i < randomThreatCount; i++)
            {
                float lat = Random.Range(-90f, 90f);
                float lon = Random.Range(-180f, 180f);
                Vector3 localPos = LatLonToLocal(lat, lon, radius);
                CreateThreatMarker(localPos);
            }
            statusText.text = $"{randomThreatCount} Random Threats Detected!";
        }
        else
        {
            foreach (var ll in threatLatLon)
            {
                Vector3 localPos = LatLonToLocal(ll.x, ll.y, radius);
                CreateThreatMarker(localPos);
            }
            statusText.text = "Threats Detected at Known Locations!";
        }

        // ✅ Show the button now

        infoButton.gameObject.SetActive(true);
        infoButton.interactable = true;

    }

    Vector3 LatLonToLocal(float latDeg, float lonDeg, float radius)
    {
        float lat = latDeg * Mathf.Deg2Rad;
        float lon = lonDeg * Mathf.Deg2Rad;

        float x = Mathf.Cos(lat) * Mathf.Cos(lon);
        float z = Mathf.Cos(lat) * Mathf.Sin(lon);
        float y = Mathf.Sin(lat);

        return new Vector3(x, y, z).normalized * radius;
    }

    float GetEarthLocalRadius()
    {
        SphereCollider sphere = earth.GetComponent<SphereCollider>();
        return sphere.radius;
    }

    void CreateThreatMarker(Vector3 localPos)
    {
        GameObject marker;

        if (threatPrefab == null)
        {
            marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            var mr = marker.GetComponent<MeshRenderer>();
            mr.material = new Material(Shader.Find("Standard"));
            mr.material.color = Color.red;
        }
        else
        {
            marker = Instantiate(threatPrefab);
        }

        marker.transform.SetParent(earth, false);
        marker.transform.localPosition = localPos;
        marker.transform.up = marker.transform.localPosition.normalized;
        marker.transform.localScale = Vector3.one * markerBaseScale;

        Pulse pulse = marker.AddComponent<Pulse>();
        pulse.amplitude = pulseAmplitude;
        pulse.speed = pulseSpeed;
    }

    void ClearThreats()
    {
        foreach (Transform child in earth)
        {
            if (child.GetComponent<Pulse>() != null)
                Destroy(child.gameObject);
        }
    }
    void ShowInfoPanel()
    {
        infoPanel.SetActive(true);
    }

}