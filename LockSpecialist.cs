using System;

namespace ClassicHeist
{
    public class LockSpecialist : IRobber
    {
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; }

        public void PerformSkill(Bank bank)
        {
            bank.VaultScore -= SkillLevel;
            Console.WriteLine($"{Name} is opening the vault. Decreased security {SkillLevel} points");

            if (bank.VaultScore <= 0)
            {
                Console.WriteLine($"{Name} has successfully opened the vault!");
            }
        }
    }
}