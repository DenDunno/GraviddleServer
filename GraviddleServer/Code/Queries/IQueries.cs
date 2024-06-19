namespace GraviddleServer.Code.Queries;

public interface IQueries<in T>
{
    string Insert(T element);
    string Remove(T element);
    string Contains(T element);
    string GetAll();
}