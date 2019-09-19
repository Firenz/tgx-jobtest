using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace bamboohr_jobtest
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!\n");
            Jail jail = new Jail();
            CriminalOrganization criminalOrganization = LoadCriminalOrganizationDataJSON(@"data/datos.json");
            criminalOrganization.PrintHierarchy();
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
