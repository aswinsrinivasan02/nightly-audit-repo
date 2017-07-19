using BallyTech.Infrastructure.ExceptionHandling;
using BallyTech.Infrastructure.Types;
using System;
using System.Data.SqlClient;
using System.ServiceModel;

namespace ExceptionHandling
{
    public class FaultExceptionHandler : IExceptionHandler
    {
        public Exception Handle(Exception exception, out bool handled)
        {
            handled = false;
            FaultException endPointException = exception as FaultException;
            if (exception != null)
            {
                handled = true;
            }

            return exception;
        }

        
    }
    public class DataExceptionConvertor : IExceptionConvertor
    {
        public object Handle(Exception exception, out bool converted)
        {
            converted = false;
            if (exception is IDataException)
            {
                converted = true;
                return ((IDataException)exception).GetObject();
            }

            return exception;
        }
    }

    public class DataExceptionHandler : IExceptionHandler
    {
        public Exception Handle(Exception exception, out bool handled)
        {
            handled = true;
            if (exception is IDataException)
            {
                handled = true;
                return Activator.CreateInstance(typeof(FaultException<>).MakeGenericType(exception.GetType().GetGenericArguments()[0]), ((IDataException)exception).GetObject()) as Exception;
            }

            return exception;
        }
    }

    public class PrimaryKeyViolationHandler : IExceptionHandler
    {
        public Exception Handle(Exception exception, out bool handled)
        {
            handled = false;
            SqlException sqlEx = exception as SqlException;
            if (sqlEx != null)
            {
                if (sqlEx.Number == 2627)
                {
                    handled = true;
                    exception = new DuplicateKeyException(sqlEx.Number, sqlEx);
                }
            }

            return exception;
        }
    }

    public class OptimisticLockViolationHandler : IExceptionHandler
    {
        public Exception Handle(Exception exception, out bool handled)
        {
            handled = false;
            SqlException excep = exception as SqlException;
            if (excep != null)
            {
                if (excep.Number == 999999)
                {
                    handled = true;
                    exception = new OptimisticLockException(excep.Number, excep);
                }
            }

            return exception;
        }
    }

    public class ModuleExceptionHandler : IExceptionHandler
    {
        public Exception Handle(Exception exception, out bool handled)
        {
            handled = true;
            ModuleException excep = exception as ModuleException;
            if (excep != null)
            {
                handled = true;
            }

            return exception;
        }
    }

    public class DataExceptionHandlerIdentifier : IExceptionHandler
    {
        public Exception Handle(Exception exception, out bool handled)
        {
            handled = exception is IDataException;
            return exception;
        }
    }

    public class EndpointNotFoundExceptionHandler : IExceptionHandler
    {
        public Exception Handle(Exception exception, out bool handled)
        {
            handled = false;
            EndpointNotFoundException endPointException = exception as EndpointNotFoundException;
            if (exception != null)
            {
                handled = true;
            }

            return exception;
        }
    }
}
