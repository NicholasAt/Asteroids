namespace Assets.Game.Scripts.Data.Configs.Settings
{
    public interface IValidatableSettings
    {
        bool TryValidate(out string error);
    }
}