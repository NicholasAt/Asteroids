using System;

namespace Assets.Scripts.Gameplay.Interfaces.Common
{
    public interface IPoolable<TContext>
    {
        event Action OnShow;
        event Action OnHide;
        void Show(TContext context);
        void Hide();
    }
}