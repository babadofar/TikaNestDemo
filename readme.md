# Demo console app for indexing windows files with elasticsearch
## What is it?
A simple test program showing how to use TikaOnDotNet to extract metadata from files on a windows file system, and index them into Elasticsearch.
## How can I use it?
Clone the repo, and open the program in visual Studio. Running the program will read all documents from your "my documents"
folder, and index into Elasticsearch.
If you want to search the files you indexed, you can for instance use Sense
 ( automagically installed by the below-mentioned ansible vagrant box at [http://localhost:5601/app/sense](http://localhost:5601/app/sense)
Or, if no sense available, you should see something on this [http://localhost:9200/files/_search](http://localhost:9200/files/_search)

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