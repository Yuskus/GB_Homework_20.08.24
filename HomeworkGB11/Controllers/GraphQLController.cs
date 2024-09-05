using Microsoft.AspNetCore.Mvc;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;

namespace HomeworkGB11.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GraphQLController : ControllerBase
    {
        [HttpPost("interaction")]
        public async Task<ActionResult<string>> QueryEmployees([FromBody] string queryOrMutation)
        {
            try
            {
                var graphQLClient = new GraphQLHttpClient($"{Request.Scheme}://{Request.Host}/graphql", new NewtonsoftJsonSerializer());
                var request = new GraphQLRequest() { Query = queryOrMutation };
                var response = await graphQLClient.SendQueryAsync<string>(request);
                string result = response.Data;
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
