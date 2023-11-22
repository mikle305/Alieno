namespace Additional.Game
{
    public interface IObjectPool<T>
    {
        public T Take();
        public T[] TakeMany(int count);
        public void Release(T obj);
    }
}