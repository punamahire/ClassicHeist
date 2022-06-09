using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassicHeist
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IRobber> rolodex = new List<IRobber>();

            Hacker h1 = new Hacker()
            {
                Name = "Billy",
                SkillLevel = 70,
                PercentageCut = 40
            };
            Hacker h2 = new Hacker()
            {
                Name = "Bob",
                SkillLevel = 80,
                PercentageCut = 50
            };
            LockSpecialist ls1 = new LockSpecialist()
            {
                Name = "Jacky",
                SkillLevel = 60,
                PercentageCut = 25
            };
            LockSpecialist ls2 = new LockSpecialist()
            {
                Name = "Jackson",
                SkillLevel = 75,
                PercentageCut = 30
            };
            Muscle m1 = new Muscle()
            {
                Name = "Tylor",
                SkillLevel = 65,
                PercentageCut = 10
            };
            Muscle m2 = new Muscle()
            {
                Name = "Tommy",
                SkillLevel = 90,
                PercentageCut = 20
            };

            rolodex.Add(h1);
            rolodex.Add(h2);
            rolodex.Add(ls1);
            rolodex.Add(ls2);
            rolodex.Add(m1);
            rolodex.Add(m2);

            Console.WriteLine($"The number of current operatives in Rolodex is: {rolodex.Count}");

            foreach (var robber in rolodex)
            {
                Console.WriteLine($"{robber.Name} is a {robber.GetType()}");
            }

            Console.Write("Enter the name of the new possible crew member: ");
            string name = Console.ReadLine();

            while (!String.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("1. Hacker (Disables alarms)");
                Console.WriteLine("2. Muscle (Disarms guards)");
                Console.WriteLine("3. Lock Specialist (cracks vault)");
                Console.Write("Select which specialty this operative has (1-3): ");

                int speciality = Int32.Parse(Console.ReadLine());

                Console.WriteLine();
                Console.Write("Enter the crew member's skill level (1 - 100): ");
                int skillLvl = Int32.Parse(Console.ReadLine());

                Console.WriteLine();
                Console.Write("How much percentage cut the crew member demands for each mission? (1 -100): ");
                int percentCut = Int32.Parse(Console.ReadLine());

                switch (speciality)
                {
                    case 1:
                        Hacker h = new Hacker()
                        {
                            Name = name,
                            SkillLevel = skillLvl,
                            PercentageCut = percentCut
                        };
                        rolodex.Add(h);
                        break;
                    case 2:
                        LockSpecialist ls = new LockSpecialist()
                        {
                            Name = name,
                            SkillLevel = skillLvl,
                            PercentageCut = percentCut
                        };
                        rolodex.Add(ls);
                        break;
                    case 3:
                        Muscle ms = new Muscle()
                        {
                            Name = name,
                            SkillLevel = skillLvl,
                            PercentageCut = percentCut
                        };
                        rolodex.Add(ms);
                        break;
                    default:
                        Console.WriteLine("Enter the appropriate speciality option.");
                        break;
                }

                Console.Write("Enter the name of the new possible crew member: ");
                name = Console.ReadLine();
            }

            // create random number
            int generateRandom()
            {
                Random r = new Random();
                int randomNum = r.Next(0, 101);
                return randomNum;
            }

            Bank myBank = new Bank();
            myBank.AlarmScore = generateRandom();
            myBank.VaultScore = generateRandom();
            myBank.SecurityGuardScore = generateRandom();

            Random r = new Random();
            int randomNum = r.Next(50000, 100000);
            myBank.CashOnHand = randomNum;

            string mostSecure = "";
            string leastSecure = "";
            if (myBank.AlarmScore > myBank.VaultScore)
            {
                if (myBank.AlarmScore > myBank.SecurityGuardScore)
                {
                    mostSecure = "Alarm";
                }
                else
                {
                    mostSecure = "SecurityGuard";
                }
            }
            else if (myBank.VaultScore > myBank.SecurityGuardScore)
            {
                mostSecure = "Vault";
            }
            else
            {
                mostSecure = "SecurityGuard";
            }

            if (myBank.AlarmScore < myBank.VaultScore)
            {
                if (myBank.AlarmScore < myBank.SecurityGuardScore)
                {
                    leastSecure = "Alarm";
                }
                else
                {
                    leastSecure = "SecurityGuard";
                }
            }
            else if (myBank.VaultScore < myBank.SecurityGuardScore)
            {
                leastSecure = "Vault";
            }
            else
            {
                leastSecure = "SecurityGuard";
            }

            Console.WriteLine($"Most secure system in the bank: {mostSecure}");
            Console.WriteLine($"Least secure system in the bank: {leastSecure}");

            // Display Rolodex Report 
            int i = 0;
            string splType;
            Console.WriteLine("Index Name   Speciality  Skill-level  Percentage-cut");
            foreach (var rob in rolodex)
            {
                splType = rob.GetType().ToString();
                Console.WriteLine($"{i + 1}.    {rob.Name}  {splType.Split('.')[1]}   {rob.SkillLevel}  {rob.PercentageCut}");
                i++;
            }

            List<IRobber> crew = new List<IRobber>();
            Console.WriteLine();

            Console.Write($"Select the operative to include in the heist (1 - {i}): ");
            int index = Int32.Parse(Console.ReadLine());
            crew.Add(rolodex[index - 1]);
            int crewIndex = 0;
            Console.WriteLine("Name   Speciality  Skill-level  Percentage-cut");
            Console.WriteLine($"{crew[0].Name}  {crew[0].GetType()}   {crew[0].SkillLevel}  {crew[0].PercentageCut}");

            int num = -1;
            int totalCut = crew[0].PercentageCut;
            List<int> inputIndexes = new List<int>();
            inputIndexes.Add(index);

            while (true)
            {
                Console.WriteLine();
                Console.Write($"Select the operative to include in the heist (1 - {i}): ");

                // Int.TryParse will return false if the string is not a valid integer 
                if (!int.TryParse(Console.ReadLine(), out num))
                {
                    break;
                }
                index = num;
                Console.WriteLine("percent cut: " + crew[crewIndex].PercentageCut);

                if (!inputIndexes.Contains(index) && totalCut < 100
                        && (totalCut + crew[crewIndex].PercentageCut) < 100)
                {
                    inputIndexes.Add(index);
                    crew.Add(rolodex[index - 1]);
                    crewIndex++;

                    Console.WriteLine("Name   Speciality  Skill-level  Percentage-cut");
                    foreach (var rob in crew)
                    {
                        splType = rob.GetType().ToString();
                        Console.WriteLine($"{rob.Name}  {splType.Split('.')[1]}   {rob.SkillLevel}  {rob.PercentageCut}");
                        totalCut += rob.PercentageCut;
                    }
                }
            }

            Console.WriteLine();
            foreach (var robber in crew)
            {
                robber.PerformSkill(myBank);
            }

            Console.WriteLine();
            if (myBank.AlarmScore <= 0 && myBank.VaultScore <= 0
                    && myBank.SecurityGuardScore <= 0)
            {
                Console.WriteLine("Heist was a huge success!!");
                Console.WriteLine();

                foreach (var robber in crew)
                {
                    Console.WriteLine($"{robber.Name}'s share: {robber.PercentageCut}");
                }
            }
            else
            {
                Console.WriteLine("Heist failed!!");
            }
        }

    }
}

// Open Issues:
// 1. the report should not include operatives that require a percentage cut that can't be offered
// (line 223 -224 needs to be worked on)
// 2.  (You may want to update the IRobber interface and/or the implementing classes to be able to print out the specialty)
// (good to have this implemented, not necessary though.)

