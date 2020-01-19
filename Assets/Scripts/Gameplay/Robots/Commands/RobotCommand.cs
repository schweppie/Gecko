using JP.Framework.Commands;
using UnityEngine;

namespace Gameplay.Robots.Commands
{
    public abstract class RobotCommand : Command
    {
        protected Robot robot;

        public void Initialize(Robot robot)
        {
            this.robot = robot;
        }

        public override void Undo()
        {
            Debug.Log("No more undo unfortunately.. But there is better looking visualisation now :D");
        }
    }
}
