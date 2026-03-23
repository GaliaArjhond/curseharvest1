using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private TextMeshProUGUI staminaText;

    [Header("Stamina Drain")]
    public float walkStaminaDrain = 3f;

    [Header("Stamina Regen")]
    [SerializeField] private float staminaRegenRate = 5f;
    [SerializeField] private float staminaRegenDelay = 1f;
    private float staminaRegenTimer = 0f;

    [Header("Health Regen")]
    [SerializeField] private float healthRegenRate = 2f;
    [SerializeField] private float regenDelay = 5f;
    private float regenTimer = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        UpdateUI();
    }

    void Update()
    {
        HandleStaminaRegen();
        HandleHealthRegen();
        UpdateUI();
    }

    public void DrainStamina(float amount)
    {
        if (currentStamina <= 0) return;
        currentStamina -= amount;
        currentStamina = Mathf.Max(currentStamina, 0);
        staminaRegenTimer = staminaRegenDelay;
    }

    void HandleStaminaRegen()
    {
        if (staminaRegenTimer > 0) { staminaRegenTimer -= Time.deltaTime; return; }
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }
    }

    void HandleHealthRegen()
    {
        if (regenTimer > 0) { regenTimer -= Time.deltaTime; return; }
        if (currentHealth < maxHealth)
        {
            currentHealth += healthRegenRate * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        regenTimer = regenDelay;
        if (currentHealth <= 0) Die();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    public bool HasStamina(float amount) { return currentStamina >= amount; }
    public float GetStamina() { return currentStamina; }
    public float GetHealth() { return currentHealth; }
    public float GetMaxHealth() { return maxHealth; }      // ← add this
    public float GetMaxStamina() { return maxStamina; }     // ← add this

    void Die()
    {
        Debug.Log("Player died!");
    }

    void UpdateUI()
    {
        if (healthBar != null) healthBar.value = currentHealth / maxHealth;
        if (staminaBar != null) staminaBar.value = currentStamina / maxStamina;
        if (healthText != null) healthText.text = Mathf.CeilToInt(currentHealth) + " / " + (int)maxHealth;
        if (staminaText != null) staminaText.text = Mathf.CeilToInt(currentStamina) + " / " + (int)maxStamina;
    }
}