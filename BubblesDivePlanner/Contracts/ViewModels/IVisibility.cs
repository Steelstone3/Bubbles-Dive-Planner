namespace BubblesDivePlanner.Contracts.ViewModels
{
    public interface IVisibility
    {
        public bool IsUiVisible { get; set; }
        
        public bool IsUiEnabled { get; set; }

        public bool IsVisible { get; set; }
    }
}