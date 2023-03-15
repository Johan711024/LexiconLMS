using LexiconLMS.Shared.DTOs;

namespace LexiconLMS.Client.State
{
    public class StateContainer
    {
        private CourseDto? currCourse;
        private ModuleDto? currModule;
        private ActivityDto? currActivity;
        private bool modalOpenStatus = false;
        private string modalContentType = "";

        private string? savedString;

        public CourseDto EditCourse
        {
            get => currCourse!;
            set
            {
                currCourse = value;
                NotifyStateChanged();
            }
        }
        public ModuleDto EditModule
        {
            get => currModule!;
            set
            {
                currModule = value;
                NotifyStateChanged();
            }
        }
        public ActivityDto EditActivity
        {
            get => currActivity!;
            set
            {
                currActivity = value;
                NotifyStateChanged();
            }
        }
        public bool ModalOpen
        {
            get => modalOpenStatus;
            set
            {
                modalOpenStatus = value;
                NotifyStateChanged();
            }
        }
        public string ModalContentType
        {
            get => modalContentType;
            set
            {
                modalContentType = value;
                NotifyStateChanged();
            }
        }
        public string Property
        {
            get => savedString ?? string.Empty;
            set
            {
                savedString = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
