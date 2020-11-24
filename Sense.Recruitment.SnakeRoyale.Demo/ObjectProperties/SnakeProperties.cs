using Sense.Recruitment.SnakeRoyale.Engine;
using System.Collections.Generic;

namespace Sense.Recruitment.SnakeRoyale.Demo.Logic.Models
{
    public class SnakeProperties : ObjectProperties
    {
        public int Length { get; set; } = 1;
        public LinkedList<GameObject> Tail { get; set; } = new LinkedList<GameObject>();
        public GameObject Head { get; set; }
       
        public SnakeProperties(GameObject head)
        {
            Head = head;
        }
       
        public int Score { get; set; } = 0;
    }
}
