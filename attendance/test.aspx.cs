using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace attendance {
    public partial class test : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            string url = string.Format("https://demo.edigitalnepal.com/demo/apis/v3/public/biometric-attendance/log/save");
            WebRequest requestObject = WebRequest.Create(url);
            requestObject.Method = "POST";
            requestObject.ContentType = "application/json";
            string postData = "{\"data\":[{\"din\":1,\"dn\":1,\"clock\":\"2021-02-28 18:30:00\",\"mode\":\"F\",\"remarks\":\"\"},{\"din\":1,\"dn\":1,\"clock\":\"2021-02-28 18:30:00\",\"mode\":\"F\",\"remarks\":\"\"}],\"teamId\":\"{{aXlOQiLhvm5tkpqOudbBIA==}}\"}";
            using (var streamWriter = new StreamWriter(requestObject.GetRequestStream())) {
                streamWriter.Write(postData);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = requestObject.GetResponse();
                using (var streamReader = new StreamReader(requestObject.GetRequestStream())) {
                    var result2 = streamReader.ReadToEnd();
                }
            }
        }
    }
}