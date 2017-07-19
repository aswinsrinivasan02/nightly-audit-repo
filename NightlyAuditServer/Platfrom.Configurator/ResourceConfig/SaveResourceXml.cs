using BallyTech.Infrastructure.Configuration;
using BallyTech.Library.ResourceManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Configurator
{
    public class SaveResourceXml
    {
        public static void SaveStringResourceXml(String filePath, IDataProvider iDataProvider)
        {
            
            XmlTextReader xmlTextReader = new XmlTextReader(filePath);
            DeleteResourceRecords(iDataProvider);
            string strQuery = "INSERT INTO tStringResource (ResourceKey, ResourceValue, Language) VALUES(@ResourceKey, @ResourceValue, @Language)";
            using (IDbConnection iDbConnection = iDataProvider.GetConnection)
            {
                iDbConnection.Open();


                while (xmlTextReader.Read())
                {
                    List<KeyValuePair<string, object>> resourceParameters = new List<KeyValuePair<string, object>>();

                    switch (xmlTextReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (xmlTextReader.Name == "Resource")
                            {
                                resourceParameters.Add(new KeyValuePair<string, object>("@ResourceKey", xmlTextReader.GetAttribute("ResourceKey")));
                                resourceParameters.Add(new KeyValuePair<string, object>("@ResourceValue", xmlTextReader.GetAttribute("ResourceValue")));
                                resourceParameters.Add(new KeyValuePair<string, object>("@Language", xmlTextReader.GetAttribute("Language")));
                            }
                            break;
                        default:
                            break;
                    }



                    if (resourceParameters != null && resourceParameters.Count > 0)
                    {
                        IDbCommand command = iDataProvider.GetCommand(strQuery, iDbConnection, iDataProvider.GetParameter(resourceParameters), CommandType.Text);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }



        private static void DeleteResourceRecords(IDataProvider iDataProvider)
        {
            string strDeleteStringResource = "Delete From tStringResource";
            
            using (IDbConnection iDbConnection = iDataProvider.GetConnection)
            {
                iDbConnection.Open();

                IDbCommand resourCommand = iDataProvider.GetCommand(strDeleteStringResource, iDbConnection, null,null, CommandType.Text);
                resourCommand.ExecuteNonQuery();

            }
        }
    }
}
