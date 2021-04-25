using System;
using System.Collections.Generic;

namespace DocumentManager.Infrastructure.Contracts
{
    public interface INotification
    {
        void AddModelError(string modelId, string error, ExceptionType exType, Exception ex);

        void AddGeneralError(string error, ExceptionType exType, Exception ex);

        bool HasModelErrors { get; }

        bool HasGeneralErrors { get; }

        IDictionary<string, Error> ModelErrors { get; }

        IList<Error> GeneralErrors { get; }
    }
}
