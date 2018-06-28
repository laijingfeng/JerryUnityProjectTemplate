using System;
using System.Collections;
//using UnityEngine.Experimental.Networking;//Unity5.3.4f1
using UnityEngine.Networking;

namespace Jerry
{
    public class UnityWebRequestUtil : Singleton<UnityWebRequestUtil>
    {
        public UnityWebRequestUtil()
        {
            //不加默认构造函数，IL2CPP会报错
        }
        
        public enum HttpMethod
        {
            GET = 0,
            POST,
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="callback">code-rsp-err</param>
        /// <param name="postData">post数据</param>
        public void HttpRequest(string url, HttpMethod method, Action<long, string, string> callback = null, string postData = "")
        {
            new JerryCoroutine.CorTask(IE_HttpRequest(url, method, callback, postData));
        }

        private IEnumerator IE_HttpRequest(string url, HttpMethod method, Action<long, string, string> callback, string postData)
        {
            using (UnityWebRequest request = new UnityWebRequest(url))
            {
                request.method = method.ToString();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("CLEARANCE", "I_AM_ADMIN");
                request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                if (method == HttpMethod.POST)
                {
                    byte[] postBytes = System.Text.Encoding.Default.GetBytes(postData);
                    request.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
                }
                yield return request.Send();
                if (request.isError)
                {
                    if (callback != null)
                    {
                        callback(request.responseCode, null, request.error);
                    }
                }
                else
                {
                    if (callback != null)
                    {
                        callback(request.responseCode, request.downloadHandler.text, null);
                    }
                }
            }
        }
    }
}