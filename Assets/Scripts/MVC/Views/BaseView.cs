using System;
using UnityEngine;

namespace MVC
{
    public class BaseView : MonoBehaviour, IView, IDisposable
    {
        public virtual void Dispose()
        {
        }
    }

    public interface IView
    {
        
    }
}

