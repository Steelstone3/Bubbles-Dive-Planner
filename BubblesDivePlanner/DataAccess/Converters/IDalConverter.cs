public interface IDalConverter<DalModel, Model>
{
    DalModel ConvertTo(Model abstractType);
    Model ConvertFrom(DalModel dalCoverterType);
}