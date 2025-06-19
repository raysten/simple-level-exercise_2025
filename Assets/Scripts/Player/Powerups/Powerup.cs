namespace Player.Powerups
{
    public class Powerup
    {
        private readonly float _duration;
        private float _lifetime;

        public EPlayerStatistic Statistic { get; }
        public float MultiplierValue { get; }

        public Powerup(PowerupConfig config)
        {
            Statistic = config.Statistic;
            _duration = config.Duration;
            MultiplierValue = config.MultiplierValue;
        }

        public bool UpdateAndCheckIfFinished(float deltaTime)
        {
            var isFinished = false;
            _lifetime += deltaTime;

            if (_lifetime >= _duration)
            {
                isFinished = true;
            }

            return isFinished;
        }
    }
}
