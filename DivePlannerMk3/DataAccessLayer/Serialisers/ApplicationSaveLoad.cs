using System;
using System.Collections.Generic;
using System.IO;
using DivePlannerMk3.Contracts.DataAccessContracts;
using Newtonsoft.Json;

namespace DivePlannerMk3.DataAccessLayer.Serialisers
{
    public class ApplicationSaveLoad
    {
            /*string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                Environment.OSVersion.Platform == PlatformID.MacOSX)
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");*/

            // File name  
            //string fileName = $"{homePath}{Path.DirectorySeparatorChar}{Path.DirectorySeparatorChar}DivePlan.json";

        //TODO AH Entity Models parameters
        public void SaveApplication(List<IEntityModel> entityModels)
        {
            
            string fileName = $"DivePlan.json";
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    var jsonFile = string.Empty;

                    foreach(var entityModel in entityModels)
                    {
                        jsonFile += JsonConvert.SerializeObject(entityModel, Formatting.Indented);
                    }

                    writer.Write(jsonFile);
                }
            }
            catch (UnauthorizedAccessException uaex)
            {
                Console.Write(uaex.Message);
            }
            catch (IOException ioex)
            {
                Console.Write(ioex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public List<IEntityModel> LoadApplication()
        {
            //TODO AH Load from the default file location for now
            return null;
        }
    }
}