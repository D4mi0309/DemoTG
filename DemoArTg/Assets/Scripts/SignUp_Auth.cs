using UnityEngine;
using TMPro;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Firebase;

public class RegisterAuth : MonoBehaviour
{
    FirebaseAuth auth;
    [SerializeField] TMP_InputField email;
    [SerializeField] TMP_InputField password;
    [SerializeField] TMP_InputField confirmPassword;
    [SerializeField] TextMeshProUGUI errorText;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        password.contentType = TMP_InputField.ContentType.Password;
        confirmPassword.contentType = TMP_InputField.ContentType.Password;
        password.ForceLabelUpdate();
        confirmPassword.ForceLabelUpdate();
    }

    public async void OnRegister()
    {
        if (password.text != confirmPassword.text)
        {
            ShowError("Las contraseñas no coinciden!");
            return;
        }

        if (string.IsNullOrEmpty(email.text) || string.IsNullOrEmpty(password.text))
        {
            ShowError("Email y/o contraseña no pueden estar vacíos!");
            return;
        }

        if (password.text.Length < 6)
        {
            ShowError("La contraseña debe tener minimo 6 caracteres.");
            return;
        }

        try
        {
            await auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text);
            SceneManager.LoadScene("Home_view");
        }
        catch (FirebaseException ex)
        {
            HandleError(ex);
        }
    }

    void ShowError(string message)
    {
        if (errorText != null)
            errorText.text = message;
    }

    void HandleError(FirebaseException ex)
    {
        string msg = "Error. Corrija los campos e intente nuevamente.";
        ShowError(msg);
    }
}
