public interface ISerialiser<T>
{
    string Write(T type);
    T Read(string json);
}