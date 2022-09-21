using System;
namespace MVC
{
    public class BaseController: IDisposable
    {
        protected BaseView _baseView;
        public virtual void Update()
        {
      
        }

        public virtual void Init(BaseView view)
        {
        }

        public virtual void Dispose()
        {
        }
    }
}
 
