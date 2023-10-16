using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class ResponseTemp<T>: Response<T>
    {
        public string StatusCode {get;set;}
        public string ErrorMessage {get;set;}

        public ResponseTemp(T data, string statusCode, string erorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = erorMessage;
            Data = data;
        }
    }
}