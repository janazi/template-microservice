using Flunt.Notifications;
using MicroserviceBase.Application;
using System.Collections.Generic;

namespace MicroserviceBase
{
    public class ApiError
    {
        public ApiError(IEnumerable<Notification> errors, ErrorCode? error = null)
        {
            ErrorType = error;
            Errors = errors;
        }

        public ErrorCode? ErrorType { get; }
        public IEnumerable<Notification> Errors { get; }

        public static ApiError FromResult(Result result)
        {
            return new(result.Notifications, result.Error);
        }
    }
}
