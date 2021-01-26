using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TM.Infrastructure.Json;

namespace TM.Infrastructure.TencentCloud.Cos
{
    enum HttpMethod { Get, Post };

    class Request
    {
        HttpWebRequest request;

        public async Task<COSResult> SendRequestStreamAsync(string url, Dictionary<string, object> data, HttpMethod requestMethod,
            Dictionary<string, string> header, int timeOut, Stream stream = null, long offset = -1, int sliceSize = 0)
        {
            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                if (requestMethod == HttpMethod.Get)
                {
                    var paramStr = "";
                    foreach (var key in data.Keys)
                    {
                        paramStr += string.Format("{0}={1}&", key, HttpUtility.UrlEncode(data[key].ToString()));
                    }
                    paramStr = paramStr.TrimEnd('&');
                    url += (url.EndsWith("?") ? "&" : "?") + paramStr;
                }

                request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Accept = CosDefaultValue.ACCEPT;
                request.KeepAlive = true;
                request.UserAgent = CosDefaultValue.USER_AGENT_VERSION;
                request.Timeout = timeOut;
                foreach (var key in header.Keys)
                {
                    if (key == "Content-Type")
                    {
                        request.ContentType = header[key];
                    }
                    else
                    {
                        request.Headers.Add(key, header[key]);
                    }
                }
                if (requestMethod == HttpMethod.Post)
                {
                    request.Method = requestMethod.ToString().ToUpper();
                    using (var memStream = new MemoryStream())
                    {
                        if (header.ContainsKey("Content-Type") && header["Content-Type"] == "application/json")
                        {
                            var json = data.ToJson();//JsonHelper.ToJSON(data);
                            var jsonByte = Encoding.GetEncoding("utf-8").GetBytes(json.ToString());
                            await memStream.WriteAsync(jsonByte, 0, jsonByte.Length);
                        }
                        else
                        {
                            var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
                            var beginBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                            var endBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                            request.ContentType = "multipart/form-data; boundary=" + boundary;

                            var strBuf = new StringBuilder();
                            foreach (var key in data.Keys)
                            {
                                strBuf.Append("\r\n--" + boundary + "\r\n");
                                strBuf.Append("Content-Disposition: form-data; name=\"" + key + "\"\r\n\r\n");
                                strBuf.Append(data[key].ToString());
                            }
                            var paramsByte = Encoding.GetEncoding("utf-8").GetBytes(strBuf.ToString());
                            await memStream.WriteAsync(paramsByte, 0, paramsByte.Length);



                            if (stream != null)
                            {
                                stream.Seek(0, SeekOrigin.Begin);
                                await memStream.WriteAsync(beginBoundary, 0, beginBoundary.Length);

                                const string filePartHeader =
                                    "Content-Disposition: form-data; name=\"fileContent\"; filename=\"{0}\"\r\n" +
                                    "Content-Type: application/octet-stream\r\n\r\n";
                                var headerText = string.Format(filePartHeader, "tmp");
                                var headerbytes = Encoding.UTF8.GetBytes(headerText);
                                await memStream.WriteAsync(headerbytes, 0, headerbytes.Length);

                                if (offset == -1)
                                {
                                    var buffer = new byte[1024];
                                    int bytesRead;
                                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                                    {
                                        await memStream.WriteAsync(buffer, 0, bytesRead);
                                    }
                                }
                                else
                                {
                                    var buffer = new byte[sliceSize];
                                    int bytesRead;
                                    stream.Seek(offset, SeekOrigin.Begin);
                                    bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                                    await memStream.WriteAsync(buffer, 0, bytesRead);
                                }
                            }
                            await memStream.WriteAsync(endBoundary, 0, endBoundary.Length);
                        }
                        request.ContentLength = memStream.Length;
                        var requestStream = await request.GetRequestStreamAsync();//.GetRequestStream();
                        memStream.Position = 0;
                        var tempBuffer = new byte[memStream.Length];
                        await memStream.ReadAsync(tempBuffer, 0, tempBuffer.Length);

                        await requestStream.WriteAsync(tempBuffer, 0, tempBuffer.Length);
                        requestStream.Close();
                    }
                }

                var response = request.GetResponse();
                using (var s = response.GetResponseStream())
                {
                    var reader = new StreamReader(s, Encoding.UTF8);
                    var resultRead = await reader.ReadToEndAsync();

                    return resultRead.ToObject<COSResult>();
                }
            }
            catch (WebException we)
            {
                if (we.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var s = we.Response.GetResponseStream())
                    {
                        var reader = new StreamReader(s, Encoding.UTF8);
                        var resultRead = await reader.ReadToEndAsync();
                        return resultRead.ToObject<COSResult>();
                    }
                }
                else
                {
                    throw we;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
