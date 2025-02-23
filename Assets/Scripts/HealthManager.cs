using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Range(0, 100)]
    private float hpAmount;
    private float MaxHealth = 100;
    private Slider _slider;
    public Gradient ColorGradient;
    public Image FillImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _slider = GetComponent<Slider>();
        hpAmount = MaxHealth;
    }
    public void HealTaken(float heal)
    {
        hpAmount = hpAmount + heal;
        SetValue(hpAmount / MaxHealth);
    }
    public void DamageTaken(float damage)
    {
        hpAmount = hpAmount - damage;
        SetValue(hpAmount/MaxHealth);
    }
    public void SetValue(float hp)
    {
        _slider.value = hp;
        FillImage.color = ColorGradient.Evaluate(hp);
        _slider.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        DamageTaken(10);
    }
}
