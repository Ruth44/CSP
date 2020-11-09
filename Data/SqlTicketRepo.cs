using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CSP.Models;

namespace CSP.Data{
    public class SqlTicketRepo : ITicketRepo
    {
        private readonly CSPContext _context;
                private readonly DbSet<Ticket> _ticket;



        public SqlTicketRepo(CSPContext context)
        {
            _context=context;
            _ticket=_context.Set<Ticket>();
                    
                    

        }

        public bool SaveChanges()
        {
          return (_context.SaveChanges() >= 0);
        }

  

        void ITicketRepo.DeleteTicket(Ticket ticket)
        {
if(ticket==null){
            throw new ArgumentNullException(nameof(ticket));

          }
          _context.Tickets.Remove(ticket);        }

       

        void ITicketRepo.UpdateTicket(Ticket ticket)
{
        }

        IEnumerable<Ticket> ITicketRepo.GetAllTickets()
        {
  return _context.Tickets.ToList();       
        }

        void ITicketRepo.CreateTicket(Ticket ticket)
        {
if(ticket==null){
            throw new ArgumentNullException(nameof(ticket));
          }
          _context.Tickets.Add(ticket);                }

        Ticket ITicketRepo.GetTicketById(int id)
        {
return _context.Tickets.FirstOrDefault(p=> p.Id==id);
        }
          Ticket ITicketRepo.GetTicketByNumber(int id)
        {
return _context.Tickets.FirstOrDefault(p=> p.TicketNumber==id);
        }
 public IEnumerable<Ticket> FindByMany(Expression<Func<Ticket,bool>> ser)
        {
  return _ticket.Where(ser).ToList();
                        // return test;       
                         }
        Ticket ITicketRepo.FindBy(Expression<Func<Ticket, bool>> cmd)
        {
 var test = _ticket.Where(cmd).FirstOrDefault();
                        return test;        }
    }
}