using DDD.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;
using System.Collections.Generic;

namespace DDD.Implementation
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispatch<T>(T domainEvent)
        {
            var dispatcher = _serviceProvider.GetService<IDomainEventHandler<T>>();

            if (dispatcher == null)
                throw new InvalidOperationException($"Dispatcher {nameof(IDomainEventHandler<T>)} cannot be null");

            dispatcher.Handle(domainEvent);
        }


        public void DispatchEvents<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
        {
            foreach (dynamic handler in GetHandlers(domainEvent))
            {
                handler.Handle((dynamic)domainEvent);
            }
        }

        private IEnumerable GetHandlers<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent
        {
            return (IEnumerable)_serviceProvider.GetService(
                typeof(IEnumerable<>).MakeGenericType(
                    typeof(IDomainEventHandler<>).MakeGenericType(eventToDispatch.GetType())));
        }
    }
}
