using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SpaUserControl.Domain.Contracts.Services;
using SpaUserControl.Domain.Models;
using SpaUserControl.WebApi.Models;
using SpaUserControl.Common.Resource;


namespace SpaUserControl.WebApi.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private IUserService _service;

        public AccountController(IUserService service)
        {
            _service = service;
        }
       
        [HttpPost, Route("")]
        public Task<HttpResponseMessage> Registrer(RegisterUserModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();
                
            try
            {
                _service.Registrer(model.Name, model.Email, model.Password, model.ConfirmPassword);
                response = Request.CreateResponse(HttpStatusCode.OK, new {name = model.Name, email = model.Email});
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPut, Authorize, Route("")]
        public Task<HttpResponseMessage> ChangeInformation(ChangeInformationModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                _service.ChangeInformation(User.Identity.Name, model.Name);
                response = Request.CreateResponse(HttpStatusCode.OK, new {name = model.Name});
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost, Route("changepassword")]
        public Task<HttpResponseMessage> ChangePassword(ChangePasswordModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _service.ChangePassword(User.Identity.Name, model.Password, model.NewPassword, model.ConfirmNewPassword);
                response = Request.CreateResponse(HttpStatusCode.OK, Messages.PasswordSuccessfulyChanges);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost, Route("resetpassword")]
        public Task<HttpResponseMessage> ResetPassword(ResetPasswordModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                 var password = _service.ResetPassword(model.Email);
                response = Request.CreateErrorResponse(HttpStatusCode.OK, String.Format(Messages.ResetPasswordEmailBody, password));
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

             var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
        
        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
        }
    }
}
