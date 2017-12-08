//Nodejs Thz 20170106
//
//var http = require("http");
//var fs = require("fs");
//var url = require("url");
//
//var server = http.createServer(function (request, response) {
//    var type = url.parse(request.url, true).query["type"];
//
//    var realPath = "./abs/" + type;
//    fs.exists(realPath, function (exists) {
//        if (!exists) {
//            console.log("未找到文件 " + type)
//        } else {
//            fs.readFile(realPath, "binary", function (err, file) {
//                if (err) {
//                    response.writeHead(500, {
//                        'Content-Type': 'text/plain'
//                    });
//
//                    response.end(err);
//                } else {
//                    response.writeHead(200, {
//                        'Content-Type': 'text/html'
//                    });
//
//                    response.write(file, "binary");
//                    response.end();
//                }
//            });
//        }
//    });
//});
//
//server.listen('8001');