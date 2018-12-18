namespace CkanApiHelper.Core
{
    public interface IApiResult
    {
        object Error { get; set; }
        string Help { get; set; }
        object Result { get; set; }
        bool Success { get; set; }
    }
}