using System;

namespace MVC
{
    public class BaseModel: IModel, IDisposable
    {
        public virtual void Dispose()
        {
        }
    }

    public interface IModel
    {
        
    }
}

