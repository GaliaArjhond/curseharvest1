using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    [Header("Book")]
    [SerializeField] private GameObject bookPanel;

    [Header("Pages")]
    [SerializeField] private GameObject[] pages; // drag all 7 pages here

    [Header("Tab Buttons")]
    [SerializeField] private Button[] tabButtons;       // all 7 buttons
    [SerializeField] private Color selectedColor = new Color(1f, 0.9f, 0.6f, 1f);
    [SerializeField] private Color unselectedColor = Color.white;

    private bool isOpen = false;
    private int currentPage = 0;

    void Start()
    {
        bookPanel.SetActive(false);
        ShowPage(0); // default to first tab
    }

    void Update()
    {
        // this will print EVERY frame so we know Update is running
        Debug.Log("Update running — Tab pressed: " + Keyboard.current.tabKey.wasPressedThisFrame);

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            Debug.Log("TAB PRESSED — toggling book");
            EventSystem.current.SetSelectedGameObject(null);
            ToggleBook();
        }
    }

    public void ToggleBook()
    {
        isOpen = !isOpen;
        bookPanel.SetActive(isOpen);

    }

    // call this from each tab button's OnClick
    public void ShowPage(int pageIndex)
    {
        currentPage = pageIndex;

        // hide all pages
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] != null)
                pages[i].SetActive(false);
        }

        // show selected page
        if (pageIndex >= 0 && pageIndex < pages.Length)
            pages[pageIndex].SetActive(true);

        // update button colors
        UpdateTabColors();
    }

    void UpdateTabColors()
    {
        for (int i = 0; i < tabButtons.Length; i++)
        {
            if (tabButtons[i] == null) continue;

            Image btnImage = tabButtons[i].GetComponent<Image>();
            if (btnImage != null)
                btnImage.color = (i == currentPage) ? selectedColor : unselectedColor;
        }
    }

    // close when pressing Tab or Escape
    void OnEnable()
    {
        // also allow Escape to close
    }
}