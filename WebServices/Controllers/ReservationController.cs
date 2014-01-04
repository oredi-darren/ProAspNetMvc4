using System.Collections.Generic;
using System.Web.Http;
using WebServices.Models;

namespace WebServices.Controllers
{
    public class ReservationController : ApiController
    {
        IReservationRepository _repository = ReservationRepository.getRepository();

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _repository.GetAll();
        }

        public Reservation GetReservation(int id)
        {
            return _repository.Get(id);
        }

        [HttpPost]
        public Reservation CreateReservation(Reservation item)
        {
            return _repository.Add(item);
        }

        [HttpPut]
        public bool UpdateReservation(Reservation item)
        {
            return _repository.Update(item);
        }

        public void DeleteReservation(int id)
        {
            _repository.Remove(id);
        }
    }
}
