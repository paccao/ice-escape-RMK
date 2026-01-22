using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System;

public class MapNamePromptUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;

    private TMP_Text okButtonText;
    private Action<string> onConfirm;

    private static readonly Regex validNameRegex = new Regex("[^a-zA-Z0-9_-]", RegexOptions.Compiled);

    void Awake()
    {
        panel.SetActive(false);
        okButton.onClick.AddListener(OnOkClicked);
        cancelButton.onClick.AddListener(OnCancelClicked);
        okButtonText = okButton.GetComponentInChildren<TMP_Text>();
        if (okButtonText == null)
        {
            Debug.LogWarning("No TMP_Text found on OK button child!");
        }
    }

    public void Show(Action<string> onConfirmCallback, string okText = "Save")
    {
        onConfirm = onConfirmCallback;
        inputField.text = "";
        panel.SetActive(true);
        inputField.ActivateInputField();
        if (okButtonText != null)
            okButtonText.text = okText;

        // Set button color
        Color targetColor = okText == "Load"
            ? new Color32(0x31, 0x98, 0xCC, 0xFF) // #3198CC
            : new Color32(0x32, 0xCD, 0x88, 0xFF); // #32CD88
        var colors = okButton.colors;
        colors.normalColor = targetColor;
        colors.highlightedColor = targetColor;
        colors.pressedColor = targetColor;
        okButton.colors = colors;
    }

    private void OnOkClicked()
    {
        string rawName = inputField.text;
        string safeName = validNameRegex.Replace(rawName, "");
        if (string.IsNullOrEmpty(safeName))
        {
            // Optionally show error to user
            inputField.text = "";
            inputField.placeholder.GetComponent<TMP_Text>().text = "Invalid name!";
            return;
        }
        panel.SetActive(false);
        onConfirm?.Invoke(safeName);
    }

    private void OnCancelClicked()
    {
        panel.SetActive(false);
    }
}
