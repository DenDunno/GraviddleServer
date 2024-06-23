namespace TelegramBotNM.Repository.Query;

public abstract class Queries<TRecord, TKey>
{
    public virtual string Insert(TRecord element) { throw new NotImplementedException(); }
    public virtual string Remove(TKey key) { throw new NotImplementedException(); }
    public virtual string Update(TRecord element) { throw new NotImplementedException(); }
    public virtual string Contains(TKey key) { throw new NotImplementedException(); }
    public virtual string GetAll() { throw new NotImplementedException(); }
    public virtual string Fetch(TKey key) { throw new NotImplementedException(); }
}