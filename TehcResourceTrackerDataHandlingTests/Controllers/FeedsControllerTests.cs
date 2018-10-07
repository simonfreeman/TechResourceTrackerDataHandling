using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using TechResourceTrackerDataHandling.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechResourceTrackerDataHandling.Controllers;
using System.Collections;
using Newtonsoft.Json;

namespace TechResourceTrackerDataHandlingTests
{
    /// <summary>
    ///  In-memory database usage is probably less efficient than proper mocking, and there's a few kinds between Linq against an sql source
    ///  and linq against a fake inmemory database but it seems just as reasonable as mocking out efcore contexts given the scale of this app
    /// </summary>
    public class FeedsControllerTests
    {
        private DbContextOptions<TechResourcesContext> OptionsForInMemoryTechResourcesContext(string databasename) => new DbContextOptionsBuilder<TechResourcesContext>()
               .UseInMemoryDatabase(databaseName: databasename)
               .Options;

        private TechResourcesContext InMemoryTechResourcesContext(string databasename) => new TechResourcesContext(OptionsForInMemoryTechResourcesContext(databasename));

        private void InsertMockFeedDataIntoInMemoryDatabase(string databasename)
        {
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(databasename))
            {
                List<Feed> feedList = new List<Feed>()
                {
                 new Feed() { Id = 1, Image = "https://static.giantbomb.com/uploads/original/11/110673/2894068-3836779617-28773.png", LastUpdated = new DateTime(2018, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), MediaTypeId = 1, Title = "Giant BeastCast", Url = "https://www.giantbomb.com/podcast-xml/beastcast/",
                     FeedItems = new List<FeedItem>()
                     {
                        new FeedItem() { Id = 1, DatePublished = new DateTime(2018, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), FeedId = 1, ItemContent = "We've got some updated impressions on Dreams and Spider-Man! Also, we explore some of the key differences between Gremlins and Gremlins 2, Abby's love of the Beach Boys, and more from the world of video games!", Seen = true, Title = "The Giant Beastcast - Episode 165", Url = "Ep165_-_The_Giant_Beastcast-07-19-2018-4836496507.mp3" },
                        new FeedItem(){ Id = 2, DatePublished = new DateTime(2018, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), FeedId = 1, ItemContent = "More from the Warhammer 40K universe, the No Man's Sky universe, and the universe of things Alex hates talking about (mostly bathrooms). We've also got the news, your emails, and some stellar pun work in this extraordinary episode.", Seen = false, Title = "The Giant Beastcast - Episode 166", Url = "Ep166_-_The_Giant_Beastcast-07-26-2018-1461758603.mp3" }
                     }
                 },
                 new Feed()  { Id = 2, Image = "https://msdnshared.blob.core.windows.net/media/2017/10/Microsoft-favicon-cropped3.png", LastUpdated = new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), MediaTypeId = 3, Title = ".NET Blog", Url = "https://blogs.msdn.microsoft.com/dotnet/feed/" },
                 new Feed() { Id = 3, Image = "https://yt3.ggpht.com/-mW8hwFBRUNs/AAAAAAAAAAI/AAAAAAAAAAA/jVL9TZ64o7A/s46-c-k-no-mo-rj-c0xffffff/photo.jpg", LastUpdated = new DateTime(2018, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), MediaTypeId = 2, Title = "LearnCode.academy", Url = "https://www.youtube.com/feeds/videos.xml?channel_id=UCVTlvUkGslCV_h-nSAId8Sw" }
                };
                myInMemoryTechResourcesContext.Feed.AddRange(feedList);
                myInMemoryTechResourcesContext.SaveChanges();
            }

        }

        [Fact]
        public void GetFeed_WithNoArguments_ReturnsAllFeedItems()
        {
            string inMemoryDatabaseName = "GetFeed_WithNoArguments_ReturnsAllFeedItems";
            InsertMockFeedDataIntoInMemoryDatabase(inMemoryDatabaseName);
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                IEnumerable<Feed> feeds = feedsController.GetFeed();
                int NumberOfTotalFeeds = myInMemoryTechResourcesContext.Feed.Count();
                int NumberOfFeedsReturned = feeds.Count();
                Assert.Equal(NumberOfTotalFeeds, NumberOfFeedsReturned);
            }
        }

        public FeedsController GetNewFeedsController(TechResourcesContext techResourcesContext) =>
            new FeedsController(techResourcesContext);


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetFeed_WithExistingIntId_ReturnsCorrespondingFeed(int existingFeedId)
        {
            string inMemoryDatabaseName = $"GetFeed_WithExistingIntId_ReturnsCorrespondingFeed{existingFeedId}";
            InsertMockFeedDataIntoInMemoryDatabase(inMemoryDatabaseName);
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                OkObjectResult actionResultFromController = (OkObjectResult)feedsController.GetFeed(existingFeedId).Result;
                Feed feedReturnedAsActionResultValue = (Feed)actionResultFromController.Value;
                Feed feedExpected = myInMemoryTechResourcesContext.Feed.Find(existingFeedId);
                Assert.Equal(feedExpected, feedReturnedAsActionResultValue);
            }
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(int.MinValue)]
        [InlineData(int.MaxValue)]
        public void GetFeed_WithUnusedFeedId_Returns404(int nonExistantFeedId)
        {
            string inMemoryDatabaseName = $"GetFeed_WithUnusedFeedId_Returns404_{nonExistantFeedId}";
            InsertMockFeedDataIntoInMemoryDatabase(inMemoryDatabaseName);
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                IActionResult actionResultFromController = feedsController.GetFeed(nonExistantFeedId).Result;
                Assert.IsType<NotFoundResult>(actionResultFromController);
            }
        }

        private class ValidPutFeedUpdate : IEnumerable<object[]>
        {
            List<object[]> validFeedUpdates = new List<object[]>
            {
               new object[]
               {
                   1,  new Feed()
                   {
                        Id = 1,
                        Image = "updatedimage.png",
                        LastUpdated = new DateTime(2018, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        MediaTypeId = 1,
                        Title = "Giant BeastCast",
                        Url = "https://www.giantbomb.com/podcast-xml/beastcast/"
                   }
                },
                new object[]
                {
                    2,  new Feed()
                    {
                       Id = 2,
                       Image = "https://msdnshared.blob.core.windows.net/media/2017/10/Microsoft-favicon-cropped3.png",
                       LastUpdated = new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                       MediaTypeId = 3,
                       Title = "updatedtitle",
                       Url = "https://blogs.msdn.microsoft.com/dotnet/feed/"
                    }
                }
            };

            public IEnumerator<object[]> GetEnumerator() => validFeedUpdates.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory, ClassData(typeof(ValidPutFeedUpdate))]
        public void PutFeed_WithValidUpdate_ReturnsNoContent(int feedIdOfUpdatedFeed, Feed updatedFeed)
        {
            string inMemoryDatabaseName = $"PutFeed_WithValidUpdate_ReturnsNoContent{feedIdOfUpdatedFeed}";
            InsertMockFeedDataIntoInMemoryDatabase(inMemoryDatabaseName);
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                IActionResult actionResultFromController = feedsController.PutFeed(feedIdOfUpdatedFeed, updatedFeed).Result;
                Assert.IsType<NoContentResult>(actionResultFromController);
            }
        }



        [Theory, ClassData(typeof(ValidPutFeedUpdate))]
        public void PutFeed_WithValidUpdateButDeletedFeedConccurencyIssue_ReturnsNotFound(int recentlyDeletedFeedId, Feed updatedVersionOfDeletedFeed)
        {
            string inMemoryDatabaseName = $"PutFeed_WithValidUpdateButDeletedFeedConccurencyIssue_ReturnsNotFound({recentlyDeletedFeedId}";
            InsertMockFeedDataIntoInMemoryDatabase(inMemoryDatabaseName);
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                Feed feedToDeleteToCauseConcurrencyIssue = myInMemoryTechResourcesContext.Feed.Find(recentlyDeletedFeedId);
                myInMemoryTechResourcesContext.Remove(feedToDeleteToCauseConcurrencyIssue);
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                IActionResult actionResultFromController = feedsController.PutFeed(recentlyDeletedFeedId, updatedVersionOfDeletedFeed).Result;
                Assert.IsType<NotFoundResult>(actionResultFromController);
            }
        }

        [Theory, ClassData(typeof(ValidPutFeedUpdate))]
        public void PutFeed_WithValidUpdate_UpdatesDatabase(int feedIdOfUpdatedFeed, Feed updatedFeed)
        {
            string inMemoryDatabaseName = $"PutFeed_WithValidUpdate_UpdatesDatabase{feedIdOfUpdatedFeed}";
            InsertMockFeedDataIntoInMemoryDatabase(inMemoryDatabaseName);
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                IActionResult actionResultFromController = feedsController.PutFeed(feedIdOfUpdatedFeed, updatedFeed).Result;
            }

            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                Feed updatedFeedFromInMemoryDatabase = myInMemoryTechResourcesContext.Feed.Find(feedIdOfUpdatedFeed);
                string serializedUpdatedFeedFromInMemoryDatabase = JsonConvert.SerializeObject(updatedFeedFromInMemoryDatabase);
                string expectedSerializedUpdatedFeed = JsonConvert.SerializeObject(updatedFeed);
                Assert.Equal(expectedSerializedUpdatedFeed,  serializedUpdatedFeedFromInMemoryDatabase);
                
            }
        }

        [Theory, ClassData(typeof(ValidPutFeedUpdate))]
        public void PutFeed_WithIncorrectFeedId_ReturnsBadRequest(int feedId, Feed updatedFeed)
        {
            string inMemoryDatabaseName = $"PutFeed_WithIncorrectFeedId_ReturnsBadRequest{feedId}";
            InsertMockFeedDataIntoInMemoryDatabase(inMemoryDatabaseName);
            int incorrectFeedId = feedId + 1;
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                IActionResult actionResultFromController = feedsController.PutFeed(incorrectFeedId, updatedFeed).Result;
                Assert.IsType<BadRequestResult>(actionResultFromController);
            }
        }

        //todo - find a way to do this. the in memory database doesn't care about nulls for required, and efcore doesn't do any validation itself...
        //[Theory]
        //[InlineData(1)]
        //[InlineData(2)]
        //public void PutFeed_WithInvalidFeed_ReturnsBadRequest(int feedId)
        //{
        //    string inMemoryDatabaseName = $"PutFeed_WithInvalidFeed_ReturnsBadRequest{feedId}";
        //    InsertMockFeedDataIntoInMemoryDatabase(inMemoryDatabaseName);
        //    Feed notAValidFeed = new Feed() { Id = feedId, Title = "" };
        //    using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
        //    {
        //        FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
        //        IActionResult actionResultFromController = feedsController.PutFeed(feedId, notAValidFeed).Result;
        //        Assert.IsType<BadRequestResult>(actionResultFromController);
        //    }
        //}

        private class ValidPostFeed : IEnumerable<object[]>
        {
            List<object[]> validFeedUpdates = new List<object[]>
            {
               new object[]
               {
                   new Feed()
                   {
                        Image = "newFeed",
                        LastUpdated = new DateTime(2018, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                        MediaTypeId = 1,
                        Title = "Giant BeastCast",
                        Url = "https://www.giantbomb.com/podcast-xml/beastcast/"
                   }
                },
                new object[]
                {
                    new Feed()
                    {
                       Image = "https://msdnshared.blob.core.windows.net/media/2017/10/Microsoft-favicon-cropped3.png",
                       LastUpdated = new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                       MediaTypeId = 3,
                       Title = "newTitle",
                       Url = "https://blogs.msdn.microsoft.com/dotnet/feed/"
                    }
                }
            };

            public IEnumerator<object[]> GetEnumerator() => validFeedUpdates.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


        [Theory, ClassData(typeof(ValidPostFeed))]
        public void PostFeed_WithValidFeed_SavesFeedToDatabase(Feed postedFeed)
        {
            string inMemoryDatabaseName = $"PostFeed_WithValidFeed_SavesFeedToDatabase{Guid.NewGuid()}";
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                IActionResult actionResultFromController = feedsController.PostFeed(postedFeed).Result;
            }
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
               Feed insertedFeed = myInMemoryTechResourcesContext.Feed.FirstOrDefault();
               Assert.NotNull(insertedFeed);
            }
        }

        [Theory, ClassData(typeof(ValidPostFeed))]
        public void PostFeed_WithValidFeed_ReturnsCreatedAtActionResultResponse(Feed postedFeed)
        {
            string inMemoryDatabaseName = $"PostFeed_WithValidFeed_ReturnsCreatedAtActionResult{Guid.NewGuid()}";
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                IActionResult actionResultFromController = feedsController.PostFeed(postedFeed).Result;
                Assert.IsType<CreatedAtActionResult>(actionResultFromController);
            }
        }

        //todo - find a way to do this. the in memory database doesn't care about nulls for required, and efcore doesn't do any validation itself...
        //[Fact]
        //public void PostFeed_WithInvalidFeed_ReturnsBadRequest()
        //{
        //    string inMemoryDatabaseName = "PostFeed_WithInvalidFeed_ReturnsBadRequest";
        //    InsertMockFeedDataIntoInMemoryDatabase(inMemoryDatabaseName);
        //    using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
        //    {
        //        FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
        //    }
        //    throw new NotImplementedException();
        //}


        [Theory]
        [InlineData(1000)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void DeleteFeed_WithUnusedFeedId_ReturnsNotFound(int unusedFeedId)
        {
            string inMemoryDatabaseName = $"DeleteFeed_WithUnusedFeedId_ReturnsNotFound{unusedFeedId}";
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                IActionResult actionResultFromController = feedsController.DeleteFeed(unusedFeedId).Result;
                Assert.IsType<NotFoundResult>(actionResultFromController);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DeleteFeed_WithValidFeedId_DeletesFeedFromDatabase(int feedIdOfFeedToDelete)
        {
            string inMemoryDatabaseName = $"DeleteFeed_WithValidFeedId_DeletesFeedFromDatabase{feedIdOfFeedToDelete}";
            InsertMockFeedDataIntoInMemoryDatabase(inMemoryDatabaseName);
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                IActionResult actionResultFromController = feedsController.DeleteFeed(feedIdOfFeedToDelete).Result;
              
            }
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                Feed attemptToFindDeletedFeed = myInMemoryTechResourcesContext.Feed.Where(x => x.Id == feedIdOfFeedToDelete).FirstOrDefault();
                Assert.Null(attemptToFindDeletedFeed);
            }


        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DeleteFeed_WithValidFeedId_ReturnsOkResponse(int feedIdOfFeedToDelete)
        {
            string inMemoryDatabaseName = $"DeleteFeed_WithValidFeedId_ReturnsOkResponse{feedIdOfFeedToDelete}";
            InsertMockFeedDataIntoInMemoryDatabase(inMemoryDatabaseName);
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                IActionResult actionResultFromController = feedsController.DeleteFeed(feedIdOfFeedToDelete).Result;
                Assert.IsType<OkObjectResult>(actionResultFromController);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DeleteFeed_WithValidFeedId_ReturnsDeletedFeed(int feedIdOfFeedToDelete)
        {
            string inMemoryDatabaseName = $"DeleteFeed_WithValidFeedId_ReturnsDeletedFeed{feedIdOfFeedToDelete}";
            InsertMockFeedDataIntoInMemoryDatabase(inMemoryDatabaseName);
            using (var myInMemoryTechResourcesContext = InMemoryTechResourcesContext(inMemoryDatabaseName))
            {
                Feed feedToBeDeleted = myInMemoryTechResourcesContext.Feed.Find(feedIdOfFeedToDelete);
                FeedsController feedsController = GetNewFeedsController(myInMemoryTechResourcesContext);
                OkObjectResult actionResultFromController = (OkObjectResult)feedsController.DeleteFeed(feedIdOfFeedToDelete).Result;
                string expectedSerializedFeedToBeDeleted = JsonConvert.SerializeObject(feedToBeDeleted);
                string serializedDeletedFeedBeingReturned = JsonConvert.SerializeObject(actionResultFromController.Value);
                Assert.Equal(expectedSerializedFeedToBeDeleted, serializedDeletedFeedBeingReturned);
            }
        }
    }
}