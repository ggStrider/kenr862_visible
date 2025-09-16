namespace Internal.Gameplay.GameItems
{
    public interface IUsableGameItem
    {
        public void UseItem(bool isUseButtonPressed);
        public void OnSwitchThisItem();
    }
}