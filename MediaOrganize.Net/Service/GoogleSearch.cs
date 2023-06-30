using System.Text.RegularExpressions;
using System.Web;

namespace MediaOrganize.Service
{
    internal class GoogleSearch
    {
        private Regex regexTitleId = new Regex("\\/tt(?<id>\\d+)\\/");
        public async Task<string> GetTitleId(string name)
        {
            var nameEncode = HttpUtility.UrlEncode($" {name}");
            using HttpClient client = new HttpClient();
            var result = await client.GetAsync($"http://www.google.com/search?btnI=I%27m%20Feeling%20Lucky&q=site:imdb.com{nameEncode}");

            var query = result?.RequestMessage?.RequestUri?.Query;
            if(query != null)
            {
                var match = regexTitleId.Match(query);

                if (match.Success)
                {
                    return match.Groups["id"].Value;
                }
            }

            return string.Empty;
        }
    }
}
