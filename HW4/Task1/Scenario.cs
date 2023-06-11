using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace HW4.Task1
{
    internal static class Scenario
    {
        public static void PlayStory()
        {
            GrandFather ded = new GrandFather(1);
            Plant plant = ded.PlantVegetable();

            Сharacter puller = ded;
            while (puller.PullVegetable(plant))
            {
                try
                {
                    puller = puller.CallNextFamilyMember();
                }
                catch (NotImplementedException)
                {
                    break;
                }
            }
        }
    }
}
