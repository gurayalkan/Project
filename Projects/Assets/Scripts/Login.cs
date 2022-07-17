using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField] private Text _usernameText;
    [SerializeField] private InputField _passwordText;
    [SerializeField] private Text _informationText;


    public void LoginButton()
    {
        if (_usernameText.text.Equals("") || _passwordText.text.Equals(""))
            _informationText.text = "Username or password cannot be empty...";

        else if (_usernameText.text.Equals("Admin") && _passwordText.text.Equals("Admin"))
        {
            _informationText.text = "Correct!";
            SceneManager.LoadScene(1);
        }


        else
            _informationText.text = "Fail!";



    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
