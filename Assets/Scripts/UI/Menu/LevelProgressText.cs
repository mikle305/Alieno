using SaveData;
using Services.Save;
using TMPro;
using UnityEngine;

namespace UI.Menu
{
    public class LevelProgressText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private SaveService _saveService;


        private void Awake()
        {
            _saveService = SaveService.Instance;
            _saveService.ProgressSaved += DisplayLevel;
            DisplayLevel();
        }

        private void DisplayLevel()
        {
            PlayerData playerProgress = _saveService.Progress.PlayerData;
            _text.text = $"{playerProgress.Level}-{playerProgress.Room}";
        }
    }
}