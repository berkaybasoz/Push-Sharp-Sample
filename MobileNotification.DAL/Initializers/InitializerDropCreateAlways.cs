using MobileNotification.DAL.Context;
using System.Data.Entity;

namespace MobileNotification.DAL.Initializers
{
    public class InitializerDropCreateAlways : DropCreateDatabaseAlways<PushContext>
    {
        protected override void Seed(PushContext context)
        { 
            base.Seed(context);
        }
    }
}
