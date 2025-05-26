using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase;

public class EmailAuth : MonoBehaviour
{
    FirebaseAuth auth;
    [SerializeField] TMP_InputField email;
    [SerializeField] TMP_InputField password;
    [SerializeField] TMP_InputField confirmPassword;
    [SerializeField] TextMeshProUGUI errorText;

    private bool cargarHome = false;
    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        // Cifrar campos de contraseña
        password.contentType = TMP_InputField.ContentType.Password;
        confirmPassword.contentType = TMP_InputField.ContentType.Password;

        password.ForceLabelUpdate();
        confirmPassword.ForceLabelUpdate();
    }

    void Update()
    {
        if (cargarHome)
        {
            cargarHome = false;
            SceneManager.LoadScene("Home_view");
        }
    }
    public void SingUp()
    {
        // Validar que las contraseñas coincidan
        if (password.text != confirmPassword.text)
        {
            Debug.LogWarning("Las contraseñas no coinciden!");
            if (errorText != null)
                errorText.text = "Las contraseñas no coinciden!";
            return;
        }

        if (password.text.Length < 6)
        {
            Debug.LogWarning("La contraseña debe tener al menos 6 caracteres!");
            if (errorText != null)
                errorText.text = "La contraseña debe tener al menos 6 caracteres!";
            return;
        }

        if (string.IsNullOrEmpty(email.text) || string.IsNullOrEmpty(password.text))
        {
            Debug.LogWarning("Email y/o contraseña no pueden estar vacíos!");
            if (errorText != null)
                errorText.text = "Email y/o contraseña no pueden estar vacíos!";
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                // Mostrar mensaje específico si el correo ya está en uso
                if (errorText != null && task.Exception != null)
                {
                    foreach (var e in task.Exception.Flatten().InnerExceptions)
                    {
                        if (e is FirebaseException firebaseEx)
                        {
                            var authError = (AuthError)firebaseEx.ErrorCode;
                            if (authError == AuthError.EmailAlreadyInUse)
                            {
                                errorText.text = "El correo ya está en uso.";
                                return;
                            }
                        }
                    }
                    errorText.text = "Error al crear usuario. Intenta nuevamente.";
                }
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Usuario Firebase creado correctamente: {0} ({1})", result.User.DisplayName, result.User.UserId);

            cargarHome = true;
        });
    }

    public void LogIn()
    {
        auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                // Mostrar mensaje específico si el correo o la contraseña no son válidos
                if (errorText != null && task.Exception != null)
                {
                    foreach (var e in task.Exception.Flatten().InnerExceptions)
                    {
                        if (e is FirebaseException firebaseEx)
                        {
                            var authError = (AuthError)firebaseEx.ErrorCode;
                            if (authError == AuthError.InvalidEmail)
                            {
                                errorText.text = "Correo electronico inválido.";
                                return;
                            }
                            if (authError == AuthError.WrongPassword)
                            {
                                errorText.text = "Contraseña incorrecta.";
                                return;
                            }
                            if (authError == AuthError.UserNotFound)
                            {
                                errorText.text = "No existe una cuenta con este correo.";
                                return;
                            }
                        }
                    }
                    errorText.text = "Error al iniciar sesión. Intenta nuevamente.";
                }
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Sesion iniciada correctamente: {0} ({1})", result.User.DisplayName, result.User.UserId);

            cargarHome = true;
        });
    }
}
