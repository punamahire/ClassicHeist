using System;

namespace ClassicHeist
{
    public class Muscle : IRobber
    {
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; }

        public void PerformSkill(Bank bank)
        {
            bank.SecurityGuardScore -= SkillLevel;
            Console.WriteLine($"{Name} is handling the security guards. Decreased security {SkillLevel} points");
            if (bank.SecurityGuardScore <= 0)
            {
                Console.WriteLine($"{Name} has beaten the security guards!");
            }
        }
    }
}