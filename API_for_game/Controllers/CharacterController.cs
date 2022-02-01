using API_for_game.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API_for_game.Controllers
{
    public class CharacterController : ApiController
    {

        private eTopRPG_ISIP25Entities _ent { get; set; } = new eTopRPG_ISIP25Entities();
        /// <summary>
        /// Метод на возвращение hp в виде переменной
        /// </summary>
        /// <param name="hp"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetHp(int hp)
        {
            var data = await _ent.Character.FirstOrDefaultAsync(x => x.Hp == hp);
            if (data != null)
            {
                data.Hp += hp;
                await _ent.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.OK, data.Hp);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        /// <summary>
        /// Метод на возврат энергии
        /// </summary>
        /// <param name="energy"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetEnergy(int energy)
        {
            var data = await _ent.Character.FirstOrDefaultAsync(x => x.Energy == energy);
            if (data != null)
            {
                data.Energy += energy;
                await _ent.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.OK, data.Energy);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
