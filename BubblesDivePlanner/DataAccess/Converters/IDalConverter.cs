public interface IDalConverter<T, I>
{
    T ConvertTo(I abstractType);
    I ConvertFrom(T dalCoverterType);
}