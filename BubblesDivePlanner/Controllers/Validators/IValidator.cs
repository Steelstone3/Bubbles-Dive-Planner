public interface IValidator<T>
{
    bool Validate(T model);
}