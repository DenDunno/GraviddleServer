namespace GraviddleServer.Code.Repository;

public interface IDump<out T>
{
    IEnumerable<T> GetAll();
}