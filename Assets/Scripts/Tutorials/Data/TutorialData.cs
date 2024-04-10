using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TutorialData
{
    [field: Header("Tutorial")]
    [SerializeField] int _id;
    [SerializeField] string _tutorialName;
    [SerializeField] string _tutorialText;
    [SerializeField] string _imagePath;
    [SerializeField] bool _hasNextPage;
    [SerializeField] int _nextPageId;

    public int Id => _id;
    public string TutorialName => _tutorialName;
    public string TutorialText => _tutorialText;
    public string ImagePath => _imagePath;
    public bool hasNextPage => _hasNextPage;
    public int NextPageId => _nextPageId;

    private Image _tutorialImage;
    public Image TutorialImage
    {
        get
        {
            if (_tutorialImage == null)
            {
                _tutorialImage = Resources.Load<Image>(_imagePath);
            }
            return _tutorialImage;
        }
    }
}
