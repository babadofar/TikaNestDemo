# Demo console app for indexing windows files with elasticsearch
## What is it?
A simple test program showing how to use [TikaOnDotNet](https://kevm.github.io/tikaondotnet/) to extract metadata from files on a windows file system, and index them into Elasticsearch. 
### OK?
TikaOnDotNet is a .NET port/wrapper of [Apache Tika](http://tika.apache.org/), using [IKVM](http://www.ikvm.net/) to magically transform java code into a .net dll. 
Using TikaOnDotNet, we don't have to use the mapper attachments plugin, but can read text content from files before indexing. It is also easy to retrieve other metadata if you should need that. Communicating with Elasticsearch is done using the wonderful [NEST](http://nest.azurewebsites.net/) library.
## How can I use it?
Clone the repo, and open the program in Visual Studio. Running the program will read all documents from your "my documents"
folder, and index into Elasticsearch.
If you want to search the files you indexed, you can for instance use Sense
 ( automagically installed by the below-mentioned ansible vagrant box at [http://localhost:5601/app/sense](http://localhost:5601/app/sense)
Or, if no sense available, you should see something on this url [http://localhost:9200/files/_search](http://localhost:9200/files/_search)
See below for a sample query with highlighting.

The program can even read input arguments - the folder to read files from, and the indexname to use in Elasticsearch
## But why doesn't it...?
Hey, this is a *simple* demo. Emphasis on simple. If you need something, do it yourself.
##Requirements
####Windows
This is a windows program
####Visual Studio
You most likely want to run this program in visual Studio. I used version 2013
####Elasticsearch
Requires elasticsearch running on localhost at port 9200. Tested with Elasticsearch 2.0, but it likely runs with even older versions. 

(you can install elasticsearch in a VM using  [https://github.com/comperiosearch/vagrant-elk-box-ansible](https://github.com/comperiosearch/vagrant-elk-box-ansible)))

#### Sample query with highlighting of body contents
Excluding the body from showing up in the search results, as it is often too large to be of any real value in this context.

    GET /files/_search
    {
      "_source": {
        "exclude": "body"
      },
      "query": {
        "match": {
          "body": "fish"
        }
      },
      "highlight": {
        "fields": {
          "body": {}
        }
      }
    }
