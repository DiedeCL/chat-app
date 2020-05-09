using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Xml;
using System.Xml.Serialization;

namespace HelloWorld.Controller
{
    public class Character
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    #region TypeConverter
    // Example N - TypeConverter
    [TypeConverter(typeof(CharacterV2Converter))]
    public class CharacterV2
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static bool TryParse(string value, out CharacterV2 c)
        {
            var result = false;
            c = null;

            var props = value.Split('_');
            if(props.Length == 2)
            {
                int id = 0;
                if(int.TryParse(props[0], out id))
                {
                    c = new CharacterV2();
                    c.ID = id;
                    c.Name = props[1];

                    result = true;
                }
            }

            return result;
        }
    }

    // Example N - TypeConverter
    public class CharacterV2Converter : TypeConverter
    {
        public override bool CanConvertFrom(
            ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(
            ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                CharacterV2 c;
                if (CharacterV2.TryParse((string)value, out c))
                {
                    return c;
                }
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
    #endregion

    #region ModelBinder
    // -------------------------------------------------------------
    //                  V3a
    public class CharacterV3a
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static bool TryParse(string value, out CharacterV3a c)
        {
            var result = false;
            c = null;

            var props = value.Split('_');
            if (props.Length == 2)
            {
                int id = 0;
                if (int.TryParse(props[0], out id))
                {
                    c = new CharacterV3a();
                    c.ID = id;
                    c.Name = props[1];

                    result = true;
                }
            }

            return result;
        }
    }

    // System.Web.Http.ModelBinding - right one
    // System.Web.ModelBinding - NOT THIS ONE! This is for ASP.NET WebForms
    public class CharacterV3aModelBinder : IModelBinder
    {
        public bool BindModel(
            HttpActionContext actionContext, 
            ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(CharacterV3a)) return false;

            var objValue = 
                bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (objValue == null) return false;

            var value = objValue.RawValue as string;
            if (value == null) return false;

            CharacterV3a c;
            if (!CharacterV3a.TryParse((string)value, out c)) return false;

            bindingContext.Model = c;
            return true;
        }
    }

    // -------------------------------------------------------------
    //                  V3b
    [ModelBinder(typeof(CharacterV3bModelBinder))]
    public class CharacterV3b
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static bool TryParse(string value, out CharacterV3b c)
        {
            var result = false;
            c = null;

            var props = value.Split('_');
            if (props.Length == 2)
            {
                int id = 0;
                if (int.TryParse(props[0], out id))
                {
                    c = new CharacterV3b();
                    c.ID = id;
                    c.Name = props[1];

                    result = true;
                }
            }

            return result;
        }
    }

    // System.Web.Http.ModelBinding - right one
    // System.Web.ModelBinding - NOT THIS ONE! This is for ASP.NET WebForms
    public class CharacterV3bModelBinder : IModelBinder
    {
        public bool BindModel(
            HttpActionContext actionContext,
            ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(CharacterV3b)) return false;

            var objValue =
                bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (objValue == null) return false;

            var value = objValue.RawValue as string;
            if (value == null) return false;

            CharacterV3b c;
            if (!CharacterV3b.TryParse((string)value, out c)) return false;

            bindingContext.Model = c;
            return true;
        }
    }

    // System.Web.Http.ModelBinding - right one
    // System.Web.ModelBinding - NOT THIS ONE! This is for ASP.NET WebForms
    public class CharacterV3cModelBinder : IModelBinder
    {
        public bool BindModel(
            HttpActionContext actionContext,
            ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(CharacterV3b)) return false;

            var objValue =
                bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (objValue == null) return false;

            var value = objValue.RawValue as string;
            if (value == null) return false;

            CharacterV3b c;
            if (!CharacterV3b.TryParse((string)value, out c)) return false;

            // diff from CharacterV3bModelBinder
            c.Name += "Boro Wins!!!";

            bindingContext.Model = c;
            return true;
        }
    }

    #endregion

    [RoutePrefix("api/mortalkombat")]
    public class ParameterBindingController : ApiController
    {
        #region How you can bind - via URI
        // Example A - Query String
        // URI: api/mortalkombat/a/character?id=1
        [HttpGet]
        [Route("a/character")]
        public string CharacterA(int id)
        {
            return "Raiden";
        }

        // Example B - Query String with multiple parameters
        // URI: api/mortalkombat/b/character?id=1&name=abc
        // URI: api/mortalkombat/b/character?name=abc&id=1
        [HttpGet]
        [Route("b/character")]
        public string CharacterB(int id, string name)
        {
            return "Liu Kang";
        }

        // Example C - Query String with multiple parameters for HTTP POST
        // URI: api/mortalkombat/c/character?id=1&name=abc
        // URI: api/mortalkombat/c/character?name=abc&id=1
        [HttpPost]
        [Route("c/character")]
        public string CharacterC(int id, string name)
        {
            return $"Character id({id}) and name({name})";
        }

        // Example D - Attribute Routing for HTTP GET/POST
        // URI: api/mortalkombat/d/character/1/abc
        [AcceptVerbs("get","post")]
        [Route("d/character/{id}/{name}")]
        public string CharacterD(int id, string name)
        {
            return $"Character id({id}) and name({name})";
        }
        #endregion

        #region How you can bind - via Body
        // Example E1 - known object
        // URI: api/mortalkombat/e1/character/
        // JSON: { "id":1, "name":"abc" }
        [HttpPost]
        [Route("e1/character")]
        public string CharacterE1(Character c)
        {
            return $"Character id({c.ID}) and name({c.Name})";
        }

        // Example E2 - unknown object
        // URI: api/mortalkombat/e2/character/
        // JSON: { "id":1, "name":"abc", "xpto":"etc" }
        [HttpPost]
        [Route("e2/character")]
        public string CharacterE2(dynamic c)
        {
            return $"Character obj({c}). name({ c.name }).";
        }

        // Example F1 - Mixing Parameters - Query String
        // URI: api/mortalkombat/f1/character?age=123
        // JSON: { "id":1, "name":"abc" }
        [HttpPost]
        [Route("f1/character")]
        public string CharacterF1(int age, Character c)
        {
            return $"Character age({age}) id({c.ID}) name({c.Name})";
        }

        // Example F2 - Mixing Parameters - Attribute Routing
        // URI: api/mortalkombat/f2/character/123
        // JSON: { "id":1, "name":"abc" }
        [HttpPost]
        [Route("f2/character/{age}")]
        public string CharacterF2(int age, Character c)
        {
            return $"Character age({age}) id({c.ID}) name({c.Name})";
        }

        // Example F3 - Mixing Parameters - Query String - dynamic
        // URI: api/mortalkombat/f3/character?age=123
        // JSON: { "id":1, "name":"abc", "xpto"="etc" }
        [HttpPost]
        [Route("f3/character")]
        public string CharacterF3(int age, dynamic c)
        {
            return $"Character age({age}) id({c.ID}) name({c.Name}) xpto({c.xpto})";
        }

        // Example F4 - Mixing Parameters - Attribute Routing - dynamic
        // URI: api/mortalkombat/f4/character/123
        // JSON: { "id":1, "name":"abc", "xpto"="etc" }
        [HttpPost]
        [Route("f4/character/{age}")]
        public string CharacterF4(int age, dynamic c)
        {
            return $"Character age({age}) id({c.ID}) name({c.Name}) xpto({c.xpto})";
        }
        #endregion

        #region FromURI Attribute
        // Example G1 - From URI - Query String
        // URI: api/mortalkombat/g1/character?id=1&name=abc
        [HttpPost]
        [Route("g1/character")]
        public string CharacterG1([FromUri] Character c)
        {
            return $"Character id({c.ID}) name({c.Name})";
        }

        // Example G2 - From URI - Query String - dynamic
        // URI: 
        //  a) api/mortalkombat/g2/character?id=1&name=abc
        //  b) api/mortalkombat/g2/character?c.id=1&c.name=abc
        //  Doesn't work because dynamic doesn't have properties!
        // URI: 
        //  c) api/mortalkombat/g2/character?c=234
        //  Works because it is like a primitive value
        [HttpPost]
        [Route("g2/character")]
        public string CharacterG2([FromUri] dynamic c)
        {
            return $"Character c({c})";
        }

        // Example G3 - From URI - Attribute Routing 
        // URI: api/mortalkombat/g3/character/1/abc
        [HttpPost]
        [Route("g3/character/{id}/{name}")]
        //[Route("g3/character/{c.id}/{c.name}")] - is valid too!
        public string CharacterG3([FromUri] Character c)
        {
            return $"Character id({c.ID}) name({c.Name})";
        }

        // Example G4 - From URI - Attribute Routing - dynamic
        // URI: api/mortalkombat/g4/character/1/abc
        //  Will not work because dynamic doesn't have properties.
        [HttpPost]
        [Route("g4/character/{c.id}/{c.name}")]
        public string CharacterG4([FromUri] dynamic c)
        {
            return $"Character id({c.id}) name({c.name})";
        }

        // Example G5 - From URI - Multiple Params - Attribute Routing V1
        // URI: api/mortalkombat/g5/character/1/abc
        [HttpPost]
        [Route("g5/character/{ID}/{name}")]
        public string CharacterG5([FromUri] Character c1, [FromUri] Character c2)
        {
            return $"Character id({c1.ID}) name({c1.Name})" +
                $"Character id({c2.ID}) name({c2.Name})";
        }

        // Example G6 - From URI - Multiple Params - Attribute Routing V2
        // URI: api/mortalkombat/g6/character/1/abc/2/def
        [HttpPost]
        [Route("g6/character/{c1.ID}/{c1.name}/{c2.id}/{c2.name}")]
        public string CharacterG6([FromUri] Character c1, [FromUri] Character c2)
        {
            return $"Character id({c1.ID}) name({c1.Name})" +
                $"Character id({c2.ID}) name({c2.Name})";
        }

        // Example G7 - From URI - Multiple Params - Query String
        // URI: api/mortalkombat/g7/character?id=1&name=abc
        // URI: api/mortalkombat/g7/character?c1.id=1&c1.name=abc&c2.id=2&c2.name=def
        [HttpPost]
        [Route("g7/character")]
        public string CharacterG7([FromUri] Character c1, [FromUri] Character c2)
        {
            return $"Character id({c1.ID}) name({c1.Name})" +
                $"Character id({c2.ID}) name({c2.Name})";
        }
        #endregion

        #region FromBody Attribute
        // Example H1 - From Body
        // URI: api/mortalkombat/h1/character
        [HttpPost]
        [Route("h1/character")]
        public string CharacterH1([FromBody] int c)
        {
            return $"Character c({c})";
        }

        // Example H2 - From Body
        // URI: api/mortalkombat/h2/character
        // ERROR - Only one parameter is allowed to have [FromBody]
        [HttpPost]
        [Route("h2/character")]
        public string CharacterH2([FromBody] int c, [FromBody] int d)
        {
            return $"Character c({c}) d({d})";
        }

        // Example H3 - From Body - raw json
        // URI: api/mortalkombat/h3/character
        // String: "{\"id\":1, \"name\":\"abc\"}" 
        [HttpPost]
        [Route("h3/character")]
        public string CharacterH3([FromBody] string s)
        {
            Character c = JsonConvert.DeserializeObject<Character>(s);
            return $"Character id({c.ID}) name({c.Name})";
        }
        #endregion

        #region Arrays and Lists - From URI
        // Example I1 - Arrays and Lists - From URI
        // URI: api/mortalkombat/i1/character?ids=1&ids=4&ids=18
        [HttpGet]
        [Route("i1/character")]
        public string CharacterI1([FromUri] int[] ids)
        {
            var res = "";
            foreach (int id in ids) res += id + ", ";
            return $"Character ids({res})";
        }

        // Example I2 - Arrays and Lists - From URI
        // URI: api/mortalkombat/i2/character?ids=1&ids=4&ids=18
        [HttpGet]
        [Route("i2/character")]
        public string CharacterI2([FromUri] List<int> ids)
        {
            var res = "";
            foreach (int id in ids) res += id + ", ";
            return $"Character ids({res})";
        }

        // Example I3 - Arrays and Lists - From URI
        // URI: api/mortalkombat/i3/character?c.id=1&c.name=abc&c.id=2&c.name=xyz
        //  This will NOT work!
        [HttpGet]
        [Route("i3/character")]
        public string CharacterI3([FromUri] Character[] c)
        {
            var res = "";
            foreach (Character s in c) res += $"({s.ID},{s.Name}), ";
            return $"Characters({res})";
        }
        #endregion

        #region Arrays and Lists - From Body
        // Example J1 - Arrays and Lists - From Body
        // URI: api/mortalkombat/j1/character
        [HttpPost]
        [Route("j1/character")]
        public string CharacterJ1([FromBody] List<Character> c)
        {
            var res = "";
            foreach (Character s in c) res += $"({s.ID},{s.Name}), ";
            return $"Characters({res})";
        }

        // Example J2 - Arrays and Lists - From Body
        // URI: api/mortalkombat/j2/character
        [HttpPost]
        [Route("j2/character")]
        public string CharacterJ2(List<List<Character>> c)
        {
            var res = "";
            foreach (List<Character> ls in c)
                foreach(Character s in ls) res += $"({s.ID},{s.Name}), ";
            return $"Characters({res})";
        }
        #endregion

        #region Form-data
        // Example k1 - Form-data
        // URI: api/mortalkombat/k1/character
        [HttpPost]
        [Route("k1/character")]
        public string CharacterK1()
        {
            var name = HttpContext.Current.Request.Form["name"];
            return $"Character name({name})";
        }

        // Example k2 - Form-data
        // URI: api/mortalkombat/k2/character
        [HttpPost]
        [Route("k2/character")]
        public string CharacterK2()
        {
            var form = HttpContext.Current.Request.Form;

            var response = "";
            foreach(string key in form.Keys)
                response += $"{key}({form[key]}) ";
            
            return response;
        }
        #endregion

        #region URL Encoded
        // Example l1 - URL-Encoded
        // URI: api/mortalkombat/l1/character
        [HttpPost]
        [Route("l1/character")]
        public string CharacterL1()
        {
            var form = HttpContext.Current.Request.Form;

            var response = "";
            foreach (string key in form.Keys)
                response += $"{key}({form[key]}) ";

            return response;
        }
        
        // Example l2 - URL Encoded
        // URI: api/mortalkombat/l2/character
        [HttpPost]
        [Route("l2/character")]
        public async Task<string> CharacterL2(HttpRequestMessage request)
        {
            var form = await request.Content.ReadAsFormDataAsync();

            var response = "";
            foreach (string key in form.Keys)
                response += $"{key}({form[key]}) ";

            return response;
        }
        #endregion

        #region XML
        // Example m0 - XML
        // URI: api/mortalkombat/m0/character
        //<Character>
        //   <ID>734</ID>
        //   <Name>Sonya</Name>
        //</Character>
        [HttpPost]
        [Route("m0/character")]
        public string CharacterM0(HttpRequestMessage request)
        {
            Character c = new Character();

            var doc = new XmlDocument();
            doc.Load(request.Content.ReadAsStreamAsync().Result);

            var xmlString = doc.DocumentElement.OuterXml;

            var serializer = new XmlSerializer(typeof(Character));
            using (TextReader reader = new StringReader(xmlString))
            {
                c = (Character)serializer.Deserialize(reader);
            }

            return $"Character id({c.ID}) name({c.Name})";
        }

        // Example m1 - XML
        // URI: api/mortalkombat/m1/character
        //<Character>
        //   <ID>734</ID>
        //   <Name>Sonya</Name>
        //</Character>
        [HttpPost]
        [Route("m1/character")]
        public string CharacterM1(Character c)
        {
            return $"Character id({c.ID}) name({c.Name})";
        }

        // Example m2 - XML - List
        // URI: api/mortalkombat/m2/character
        //<ArrayOfCharacter>
        //  <Character>
        //      <ID>734</ID>
        //      <Name>Sonya</Name>
        //  </Character>
        //  <Character>
        //      <ID>932</ID>
        //      <Name>Goro</Name>
        //  </Character>
        //</ArrayOfCharacter>
        [HttpPost]
        [Route("m2/character")]
        public string CharacterM2(List<Character> cs)
        {
            var res = "";
            foreach (Character c in cs)
                res += $"Character id({c.ID}) name({c.Name})";
            return res;
        }
        #endregion

        #region TypeConverter
        // Example n1 - TypeConverter - Query String
        // URI: api/mortalkombat/n1/character?c=123_abc
        [HttpGet]
        [Route("n1/character")]
        public string CharacterN1(CharacterV2 c)
        {
            return $"Character id({c.ID}) name({c.Name})";
        }

        // Example n2 - TypeConverter - Attribute Routing
        // URI: api/mortalkombat/n2/character/123_abc
        [HttpGet]
        [Route("n2/character/{c}")]
        public string CharacterN2(CharacterV2 c)
        {
            return $"Character id({c.ID}) name({c.Name})";
        }
        #endregion

        #region ModelBinder
        // Example o1 - ModelBinder - Add attribute to parameter v1
        // URI: api/mortalkombat/o1/character/123_abc
        [HttpGet]
        [Route("o1/character/{c}")]
        public string CharacterO1(
            [ModelBinder(typeof(CharacterV3aModelBinder))] CharacterV3a c)
        {
            return $"Character id({c.ID}) name({c.Name})";
        }

        // Example o2 - ModelBinder - Add attribute to parameter v2
        // URI: api/mortalkombat/o2/character/123_abc
        [HttpGet]
        [Route("o2/character/{c}")]
        public string CharacterO2([ModelBinder] CharacterV3a c)
        {
            return $"Character id({c.ID}) name({c.Name})";
        }

        // Example o3 - ModelBinder - Add attribute to class type
        // URI: api/mortalkombat/o3/character/123_abc
        [HttpGet]
        [Route("o3/character/{c}")]
        public string CharacterO3(CharacterV3b c)
        {
            return $"Character id({c.ID}) name({c.Name})";
        }

        // Example o4 - ModelBinder - Add attribute to class type
        // URI: api/mortalkombat/o4/character/123_abc
        [HttpGet]
        [Route("o4/character/{c}")]
        public string CharacterO4(
            [ModelBinder(typeof(CharacterV3cModelBinder))] CharacterV3b c)
        {
            return $"Character id({c.ID}) name({c.Name})";
        }
        #endregion
    }
}
