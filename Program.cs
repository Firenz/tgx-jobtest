using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace bamboohr_jobtest
{
    class Program
    {

        static void Main(string[] args)
        {
            Jail jail = new Jail();
            CriminalOrganization criminalOrganization = LoadCriminalOrganizationDataJSON(@"data/datos.json");
            criminalOrganization.PrintHierarchy();
            
            jail.Enter("Jhon", ref criminalOrganization);
            Console.WriteLine();
            criminalOrganization.PrintHierarchy();

            jail.Exit("Jhon", ref criminalOrganization);
            Console.WriteLine();
            criminalOrganization.PrintHierarchy();

            jail.Enter("Pascual", ref criminalOrganization);
            Console.WriteLine();
            criminalOrganization.PrintHierarchy();

            jail.Exit("Pascual", ref criminalOrganization);
            Console.WriteLine();
            criminalOrganization.PrintHierarchy();

            jail.Enter("Andy", ref criminalOrganization);
            Console.WriteLine();
            criminalOrganization.PrintHierarchy();

            jail.Exit("Andy", ref criminalOrganization);
            Console.WriteLine();
            criminalOrganization.PrintHierarchy();

            criminalOrganization.PrintHighestBoss();

            Console.WriteLine();
        }

        public static CriminalOrganization LoadCriminalOrganizationDataJSON(string filePath)
        {
            CriminalOrganization criminalOrganization = new CriminalOrganization();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                JObject jObj = JObject.Parse(json);

                foreach(JToken item in jObj["members"])
                {
                    criminalOrganization.AddMember(
                        item["name"].ToString(),
                        Int16.Parse(item["seniority"].ToString()),
                        item["boss"].ToString()
                    );
                }
                Console.WriteLine();
            }

            return criminalOrganization;
        }
    }
}
