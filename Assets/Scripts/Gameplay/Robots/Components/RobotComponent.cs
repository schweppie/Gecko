namespace Gameplay.Robots.Components
{
    public abstract class RobotComponent
    {
        protected Robot robot;
        public virtual void Initialize(Robot robot)
        {
            this.robot = robot;
        }
    }
}
