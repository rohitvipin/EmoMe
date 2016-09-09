using System;

namespace EmoMe.Services.Interfaces
{
    public interface ILoggingService
    {
        void Error(Exception exception);
    }
}