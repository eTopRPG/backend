using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using API_for_game.Models;

namespace API_for_game.Controllers
{
    public class UserController : ApiController
    {
        private eTopRPG_ISIP25Entities _ent { get; set; } = new eTopRPG_ISIP25Entities();


        /// <summary>
        /// Метод GET для авторизации в системе
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Get(string login, string password)
        {
            var user = await _ent.User.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

            if (user != null)
            {
                var userData = new UserModel()
                {
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password
                };

                return Request.CreateResponse(HttpStatusCode.OK, userData);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Метод POST для регистрации
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post([FromBody] UserModel userModel)
        {
            var user = new User()
            {
                Id = userModel.Id,
                Login = userModel.Login,
                Password = userModel.Password
            };

            _ent.User.Add(user);
            await _ent.SaveChangesAsync();

            var userData = new UserModel()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password
            };

            return Request.CreateResponse(HttpStatusCode.OK, userData);
        }

    }
}
