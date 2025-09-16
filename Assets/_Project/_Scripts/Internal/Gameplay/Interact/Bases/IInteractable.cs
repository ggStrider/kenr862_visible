namespace Internal.Gameplay.Interact
{
    public interface IInteractable
    {
        public void Interact(bool isInteractButtonPressed);
        public int Priority { get; set; }
    }
}