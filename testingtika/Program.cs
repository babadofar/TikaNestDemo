using System;
using Nest;
using TikaOnDotNet;
//Simple test of indexing documents into Elasticsearch 
//using Apache Tika wrapped by the TikoOnDotNet project 
//and the NEST elasticsearch client library for C#
namespace testingtika
{
    //POCO for the formatting of the items to feed into Elasticsearch
    public class Article
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        [ElasticProperty(Index = FieldIndexOption.Analyzed, TermVector = TermVectorOption.WithPositionsOffsets)]
        public string Body { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
    }

    class Program
    {

        private static TextExtractor _cut;
        private static string _IndexName = "files";

        private static ElasticClient elastic()
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(
                node,
                defaultIndex: _IndexName
            );
            var client = new ElasticClient(settings);
            return client;
        }


        // Extract content and metadata from the file.
        // put the information into an Article object
        private static Article tika(string filename)
        {

            var result = _cut.Extract(filename);
            var article = new Article()
            {
                Id = filename,
                ContentType = result.ContentType,
                Body = result.Text
            };
            if (result.Metadata.ContainsKey("title"))
                article.Title = result.Metadata["title"];
            else
                article.Title = filename;
            if (result.Metadata.ContainsKey("Content-Length"))
            {
                long longtest;
                if (long.TryParse(result.Metadata["Content-Length"], out longtest))
                {
                    article.ContentLength = longtest;
                }
            }
            if (result.Metadata.ContainsKey("Author"))
                article.Author = result.Metadata["Author"];
            return article;
        }

        static void Main(string[] args)
        {
            string currentDir = "";
            if (args.Length == 0)
            {
                currentDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            } else
                currentDir = args[0];
            if (args.Length > 1)
                _IndexName = args[1];
            var files = System.IO.Directory.GetFiles(currentDir);
            _cut = new TextExtractor();
            var es = elastic();
            es.CreateIndex(_IndexName, ind => ind.Index(_IndexName)
                .AddMapping<Article>(t => t.MapFromAttributes()));

            foreach (string file in files)
            {
                try
                {
                    var test = tika(file);
                    var indexresult = es.Index<Article>(test);
                    Console.WriteLine(indexresult.Id);
                }
                catch (Exception e)
                {
                    
                    Console.WriteLine(e.Message);
                    continue;
                }
            }

        }
    }
}
