using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApiRest.Models;

namespace AspNetWebApiRest.Controllers
{
    public class AccountController : ApiController
    {
        public  static List<Account> _accounts { get; set; } = new List<Account>();

        public static int refId = 100;
        public Response _response = new Response();


        public IEnumerable<Account> Get()
        {
            return _accounts;
        }

        public HttpResponseMessage Get(int id)
        {
            var item = _accounts.FirstOrDefault(x => x.accountNumber == id);
            if (item != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Post([FromBody] Account model)
        {
             void Error()
            {
                refId = refId + 1;
                _response.referenceNumber = refId;
                _response.isError = true;
            }
             void Ok()
            {

                refId = refId + 1;
                _response.referenceNumber = refId;
                _response.isError = false;
            }

            // Error Cases
            //0 Null Currecy Code
            //1 Invalid Currency Code

            // Case 0
            if (string.IsNullOrEmpty(model?.currencyCode))
            {
                Error();
                var message = String.Format("Null Currency Code. Accepted Codes: TRY,EUR,USD.Referance Number {0} İsError:{1} ",_response.referenceNumber,_response.isError);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            }
    
            foreach (var item in _accounts)
            {
                if (item.accountNumber == model.accountNumber)
                {
                    Error();
                    var message = String.Format("Account Number:{0} already exist. Referance Number {1} İsError:{2}",model.accountNumber, _response.referenceNumber,_response.isError);
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, message);
                }
            }

            // Case 1
            if (model.currencyCode == "TRY" || model.currencyCode == "USD" || model.currencyCode == "EUR")
            {
                Ok();
                _accounts.Add(model);
                return Request.CreateResponse(HttpStatusCode.Created, _response);
            }
            else {
                Error();
                var message = String.Format("Wrong Currency Code. Your Code : {0} Accepted Codes: TRY,EUR,USD. Referance Number {1}  İsError:{2}", model.currencyCode,_response.referenceNumber,_response.isError);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
            }
        }
    }
}