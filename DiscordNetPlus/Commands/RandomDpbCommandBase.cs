using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DiscordNetPlus.DataServices;
using DiscordNetPlus.Extentions;
using Newtonsoft.Json.Linq;

namespace DiscordNetPlus.Commands
{
    public abstract class RandomDpbCommandBase : CommandBase
    {
        #region Fields and Properties

        private const string SearchPath = "search";

        #endregion

        #region Methods

        protected async Task ReplyRandomDerpibooru(string query)
        {
            var dpb = await RandomDerpibooru(query);

            var reply = await Context.Channel.SendFileAsync(dpb.ImageStream, Path.Combine(dpb.Url), $"derpibooru.org エントリーID: {dpb.Id}");
            
            var sendDuration = await RecordStopwatch("Sendfile", false);

            var filesize = (long)reply.Attachments.ElementAt(0).Size;
            await Context.Log(
                $"Filesize: {filesize.ToFileSizeString()} ({(filesize / sendDuration * 1000).ToFileSizeString()}ps)");


        }

        protected async Task<(Stream ImageStream, string Id, string Url)> RandomDerpibooru(string query)
        {
            await Context.Log($"derpibooru random: {query}");

            using (TypingIndicator)
            {
                StartStopwatch();

                var id = await Random(query);
                await RecordStopwatch("Random art request");
                
                var url = await FetchArt(id);
                await RecordStopwatch("Art attribution request");
                
                var stream = await UrlToStream(url);
                await RecordStopwatch("Stream convertion");

                return (stream, id.ToString(), url);
            }
        }

        private static async Task<Stream> UrlToStream(string url)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStreamAsync(url);
            }
        }

        private static async Task<int> Random(string query)
        {
            string response;

            using (var dpb = new Derpibooru()) response = await dpb.Request($"{SearchPath}.json?q={query}&random_image=y");

            var data = JObject.Parse(response);

            if (!data.HasValues)
                throw new ArgumentException($"'{query}' に該当するエントリーは見つかりませんでした。");

            var id = int.Parse(data["id"].ToString());
            return id;
        }

        private static async Task<string> FetchArt(int id)
        {
            string response;
            using (var dpb = new Derpibooru()) response = await dpb.Request($"{id}.json");

            var data = JObject.Parse(response);
            var url = "https:" + data["image"];

            return url;
        }
        
        //private static int GetTotalPageCount(dynamic data)
        //{
        //    return int.Parse(data.total.ToString())/ JArray.Parse(data.search.ToString()).Count;
        //}

        //private static List<(string, string)> ParsePageList(dynamic data)
        //{
        //    IEnumerable<JToken> images = JArray.Parse(data.search.ToString());

        //    var urls = images.Select(img => ($"https:{img["image"].ToString()}", img["id"].ToString())).ToList();
        //    return urls;
        //}
        #endregion

    }
}