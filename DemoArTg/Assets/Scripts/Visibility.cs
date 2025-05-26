using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordToggle : MonoBehaviour
{
    public TMP_InputField passwordInput;
    public Button toggleButton;
    public Sprite showIcon;   // Ojo abierto
    public Sprite hideIcon;   // Ojo cerrado

    private bool isVisible = false;

    void Start()
    {
        // Asegura que empieza como oculto
        SetVisibility(false);

        // Asocia el botón
        toggleButton.onClick.AddListener(TogglePasswordVisibility);
    }

    void TogglePasswordVisibility()
    {
        isVisible = !isVisible;
        SetVisibility(isVisible);
    }

    void SetVisibility(bool visible)
    {
        passwordInput.contentType = visible ? TMP_InputField.ContentType.Standard : TMP_InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate();

        // Cambia ícono si usas imágenes
        if (toggleButton.image != null && showIcon != null && hideIcon != null)
        {
            toggleButton.image.sprite = visible ? hideIcon : showIcon;
        }
    }
}
