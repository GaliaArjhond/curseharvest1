using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Hotbar : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private Image[] slots;

    [Header("Selection Visual")]
    [SerializeField] private Color normalColor = new Color(0.1f, 0.1f, 0.1f, 0.8f);
    [SerializeField] private Color selectedColor = new Color(0.9f, 0.7f, 0.1f, 1f);

    private int selectedSlot = 0;
    private int slotCount = 8;

    void Start()
    {
        UpdateVisual();
    }

    void Update()
    {
        HandleNumberKeys();
        HandleScrollWheel();
    }

    void HandleNumberKeys()
    {
        // new Input System uses Keyboard class
        if (Keyboard.current.digit1Key.wasPressedThisFrame) SelectSlot(0);
        if (Keyboard.current.digit2Key.wasPressedThisFrame) SelectSlot(1);
        if (Keyboard.current.digit3Key.wasPressedThisFrame) SelectSlot(2);
        if (Keyboard.current.digit4Key.wasPressedThisFrame) SelectSlot(3);
        if (Keyboard.current.digit5Key.wasPressedThisFrame) SelectSlot(4);
        if (Keyboard.current.digit6Key.wasPressedThisFrame) SelectSlot(5);
        if (Keyboard.current.digit7Key.wasPressedThisFrame) SelectSlot(6);
        if (Keyboard.current.digit8Key.wasPressedThisFrame) SelectSlot(7);
    }

    void HandleScrollWheel()
    {
        // new Input System uses Mouse class
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll > 0f)
        {
            selectedSlot--;
            if (selectedSlot < 0) selectedSlot = slotCount - 1;
            UpdateVisual();
        }
        else if (scroll < 0f)
        {
            selectedSlot++;
            if (selectedSlot >= slotCount) selectedSlot = 0;
            UpdateVisual();
        }
    }

    public void SelectSlot(int index)
    {
        selectedSlot = index;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].color = (i == selectedSlot) ? selectedColor : normalColor;
        }
    }

    public int GetSelectedSlot() { return selectedSlot; }
}