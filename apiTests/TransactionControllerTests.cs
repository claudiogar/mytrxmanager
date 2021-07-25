using Xunit;
using api.Controllers;
using FakeItEasy;
using api.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using api.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using api.Models.ApiModels;

namespace apiTests
{
    public class TransactionControllerTests 
    {
        [Fact]
        public async Task GetTrx_WithoutQueryParameters_RequestGoesThrough()
        {
            // arrange
            var repo = A.Fake<ITransactionDbContext>();
            var controller = new TransactionController(repo, A.Fake<ILogger<TransactionController>>());

            // act
            var response = await controller.GetTransactions(new GetQueryParameters());

            // assert
            A.CallTo(() => repo.GetTransactionsAsync(null, 10)).MustHaveHappened();
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetTrx_ValidId_ModelReturned()
        {
            // arrange
            var repo = A.Fake<ITransactionDbContext>();
            A.CallTo(() => repo.FindAsync(2)).Returns(Task.FromResult(new TransactionDbModel { Id = 2 }));

            var controller = new TransactionController(repo, A.Fake<ILogger<TransactionController>>());

            // act
            var response = await controller.GetTransaction(2);

            // assert
            Assert.NotNull(response);
            Assert.True(response.Value.Id == 2);
        }

        [Fact]
        public async Task GetTrx_ValidIdButNoEntry_NotFound()
        {
            // arrange
            var repo = A.Fake<ITransactionDbContext>();
            A.CallTo(() => repo.FindAsync(2)).Returns(Task.FromResult(new TransactionDbModel { Id = 2 }));
            A.CallTo(() => repo.FindAsync(1)).Returns(Task.FromResult<TransactionDbModel>(null));

            var controller = new TransactionController(repo, A.Fake<ILogger<TransactionController>>());

            // act
            var response = await controller.GetTransaction(1);

            // assert
            Assert.NotNull(response);
            Assert.True(response.Result is NotFoundResult);
        }

        [Fact]
        public async Task GetTrx_InvalidId_ErrorReturned()
        {
            // arrange
            var repo = A.Fake<ITransactionDbContext>();

            var controller = new TransactionController(repo, A.Fake<ILogger<TransactionController>>());

            // act
            var response = await controller.GetTransaction(-1);

            // assert
            Assert.NotNull(response);
            Assert.True(response.Result is BadRequestObjectResult);
        }

        [Fact]
        public async Task PostTrx_ValidEntry_EntryAdded()
        {
            // arrange
            var list = new List<TransactionDbModel>();
            var repo = A.Fake<ITransactionDbContext>();

            A.CallTo(() => repo.AddAsync(A<TransactionDbModel>.Ignored)).Invokes(a => list.Add(a.Arguments[0] as TransactionDbModel));
            var controller = new TransactionController(repo, A.Fake<ILogger<TransactionController>>());

            // act
            var response = await controller.PostTransaction(new TransactionApiModel
            {
                ProductType = "Drinks",
                Amount = 500
            });

            // assert
            Assert.True(list.Count == 1);
        }


        [Fact]
        public async Task PutTrx_MismatchingIds_ErrorReturned()
        {
            // arrange
            var repo = A.Fake<ITransactionDbContext>();
            var controller = new TransactionController(repo, A.Fake<ILogger<TransactionController>>());

            // act
            var response = await controller.PutTransaction(0, new TransactionApiModel
            {
                Id = 1,
                ProductType = "Drinks",
                Amount = 100
            });

            // assert
            Assert.True(response is BadRequestObjectResult);
        }


        [Fact]
        public async Task PutTrx_NewEntry_EntryAdded()
        {
            // arrange
            var list = new List<TransactionDbModel>();
            var repo = A.Fake<ITransactionDbContext>();

            A.CallTo(() => repo.UpdateAsync(A<TransactionDbModel>.Ignored)).Invokes(a => list.Add(a.Arguments[0] as TransactionDbModel));
            var controller = new TransactionController(repo, A.Fake<ILogger<TransactionController>>());

            // act
            var response = await controller.PutTransaction(1, new TransactionApiModel
            {
                Id = 1,
                ProductType = "Drinks",
                Amount = 100
            });

            // assert
            Assert.True(list.Count == 1);
        }

        [Fact]
        public async Task PutTrx_ExistingEntry_EntryUpdated()
        {
            // arrange
            var list = new List<TransactionDbModel>
            {
                new TransactionDbModel
                {
                    Id = 1,
                    Amount = 500
                }
            };
            var repo = A.Fake<ITransactionDbContext>();

            A.CallTo(() => repo.UpdateAsync(A<TransactionDbModel>.Ignored)).Invokes(a => {
                list.Clear();
                list.Add(a.Arguments[0] as TransactionDbModel);
            });
            var controller = new TransactionController(repo, A.Fake<ILogger<TransactionController>>());

            // act
            var response = await controller.PutTransaction(1, new TransactionApiModel
            {
                Id = 1,
                ProductType = "Drinks",
                Amount = 100
            });

            // assert
            Assert.True(list.Count == 1);
            Assert.True(list[0].Amount == 100);
        }

        [Fact]
        public async Task DeleteTrx_ValidEntry_EntryDeleted()
        {
            // arrange
            var list = new List<TransactionDbModel>
            {
                new TransactionDbModel
                {
                    Id = 1,
                    Amount = 500
                }
            };

            var repo = A.Fake<ITransactionDbContext>();
            A.CallTo(() => repo.RemoveAsync(A<TransactionDbModel>.Ignored)).Invokes(a => list.RemoveAt(0));

            var controller = new TransactionController(repo, A.Fake<ILogger<TransactionController>>());

            // act
            var response = await controller.DeleteTransaction(0);

            // assert
            Assert.True(list.Count == 0);
        }

    }
}
