using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Models
{
    public class ReservationRepository
        : IReservationRepository
    {
        private IList<Reservation> _data = new List<Reservation>
        {
            new Reservation { ReservationId = 1, ClientName = "Adam", Location = "London" },
            new Reservation { ReservationId = 2, ClientName = "Steve", Location = "New York" },
            new Reservation { ReservationId = 3, ClientName = "Jacqui", Location = "Paris" }
        };

        private static IReservationRepository _repository = new ReservationRepository();
        public static IReservationRepository getRepository()
        {
            return _repository;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _data;
        }

        public Reservation Get(int id)
        {
            var matches = _data.Where(r => r.ReservationId == id);
            return matches.Count() > 0 ? matches.First() : null;
        }

        public Reservation Add(Reservation item)
        {
            item.ReservationId = _data.Count + 1;
            _data.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            var item = Get(id);
            if (item != null)
            {
                _data.Remove(item);
            }
        }

        public bool Update(Reservation item)
        {
            var storedItem = Get(item.ReservationId);
            if (storedItem != null)
            {
                storedItem.ClientName = item.ClientName;
                storedItem.Location = item.Location;
                return true;
            }

            return false;
        }
    }
}