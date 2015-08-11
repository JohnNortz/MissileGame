using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{

    public float _Health;
    public Text HealthText;  
    
    void Update()
    {
        HealthText.text = _Health.ToString();
    }

    public void UpdateHealth(float current)
    {
        _Health = current;
    }
}
