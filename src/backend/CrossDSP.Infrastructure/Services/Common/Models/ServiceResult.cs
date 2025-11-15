using System.Collections.Generic;

namespace CrossDSP.Infrastructure.Services.Common.Models
{
    public record ServiceResult<T> where T : class
    {
        public ServiceResult(){}

        public ServiceResult(
            T? data
        )
        {
            Data = data;
        }

        public T? Data { get; init; } = default;

        public bool HasData { get => Data != default; }
    }

    public record ServiceResults<T> where T : class
    {
        public ServiceResults() {}

        public ServiceResults(
            IEnumerable<T> data
        )
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; init; } = [];

        public bool HasData 
        { 
            get => Data != default && Data.Any(); 
        }
    }
}