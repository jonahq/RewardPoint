using Moq;
using RewardPoint.Core.Repositories;
using RewardPoint.Infrastructure.Services;
using RewardPoint.Core.Models;

namespace UnitTest
{
    public class Tests
    {
        private RewardServices _rewardServices;
        private Mock<ITransactionRepo> _transactionRepo;
        [SetUp]
        public void Setup()
        {
            _transactionRepo = new Mock<ITransactionRepo>();
            _rewardServices = new RewardServices(_transactionRepo.Object);
        }
        [Test]
        //This tests an independent function ConvertReward to see if 
        //it converts money to reward points in different cases correctly
        public void Test1() 
        {
            var response = _rewardServices.ConvertReward(10);
            Assert.That(response, Is.EqualTo(0));
            response = _rewardServices.ConvertReward(50);
            Assert.That(response, Is.EqualTo(0));
            response = _rewardServices.ConvertReward(70);
            Assert.That(response, Is.EqualTo(20));
            response = _rewardServices.ConvertReward(100);
            Assert.That(response, Is.EqualTo(50));
            response = _rewardServices.ConvertReward(110);
            Assert.That(response, Is.EqualTo(70));
        }

        [Test]
        //Test rewardService to see if it outputs the correct value
        //Inputs contain the transaction with price of no more than 50, equal to 100
        //and over 100 to see if the reward points are correct for these months and add to correct total points
        public void Test2()
        {
            var trans_records = new Dictionary<int, List<TransactionModel>>()
            { 
                {1, new List<TransactionModel>(){
                new TransactionModel(){Date=new DateTime(2023, 01, 01),TransactionId=1,ItemPrices=100,UserId=1},
                new TransactionModel(){Date=new DateTime(2022, 12, 01),TransactionId=2,ItemPrices=50,UserId=1},
                new TransactionModel(){Date=new DateTime(2022, 11, 01),TransactionId=3,ItemPrices=101,UserId=1},
                new TransactionModel(){Date=new DateTime(2022, 10, 01),TransactionId=4,ItemPrices=300,UserId=1}
                }} 
            };
            _transactionRepo.Setup(m => m.getTransRecord()).Returns(trans_records);

            var response = _rewardServices.CalculatePoints(1, 3);
            Assert.That(response.Count, Is.EqualTo(4));
            Assert.That(response[0].Month, Is.EqualTo("2023, January"));
            Assert.That(response[0].RewardPoint, Is.EqualTo(50));
            Assert.That(response[1].Month, Is.EqualTo("2022, December"));
            Assert.That(response[1].RewardPoint, Is.EqualTo(0));
            Assert.That(response[2].Month, Is.EqualTo("2022, November"));
            Assert.That(response[2].RewardPoint, Is.EqualTo(52));
            Assert.That(response[3].Month, Is.EqualTo("Total"));
            Assert.That(response[3].RewardPoint, Is.EqualTo(102));
            //A customer who is not in transaction record, should return 0
            response = _rewardServices.CalculatePoints(0, 3);
            Assert.That(response.Count, Is.EqualTo(4));
            Assert.That(response[3].Month, Is.EqualTo("Total"));
            Assert.That(response[3].RewardPoint, Is.EqualTo(0));
        }

        [Test]
        //Still testing rewardService in different case
        //One month doesn't have any transaction record, and the January
        //contain several transaction records
        public void Test3()
        {
            var trans_records = new Dictionary<int, List<TransactionModel>>()
            {
                {1, new List<TransactionModel>(){
                new TransactionModel(){Date=new DateTime(2023, 01, 01),TransactionId=1,ItemPrices=100,UserId=1},
                new TransactionModel(){Date=new DateTime(2023, 01, 05),TransactionId=2,ItemPrices=150,UserId=1},
                new TransactionModel(){Date=new DateTime(2022, 11, 01),TransactionId=3,ItemPrices=101,UserId=1},
                new TransactionModel(){Date=new DateTime(2022, 10, 01),TransactionId=4,ItemPrices=300,UserId=1}
                }}
            };
            _transactionRepo.Setup(m => m.getTransRecord()).Returns(trans_records);

            var response = _rewardServices.CalculatePoints(1, 3);
            Assert.That(response.Count, Is.EqualTo(4));
            Assert.That(response[0].Month, Is.EqualTo("2023, January"));
            Assert.That(response[0].RewardPoint, Is.EqualTo(200));
            Assert.That(response[1].Month, Is.EqualTo("2022, December"));
            Assert.That(response[1].RewardPoint, Is.EqualTo(0));
            Assert.That(response[2].Month, Is.EqualTo("2022, November"));
            Assert.That(response[2].RewardPoint, Is.EqualTo(52));
            Assert.That(response[3].Month, Is.EqualTo("Total"));
            Assert.That(response[3].RewardPoint, Is.EqualTo(252));
        }

