public interface IProgressable
{
    public bool IsBetterProgress { get; }
    public void SaveProgress();
}
