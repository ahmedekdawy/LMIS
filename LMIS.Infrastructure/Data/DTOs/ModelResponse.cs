using LMIS.Infrastructure.Enums;

using System;

namespace LMIS.Infrastructure.Data.DTOs
{
    public class ModelResponse
    {
        private readonly ResponseStatus _status;
        private readonly Exception _exception;
        private readonly int? _errorId;
        private readonly object _data;

        public ResponseStatus Status
        {
            get { return _status; } 
        }

        public Exception Exception
        {
            get { return _exception; }
        }

        public int? ErrorId
        {
            get { return _errorId; }
        }

        public object Data
        {
            get { return _data; }
        }

        public ModelResponse(Exception ex, object data = null)
        {
            _status = ResponseStatus.Exception;
            _exception = ex;
            _data = data;
        }

        public ModelResponse(int id, object data = null)
        {
            if (id == 0)
                _status = ResponseStatus.Success;
            //if (id == -2)
            //    _status = ResponseStatus.InRelation;
            else
            {
                switch (id)
                {
                    case -2:
                        _status = ResponseStatus.InRelation;
                        break;
                    case -3:
                        _status = ResponseStatus.AuthorizationError;
                        break;
                    case -4:
                        _status = ResponseStatus.Exist;
                        break;
                    default: _status = ResponseStatus.ValidationError;
                        break;
                }
                
                _errorId = id;
            }

            _data = data;
       }
    }
}