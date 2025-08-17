public interface IValidator<T>
{
    bool IsValid(T model);
}