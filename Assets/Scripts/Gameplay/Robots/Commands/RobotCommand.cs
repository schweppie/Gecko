using JP.Framework.Commands;

namespace Gameplay.Robots.Commands
{
    public abstract class RobotCommand : Command
    {
        protected Robot robot;

        public void Initialize(Robot robot)
        {
            this.robot = robot;
        }

        public virtual void Prewarm()
        {
        }
    }
}
