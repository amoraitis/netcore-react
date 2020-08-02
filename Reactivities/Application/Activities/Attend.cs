using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Attend
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
                this._context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await this._context.Activities.FindAsync(request.Id);

                if (activity == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Activity = "Couldn't find activity" });
                }

                var user = await this._context.Users.SingleOrDefaultAsync(x => x.UserName == this._userAccessor.GetCurrentUsername());

                var attendance = await this._context.UserActivities.SingleOrDefaultAsync(x => x.ActivityId == activity.Id && x.AppUserId == user.Id);

                if (attendance != null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new {Attendance = "Already attending this activity"});
                }

                attendance = new Domain.UserActivity{
                    Activity = activity,
                    AppUser = user,
                    IsHost = false,
                    DateJoined = DateTime.Now
                };

                this._context.UserActivities.Add(attendance);

                var success = await _context.SaveChangesAsync() > 0;

                return success
                ? Unit.Value
                : throw new Exception("Problem saving changes");
            }
        }
    }
}