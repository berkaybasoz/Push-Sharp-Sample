using MobileNotification.DAL.Context;
using System.Data.Entity;

namespace MobileNotification.DAL.Initializers
{
    public class InitializerIfNotExists : CreateDatabaseIfNotExists<PushContext>
    {
        protected override void Seed(PushContext context)
        { 
            base.Seed(context);
        }
    }
}
