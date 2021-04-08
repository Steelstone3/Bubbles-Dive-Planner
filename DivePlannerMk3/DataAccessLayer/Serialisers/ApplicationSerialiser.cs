using System;
using System.Collections.Generic;
using System.IO;
using DivePlannerMk3.Contracts.DataAccessContracts;
using Newtonsoft.Json;

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

        public List<IEntityModel> DeserialiseApplication(string fileResult)
        {
            //TODO AH potentially load from the file based on the file result in a given directory that would also need to be known
            //TODO AH then extract the file to a reader look a tut. up online
            return null;
        }
    }
}