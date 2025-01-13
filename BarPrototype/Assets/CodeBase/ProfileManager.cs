using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [Header("Registration Panel")]
    [SerializeField] private GameObject registrationPanel;
    [SerializeField] private InputField nameInputField;
    [SerializeField] private TMP_Dropdown genderDropdown;
    [SerializeField] private Button submitButton;

    [Header("Profile Panel")]
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private Text nameText;
    [SerializeField] private Text genderText;
    [SerializeField] private Image profileImage;
    [SerializeField] private Sprite maleSprite;
    [SerializeField] private Sprite femaleSprite;

    private const string NameKey = "UserName";
    private const string GenderKey = "UserGender";

    private void Start()
    {
        if (PlayerPrefs.HasKey(NameKey) && PlayerPrefs.HasKey(GenderKey))
        {
            ShowProfile();
        }
        else
        {
            registrationPanel.SetActive(true);
            profilePanel.SetActive(false);
        }

        submitButton.onClick.AddListener(OnSubmit);
    }

    private void OnSubmit()
    {
        string userName = nameInputField.text;
        string gender = genderDropdown.value == 0 ? "Male" : "Female";

        PlayerPrefs.SetString(NameKey, userName);
        PlayerPrefs.SetString(GenderKey, gender);
        PlayerPrefs.Save();

        ShowProfile();
    }

    private void ShowProfile()
    {
        registrationPanel.SetActive(false);

        string userName = PlayerPrefs.GetString(NameKey);
        string gender = PlayerPrefs.GetString(GenderKey);

        nameText.text = $"Name: {userName}";
        genderText.text = $"Gender: {(gender == "Male" ? "Мужской" : "Женский")}";
        profileImage.sprite = gender == "Male" ? maleSprite : femaleSprite;

        profilePanel.SetActive(true);
    }
}
