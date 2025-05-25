public interface IState<T>
{
    public void ExecuteEnter(T sender);
    public void Execute();
    public void FixedExecute();
    public void ExecuteExit();
}
