namespace Blazor_eCommerce_Project.Common
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }    
    }
}
