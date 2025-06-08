using System.Collections;
using UnityEngine;
using SimpleFileBrowser;
using System.IO;
using Zenject;
using UnityEngine.UI;

public class AvatarChanger : MonoBehaviour
{
    [SerializeField] private string _notificationText = "Необходимо авторизоваться!";
    [SerializeField] private Image _avatarImage;

    [Inject] private UserData _userData;
    [Inject] private NotificationDisplayer _notificationDisplayer;
    [Inject] private StorageService _storageService;

    private void OnEnable()
    {
        LoadAvatarAsPosible();
    }

    public void OnAvatarChangePressed()
    {
        if (_userData == null || _userData.User == null)
        {
            _notificationDisplayer.ShowNotification(_notificationText);
            return;
        }

        #if UNITY_EDITOR || UNITY_STANDALONE
        OpenFileBrowser();
        #elif UNITY_ANDROID
        PickImage();
        #endif
    }

    private void OpenFileBrowser()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    private void PickImage()
    {
        NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                NativeGallery.SaveImageToGallery(path, "Kisy", "avatar.png", (success, error) =>
                {
                    if (success)
                    {
                        ApplyAvatar(path);
                    }
                    else
                    {
                        Debug.LogError("Ошибка сохранения изображения: " + error);
                    }
                });
            }
        });
    }

    private IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, true, null, null, "Select Files", "Load");

        if (FileBrowser.Success)
            OnFilesSelected(FileBrowser.Result);
    }

    private void OnFilesSelected(string[] filePaths)
    {
        for (int i = 0; i < filePaths.Length; i++)
            Debug.Log(filePaths[i]);

        string filePath = filePaths[0];

        string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(filePath));
        FileBrowserHelpers.CopyFile(filePath, destinationPath);
        ApplyAvatar(destinationPath);
    }

    private Texture2D LoadTextureFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Файл не существует: " + filePath);
            return null;
        }

        try
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
            return texture;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Ошибка загрузки текстуры: " + e.Message);
            return null;
        }
    }
    private Sprite ConvertToSprite(Texture2D texture)
    {
        return Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f)
        );
    }

    public void LoadAvatarAsPosible()
    {
        if (_userData == null || _userData.User == null)
        {
            _notificationDisplayer.ShowNotification(_notificationText);
            return;
        }

        if (!string.IsNullOrEmpty(_userData.User.AvatarImagePath))
        {
            Texture2D texture = LoadTextureFromFile(_userData.User.AvatarImagePath);

            if (texture != null)
            {
                _avatarImage.sprite = ConvertToSprite(texture);
            }
            else
            {
                Debug.LogWarning("Avatar image not found or could not be loaded.");
            }
        }
    }

    public void ApplyAvatar(string path)
    {
        _userData.User.InitializeAvatar(path);
        _avatarImage.sprite = ConvertToSprite(LoadTextureFromFile(path));
        StartCoroutine(_storageService.Save());
    }
}