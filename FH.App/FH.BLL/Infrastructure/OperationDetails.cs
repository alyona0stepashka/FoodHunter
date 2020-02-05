using System;
using System.Collections.Generic;
using System.Text;

namespace FH.BLL.Infrastructure
{
    public class OperationDetails
    {
        public OperationDetails(bool succeeded, string message, object prop)
        {
            Succeeded = succeeded;
            Message = message;
            Result = prop;
        }
        public bool Succeeded { get; private set; }
        public string Message { get; private set; }
        public object Result { get; private set; }
    }
}
