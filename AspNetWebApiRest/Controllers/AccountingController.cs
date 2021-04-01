using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AspNetWebApiRest.Models;

namespace AspNetWebApiRest.Controllers
{
    public class AccountingController : ApiController
    {
        private static List<MoneyT> _transfers { get; set; } = new List<MoneyT>();

        // GET api/<controller>
        public IEnumerable<MoneyT> Get()
        {
            return _transfers;
        }

        public HttpResponseMessage Post([FromBody] MoneyT model)
        {
            // Error Cases
            //1 Null Sender or Reciver.
            //2 Invalid Currency
            //3 If sender dosent have enought money
            
            List <Account> _l= new List<Account>();
            _l = AccountController._accounts.ToList();
            Response response = new Response();

            void Error()
            {
                response.referenceNumber = AccountController.refId + 1;
                response.isError = true;
            }
            void Ok() {
                response.referenceNumber = AccountController.refId + 1;
                response.isError = false;
            }
            
            var sender = _l.FirstOrDefault(x => x.accountNumber == model.Sender);
            var reciver = _l.FirstOrDefault(x => x.accountNumber == model.Reciver);

            //Case1
            if (sender!=null && reciver != null)
            {
                //Case 2
                if (sender.currencyCode == reciver.currencyCode)
                {
                    //Case3
                    if (sender.balance >= model.Amount )
                    {
                        //Transfer
                        sender.balance = sender.balance - Math.Round(model.Amount, 2);
                        reciver.balance = reciver.balance + Math.Round(model.Amount, 2);
                 
                        //Update Accounts
                        AccountController._accounts.Clear();
                        AccountController._accounts = _l.ToList();
                        model.Amount = Math.Round(model.Amount, 2);
                    
                        _transfers.Add(model);
                        Ok();
                        return Request.CreateResponse(HttpStatusCode.Created, response);
                    }
                    else
                    {
                        // Add Ref Number
                        Error();
                        var message = String.Format("insufficient balance error. Referance Number {0}.  İsError:{1}", response.referenceNumber,response.isError);
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,message);
                    }
                    //Case3 End
                }
                else
                {
                    Error();
                    var message = String.Format("Invalid Currency. Referance Number {0}. İsError:{1}", response.referenceNumber,response.isError);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
                }
                //Case2 End
            }
            else
            {
                Error();
                var message = String.Format("Invalid Account Numbers. Referance Number {0}. İsError:{1}", response.referenceNumber,response.isError);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            }
            //Case1 End
        }
    }
}