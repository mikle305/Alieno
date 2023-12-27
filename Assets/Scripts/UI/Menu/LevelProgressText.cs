using SaveData;
using Services.Save;
using TMPro;
using UnityEngine;
using VContainer;

namespace UI.Menu
{
    public class LevelProgressText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private SaveService _saveService;


        [Inject]
        public void Construct(SaveService saveService)
        {
            _saveService = saveService;
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