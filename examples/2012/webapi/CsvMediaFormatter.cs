// A Web API formatter that turns any IEnumerable<T> into CSV on Accept: text/csv.
using System;
using System.Collections;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class CsvMediaFormatter : MediaTypeFormatter
{
    public CsvMediaFormatter()
    {
        SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
    }

    public override bool CanWriteType(Type type) => typeof(IEnumerable).IsAssignableFrom(type);
    public override bool CanReadType(Type type) => false;

    public override Task WriteToStreamAsync(Type type, object value, Stream stream,
        System.Net.Http.HttpContent content, System.Net.TransportContext context)
    {
        return Task.Run(() =>
        {
            var writer = new StreamWriter(stream, Encoding.UTF8);
            PropertyInfo[] props = null;
            foreach (var item in (IEnumerable)value)
            {
                if (props == null)
                {
                    props = item.GetType().GetProperties();
                    writer.WriteLine(string.Join(",", Array.ConvertAll(props, p => p.Name)));
                }
                var cells = Array.ConvertAll(props, p => Escape(p.GetValue(item)));
                writer.WriteLine(string.Join(",", cells));
            }
            writer.Flush();
        });
    }

    static string Escape(object v)
    {
        var s = v == null ? "" : v.ToString();
        return s.Contains(",") || s.Contains("\"") ? "\"" + s.Replace("\"", "\"\"") + "\"" : s;
    }
}
