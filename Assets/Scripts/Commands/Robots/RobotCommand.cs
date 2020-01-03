using Gameplay.Robots;

namespace Commands.Robots
{
    public abstract class RobotCommand : Command
    {
        protected Robot robot;

        public void Initialize(Robot robot)
        {
            this.robot = robot;
        }
    }
}
