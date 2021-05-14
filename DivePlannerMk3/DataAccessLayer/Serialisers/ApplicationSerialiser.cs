using System;
using System.Collections.Generic;
using System.IO;
using DivePlannerMk3.Contracts.DataAccessContracts;
using DivePlannerMk3.DataAccessLayer.EntityModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DivePlannerMk3.DataAccessLayer.Serialisers
{
    public class ApplicationSerialiser
    {
        public void SerialiseApplication(List<IEntityModel> entityModels, string fileName)
        {
            //string fileName = $"DivePlan.json";
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    var jsonFile = string.Empty;
                    //TODO AH may need to change this to have each entity model hosted in a container
                    //TODO AH } closing needs to be removed until the true last entity model. Also consider first comment
                    foreach (var entityModel in entityModels)
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

        public IEnumerable<IEntityModel> DeserialiseApplication(string fileResult)
        {
            var fileContents = FilePathToFileContents(fileResult);

            //TODO AH Invalid cast
            yield return (DivePlanEntityModel)JsonConvert.DeserializeObject(fileContents);
            yield return (DiveInfoEntityModel)JsonConvert.DeserializeObject(fileContents);
            yield return (DiveResultsEntityModel)JsonConvert.DeserializeObject(fileContents);
            yield return (DiveHeaderEntityModel)JsonConvert.DeserializeObject(fileContents);         
        }

        //TODO AH Ensure the whole thing can load, catch exceptions
        private string FilePathToFileContents(string fileResult) => JObject.Parse(File.ReadAllText(fileResult)).ToString();
    }
}