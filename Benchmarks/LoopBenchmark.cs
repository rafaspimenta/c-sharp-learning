using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Benchmarks
{
    public class LoopBenchmark
    {
        private List<Customer> _customers;

        [Params(100, 1000)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _customers = new List<Customer>();

            for (int i = 0; i < Size; i++)
            {
                _customers.Add(new Customer($"Customer {i}"));
            }
        }

        [Benchmark]
        public void AsSpanLoop()
        {
            var a = String.Empty;
            foreach (var customer in CollectionsMarshal.AsSpan(_customers))
            {
                a = customer.Name;
            }
        }

        [Benchmark]
        public void Foreach()
        {
            var a = String.Empty;
            _customers.ForEach(customer => a = customer.Name);
        }
    }

    public class Customer
    {
        public string Name { get; set; }

        public Customer(string name)
        {
            Name = name;
        }
    }
}