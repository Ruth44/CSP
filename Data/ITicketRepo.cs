using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CSP.Models;

namespace CSP.Data
{
    public interface ITicketRepo
    {
        bool SaveChanges();
        IEnumerable<Ticket> GetAllTickets();
                Ticket FindBy(Expression<Func<Ticket,bool>> tic);
                IEnumerable<Ticket> FindByMany(Expression<Func<Ticket,bool>> ser);

         void CreateTicket(Ticket tic);
        Ticket GetTicketById(int id);
        Ticket GetTicketByNumber(int id);


        void UpdateTicket(Ticket tic);
        void DeleteTicket(Ticket tic);
    }
}