        [Test]
        //The order of time is not sorted so that the earlier month may come before later month in transaction record
        //also invalid months are being placed between the valid months 
        //Transaction contains edge cases like the time with valid month but invalid year (January in 2022)
        //and the day like 10/31 just the day before the valid day, and they should be ignored
        public void Test4()
        {
            var trans_records = new Dictionary<int, List<TransactionModel>>()
            {
                {1, new List<TransactionModel>(){
                new TransactionModel(){Date=new DateTime(2022, 10, 31),TransactionId=4,ItemPrices=300,UserId=1},
                new TransactionModel(){Date=new DateTime(2022, 11, 01),TransactionId=3,ItemPrices=101,UserId=1},
                new TransactionModel(){Date=new DateTime(2023, 01, 01),TransactionId=1,ItemPrices=100,UserId=1},
                new TransactionModel(){Date=new DateTime(2023, 01, 05),TransactionId=2,ItemPrices=150,UserId=1},
                new TransactionModel(){Date=new DateTime(2022, 01, 01),TransactionId=4,ItemPrices=300,UserId=1},
                new TransactionModel(){Date=new DateTime(2022, 11, 01),TransactionId=5,ItemPrices=150,UserId=1},
                new TransactionModel(){Date=new DateTime(2022, 12, 31),TransactionId=13,ItemPrices=70,UserId=1}
                }}
            };
            _transactionRepo.Setup(m => m.getTransRecord()).Returns(trans_records);

            var response = _rewardServices.CalculatePoints(1, 3);
            Assert.That(response.Count, Is.EqualTo(4));
            Assert.That(response[0].Month, Is.EqualTo("2023, January"));
            Assert.That(response[0].RewardPoint, Is.EqualTo(200));
            Assert.That(response[1].Month, Is.EqualTo("2022, December"));
            Assert.That(response[1].RewardPoint, Is.EqualTo(20));
            Assert.That(response[2].Month, Is.EqualTo("2022, November"));
            Assert.That(response[2].RewardPoint, Is.EqualTo(202));
            Assert.That(response[3].Month, Is.EqualTo("Total"));
            Assert.That(response[3].RewardPoint, Is.EqualTo(422));
        }

        [Test]
        //Doesn't have any transactions in the past 3 months
        public void Test5()
        {
            var trans_records = new Dictionary<int, List<TransactionModel>>()
            {
                {1, new List<TransactionModel>(){
                new TransactionModel(){Date=new DateTime(2022, 10, 31),TransactionId=4,ItemPrices=300,UserId=1},
                new TransactionModel(){Date=new DateTime(2021, 11, 01),TransactionId=3,ItemPrices=101,UserId=1},
                new TransactionModel(){Date=new DateTime(2022, 01, 01),TransactionId=1,ItemPrices=100,UserId=1},
                new TransactionModel(){Date=new DateTime(2000, 01, 05),TransactionId=2,ItemPrices=150,UserId=1},
                new TransactionModel(){Date=new DateTime(2002, 08, 01),TransactionId=4,ItemPrices=300,UserId=1}
                }}
            };
            _transactionRepo.Setup(m => m.getTransRecord()).Returns(trans_records);

            var response = _rewardServices.CalculatePoints(1, 3);
            Assert.That(response.Count, Is.EqualTo(4));
            Assert.That(response[0].Month, Is.EqualTo("2023, January"));
            Assert.That(response[0].RewardPoint, Is.EqualTo(0));
            Assert.That(response[1].Month, Is.EqualTo("2022, December"));
            Assert.That(response[1].RewardPoint, Is.EqualTo(0));
            Assert.That(response[2].Month, Is.EqualTo("2022, November"));
            Assert.That(response[2].RewardPoint, Is.EqualTo(0));
            Assert.That(response[3].Month, Is.EqualTo("Total"));
            Assert.That(response[3].RewardPoint, Is.EqualTo(0));
        }

        [Test]
        //Multiple customers
        public void Test6()
        {
            var trans_records = new Dictionary<int, List<TransactionModel>>()
            {
                {1, new List<TransactionModel>(){
                new TransactionModel(){Date=new DateTime(2022, 12, 31),TransactionId=4,ItemPrices=300,UserId=1},
                new TransactionModel(){Date=new DateTime(2022, 11, 01),TransactionId=1,ItemPrices=100,UserId=1},
                new TransactionModel(){Date=new DateTime(2002, 08, 01),TransactionId=6,ItemPrices=300,UserId=1}
                }},
                {2, new List<TransactionModel>(){
                new TransactionModel(){Date=new DateTime(2022, 11, 01),TransactionId=3,ItemPrices=101,UserId=2}
                }},
                {3, new List<TransactionModel>(){
                new TransactionModel(){Date=new DateTime(2000, 01, 05),TransactionId=2,ItemPrices=150,UserId=3}
                }}
            };
            _transactionRepo.Setup(m => m.getTransRecord()).Returns(trans_records);

            var response = _rewardServices.CalculatePoints(1, 3);
            Assert.That(response.Count, Is.EqualTo(4));
            Assert.That(response[3].Month, Is.EqualTo("Total"));
            Assert.That(response[3].RewardPoint, Is.EqualTo(500));
            response = _rewardServices.CalculatePoints(2, 3);
            Assert.That(response[3].Month, Is.EqualTo("Total"));
            Assert.That(response[3].RewardPoint, Is.EqualTo(52));
            response = _rewardServices.CalculatePoints(3, 3);
            Assert.That(response[3].Month, Is.EqualTo("Total"));
            Assert.That(response[3].RewardPoint, Is.EqualTo(0));
            response = _rewardServices.CalculatePoints(0, 3);
            Assert.That(response[3].Month, Is.EqualTo("Total"));
            Assert.That(response[3].RewardPoint, Is.EqualTo(0));
        }

    }


  
}