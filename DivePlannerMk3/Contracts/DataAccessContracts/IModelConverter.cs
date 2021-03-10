using Newtonsoft.Json;

namespace DivePlannerMk3.Contracts
{
    public interface IModelConverter
    {
        void ModelToEntity();

        void EntityToModel();
    }
}