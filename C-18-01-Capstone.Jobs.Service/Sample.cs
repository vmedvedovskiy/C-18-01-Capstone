using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_18_01_Capstone.Jobs.Service
{

    public interface IAmError
    {

    }

    public class UserIsAlreadyLinked: IAmError
    {

    }

    public class Result<TResult>
    {
        public readonly IAmError error;

        public readonly TResult result;

        public bool HasError
            => this.error != null;

        public bool HasResult
            => !this.HasError;

        public Result(IAmError error)
        {
            if (!result.Equals(default(TResult)))
            {
                throw new InvalidOperationException(
                    "Result already set");
            }

            this.error = error;
        }

        public Result(TResult result)
        {
            if (error != null)
            {
                throw new InvalidOperationException(
                    "Error already set");
            }

            this.result = result;
        }
    }

    public static class Result
    {
        public static Result<TResult> Create<TResult>(TResult value)
            => new Result<TResult>(value);

        public static Result<TResult> Map<TSource, TResult>(
            this Result<TSource> source,
            Func<TSource, TResult> mapping)
        {
            if (source.HasError)
            {
                return new Result<TResult>(source.error);
            }

            return Create(mapping(source.result));
        }

        public static Result<TResult> FlatMap<TSource, TResult>(
            this Result<TSource> source,
            Func<TSource, Result<TResult>> mapping)
        {
            if (source.HasError)
            {
                return new Result<TResult>(source.error);
            }

            var newResult = mapping(source.result);

            if(newResult.HasError)
            {
                return new Result<TResult>(newResult.error);
            }


            return Create(newResult.result);
        }
    }

    public class Sample
    {
        private Result<object> CreateUser()
            => Result.Create(new object());

        private Result<int> SendSms()
            => Result.Create(0);

        private Result<decimal> LinkUserToCompany()
            => new Result<decimal>(new UserIsAlreadyLinked());

        private void Operation()
        {
            var result =
                Result.Create(this.CreateUser())
                .FlatMap(_ => this.SendSms())
                .FlatMap(_ => this.LinkUserToCompany());
        }
    }
}
