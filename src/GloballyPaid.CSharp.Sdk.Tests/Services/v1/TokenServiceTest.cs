using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GloballyPaid.Tests
{
    public class TokenServiceTest : BaseTest
    {
        private const string BasePath = "/api/v1/vault/token";

        private readonly TokenService service;

        public TokenServiceTest(
            MockHttpClientFixture mockHttpClientFixture)
            : base(mockHttpClientFixture)
        {
            service = new TokenService(Client);
        }

        [Fact]
        public void Tokenize()
        {
            var expectedResult = GetPaymentInstrument();

            StubRequest(HttpMethod.Post, BasePath, HttpStatusCode.OK, expectedResult.ToJson());

            var result = service.Tokenize(GetTokenizeRequest(), GetTestRequestOptions());

            AssertRequest(HttpMethod.Post, BasePath);
            Assert.Equal(expectedResult.ToJson(), result.ToJson());
        }

        [Fact]
        public async Task Tokenize_Async()
        {
            var expectedResult = GetPaymentInstrument();

            StubRequest(HttpMethod.Post, BasePath, HttpStatusCode.OK, expectedResult.ToJson());

            var result = await service.TokenizeAsync(GetTokenizeRequest(), GetTestRequestOptions());

            AssertRequest(HttpMethod.Post, BasePath);
            Assert.Equal(expectedResult.ToJson(), result.ToJson());
        }

        [Fact]
        public void Tokenize_Error_Invalid_API_Response()
        {
            StubRequest(HttpMethod.Post, BasePath, HttpStatusCode.OK, GetInvalidJson());

            var exception = Assert.Throws<GloballyPaidException>(() =>
               service.Tokenize(GetTokenizeRequest(), GetTestRequestOptions()));

            Assert.Equal(HttpStatusCode.OK, exception.GloballyPaidResponse.StatusCode);
            Assert.Equal("Exception of type 'GloballyPaid.GloballyPaidException' was thrown.", exception.Message);
            //Assert.Equal($"Invalid response object from API: \"{GetInvalidJson()}\"", exception.ErrorMessage);
            Assert.Equal($"{GetInvalidJson()}", exception.GloballyPaidResponse.Content);
        }

        [Fact]
        public void Tokenize_Error_Invalid_Status()
        {
            StubRequest(HttpMethod.Post, BasePath, HttpStatusCode.BadRequest, GetInvalidStatusError());

            var exception = Assert.Throws<GloballyPaidException>(() =>
               service.Tokenize(GetTokenizeRequest(), GetTestRequestOptions()));

            Assert.Equal(HttpStatusCode.BadRequest, exception.GloballyPaidResponse.StatusCode);
            Assert.Equal("Exception of type 'GloballyPaid.GloballyPaidException' was thrown.", exception.Message);
            Assert.Equal($"{GetInvalidStatusError()}", exception.ErrorMessage);
            Assert.Equal($"{GetInvalidStatusError()}", exception.GloballyPaidResponse.Content);
        }

        private TokenizeRequest GetTokenizeRequest()
        {
            return new TokenizeRequest
            {
                PaymentInstrument = GetPaymentInstrumentRequest(),
                KountSessionId = "kount_session_id",
                TraceId = "trace_id"
            };
        }
       
    }
}
