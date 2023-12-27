using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace DynmaicVars
{

    public class UpdateAssemblyInfoTask : Microsoft.Build.Utilities.Task
    {
        [Required]
        public string FileToUpdate { get; set; }

        public override bool Execute()
        {
            try
            {
                string text = File.ReadAllText(FileToUpdate);

                // Update GUID
                var newGuid = Guid.NewGuid();
                string guidPattern = @"\[assembly: Guid\("".*?""\)\]";
                string guidReplacement = $"[assembly: Guid(\"{newGuid}\")]";
                text = Regex.Replace(text, guidPattern, guidReplacement);

                // Update other attributes with random strings
                text = UpdateAttribute(text, "AssemblyTitle", GenerateRandomString());
                text = UpdateAttribute(text, "AssemblyDescription", GenerateRandomString());
                text = UpdateAttribute(text, "AssemblyCompany", GenerateRandomString());
                text = UpdateAttribute(text, "AssemblyProduct", GenerateRandomString());

                // Update Copyright with a random year
                int randomYear = new Random().Next(2000, DateTime.Now.Year + 1);
                string copyrightPattern = @"\[assembly: AssemblyCopyright\("".*?""\)\]";
                string copyrightReplacement = $"[assembly: AssemblyCopyright(\"Copyright © {randomYear}\")]";
                text = Regex.Replace(text, copyrightPattern, copyrightReplacement);

                File.WriteAllText(FileToUpdate, text);

                Log.LogMessage($"AssemblyInfo updated with new GUID and random strings.");
                return true;
            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex);
                return false;
            }
        }

        private string UpdateAttribute(string text, string attributeName, string newValue)
        {
            string pattern = $@"\[assembly: {attributeName}\("".*?""\)\]";
            string replacement = $"[assembly: {attributeName}(\"{newValue}\")]";
            return Regex.Replace(text, pattern, replacement);
        }

        private string GenerateRandomString()
        {
            return Guid.NewGuid().ToString().Substring(0, 8); 
        }
    }
}