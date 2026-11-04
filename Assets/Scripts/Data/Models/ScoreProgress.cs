using System;

namespace Assets.Scripts.Data.Models
{
    public class ScoreProgress
    {
        public int Score { get; private set; }
        public event Action<int> OnScoreChange;

        public void IncrementScore(int increment)
        {
            Score += increment;
            OnScoreChange?.Invoke(Score);
        }
    }
}