using System;

namespace Сockroach
{
    public class Сockroach<T>
    {
        public T Logo { get; set; }

        public Сockroach(T logo)
        {
            Logo = logo;
        }
    }
}
