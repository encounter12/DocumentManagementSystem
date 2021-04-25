using DocumentManager.Infrastructure.Contracts;
using System;
using System.Collections.Generic;

namespace DocumentManager.Infrastructure
{
    public class Notification : INotification
    {
        private readonly IDictionary<string, Error> _modelErrors = new Dictionary<string, Error>();

        private readonly IList<Error> _generalErrors = new List<Error>();

        public Notification()
        {
        }

        public void AddModelError(string modelId, string error, ExceptionType exType, Exception ex) 
            => _modelErrors.Add(modelId, new Error(error, exType, ex));

        public void AddGeneralError(string error, ExceptionType exType, Exception ex) 
            => _generalErrors.Add(new Error(error, exType, ex));

        public bool HasModelErrors => _modelErrors.Count > 0;

        public bool HasGeneralErrors => _generalErrors.Count > 0;

        public IDictionary<string, Error> ModelErrors
        {
            get
            {
                return _modelErrors;
            }
        }

        public IList<Error> GeneralErrors
        {
            get
            {
                return _generalErrors;
            }
        }
    }

    public class Error
    {
        public string Message { get; }

        public Exception Cause { get; }

        public Error(string message, ExceptionType exType, Exception cause)
        {
            Message = message;
            ErrorType = exType;
            Cause = cause;
        }

        public ExceptionType ErrorType { get; }
    }

    public enum ExceptionType
    {
        ConcurrencyException
    }
}
