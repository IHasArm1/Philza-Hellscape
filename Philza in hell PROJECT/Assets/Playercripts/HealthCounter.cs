using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    TMPro.TextMeshProUGUI _healthText;
    string healthCount;

    // Start is called before the first frame update
    void Start()
    {
        _healthText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetHealthCounter(int health)
    {
        healthCount = health.ToString();
        _healthText.text = healthCount;
    }

}
