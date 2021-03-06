using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Commmand : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Commmand>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Commmand request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if(activity == null){
                    throw new RestException(HttpStatusCode.NotFound, new { activity = "Couldn't find activity."});
                }

                _context.Remove(activity);

                var success = await _context.SaveChangesAsync() > 0;

                return success
                ? Unit.Value
                : throw new Exception("Problem saving changes");
            }
        }
    }
}