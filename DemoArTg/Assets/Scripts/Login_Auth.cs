using UnityEngine;
using TMPro;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Firebase;

public class LoginAuth : MonoBehaviour
{
    FirebaseAuth auth;
    [SerializeField] TMP_InputField email;
    [SerializeField] TMP_InputField password;
    [SerializeField] TextMeshProUGUI errorText;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        password.contentType = TMP_InputField.ContentType.Password;
        password.ForceLabelUpdate();
    }

    public async void OnLogin()
    {
        if (string.IsNullOrEmpty(email.text) || string.IsNullOrEmpty(password.text))
        {
            ShowError("Email y/o contraseña no pueden estar vacíos.");
            return;
        }

        try
        {
            await auth.SignInWithEmailAndPasswordAsync(email.text, password.text);
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
        string msg = "Email y/o contraseña invalidos.";
        ShowError(msg);
    }
}
