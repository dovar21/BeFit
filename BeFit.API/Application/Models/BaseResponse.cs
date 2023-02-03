namespace BeFit.API.Application.Models;

using System.Net;

public class BaseResponse : BaseResponse<ErrorModel>
{
    public BaseResponse(HttpStatusCode statusCode, params ErrorModel[] errors) : base(errors)
    {
    }
    public BaseResponse(HttpStatusCode statusCode, IEnumerable<ErrorModel> errors) : base(errors)
    {
    }
    public static BaseResponse Success() => new BaseResponse(HttpStatusCode.OK);
    public static BaseResponse Fail(params ErrorModel[] errors) => new BaseResponse(HttpStatusCode.BadRequest, errors);
    public static BaseResponse Fail(IEnumerable<ErrorModel> errors) => new BaseResponse(HttpStatusCode.BadRequest, errors);
    public static BaseResponse NotFound(params ErrorModel[] errors) => new BaseResponse(HttpStatusCode.NotFound, errors);
    public static BaseResponse NotFound() => new BaseResponse(HttpStatusCode.NotFound, new ErrorModel("Entity not found"));
    public static BaseResponse Unauthorized(params ErrorModel[] errors) => new BaseResponse(HttpStatusCode.Unauthorized, errors);
}

public class BaseResponse<TMessage>
{
    public IList<TMessage> Errors { get; set; }

    public BaseResponse(params TMessage[] errors)
    {
        Errors = errors?.ToList() ?? new List<TMessage>();
    }
    public BaseResponse(IEnumerable<TMessage> errors)
    {
        Errors = errors?.ToList() ?? new List<TMessage>();
    }

}
