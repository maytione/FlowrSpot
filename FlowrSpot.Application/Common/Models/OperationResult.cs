using FlowrSpot.Application.Common.Enums;


namespace FlowrSpot.Application.Common.Models
{
    public class OperationResult<T>
    {
        public T? Data { get; set; }
        public bool IsError { get; private set; }
        public HashSet<Error> Errors { get; } = [];


        public void AddError(ErrorCode code, string message)
        {
            HandleError(code, message);
        }

        public void AddUnknownError(string message)
        {
            HandleError(ErrorCode.UnknownError, message);
        }

        public void ResetIsErrorFlag()
        {
            IsError = false;
        }

        private void HandleError(ErrorCode code, string message)
        {
            Errors.Add(new Error { Code = code, Message = message });
            IsError = true;
        }

    }
}
