using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest{
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;
            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await this.context.Activities.FindAsync(request.Activity.Id);
                
                activity.Category = request.Activity.Category ?? activity.Category; 
                activity.Title = request.Activity.Title ?? activity.Title;
                activity.Date = request.Activity.Date;

                await this.context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}